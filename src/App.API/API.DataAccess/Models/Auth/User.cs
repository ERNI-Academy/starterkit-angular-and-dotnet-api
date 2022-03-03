using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace App.API.DataAccess.Models.Auth;

/// <summary>
/// The user.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    [NotNull]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    [NotNull]
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password hash.
    /// </summary>
    [NotNull]
    public string PasswordHash { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is default.
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// Gets or sets the refresh tokens.
    /// </summary>
    public List<RefreshToken> RefreshTokens { get; set; }

    /// <summary>
    /// Gets or sets the created.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the updated.
    /// </summary>
    public DateTime? Updated { get; set; }

    /// <summary>
    /// The owns token.
    /// </summary>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool OwnsToken(string token)
    {
        return RefreshTokens?.Find(x => x.Token == token) != null;
    }
}
