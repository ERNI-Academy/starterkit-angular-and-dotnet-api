using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App.API.DataAccess.Models.Auth;
using App.API.Services.Auth;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App.API.Middleware;

/// <summary>
/// The jwt middleware.
/// </summary>
public class JwtMiddleware
{
    /// <summary>
    /// The next.
    /// </summary>
    private readonly RequestDelegate next;

    /// <summary>
    /// The jwt settings.
    /// </summary>
    private readonly JwtSettings jwtSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next.</param>
    /// <param name="jwtSettings">The jwt settings.</param>
    public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings)
    {
        this.next = next;
        this.jwtSettings = jwtSettings.Value;
    }

    /// <summary>
    /// The invoke.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            AttachAccountToContext(context, token);
        }

        await next(context);
    }

    /// <summary>
    /// The attach account to context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="token">The token.</param>
    private void AttachAccountToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecurityKey);
            tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                },
                out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
            var userRole = jwtToken.Claims.First(x => x.Type == "role").Value;

            // attach account to context on successful jwt validation
            context.Items["RequesterId"] = userId;
            if (Enum.TryParse(userRole, out Role role))
            {
                context.Items["RequesterRole"] = role;
            }
        }
        catch
        {
            // do nothing if jwt validation fails
            // account is not attached to context so request won't have access to secure routes
        }
    }
}
