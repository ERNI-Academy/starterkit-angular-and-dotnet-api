using System;

using App.API.Services.OpcUa;
using App.API.Services.OpcUa.Interfaces;
using App.API.Services.OpcUa.OfflineServices;
using App.API.Services.OpcUa.OnlineServices;

using Microsoft.Extensions.DependencyInjection;

namespace App.API.Extensions;

/// <summary>
/// The upc ua extensions.
/// </summary>
public static class UpcUaExtensions
{
    /// <summary>
    /// The opc ua mode.
    /// </summary>
    private static OpcUaMode opcUaMode;

    /// <summary>
    /// The add opc ua.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddOpcUa(this IServiceCollection services)
    {
        if (!Enum.TryParse(Environment.GetEnvironmentVariable("OPCUA_MODE"), out opcUaMode))
        {
            opcUaMode = OpcUaMode.Offline;
        }

        if (opcUaMode == OpcUaMode.Offline)
        {
            services.AddSingleton<ILogService, LogOfflineService>();
            services.AddSingleton<IModuleService, ModuleOfflineService>();
            services.AddSingleton<ISensorService, SensorOfflineService>();
        }
        else
        {
            services.AddTransient<ILogService, LogOnlineService>();
            services.AddTransient<IModuleService, ModuleOnlineService>();
            services.AddTransient<ISensorService, SensorOnlineService>();
        }

        return services;
    }
}
