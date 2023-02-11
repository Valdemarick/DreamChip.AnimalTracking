using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(5)]
public sealed class AnimalVisitedLocationTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table("animal_visited_location").Exists())
        {
            Create.Table("animal_visited_location")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("date_time_of_visit_location_point").AsDateTime().NotNullable()
                .WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("animal_id").AsInt64().NotNullable()
                .WithColumn("location_id").AsInt64().Nullable();
        }
    }

    public override void Down()
    {
        Delete.Table("animal_visited_location");
    }
}
