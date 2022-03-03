using System.ComponentModel.DataAnnotations;

using App.API.DataAccess.Models.Auth;

namespace App.API.Services.Auth.ApiModels;

/// <summary>
/// The user response.
/// </summary>
public class UserResponse
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public int Id { get; set; }

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
    [EnumDataType(typeof(Role))]
    public string Role { get; set; }
}
