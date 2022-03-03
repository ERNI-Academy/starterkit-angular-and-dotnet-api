using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App.API.Extensions;

/// <summary>
/// The host extensions.
/// </summary>
public static class HostExtensions
{
    /// <summary>
    /// The migrate db context.
    /// </summary>
    /// <param name="host">The host.</param>
    /// <param name="seeder">The seeder.</param>
    /// <typeparam name="TContext">Action seeder</typeparam>
    /// <returns>The <see cref="IHost"/>.</returns>
    public static IHost MigrateDbContext<TContext>(
        this IHost host,
        Action<TContext, IServiceProvider> seeder) where TContext : DbContext
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var logger = services.GetRequiredService<ILogger<TContext>>();

            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                // https://github.com/dotnet-architecture/eShopOnContainers.git
                // In the original code there was a Polly Policy to retry in case the connection failed (probably due to delayed startup of containers)
                InvokeSeeder(seeder, context, services);

                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
            }
        }

        return host;
    }

    /// <summary>
    /// The invoke seeder.
    /// </summary>
    /// <param name="seeder">The seeder.</param>
    /// <param name="context">The context.</param>
    /// <param name="services">The services.</param>
    /// <typeparam name="TContext">Action seeder</typeparam>
    private static void InvokeSeeder<TContext>(
        Action<TContext, IServiceProvider> seeder,
        TContext context,
        IServiceProvider services)
        where TContext : DbContext
    {
        var logger = services.GetRequiredService<ILogger<TContext>>();

        if (bool.TryParse(Environment.GetEnvironmentVariable("RESET_DATABASE"), out bool resetDatabase) && resetDatabase)
        {
            logger.LogInformation("Reseting database {DbContextName}", typeof(TContext).Name);
            context.Database.EnsureDeleted();
            // EnsureCreated is done by the Migrate()
            // context.Database.EnsureCreated();
        }

        context.Database.Migrate();
        seeder(context, services);
    }
}
