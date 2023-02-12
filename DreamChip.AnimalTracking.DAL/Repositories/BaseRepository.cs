using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public abstract class BaseRepository
{
    private readonly string _connectionString;

    protected BaseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection")!;
    }

    public async Task<IDbConnection> OpenConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        DefaultTypeMap.MatchNamesWithUnderscores = true;

        return connection;
    }
}
