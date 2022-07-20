using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicinePlanner.Data.Migrations
{
    public partial class AddDailyPlanning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyPlannings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntakeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Consumed = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<int>(type: "int", nullable: false),
                    PlanningId = table.Column<int>(type: "int", nullable: true),
                    NotificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPlannings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyPlannings_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyPlannings_Plannings_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Plannings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlannings_NotificationId",
                table: "DailyPlannings",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPlannings_PlanningId",
                table: "DailyPlannings",
                column: "PlanningId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyPlannings");
        }
    }
}
