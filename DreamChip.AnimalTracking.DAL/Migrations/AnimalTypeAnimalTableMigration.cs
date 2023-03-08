using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(6)]
public class AnimalTypeAnimalTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table("animal_type_animal").Exists())
        {
            Create.Table("animal_type_animal")
                .WithColumn("animal_type_id").AsInt64().NotNullable()
                .WithColumn("animal_id").AsInt64().NotNullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table("animal_type_animal").Exists())
        {
            Delete.Table("animal_type_animal");
        }
    }
}
