namespace App.API.Services.Auth.ApiModels;

/// <summary>
/// The revoke token request.
/// </summary>
public class RevokeTokenRequest
{
    /// <summary>
    /// Gets or sets the token.
    /// </summary>
    public string Token { get; set; }
}
