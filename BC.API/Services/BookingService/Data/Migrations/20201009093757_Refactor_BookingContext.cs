using Microsoft.EntityFrameworkCore.Migrations;

namespace BC.API.Services.BookingService.Data.Migrations
{
    public partial class Refactor_BookingContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                schema: "booking",
                table: "ScheduleDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                schema: "booking",
                table: "ScheduleDays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
