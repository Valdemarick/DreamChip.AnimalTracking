using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.Enums;
using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(4)]
public sealed class AnimalTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table(nameof(Animal)).Exists())
        {
            Create.Table(nameof(Animal))
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Length").AsFloat().NotNullable()
                .WithColumn("Weight").AsFloat().NotNullable()
                .WithColumn("Height").AsFloat().NotNullable()
                .WithColumn("Gender").AsInt16().NotNullable()
                .WithColumn("LifeStatus").AsInt16().NotNullable().WithDefaultValue((uint)LifeStatus.Alive)
                .WithColumn("ChipperId").AsInt32().NotNullable()
                .WithColumn("DeathDateTime").AsDateTime().Nullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table(nameof(Animal)).Exists())
        {
            Delete.Table(nameof(Animal));
        }
    }
}
