using FluentMigrator;

namespace DreamChip.AnimalTracking.DAL.Migrations;

[Migration(3)]
public sealed class AccountTableMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table("account").Exists())
        {
            Create.Table("account")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("first_name").AsString().NotNullable()
                .WithColumn("last_name").AsString().NotNullable()
                .WithColumn("email").AsString().NotNullable()
                .WithColumn("password").AsString().NotNullable();
        }
    }

    public override void Down()
    {
        Delete.Table("account");
    }
}
