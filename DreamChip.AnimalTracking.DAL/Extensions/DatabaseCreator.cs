using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace DreamChip.AnimalTracking.DAL.Extensions;

/// <summary>
/// A static class that's responsible for database creation and data seeding.
/// </summary>
public static class DatabaseCreator
{
    public static async Task CreateDatabase(IServiceCollection services)
    {
        try
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                Console.WriteLine("Started to create database...");

                await CreateDatabaseAndSeedData(serviceProvider);

                var runner = serviceProvider.GetService<IMigrationRunner>();
                runner.MigrateUp();

                Console.WriteLine("Database created successfully");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while creating database");
            throw;
        }
    }

    private static async Task CreateDatabaseAndSeedData(IServiceProvider serviceProvider)
    {
        var config = serviceProvider.GetRequiredService<IConfiguration>();
        var connectionString = config.GetConnectionString("DatabaseConnection");
        if (connectionString == null)
        {
            throw new InvalidOperationException("Connection string not found.");
        }

        var parameters = connectionString.Split(";");
        var paramName = "Database";
        var paramValues = parameters
            .First(x => x.StartsWith(paramName, StringComparison.InvariantCultureIgnoreCase))
            .Split('=');

        if (paramValues.Length != 2)
        {
            throw new InvalidOperationException($"Param value for {paramName} not found");
        }

        var databaseName = paramValues[1];

        connectionString = string.Join(";", parameters.Where(s => !s.StartsWith(paramName, StringComparison.InvariantCultureIgnoreCase)));

        await using (var connection = new NpgsqlConnection(connectionString))
        { 
            await connection.OpenAsync();

            var findDatabaseQuery = "SELECT * FROM postgres.pg_catalog.pg_database where datname = @name";
            await using var command = new NpgsqlCommand(findDatabaseQuery, connection);
            command.Parameters.AddWithValue("name", databaseName);

            var result = await command.ExecuteScalarAsync();
            if (result is null)
            {
                command.CommandText = $"CREATE DATABASE \"{databaseName}\"";
                await command.ExecuteScalarAsync();
            }
        }
    }
}
