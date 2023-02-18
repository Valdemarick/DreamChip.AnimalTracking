using DreamChip.AnimalTracking.WebApi.Filters;
using Serilog;

namespace DreamChip.AnimalTracking.WebApi;

internal static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddScoped<AuthorizationFilter>();

        return services;
    }

    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Services.AddLogging(config =>
        {
            config.ClearProviders();
            config.AddSerilog(logger);
        });
    }
}
