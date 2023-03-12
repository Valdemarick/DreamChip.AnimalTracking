using DreamChip.AnimalTracking.Domain.Entities;
using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(2)]
public sealed class LocationTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table(nameof(Location)).Exists())
        {
            Create.Table(nameof(Location))
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Latitude").AsDouble().NotNullable()
                .WithColumn("Longitude").AsDouble().NotNullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table(nameof(Animal)).Exists())
        {
            Delete.Table(nameof(Location));
        }
    }
}
