using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwt.Migrations
{
    public partial class removeAccountingIdfromInvoicemaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceMasters_AccountingMasters_AccountingMasterId",
                table: "InvoiceMasters");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceMasters_AccountingMasterId",
                table: "InvoiceMasters");

            migrationBuilder.DropColumn(
                name: "AccountingMasterId",
                table: "InvoiceMasters");

            migrationBuilder.CreateTable(
                name: "A",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingMasters_InvoiceMasterId",
                table: "AccountingMasters",
                column: "InvoiceMasterId",
                unique: true,
                filter: "[InvoiceMasterId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingMasters_InvoiceMasters_InvoiceMasterId",
                table: "AccountingMasters",
                column: "InvoiceMasterId",
                principalTable: "InvoiceMasters",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingMasters_InvoiceMasters_InvoiceMasterId",
                table: "AccountingMasters");

            migrationBuilder.DropTable(
                name: "A");

            migrationBuilder.DropIndex(
                name: "IX_AccountingMasters_InvoiceMasterId",
                table: "AccountingMasters");

            migrationBuilder.AddColumn<int>(
                name: "AccountingMasterId",
                table: "InvoiceMasters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceMasters_AccountingMasterId",
                table: "InvoiceMasters",
                column: "AccountingMasterId",
                unique: true,
                filter: "[AccountingMasterId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceMasters_AccountingMasters_AccountingMasterId",
                table: "InvoiceMasters",
                column: "AccountingMasterId",
                principalTable: "AccountingMasters",
                principalColumn: "Id");
        }
    }
}
