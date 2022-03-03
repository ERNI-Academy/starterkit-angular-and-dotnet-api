
using App.API.Services.Auth.ApiModels;

namespace App.API.Services.Auth;
/// <summary>
/// The AuthService interface.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// The authenticate.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <param name="ipAddress">The ip address.</param>
    /// <returns>The <see cref="AuthenticateResponse"/>.</returns>
    AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);

    /// <summary>
    /// The refresh token.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <param name="ipAddress">The ip address.</param>
    /// <returns>The <see cref="AuthenticateResponse"/>.</returns>
    AuthenticateResponse RefreshToken(string token, string ipAddress);

    /// <summary>
    /// The revoke token.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <param name="requesterId">The requester id.</param>
    /// <param name="ipAddress">The ip address.</param>
    void RevokeToken(string token, int requesterId, string ipAddress);
}
