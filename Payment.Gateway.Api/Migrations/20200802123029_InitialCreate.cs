using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Gateway.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentData",
                columns: table => new
                {
                    PaymentId = table.Column<string>(nullable: false),
                    BankAccountNumber = table.Column<int>(nullable: false),
                    SortCode = table.Column<int>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PaymentValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentData", x => x.PaymentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentData");
        }
    }
}
