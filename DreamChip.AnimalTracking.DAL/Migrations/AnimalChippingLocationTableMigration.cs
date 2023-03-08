using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(7)]
public sealed class AnimalChippingLocationTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table("animal_chipping_location").Exists())
        {
            Create.Table("animal_chipping_location")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("animal_id").AsInt64().NotNullable()
                .WithColumn("location_id").AsInt64().NotNullable()
                .WithColumn("chipping_date_time").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
        }
    }

    public override void Down()
    {
        if (Schema.Table("animal_chipping_location").Exists())
        {
            Delete.Table("animal_chipping_location");
        }
    }
}
