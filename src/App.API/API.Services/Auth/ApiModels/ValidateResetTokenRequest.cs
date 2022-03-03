
using System.ComponentModel.DataAnnotations;

namespace App.API.Services.Auth.ApiModels;
/// <summary>
/// The validate reset token request.
/// </summary>
public class ValidateResetTokenRequest
{
    /// <summary>
    /// Gets or sets the token.
    /// </summary>
    [Required]
    public string Token { get; set; }
}
