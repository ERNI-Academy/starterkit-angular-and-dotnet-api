
using System.ComponentModel.DataAnnotations;

namespace App.API.Services.Auth.ApiModels;
/// <summary>
/// The reset password request.
/// </summary>
public class ResetPasswordRequest
{
    /// <summary>
    /// Gets or sets the old password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; }

    /// <summary>
    /// Gets or sets the new password.
    /// </summary>
    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
}
