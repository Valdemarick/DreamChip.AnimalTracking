using DreamChip.AnimalTracking.Domain.Entities;
using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(3)]
public sealed class AccountTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table(nameof(Account)).Exists())
        {
            Create.Table(nameof(Account))
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table(nameof(Account)).Exists())
        {
            Delete.Table(nameof(Account));
        }
    }
}
