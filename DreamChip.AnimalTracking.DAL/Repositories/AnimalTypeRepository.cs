using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public sealed class AnimalTypeRepository : BaseRepository, IAnimalTypeRepository
{
    public AnimalTypeRepository(IConfiguration configuration) : base(configuration)
    {
    }
    
    public async Task<AnimalType?> GetByIdAsync(long id)
    {
        var sql = @"SELECT id, type
                    FROM public.animal_type
                    WHERE id = @id";

        var connection = await OpenConnection();

        return await connection.QueryFirstOrDefaultAsync<AnimalType>(sql, new { id });
    }

    public async Task<List<AnimalType>> GetByIdsAsync(List<long> ids)
    {
        var sql = @"SELECT id, type
                    FROM public.animal_type
                    WHERE id = ANY(@ids)";

        var connection = await OpenConnection();

        return (await connection.QueryAsync<AnimalType>(sql, new { ids })).ToList();
    }

    public async Task<AnimalType?> GetByName(string type)
    {
        var sql = @"SELECT id, type
                    FROM public.animal_type
                    WHERE type = @type";

        var connection = await OpenConnection();

        return await connection.QueryFirstOrDefaultAsync<AnimalType>(sql, new { type });
    }
        
    public async Task<long> CreateAsync(AnimalType animalType)
    {
        var sql = @"INSERT INTO public.animal_type (type)
                    VALUES (@type)
                    RETURNING id";

        var connection = await OpenConnection();
        var id = await connection.ExecuteScalarAsync<long>(sql, new { type = animalType.Type });

        return id;
    }

    public async Task UpdateAsync(AnimalType animalType)
    {
        var sql = @"UPDATE public.animal_type
                    SET type = @type
                    WHERE id = @id";

        var connection = await OpenConnection();
        var id = await connection.ExecuteAsync(sql, new
        {
            id = animalType.Id,
            type = animalType.Type
        });
    }

    public async Task DeleteAsync(long id)
    {
        var sql = @"DELETE FROM public.animal_type
                    WHERE id = @id";

        var connection = await OpenConnection();
        await connection.ExecuteAsync(sql, new { id });
    }
}
