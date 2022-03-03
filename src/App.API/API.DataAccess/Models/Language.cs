using System.ComponentModel.DataAnnotations;

namespace App.API.DataAccess.Models;

/// <summary>
/// The language.
/// </summary>
public class Language
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the localization.
    /// </summary>
    public string Localization { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the language is currently active in the UI.
    /// </summary>
    public bool IsActive { get; set; }
}
