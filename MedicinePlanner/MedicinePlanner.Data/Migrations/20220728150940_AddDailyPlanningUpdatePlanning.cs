using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicinePlanner.Data.Migrations
{
    public partial class AddDailyPlanningUpdatePlanning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consumed",
                table: "Plannings");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Plannings");

            migrationBuilder.CreateTable(
                name: "DailyPlannings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntakeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dosage = table.Column<int>(type: "int", nullable: false),
                    Consumed = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<int>(type: "int", nullable: false),
                    PlanningId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPlannings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyPlannings_Plannings_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Plannings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlannings_PlanningId",
                table: "DailyPlannings",
                column: "PlanningId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyPlannings");

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
