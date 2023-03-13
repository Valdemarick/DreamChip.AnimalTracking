using System.Text;
using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.DAL.Extensions;
using DreamChip.AnimalTracking.DAL.RepositoryMetadata;
using DreamChip.AnimalTracking.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public sealed class LocationRepository : BaseRepository<Location, long>, ILocationRepository
{
    public LocationRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string TableName { get; set; } = nameof(Location);
    protected override string[] Columns { get; set; } = { "Id", "Latitude", "Longitude" };

    public override async Task<Location?> GetByIdAsync(long id)
    {
        var sql = new StringBuilder()
            .Select(TableName, Columns)
            .AddColumns(AnimalVisitedLocationTableMetadata.TableName, AnimalVisitedLocationTableMetadata.Columns)
            .From(TableName)
            .LeftJoin(TableName, AnimalVisitedLocationTableMetadata.TableName, "Id", "LocationId")
            .Where($"\"{TableName}\".\"Id\" = @Id")
            .ToString();

        var connection = await OpenConnection();

        var location = (await connection.QueryAsync<Location?, AnimalVisitedLocation?, Location?>(
                sql,
                (location, animalVisitedLocation) =>
                {
                    if (animalVisitedLocation is not null)
                    {
                        location?.AnimalVisitedLocations.Add(animalVisitedLocation);
                    }

                    return location;
                },
                new { id }))
            .AsQueryable()
            .FirstOrDefault();
        
        connection.Close();

        return location;
    }

    public async Task<Location?> GetByCoordinatesAsync(double latitude, double longitude)
    {
        var sql = new StringBuilder()
            .Select(TableName, Columns)
            .From(TableName)
            .Where(new[] { $"\"{TableName}\".\"Latitude\" = @Latitude", $"\"{TableName}\".\"Longitude\" = @Longitude" })
            .ToString();

        var connection = await OpenConnection();

        var location = await connection.QueryFirstOrDefaultAsync<Location>(sql, new
        {
            latitude, longitude
        });
        
        connection.Close();

        return location;
    }
}
