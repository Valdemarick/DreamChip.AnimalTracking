using System.Text;
using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.DAL.Extensions;
using DreamChip.AnimalTracking.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public sealed class AnimalTypeRepository : BaseRepository<AnimalType, long>, IAnimalTypeRepository
{
    public AnimalTypeRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string TableName { get; set; } = nameof(AnimalType);
    protected override string[] Columns { get; set; } = { "Id", "Type" };

    public async Task<List<AnimalType>> GetByIdsAsync(List<long> ids)
    {
        var sql = new StringBuilder()
            .Select(TableName, Columns)
            .From(TableName)
            .Where("\"Id\" = ANY(@ids)")
            .ToString();

        var connection = await OpenConnection();

        var animalTypes = (await connection.QueryAsync<AnimalType>(sql, new { ids })).ToList();
        
        connection.Close();

        return animalTypes;
    }

    public async Task<AnimalType?> GetByName(string type)
    {
        var sql = new StringBuilder()
            .Select(TableName, Columns)
            .From(TableName)
            .Where("\"Type\" = @type")
            .ToString();

        var connection = await OpenConnection();

        var animalType = await connection.QueryFirstOrDefaultAsync<AnimalType>(sql, new { type });
        
        connection.Close();

        return animalType;
    }
}
