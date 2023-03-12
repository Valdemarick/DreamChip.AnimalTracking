using DreamChip.AnimalTracking.Domain.Entities;
using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(7)]
public sealed class AnimalChippingLocationTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table(nameof(AnimalChippingLocation)).Exists())
        {
            Create.Table(nameof(AnimalChippingLocation))
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AnimalId").AsInt64().NotNullable()
                .WithColumn("LocationId").AsInt64().NotNullable()
                .WithColumn("ChippingDateTime").AsDateTime().NotNullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table(nameof(AnimalChippingLocation)).Exists())
        {
            Delete.Table(nameof(AnimalChippingLocation));
        }
    }
}
