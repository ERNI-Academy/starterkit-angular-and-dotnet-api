namespace App.API.Services.Auth;

/// <summary>
/// The jwt settings.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Gets or sets the security key.
    /// </summary>
    public string SecurityKey { get; set; }

    /// <summary>
    /// Gets or sets the issuer.
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// Gets or sets the audience.
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// Gets or sets the expiration in days.
    /// </summary>
    public int ExpirationInDays { get; set; }
}
