using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.DAL.Repositories;

namespace DreamChip.AnimalTracking.DAL;

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

        services.AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<ILocationRepository, LocationRepository>();

        return services;
    }
}