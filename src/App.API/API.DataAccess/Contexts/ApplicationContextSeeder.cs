using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.API.DataAccess.Models;
using App.API.DataAccess.Models.Auth;
using Microsoft.Extensions.Logging;

namespace App.API.DataAccess.Contexts;
/// <summary>
/// The application context seeder.
/// </summary>
public class ApplicationContextSeeder
{
    /// <summary>
    /// The log.
    /// </summary>
    private readonly ILogger<ApplicationContextSeeder> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationContextSeeder"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public ApplicationContextSeeder(ILogger<ApplicationContextSeeder> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// The seed database async.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public async Task SeedAsync(ApplicationContext context)
    {
        await Task.Run(async () =>
        {
            logger.LogInformation("Initialize DB Seed");

            await SeedItemsAsync(context);
            await SeedUsersAsync(context);
            await SeedLanguagesAsync(context);
        });
    }

    /// <summary>
    /// The get preconfigured items.
    /// </summary>
    /// <returns>
    /// The list of the preconfigured items.
    /// </returns>
    private static IEnumerable<Item> GetPreconfiguredItems()
    {
        return new List<Item>()
                   {
                       CreateItem("Item1"),
                       CreateItem("Item2"),
                   };
    }

    /// <summary>
    /// The create item.
    /// </summary>
    /// <param name="itemName">The item name.</param>
    /// <returns>The <see cref="Item"/>.</returns>
    private static Item CreateItem(string itemName)
    {
        var item = new Item(itemName);
        item.AddTag("Label1");
        item.AddTag("Label2");
        return item;
    }

    /// <summary>
    /// The seed items async.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    private async Task SeedItemsAsync(ApplicationContext context)
    {
        if (!context.Items.Any())
        {
            logger.LogDebug("Seeding Items");
            await context.AddRangeAsync(GetPreconfiguredItems());
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// The seed users async.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    private async Task SeedUsersAsync(ApplicationContext context)
    {
        if (!context.Users.Any())
        {
            logger.LogDebug("Seeding preconfigured Users");
            var preconfiguredRoles = new List<User>()
            {
                new User { Username = "oper", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test123."), FirstName = "Operator", Role = Role.Operator, Created = DateTime.UtcNow, IsDefault = true },
                new User { Username = "main", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test123."), FirstName = "Maintenance", Role = Role.Maintenance, Created = DateTime.UtcNow, IsDefault = true },
                new User { Username = "comm", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test123."), FirstName = "Commissioning", Role = Role.Commissioning, Created = DateTime.UtcNow, IsDefault = true },
                new User { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test123."), FirstName = "Administrator", Role = Role.Admin, Created = DateTime.UtcNow, IsDefault = true },
            };

            await context.AddRangeAsync(preconfiguredRoles);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// The seed languages async.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    private async Task SeedLanguagesAsync(ApplicationContext context)
    {
        if (!context.Languages.Any())
        {
            logger.LogDebug("Seeding preconfigured Languages");
            var preconfiguredLanguage = new List<Language>()
                                            {
                                                new Language { Name = "English", Localization = "en", IsActive = true },
                                                new Language { Name = "Español", Localization = "es", IsActive = false },
                                            };


            await context.AddRangeAsync(preconfiguredLanguage);
            await context.SaveChangesAsync();
        }
    }
}

