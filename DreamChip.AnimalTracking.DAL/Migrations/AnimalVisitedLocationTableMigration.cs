using DreamChip.AnimalTracking.Domain.Entities;
using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(5)]
public sealed class AnimalVisitedLocationTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table(nameof(AnimalVisitedLocation)).Exists())
        {
            Create.Table(nameof(AnimalVisitedLocation))
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("DateTimeOfVisitLocation").AsDateTime().NotNullable()
                .WithColumn("AnimalId").AsInt64().NotNullable()
                .WithColumn("LocationId").AsInt64().Nullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table(nameof(AnimalVisitedLocation)).Exists())
        {
            Delete.Table(nameof(AnimalVisitedLocation));
        }
    }
}
