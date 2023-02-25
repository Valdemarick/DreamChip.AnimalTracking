using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public sealed class LocationRepository : BaseRepository, ILocationRepository
{
    private readonly string[] _columns = { "id", "latitude", "longitude" };
    
    public LocationRepository(IConfiguration configuration) : base(configuration)
    {
    }
    
    public async Task<Location?> GetByIdAsync(long id)
    {
        var sql = $@"SELECT {_columns}
                    FROM public.location
                    WHERE id = @id";

        var connection = await OpenConnection();

        return await connection.QueryFirstAsync<Location>(sql, new { id });
    }
}