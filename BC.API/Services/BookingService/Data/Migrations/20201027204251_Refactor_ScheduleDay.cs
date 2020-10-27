using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BC.API.Services.BookingService.Data.Migrations
{
    public partial class Refactor_ScheduleDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                schema: "booking",
                table: "ScheduleDays");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                schema: "booking",
                table: "ScheduleDays",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                schema: "booking",
                table: "ScheduleDays",
                newName: "StartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                schema: "booking",
                table: "ScheduleDays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
