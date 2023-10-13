using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwt.Migrations
{
    /// <inheritdoc />
    public partial class addAccountingToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingMasters_InvoiceMasters_InvoiceMasterId",
                table: "AccountingMasters");

            migrationBuilder.DropIndex(
                name: "IX_AccountingMasters_InvoiceMasterId",
                table: "AccountingMasters");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceMasters_AccountingMasters_AccountingMasterId",
                table: "InvoiceMasters");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceMasters_AccountingMasterId",
                table: "InvoiceMasters");

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
    }
}
