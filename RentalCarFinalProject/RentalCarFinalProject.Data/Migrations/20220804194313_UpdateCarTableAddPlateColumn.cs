using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalCarFinalProject.Data.Migrations
{
    public partial class UpdateCarTableAddPlateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "Plate",
                table: "Cars",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Plate",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cars",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
