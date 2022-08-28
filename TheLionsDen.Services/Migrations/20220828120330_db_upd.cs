using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheLionsDen.Services.Migrations
{
    public partial class db_upd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialRequests",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "CVC",
                table: "PaymentDetails");

            migrationBuilder.RenameColumn(
                name: "ExpDate",
                table: "PaymentDetails",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "CardNumber",
                table: "PaymentDetails",
                newName: "StripeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StripeId",
                table: "PaymentDetails",
                newName: "CardNumber");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "PaymentDetails",
                newName: "ExpDate");

            migrationBuilder.AddColumn<string>(
                name: "SpecialRequests",
                table: "Reservation",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CVC",
                table: "PaymentDetails",
                type: "int",
                nullable: true);
        }
    }
}
