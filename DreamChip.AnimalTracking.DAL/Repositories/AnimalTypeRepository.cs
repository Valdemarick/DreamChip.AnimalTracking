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
}
