using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicinePlanner.Data.Migrations
{
    public partial class UpdatePlanning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consumed",
                table: "Plannings");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Plannings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Consumed",
                table: "Plannings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Message",
                table: "Plannings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
