using Api.API.Infrastructure.Notifications;
using Api.API.Infrastructure.Video;
using App.API.Services.Auth;
using App.API.Services.Items;
using App.API.Services.Language;
using App.API.SignalR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.API.Extensions;

/// <summary>
/// The service extentions.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// The add custom services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        // App.API
        services.AddTransient<INotifyClient, SignalRClientNotifier>();

        // App.Infraestructure
        services.AddScoped<IVideoStreamService, VideoStreamService>();

        // App.Services
        services.AddScoped<ItemsService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<ILanguageService, LanguageService>();
        
        services.Configure<JwtSettings>(configuration.GetSection("JWT"));

        return services;
    }
}
