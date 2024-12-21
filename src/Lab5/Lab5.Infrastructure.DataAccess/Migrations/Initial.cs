using FluentMigrator;

namespace Lab5.Infrastructure.DataAccess.Migrations;

[Migration(1)]
public class Initial : Migration
{
    public override void Up()
    {
        Create.Table("accounts")
            .WithColumn("Account_id")
            .AsInt64()
            .Identity()
            .PrimaryKey()
            .WithColumn("AccountPassword")
            .AsInt64()
            .NotNullable()
            .WithColumn("Balance")
            .AsDecimal(18, 2)
            .NotNullable();

        Create.Table("OperationHistory")
            .WithColumn("OperationId")
            .AsGuid()
            .PrimaryKey()
            .WithColumn("AccountId")
            .AsInt64()
            .Identity()
            .NotNullable()
            .WithColumn("OperationType")
            .AsString(20)
            .NotNullable()
            .WithColumn("Amount")
            .AsDecimal(18, 2)
            .NotNullable();
    }

    public override void Down()
    {
        Delete.Table("OperationHistory");
        Delete.Table("accounts");
    }
}