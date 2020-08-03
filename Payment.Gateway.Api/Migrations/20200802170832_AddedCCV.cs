using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Gateway.Api.Migrations
{
    public partial class AddedCCV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CcvNumber",
                table: "PaymentData",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CcvNumber",
                table: "PaymentData");
        }
    }
}
