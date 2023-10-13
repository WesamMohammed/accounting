using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwt.Migrations
{
    public partial class removeUniques : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_productUnits_ProductId_UnitName",
                table: "productUnits");

            migrationBuilder.DropIndex(
                name: "IX_productUnits_UnitBarCode",
                table: "productUnits");

            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "UnitName",
                table: "productUnits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_productUnits_ProductId",
                table: "productUnits",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_productUnits_ProductId",
                table: "productUnits");

            migrationBuilder.AlterColumn<string>(
                name: "UnitName",
                table: "productUnits",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_productUnits_ProductId_UnitName",
                table: "productUnits",
                columns: new[] { "ProductId", "UnitName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_productUnits_UnitBarCode",
                table: "productUnits",
                column: "UnitBarCode",
                unique: true,
                filter: "[UnitBarCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);
        }
    }
}
