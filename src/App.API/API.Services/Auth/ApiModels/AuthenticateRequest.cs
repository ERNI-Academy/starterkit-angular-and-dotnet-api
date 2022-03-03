using System.ComponentModel.DataAnnotations;

namespace App.API.Services.Auth.ApiModels;

/// <summary>
/// Request to authenticate user into the web Api. 
/// </summary>
public class AuthenticateRequest
{
    /// <summary>
    /// Gets or sets the user name of the user who wants to be authenticated. 
    /// </summary>
    [Required]
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password of the user who wants to be authenticated. 
    /// </summary>
    [Required]
    public string Password { get; set; }
}
