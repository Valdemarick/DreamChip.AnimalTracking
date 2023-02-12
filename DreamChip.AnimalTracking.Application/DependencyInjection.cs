using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DreamChip.AnimalTracking.Application.Services;

namespace DreamChip.AnimalTracking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<AccountService>();

        return services;
    }
}
