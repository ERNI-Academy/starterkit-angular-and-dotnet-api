using System.ComponentModel.DataAnnotations;

using App.API.DataAccess.Models.Auth;

namespace App.API.Services.Auth.ApiModels;

/// <summary>
/// The update user request.
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    /// <remarks>
    /// The number correspond to: 
    /// 0 => Operator
    /// </remarks>
    [EnumDataType(typeof(Role))]
    public Role Role { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    [MinLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
