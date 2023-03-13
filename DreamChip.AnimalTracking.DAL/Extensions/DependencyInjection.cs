using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.DAL.Repositories;

namespace DreamChip.AnimalTracking.DAL.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DatabaseConnection");

        services.AddFluentMigratorCore()
            .ConfigureRunner(config =>
            {
                config.AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly())
                    .For
                    .All();
            });

        services.AddTransient<IAccountRepository, AccountRepository>()
            .AddTransient<ILocationRepository, LocationRepository>()
            .AddTransient<IAnimalTypeRepository, AnimalTypeRepository>()
            .AddTransient<IAnimalRepository, AnimalRepository>()
            .AddTransient<IAnimalVisitedLocationRepository, AnimalVisitedLocationRepository>();
        
        DapperExtensions.DapperAsyncExtensions.SetMappingAssemblies(new List<Assembly>()
        {
            Assembly.GetExecutingAssembly()
        });

        return services;
    }
}