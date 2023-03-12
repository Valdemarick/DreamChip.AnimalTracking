using System.Text;
using Dapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.DAL.Extensions;
using DreamChip.AnimalTracking.DAL.RepositoryMetadata;
using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.ValueObjects.Animal;
using Microsoft.Extensions.Configuration;

namespace DreamChip.AnimalTracking.DAL.Repositories;

public sealed class AnimalRepository : BaseRepository<Animal, long>, IAnimalRepository
{
    public AnimalRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string TableName { get; set; } = nameof(Animal);
    protected override string[] Columns { get; set; } = { "Id", "Weight", "Length", "Height", "Gender", "ChipperId", "LifeStatus", "DeathDateTime" };

    public override async Task<Animal?> GetByIdAsync(long id)
    {
        var animalVisitedLocation = (AnimalVisitedLocationTableMetadata.TableName, AnimalVisitedLocationTableMetadata.Columns);
        var animalType = (AnimalTypeTableMetadata.TableName, AnimalTypeTableMetadata.Columns);
        var animalTypeAnimal = (AnimalTypeAnimalTableMetadata.TableName, AnimalTypeAnimalTableMetadata.Columns);
        var animalChippingLocation = (AnimalChippingLocationTableMetadata.TableName, AnimalChippingLocationTableMetadata.Columns);

        var subQuery = new StringBuilder()
            .Select(animalType.TableName, animalType.Columns)
            .AddColumns(animalTypeAnimal.TableName, animalTypeAnimal.Columns)
            .From(animalType.TableName)
            .InnerJoin(animalType.TableName, animalTypeAnimal.TableName, "Id", "AnimalTypeId")
            .ToString();

        var alias = "AnimalType";
        
        var sql = new StringBuilder()
            .Select(TableName, Columns)
            .AddColumns(animalVisitedLocation.TableName, animalVisitedLocation.Columns)
            .AddColumns(animalType.TableName, animalType.Columns)
            .AddColumns(animalChippingLocation.TableName, animalChippingLocation.Columns)
            .From(TableName)
            .LeftJoin(TableName, animalVisitedLocation.TableName, "Id", "AnimalId")
            .LeftJoinSubQuery(TableName, $"({subQuery})", alias, "Id", "AnimalId")
            .LeftJoin(TableName, animalChippingLocation.TableName, "Id", "AnimalId")
            .Where($"\"{TableName}\".\"Id\" = @Id")
            .ToString();

        var dict = new Dictionary<long, Animal>();
        
        var connection = await OpenConnection();

        var rows =
            (await connection.QueryAsync<Animal, AnimalVisitedLocation, AnimalType, AnimalChippingLocation, Animal>(
                sql, (animal, animalVisitedLocation, animalType, animalChippingLocation) =>
                {
                    Animal entry;

                    if (!dict.TryGetValue(id, out entry))
                    {
                        entry = animal;
                        dict.Add(id, animal);
                    }

                    if (animalType is not null)
                    {
                        entry.AnimalTypes.Add(animalType);
                    }

                    if (animalVisitedLocation is not null)
                    {
                        entry.AnimalVisitedLocations.Add(animalVisitedLocation);
                    }

                    if (animalChippingLocation is not null)
                    {
                        entry.ChippingLocation = animalChippingLocation;
                    }

                    return entry;
                },
                new { id }))
            .AsQueryable()
            .FirstOrDefault();

        connection.Close();

        return rows;
    }

    public async Task<List<Animal>> GetPageAsync(AnimalPageRequest request)
    {
        var animalVisitedLocation = (AnimalVisitedLocationTableMetadata.TableName, AnimalVisitedLocationTableMetadata.Columns);
        var animalType = (AnimalTypeTableMetadata.TableName, AnimalTypeTableMetadata.Columns);
        var animalTypeAnimal = (AnimalTypeAnimalTableMetadata.TableName, AnimalTypeAnimalTableMetadata.Columns);
        var animalChippingLocation = (AnimalChippingLocationTableMetadata.TableName, AnimalChippingLocationTableMetadata.Columns);

        var subQuery = new StringBuilder()
            .Select(animalType.TableName, animalType.Columns)
            .AddColumns(animalTypeAnimal.TableName, animalTypeAnimal.Columns)
            .From(animalType.TableName)
            .InnerJoin(animalType.TableName, animalTypeAnimal.TableName, "Id", "AnimalTypeId")
            .ToString();

        var alias = "AnimalType";

        var filter = CreateAnimalPageFilter(request).ToList();
        
        var sql = new StringBuilder()
            .Select(TableName, Columns)
            .AddColumns(animalVisitedLocation.TableName, animalVisitedLocation.Columns)
            .AddColumns(animalType.TableName, animalType.Columns)
            .AddColumns(animalChippingLocation.TableName, animalChippingLocation.Columns)
            .From(TableName)
            .LeftJoin(TableName, animalVisitedLocation.TableName, "Id", "AnimalId")
            .LeftJoinSubQuery(TableName, $"({subQuery})", alias, "Id", "AnimalId")
            .LeftJoin(TableName, animalChippingLocation.TableName, "Id", "AnimalId")
            .Where(filter)
            .OrderByAscending("\"Animal\".\"Id\"")
            .Offset(request.From, request.Size)
            .ToString();

        var dict = new Dictionary<long, Animal>();
        
        var connection = await OpenConnection();

        var animals = (await connection.QueryAsync<Animal, AnimalVisitedLocation, AnimalType, AnimalChippingLocation, Animal>(
                sql, (animal, animalVisitedLocation, animalType, animalChippingLocation) =>
                {
                    Animal entry;

                    if (!dict.TryGetValue(animal.Id, out entry))
                    {
                        entry = animal;
                        dict.Add(animal.Id, animal);
                    }

                    if (animalType is not null)
                    {
                        entry.AnimalTypes.Add(animalType);
                    }

                    if (animalVisitedLocation is not null)
                    {
                        entry.AnimalVisitedLocations.Add(animalVisitedLocation);
                    }

                    if (animalChippingLocation is not null)
                    {
                        entry.ChippingLocation = animalChippingLocation;
                    }

                    return entry;
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
                }))
            .AsQueryable()
            .ToList();
        
        connection.Close();

        return animals;
    }

    public async Task AddTypeAsync(long animalId, long typeId)
    {
        var sql = new StringBuilder()
            .Insert(AnimalTypeAnimalTableMetadata.TableName, AnimalTypeAnimalTableMetadata.Columns)
            .ToString();

        var connection = await OpenConnection();

        await connection.ExecuteAsync(sql, new
        {
            animalId = animalId,
            animalTypeId = typeId
        });
        
        connection.Close();
    }

    public override async Task<long> CreateAsync(Animal animal)
    {
        var firstInsertStatement = new StringBuilder()
            .Insert(TableName, Columns.Skip(1))
            .ReturningId()
            .ToString();

        var secondInsertStatement = new StringBuilder()
            .Insert(AnimalChippingLocationTableMetadata.TableName, AnimalChippingLocationTableMetadata.Columns.Skip(1))
            .ToString();
        
        var thirdInsertStatement = new StringBuilder()
            .Insert(AnimalTypeAnimalTableMetadata.TableName, AnimalTypeAnimalTableMetadata.Columns)
            .ToString();

        var connection = await OpenConnection();
        using var transaction = connection.BeginTransaction();

        long animalId = 0;
        
        try
        {
            animalId = await connection.ExecuteScalarAsync<long>(firstInsertStatement, animal, transaction);

            await connection.ExecuteAsync(secondInsertStatement, new
            {
                animalId = animalId,
                locationId = animal.ChippingLocation?.LocationId,
                chippingDateTime = DateTime.UtcNow
            }, transaction);

            foreach (var animalType in animal.AnimalTypes)
            {
                await connection.ExecuteAsync(thirdInsertStatement, new
                {
                    animalId = animalId,
                    animalTypeId = animalType.Id
                }, transaction);
            }
        }
        catch
        {
            transaction.Rollback();

            throw;
        }
        
        transaction.Commit();
        
        connection.Close();

        return animalId;
    }

    public override async Task DeleteAsync(long id)
    {
        var firstDeleteStatement = new StringBuilder()
            .Delete(TableName)
            .Where($"\"{TableName}\".\"Id\" = @Id")
            .ToString();

        var secondDeleteStatement = new StringBuilder()
            .Delete(AnimalChippingLocationTableMetadata.TableName)
            .Where($"\"{AnimalChippingLocationTableMetadata.TableName}\".\"AnimalId\" = @AnimalId")
            .ToString();

        var thirdDeleteStatement = new StringBuilder()
            .Delete(AnimalTypeAnimalTableMetadata.TableName)
            .Where($"\"{AnimalTypeAnimalTableMetadata.TableName}\".\"AnimalId\" = @AnimalId")
            .ToString();

        var connection = await OpenConnection();
        using var transaction = connection.BeginTransaction();

        try
        {
            await connection.ExecuteAsync(firstDeleteStatement, new { id });

            await connection.ExecuteAsync(secondDeleteStatement, new { animalId = id });

            await connection.ExecuteAsync(thirdDeleteStatement, new { animalId = id });
        }
        catch
        {
            transaction.Rollback();

            throw;
        }
        
        transaction.Commit();
        
        connection.Close();
    }

    private IEnumerable<string> CreateAnimalPageFilter(AnimalPageRequest request)
    {
        var filterConditions = new List<string>();

        if (request.Gender is not null)
        {
            filterConditions.Add("\"Gender\" = @Gender");
        }

        if (request.LifeStatus is not null)
        {
            filterConditions.Add("\"FirstName\" = @LifeStatus");
        }

        if (request.StartDateTime is not null)
        {
            filterConditions.Add("\"ChippingDateTime\" > @StartDateTime");
        }
        
        if (request.EndDateTime is not null)
        {
            filterConditions.Add("\"ChippingDateTime\" < @EndDateTime");
        }

        if (request.ChipperId is not null)
        {
            filterConditions.Add("\"ChipperId\" = @ChipperId");
        }

        if (request.ChippingLocationId is not null)
        {
            filterConditions.Add("\"ChippingLocationId\" = @ChippingLocationId");
        }

        return filterConditions;
    }
}
