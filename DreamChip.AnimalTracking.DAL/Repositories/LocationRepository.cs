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
        var sql = $@"SELECT {GetLocationColumns("l")},
                            {GetAnimalVisitedLocationColumns("avl")},
                            {GetAnimalColumns("a")}
                     FROM public.location l
                     LEFT JOIN public.animal_visited_location avl ON l.id = avl.location_id
                     LEFT JOIN animal a ON avl.animal_id = a.id
                     WHERE l.id = @id";

        var connection = await OpenConnection();

        return (await connection.QueryAsync<Location, AnimalVisitedLocation, Animal, Location?>(
                sql,
                (location, animalVisitedLocation, animal) =>
                {
                    animal?.AnimalVisitedLocations.Add(animalVisitedLocation);
                    location?.AnimalVisitedLocations.Add(animalVisitedLocation);

                    return location;
                }, 
                new { id },
                splitOn: "location_id, id"))
            .AsQueryable()
            .FirstOrDefault();
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

    public async Task<Location> UpdateAsync(Location location)
    {
        var sql = @"UPDATE public.location
                    SET latitude = @latitude, longitude = @longitude
                    WHERE id = @id";

        var connection = await OpenConnection();

        await connection.ExecuteAsync(sql, new
        {
            id = location.Id,
            latitude = location.Latitude,
            longitude = location.Longitude
        });

        return location;
    }

    public async Task DeleteAsync(long id)
    {
        var sql = @"DELETE FROM public.location
                    WHERE id = @id";

        var connection = await OpenConnection();
        await connection.ExecuteAsync(sql, new { id });
    }

    private string GetAnimalVisitedLocationColumns(string? tableName = null)
    {
        var alias = tableName is not null ? $"{tableName}." : null; 
        
        return $"{alias}animal_id, {alias}location_id";
    }

    private string GetLocationColumns(string? tableName = null)
    {
        var alias = tableName is not null ? $"{tableName}." : null;

        return $"{alias}id, {alias}latitude, {alias}longitude";
    }

    private string GetAnimalColumns(string? tableName = null)
    {
        var alias = tableName is not null ? $"{tableName}." : null;

        return $"{alias}id";
    }
}