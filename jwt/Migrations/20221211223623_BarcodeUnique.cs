using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwt.Migrations
{
    public partial class BarcodeUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_productUnits_UnitBarCode",
                table: "productUnits",
                column: "UnitBarCode",
                unique: true,
                filter: "[UnitBarCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_productUnits_UnitBarCode",
                table: "productUnits");
        }
    }
}
