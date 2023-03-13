using DreamChip.AnimalTracking.Domain.Entities;
using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(1)]
public sealed class AnimalTypeTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table(nameof(AnimalType)).Exists())
        {
            Create.Table(nameof(AnimalType))
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Type").AsString().NotNullable().Unique();
        }
    }

    public override void Down()
    {
        if (Schema.Table(nameof(AnimalType)).Exists())
        {
            Delete.Table(nameof(AnimalType));
        }
    }
}
