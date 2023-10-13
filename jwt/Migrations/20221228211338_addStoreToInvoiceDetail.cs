using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwt.Migrations
{
    /// <inheritdoc />
    public partial class addStoreToInvoiceDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "InvoiceDetails",
                type: "int",
                nullable: true,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_StoreId",
                table: "InvoiceDetails",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Stores_StoreId",
                table: "InvoiceDetails",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Stores_StoreId",
                table: "InvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetails_StoreId",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "InvoiceDetails");
        }
    }
}
