using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(6)]
public class AnimalTypeAnimalTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table("AnimalTypeAnimal").Exists())
        {
            Create.Table("AnimalTypeAnimal")
                .WithColumn("AnimalTypeId").AsInt64().NotNullable()
                .WithColumn("AnimalId").AsInt64().NotNullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table("AnimalTypeAnimal").Exists())
        {
            Delete.Table("AnimalTypeAnimal");
        }
    }
}
