using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(4)]
public sealed class AnimalTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table("animal").Exists())
        {
            Create.Table("animal")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("length").AsFloat().NotNullable()
                .WithColumn("weight").AsFloat().NotNullable()
                .WithColumn("height").AsFloat().NotNullable()
                .WithColumn("gender").AsInt16().NotNullable()
                .WithColumn("life_status").AsInt16().NotNullable()
                .WithColumn("chipper_id").AsInt32().NotNullable()
                .WithColumn("death_date_time").AsDateTime().Nullable();
        }
    }

    public override void Down()
    {
        Delete.Table("animal");
    }
}
