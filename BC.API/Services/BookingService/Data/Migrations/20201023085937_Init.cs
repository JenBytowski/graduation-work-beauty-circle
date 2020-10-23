using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BC.API.Services.BookingService.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "booking");

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConnectedBookingsPrefered = table.Column<bool>(type: "bit", nullable: false),
                    PreferedGapInMinutes = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleDays",
                schema: "booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleDays_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "booking",
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleDayItems",
                schema: "booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleDayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Booking_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceMin = table.Column<int>(type: "int", nullable: true),
                    PriceMax = table.Column<int>(type: "int", nullable: true),
                    DurationInMinutesMax = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDayItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleDayItems_ScheduleDays_ScheduleDayId",
                        column: x => x.ScheduleDayId,
                        principalSchema: "booking",
                        principalTable: "ScheduleDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDayItems_ScheduleDayId",
                schema: "booking",
                table: "ScheduleDayItems",
                column: "ScheduleDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDays_ScheduleId",
                schema: "booking",
                table: "ScheduleDays",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleDayItems",
                schema: "booking");

            migrationBuilder.DropTable(
                name: "ScheduleDays",
                schema: "booking");

            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "booking");
        }
    }
}
