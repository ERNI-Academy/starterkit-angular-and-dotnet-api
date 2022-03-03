
using System.ComponentModel.DataAnnotations;

namespace App.API.Services.Auth.ApiModels;
/// <summary>
/// The reset password admin request.
/// </summary>
public class ResetPasswordAdminRequest
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    [Required]
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the new password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
}
