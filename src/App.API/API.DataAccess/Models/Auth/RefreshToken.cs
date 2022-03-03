using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace App.API.DataAccess.Models.Auth;

/// <summary>
/// The refresh token.
/// </summary>
[Owned]
public class RefreshToken
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the account.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Gets or sets the token.
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Gets or sets the expires.
    /// </summary>
    public DateTime Expires { get; set; }

    /// <summary>
    /// The is expired.
    /// </summary>
    public bool IsExpired => DateTime.UtcNow >= Expires;

    /// <summary>
    /// Gets or sets the created.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the created by ip.
    /// </summary>
    public string CreatedByIp { get; set; }

    /// <summary>
    /// Gets or sets the revoked.
    /// </summary>
    public DateTime? Revoked { get; set; }

    /// <summary>
    /// Gets or sets the revoked by ip.
    /// </summary>
    public string RevokedByIp { get; set; }

    /// <summary>
    /// Gets or sets the replaced by token.
    /// </summary>
    public string ReplacedByToken { get; set; }

    /// <summary>
    /// The is active.
    /// </summary>
    public bool IsActive => Revoked == null && !IsExpired;
}
