using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.ValueObjects.Animal;
using Microsoft.Extensions.Configuration;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public sealed class AnimalRepository : BaseRepository, IAnimalRepository
{
    public AnimalRepository(IConfiguration configuration) : base(configuration)
    {
    }
    
    public async Task<Animal?> GetByIdAsync(long id)
    {
        var sql = $@"SELECT {GetAnimalColumns("an")},
                            {GetAnimalVisitedLocationColumns("avl")},
                            {GetAnimalTypeAnimalColumns("ata")},
                            {GetAnimalTypeColumns("an_t")},
                            {GetAnimalChippingLocationColumns("acl")}
                     FROM public.animal an
                     LEFT JOIN public.animal_visited_location avl ON avl.animal_id = an.id
                     LEFT JOIN public.animal_type_animal ata ON ata.animal_id = an.id
                     LEFT JOIN public.animal_type an_t ON an_t.id = ata.animal_type_id
                     LEFT JOIN public.animal_chipping_location acl ON acl.animal_id = an.id
                     WHERE an.id = @id";

        var connection = await OpenConnection();

        return (await connection.QueryAsync<Animal, AnimalVisitedLocation, AnimalType, AnimalChippingLocation, Animal>(
                sql, (animal, animalVisitedLocation, animalType, animalChippingLocation) =>
                {
                    if (animalVisitedLocation is not null)
                    {
                        animal.AnimalVisitedLocations.Add(animalVisitedLocation);
                    }

                    if (animalType is not null)
                    {
                        animal.AnimalTypes.Add(animalType);
                    }

                    if (animalChippingLocation is not null)
                    {
                        animal.ChippingLocation = animalChippingLocation;
                    }

                    return animal;
                },
                new { id },
                splitOn: "animal_id, animal_id, id, animal_id"))
            .AsQueryable()
            .FirstOrDefault();
    }

    public async Task<List<Animal>> GetPageAsync(AnimalPageRequest request)
    {
        var sql = $@"SELECT {GetAnimalColumns("an")},
                            {GetAnimalVisitedLocationColumns("avl")},
                            {GetAnimalTypeAnimalColumns("ata")},
                            {GetAnimalTypeColumns("an_t")},
                            {GetAnimalChippingLocationColumns("acl")}
                     FROM public.animal an
                     LEFT JOIN public.animal_visited_location avl ON avl.animal_id = an.id
                     LEFT JOIN public.animal_type_animal ata ON ata.animal_id = an.id
                     LEFT JOIN public.animal_type an_t ON an_t.id = ata.animal_type_id
                     LEFT JOIN public.animal_chipping_location acl ON an.id = acl.animal_id
                     WHERE an.chipper_id = @chipperId AND acl.chipping_location_id AND an.life_statues::TEXT = @lifeStatus AND
                     an.gender::TEXT = @gender AND acl.chipping_date_time BETWEEN @startDateTime AND @endDateTime
                     ORDER BY an.id
                     OFFSET @from ROWS FETCH NEXT @size ROWS ONLY";

        var connection = await OpenConnection();

        return (await connection.QueryAsync<Animal, AnimalVisitedLocation, AnimalType, AnimalChippingLocation, Animal>(
                sql, (animal, animalVisitedLocation, animalType, animalChippingLocation) =>
                {
                    if (animalVisitedLocation is not null)
                    {
                        animal.AnimalVisitedLocations.Add(animalVisitedLocation);
                    }

                    if (animalType is not null)
                    {
                        animal.AnimalTypes.Add(animalType);
                    }

                    if (animalChippingLocation is not null)
                    {
                        animal.ChippingLocation = animalChippingLocation;
                    }

                    return animal;
                },
                new
                {
                    startDateTime = request.StartDateTime,
                    endDateTime = request.EndDateTime,
                    chipperId = request.ChipperId,
                    chippingLocationId = request.ChippingLocationId,
                    lifeStatus = request.LifeStatus,
                    gender = request.Gender,
                    from = request.From,
                    size = request.Size
                },
                splitOn: "animal_id, animal_id, id, id"))
            .AsQueryable()
            .ToList();
    }

    private string GetAnimalColumns(string? tableName = null)
    {
        var alias = tableName is not null ? $"{tableName}." : null;

        return $"{alias}id, {alias}weight, {alias}length, {alias}height, {alias}gender, {alias}chipper_id," +
               $"{alias}life_status, {alias}death_date_time";
    }

    private string GetAnimalVisitedLocationColumns(string? tableName = null)
    {
        var alias = tableName is not null ? $"{tableName}." : null;

        return $"{alias}location_id, {alias}animal_id";
    }

    private string GetAnimalTypeAnimalColumns(string? tableName = null)
    {
        var alias = tableName is not null ? $"{tableName}." : null;

        return $"{alias}animal_type_id, {alias}animal_id";
    }

    private string GetAnimalTypeColumns(string? tableName = null)
    {
        var alias = tableName is not null ? $"{tableName}." : null;

        return $"{alias}id";
    }

    private string GetAnimalChippingLocationColumns(string? tableName = null)
    {
        var alias = tableName is not null ? $"{tableName}." : null;

        return $"{alias}animal_id, {alias}location_id, {alias}chipping_date_time";
    }
}
