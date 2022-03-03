using System;
using System.IO;

using App.API.DataAccess.Contexts;
using App.API.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace App.API;

/// <summary>
/// The program.
/// </summary>
public class Program
{
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();

    /// <summary>
    /// The main.
    /// </summary>
    /// <param name="args">The args.</param>
    public static void Main(string[] args)
    {
        var logLevelSwitch = new LoggingLevelSwitch { MinimumLevel = LogEventLevel.Information };
        if (Enum.TryParse(Environment.GetEnvironmentVariable("LOG_LEVEL"), out LogEventLevel logLevel))
        {
            logLevelSwitch.MinimumLevel = logLevel;
        }

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(logLevelSwitch)
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();

        try
        {
            Log.Information("Starting web host");
            var host = CreateHostBuilder(args).Build();

            host.MigrateDbContext<ApplicationContext>((context, services) =>
            {
                var env = services.GetService<IWebHostEnvironment>();
                var logger = services.GetService<ILogger<ApplicationContextSeeder>>();

                new ApplicationContextSeeder(logger)
                    .SeedAsync(context)
                    .Wait();
            });

            host.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    /// <summary>
    /// The create host builder.
    /// </summary>
    /// <param name="args">The args.</param>
    /// <returns>The <see cref="IHostBuilder"/>.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog();
}
