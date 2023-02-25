using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(2)]
public sealed class LocationPointTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table("location").Exists())
        {
            Create.Table("location_point")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("latitude").AsDouble().NotNullable()
                .WithColumn("longitude").AsDouble().NotNullable();
        }
    }

    public override void Down()
    {
        Delete.Table("location");
    }
}
