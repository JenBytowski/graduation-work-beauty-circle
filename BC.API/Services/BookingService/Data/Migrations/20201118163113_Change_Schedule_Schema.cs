using Microsoft.EntityFrameworkCore.Migrations;

namespace BC.API.Services.BookingService.Data.Migrations
{
    public partial class Change_Schedule_Schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferedGapInMinutes",
                schema: "booking",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "ConnectionGapInMinutes",
                schema: "booking",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpaceGapInMinutes",
                schema: "booking",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeStepInMinutes",
                schema: "booking",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionGapInMinutes",
                schema: "booking",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "SpaceGapInMinutes",
                schema: "booking",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TimeStepInMinutes",
                schema: "booking",
                table: "Schedules");

            migrationBuilder.AddColumn<bool>(
                name: "PreferedGapInMinutes",
                schema: "booking",
                table: "Schedules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
