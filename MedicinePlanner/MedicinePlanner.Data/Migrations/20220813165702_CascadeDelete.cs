using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicinePlanner.Data.Migrations
{
    public partial class CascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Medicines_MedicineId",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Medicines_MedicineId",
                table: "Stocks",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Medicines_MedicineId",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Medicines_MedicineId",
                table: "Stocks",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
