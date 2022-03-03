using System.Collections.Generic;
using System.Linq;

using App.API.DataAccess.Contexts;

using Microsoft.Extensions.Logging;

using DA = App.API.DataAccess.Models;

namespace App.API.Services.Language;

/// <summary>
/// The language service.
/// </summary>
public class LanguageService : ILanguageService
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger logger;

    /// <summary>
    /// The context.
    /// </summary>
    private readonly ApplicationContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="context">The context.</param>
    public LanguageService(ILogger<LanguageService> logger, ApplicationContext context)
    {
        this.logger = logger;
        this.context = context;
    }

    /// <inheritdoc/>
    public DA.Language CurrentActive => context.Languages.FirstOrDefault(l => l.IsActive);

    /// <inheritdoc/>
    public IEnumerable<DA.Language> GetAll()
    {
        logger.LogDebug("Get all ");
        return context.Set<DA.Language>();
    }

    /// <inheritdoc/>
    public DA.Language ChangeCurrentActive(int id)
    {
        logger.LogDebug("Changing the current language for the App UI.");

        var oldCurrent = CurrentActive;
        if (oldCurrent.Id == id)
        {
            logger.LogDebug("Not changes on the current language.");
            return oldCurrent;
        }

        var newCurrent = context.Languages.Find(id);
        if (newCurrent == null)
        {
            logger.LogDebug("Language not found");
            throw new KeyNotFoundException("Language not found");
        }

        oldCurrent.IsActive = false;
        context.Languages.Update(oldCurrent);

        newCurrent.IsActive = true;
        context.Languages.Update(newCurrent);

        context.SaveChanges();

        return newCurrent;
    }
}
