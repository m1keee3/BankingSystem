using FluentMigrator;

namespace Lab5.Infrastructure.DataAccess.Migrations;

[Migration(1)]
public class Initial : Migration
{
    public override void Up()
    {
        Create.Table("accounts")
            .WithColumn("accountid")
            .AsInt64()
            .Identity()
            .PrimaryKey()
            .WithColumn("accountpassword")
            .AsInt64()
            .NotNullable()
            .WithColumn("balance")
            .AsDecimal(18, 2)
            .NotNullable();

        Create.Table("operationhistory")
            .WithColumn("operationid")
            .AsGuid()
            .PrimaryKey()
            .WithColumn("accountid")
            .AsInt64()
            .Identity()
            .NotNullable()
            .WithColumn("operationtype")
            .AsString(20)
            .NotNullable()
            .WithColumn("amount")
            .AsDecimal(18, 2)
            .NotNullable();
    }

    public override void Down()
    {
        Delete.Table("operationhistory");
        Delete.Table("accounts");
    }
}