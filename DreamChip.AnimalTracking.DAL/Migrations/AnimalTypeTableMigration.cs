using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(1)]
public sealed class AnimalTypeTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table("animal_type").Exists())
        {
            Create.Table("animal_type")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("type").AsString().NotNullable().Unique();
        }
    }

    public override void Down()
    {
        Delete.Table("animal");
    }
}
