using System.Formats.Tar;
using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Domain.Entities;
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
                            {GetAnimalTypeColumns("an_t")}
                     FROM public.animal an
                     LEFT JOIN public.animal_visited_location avl ON avl.animal_id = an.id
                     LEFT JOIN public.animal_type_animal ata ON ata.animal_id = an.id
                     LEFT JOIN public.animal_type an_t ON an_t.id = ata.animal_type_id
                     WHERE an.id = @id";

        var connection = await OpenConnection();

        return (await connection.QueryAsync<Animal, AnimalVisitedLocation, AnimalType, Animal>(
                sql, (animal, animalVisitedLocation, animalType) =>
                {
                    if (animalVisitedLocation is not null)
                    {
                        animal.AnimalVisitedLocations.Add(animalVisitedLocation);
                    }

                    if (animalType is not null)
                    {
                        animal.AnimalTypes.Add(animalType);
                    }

                    return animal;
                },
                new { id },
                splitOn: "animal_id, animal_id, id"))
            .FirstOrDefault();
    }

    private string GetAnimalColumns(string? tableName = null)
    {
        var alias = tableName is not null ? $"{tableName}." : null;

        return $"{alias}id, {alias}weight, {alias}length";
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
}
