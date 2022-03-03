using System.Collections;
using System.Collections.Generic;

using DA = App.API.DataAccess.Models;

namespace App.API.Services.Language;

/// <summary>
/// The LanguageService interface.
/// </summary>
public interface ILanguageService
{
    /// <summary>
    /// Gets the current language.
    /// </summary>
    public DA.Language CurrentActive { get; }

    /// <summary>
    /// The get all.
    /// </summary>
    /// <returns>The lis of available languanges <see cref="IEnumerable"/>.</returns>
    IEnumerable<DA.Language> GetAll();

    /// <summary>
    /// The get current active.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>
    /// The current active language <see cref="Language"/>.
    /// </returns>
    DA.Language ChangeCurrentActive(int id);
}
