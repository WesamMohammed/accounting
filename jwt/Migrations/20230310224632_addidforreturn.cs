using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwt.Migrations
{
    public partial class addidforreturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdForReturn",
                table: "InvoiceMasters",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdForReturn",
                table: "InvoiceMasters");
        }
    }
}
