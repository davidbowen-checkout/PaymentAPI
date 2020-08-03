using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Gateway.Api.Migrations
{
    public partial class AddNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardHolderName",
                table: "PaymentData",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardHolderName",
                table: "PaymentData");
        }
    }
}
