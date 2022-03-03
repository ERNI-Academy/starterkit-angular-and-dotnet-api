
using System.ComponentModel.DataAnnotations;

using App.API.DataAccess.Models.Auth;

namespace App.API.Services.Auth.ApiModels;
/// <summary>
/// The create user request.
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    [Required]
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    [Required]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the role.
    /// </summary> 
    [Required]
    [EnumDataType(typeof(Role))]
    public Role Role { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
