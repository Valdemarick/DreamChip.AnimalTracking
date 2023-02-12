using DreamChip.AnimalTracking.WebApi.Filters;

namespace DreamChip.AnimalTracking.WebApi;

internal static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddScoped<AuthorizationFilter>();

        return services;
    }
}
