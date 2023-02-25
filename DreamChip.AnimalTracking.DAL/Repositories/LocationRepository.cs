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
        var sql = $@"SELECT {string.Join(',', _columns)}
                     FROM public.location
                     WHERE id = @id";

        var connection = await OpenConnection();

        return await connection.QueryFirstOrDefaultAsync<Location>(sql, new { id });
    }

    public async Task<Location?> GetByCoordinatesAsync(double latitude, double longitude)
    {
        var sql = $@"SELECT {string.Join(',', _columns)}
                     FROM public.location
                     WHERE latitude = @latitude AND longitude = @longitude";

        var connection = await OpenConnection();

        return await connection.QueryFirstOrDefaultAsync<Location>(sql, new
        {
            latitude = latitude,
            longitude = longitude
        });
    }

    public async Task<long> CreateAsync(Location location)
    {
        var sql = @"INSERT INTO public.location (latitude, longitude)
                    VALUES (@Latitude, @Longitude)
                    RETURNING id";

        var connection = await OpenConnection();

        var id = await connection.ExecuteScalarAsync<long>(sql, location);

        return id;
    }
}