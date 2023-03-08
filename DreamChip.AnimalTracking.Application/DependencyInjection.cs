using DreamChip.AnimalTracking.Application.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DreamChip.AnimalTracking.Application.Abstractions.Services;
using FluentValidation;

namespace DreamChip.AnimalTracking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        services.AddScoped<AccountService>()
            .AddScoped<LocationService>()
            .AddScoped<IAnimalTypeService, AnimalTypeService>()
            .AddScoped<IAnimalService, AnimalService>();

        return services;
    }
}
