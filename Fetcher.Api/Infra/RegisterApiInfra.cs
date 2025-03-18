using System.Security.Authentication;
using Fetcher.Api.Infra;
using Fetcher.Models.Configs;
using Fetcher.Services.Infra;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;

namespace Fetcher.Api;

public static class RegisterApiInfra
{
    public static void ConfigureApiInfra(this WebApplicationBuilder builder)
    {
        builder.ConfigureKestrel();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.ConfigureServicesInfra();
        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
        });
        builder.Services.AddOptions<FetcherConfig>();
        builder.Services.Configure<FetcherConfig>(builder.Configuration);
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
    }

    private static void ConfigureKestrel(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = 10 * 1024 * 1024;
            options.ListenAnyIP(24999);
            options.AddServerHeader = false;
            options.ConfigureHttpsDefaults(httpsOptions =>
            {
                httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
            });
        });
    }

    public static void UseApiInfra(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}