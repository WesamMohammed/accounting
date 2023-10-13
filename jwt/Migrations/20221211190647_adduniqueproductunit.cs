using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwt.Migrations
{
    public partial class adduniqueproductunit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_productUnits_ProductId_UnitName",
                table: "productUnits",
                columns: new[] { "ProductId", "UnitName" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_productUnits_ProductId_UnitName",
                table: "productUnits");

            migrationBuilder.AlterColumn<string>(
                name: "UnitName",
                table: "productUnits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_productUnits_ProductId",
                table: "productUnits",
                column: "ProductId");
        }
    }
}
