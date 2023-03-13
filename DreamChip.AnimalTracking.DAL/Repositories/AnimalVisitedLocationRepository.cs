using System.Text;
using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.DAL.Extensions;
using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.ValueObjects.AnimalVisitedLocation;
using Microsoft.Extensions.Configuration;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public sealed class AnimalVisitedLocationRepository : BaseRepository<AnimalVisitedLocation, long>, IAnimalVisitedLocationRepository
{
    public AnimalVisitedLocationRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string TableName { get; set; } = nameof(AnimalVisitedLocation);
    protected override string[] Columns { get; set; } =
        { "Id", "AnimalId", "LocationId", "DateTimeOfVisitLocation" };
    
    public async Task<List<AnimalVisitedLocation>> GetPageAsync(long animalId, AnimalVisitedLocationPageRequest request)
    {
        var filters = CreateAnimalVisitedLocationPageFilter(request).ToList();
        
        var sql = new StringBuilder()
            .Select(TableName, Columns)
            .From(TableName)
            .Where(filters)
            .OrderByDescending($"DateTimeOfVisitLocation")
            .Offset(request.From, request.Size)
            .ToString();

        var connection = await OpenConnection();

        var animalVisitedLocations = (await connection.QueryAsync<AnimalVisitedLocation>(sql,
                new
                {
                    animalId = animalId,
                    startDateTime = request.StartDateTime,
                    endDateTime = request.EndDateTime,
                    from = request.From,
                    size = request.Size
                }))
            .AsQueryable()
            .ToList();
        
        connection.Close();

        return animalVisitedLocations;
    }

    private IEnumerable<string> CreateAnimalVisitedLocationPageFilter(AnimalVisitedLocationPageRequest request)
    {
        var filterConditions = new List<string>();

        filterConditions.Add("\"AnimalId\" = @AnimalId");
        
        if (request.StartDateTime is not null)
        {
            filterConditions.Add("\"DateTimeOfVisitLocation\" > @StartDateTime");
        }

        if (request.EndDateTime is not null)
        {
            filterConditions.Add("\"DateTimeOfVisitLocation\" < @EndDateTime");
        }

        return filterConditions;
    }
}
