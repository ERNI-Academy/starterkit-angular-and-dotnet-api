using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using App.API.DataAccess.Contexts;
using App.API.Extensions;
using App.API.Middleware;
using App.API.Services.OpcUa.ApiModels.Notifications;
using App.API.SignalR;
using App.API.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Converters;

namespace App.API;

/// <summary>
/// The startup.
/// </summary>
public class Startup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// The configure services. This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services">The services.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR();

        services.AddControllers();

        services.AddCustomServices(Configuration);

        services.AddMvc().AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

        services.AddSwaggerGenNewtonsoftSupport();

        services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "test",
                    new OpenApiInfo
                    {
                        Title = "App.API",
                        Version = "v1",
                        Description =
                                "This document describe the endpoints to use in the UI to connect with the entire CVS system."
                    });

                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT containing userid claim",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                    });
                var security = new OpenApiSecurityRequirement
                                   {
                                       {
                                           new OpenApiSecurityScheme
                                               {
                                                   Reference = new OpenApiReference
                                                                   {
                                                                       Id = "Bearer",
                                                                       Type = ReferenceType.SecurityScheme
                                                                   },
                                                   UnresolvedReference = true
                                               },
                                           new List<string>()
                                       }
                                   };
                options.AddSecurityRequirement(security);

                // add custom models to the Swagger Schema which are not in the endpoints (used for notification models)
                options.DocumentFilter<CustomModelDocumentFilter<NotificationModels>>();

                var pathToXmlDocumentsToLoad = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => x.FullName != null && x.FullName.StartsWith(Assembly.GetExecutingAssembly().GetName().Name))
                    .Select(x => Path.Combine(AppContext.BaseDirectory, $"{x.GetName().Name}.xml"))
                    .ToList();

                pathToXmlDocumentsToLoad.ForEach(x => options.IncludeXmlComments(x));
            });

        services.AddDbContext<ApplicationContext>(b => b.UseSqlite(
            Configuration.GetConnectionString("DefaultConnection"),
           sqliteOptions => sqliteOptions.MigrationsAssembly(typeof(ApplicationContext).GetTypeInfo().Assembly.GetName().Name)));

        services.AddOpcUa();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    /// <summary>
    /// The configure. This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The app.</param>
    /// <param name="env">The env.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger(c => { });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/test/swagger.json", "App.API V1");
            });
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseMiddleware<ErrorHandlerMiddleware>();

        app.UseMiddleware<JwtMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<AppHub>("/notify");
        });

    }
}
