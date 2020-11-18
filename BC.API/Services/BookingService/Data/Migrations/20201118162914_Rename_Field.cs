using Microsoft.EntityFrameworkCore.Migrations;

namespace BC.API.Services.BookingService.Data.Migrations
{
    public partial class Rename_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConnectedBookingsPrefered",
                schema: "booking",
                table: "Schedules",
                newName: "ConnectedBookingsOnly");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConnectedBookingsOnly",
                schema: "booking",
                table: "Schedules",
                newName: "ConnectedBookingsPrefered");
        }
    }
}
