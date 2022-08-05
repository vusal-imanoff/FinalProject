using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalCarFinalProject.Data.Migrations
{
    public partial class UpdateCarTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EngineId",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FuelId",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransmissionId",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "Cars",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId",
                table: "Cars",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ColorId",
                table: "Cars",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_EngineId",
                table: "Cars",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_FuelId",
                table: "Cars",
                column: "FuelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModelId",
                table: "Cars",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TransmissionId",
                table: "Cars",
                column: "TransmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_YearId",
                table: "Cars",
                column: "YearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Colors_ColorId",
                table: "Cars",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Engines_EngineId",
                table: "Cars",
                column: "EngineId",
                principalTable: "Engines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Fuels_FuelId",
                table: "Cars",
                column: "FuelId",
                principalTable: "Fuels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Transmissions_TransmissionId",
                table: "Cars",
                column: "TransmissionId",
                principalTable: "Transmissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Years_YearId",
                table: "Cars",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Categories_CategoryId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Colors_ColorId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Engines_EngineId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Fuels_FuelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Transmissions_TransmissionId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Years_YearId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BrandId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ColorId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_EngineId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_FuelId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ModelId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_TransmissionId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_YearId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EngineId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "FuelId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TransmissionId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Cars");
        }
    }
}
