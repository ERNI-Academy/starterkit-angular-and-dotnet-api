using BC = BCrypt.Net.BCrypt;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using AutoMapper;

using App.API.DataAccess.Contexts;
using App.API.DataAccess.Models.Auth;
using App.API.Services.Auth.ApiModels;
using App.API.Services.CustomExceptions;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App.API.Services.Auth;
/// <summary>
/// The auth service.
/// </summary>
public class AuthService : IAuthService
{
    /// <summary>
    /// The context.
    /// </summary>
    private readonly ApplicationContext context;

    /// <summary>
    /// The mapper.
    /// </summary>
    private readonly IMapper mapper;

    /// <summary>
    /// The jwt settings.
    /// </summary>
    private readonly JwtSettings jwtSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <param name="mapper">
    /// The mapper.
    /// </param>
    /// <param name="jwtSettings">
    /// The jwt settings.
    /// </param>
    public AuthService(
        ApplicationContext context,
        IMapper mapper,
        IOptions<JwtSettings> jwtSettings)
    {
        this.context = context;
        this.mapper = mapper;
        this.jwtSettings = jwtSettings.Value;
    }
    
    /// <inheritdoc/>
    public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
    {
        var user = context.Users.SingleOrDefault(x => x.Username == model.Username);

        if (user == null || !BC.Verify(model.Password, user.PasswordHash))
        {
            throw new AuthenticationException("Username or password is incorrect");
        }

        // authentication successful so generate jwt and refresh tokens
        var jwtToken = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken(ipAddress);

        // save refresh token
        user.RefreshTokens.Add(refreshToken);
        context.Update(user);
        context.SaveChanges();

        var response = mapper.Map<AuthenticateResponse>(user);
        response.JwtToken = jwtToken;
        response.RefreshToken = refreshToken.Token;
        return response;
    }

    /// <inheritdoc/>
    public AuthenticateResponse RefreshToken(string token, string ipAddress)
    {
        var (refreshToken, user) = GetRefreshToken(token);

        // replace old refresh token with a new one and save
        var newRefreshToken = GenerateRefreshToken(ipAddress);
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReplacedByToken = newRefreshToken.Token;
        user.RefreshTokens.Add(newRefreshToken);
        context.Update(user);
        context.SaveChanges();

        // generate new jwt
        var jwtToken = GenerateJwtToken(user);

        var response = mapper.Map<AuthenticateResponse>(user);
        response.JwtToken = jwtToken;
        response.RefreshToken = newRefreshToken.Token;
        return response;
    }

    /// <inheritdoc/>
    public void RevokeToken(string token, int requesterId, string ipAddress)
    {
        var requester = context.Users.Find(requesterId);

        // users can revoke their own tokens and admins can revoke any tokens
        if (!requester.OwnsToken(token) && requester.Role != Role.Admin)
        {
            throw new UnauthorizedException();
        }

        var (refreshToken, user) = GetRefreshToken(token);

        // revoke token and save
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        context.Update(user);
        context.SaveChanges();
    }

    /// <summary>
    /// The generate refresh token.
    /// </summary>
    /// <param name="ipAddress">The ip address.</param>
    /// <returns>The <see cref="RefreshToken"/>.</returns>
    private static RefreshToken GenerateRefreshToken(string ipAddress)
    {
        return new RefreshToken
        {
            Token = RandomTokenString(),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };
    }

    /// <summary>
    /// The random token string.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string RandomTokenString()
    {
        var randomBytes = new byte[40];
        using (var rngCryptoServiceProvider = RandomNumberGenerator.Create())
        {
            rngCryptoServiceProvider.GetBytes(randomBytes);
        }

        // convert random bytes to hex string
        return BitConverter.ToString(randomBytes).Replace("-", string.Empty);
    }

    /// <summary>
    /// The get refresh token.
    /// </summary>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns> The token information. </returns>
    private (RefreshToken, User) GetRefreshToken(string token)
    {
        var user = context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
        if (user == null)
        {
            throw new Exception("Invalid token");
        }

        var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
        if (!refreshToken.IsActive)
        {
            throw new Exception("Invalid token");
        }

        return (refreshToken, user);
    }
    
    /// <summary>
    /// The generate jwt token.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The <see cref="string"/>.</returns>
    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSettings.SecurityKey);
        var tokenDescriptor = new SecurityTokenDescriptor
                                  {
                                      Subject =
                                          new ClaimsIdentity(
                                              new[]
                                                  {
                                                      new Claim("Id", user.Id.ToString()),
                                                      new Claim(ClaimTypes.Role, user.Role.ToString()),
                                                  }),
                                      Expires = DateTime.UtcNow.AddMinutes(15),
                                      SigningCredentials = new SigningCredentials(
                                          new SymmetricSecurityKey(key),
                                          SecurityAlgorithms.HmacSha256Signature)
                                  };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
