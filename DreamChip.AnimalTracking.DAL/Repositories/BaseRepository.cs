using System.Data;
using System.Text;
using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.DAL.Extensions;
using DreamChip.AnimalTracking.Domain;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    private readonly string _connectionString;

    protected BaseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection")!;
    }
    
    protected abstract string TableName { get; set; }
    protected abstract string[] Columns { get; set; }

    protected async Task<IDbConnection> OpenConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        return connection;
    }

    public virtual async Task<TEntity?> GetByIdAsync(TKey id)
    {
        var sql = new StringBuilder()
            .Select(TableName, Columns)
            .From(TableName)
            .Where("\"Id\" = @id")
            .ToString();

        var connection = await OpenConnection();

        var entity = await connection.QueryFirstOrDefaultAsync<TEntity>(sql, new { id });
        
        connection.Close();

        return entity;
    }

    public virtual async Task<TKey> CreateAsync(TEntity entity)
    {
        var insertColumns = Columns.Skip(1).ToList();
        
        var sql = new StringBuilder()
            .Insert(TableName, insertColumns)
            .ReturningId()
            .ToString();

        var connection = await OpenConnection();

        var id = await connection.ExecuteScalarAsync<TKey>(sql, entity);
        
        connection.Close();

        return id;
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        var updateColumns = Columns.Skip(1).ToList();

        var sql = new StringBuilder()
            .Update(TableName, updateColumns)
            .Where("\"Id\" = @Id")
            .ToString();

        var connection = await OpenConnection();

        await connection.ExecuteAsync(sql, entity);
        
        connection.Close();
    }

    public virtual async Task DeleteAsync(TKey id)
    {
        var sql = new StringBuilder()
            .Delete(TableName)
            .Where("\"Id\" = @Id")
            .ToString();

        var connection = await OpenConnection();

        await connection.ExecuteAsync(sql, new { id });
        
        connection.Close();
    }
}