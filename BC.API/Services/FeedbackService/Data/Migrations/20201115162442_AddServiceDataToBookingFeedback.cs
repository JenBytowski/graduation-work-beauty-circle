using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BC.API.Services.FeedbackService.Data.Migrations
{
    public partial class AddServiceDataToBookingFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                schema: "feedback",
                table: "BookingFeedbacks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                schema: "feedback",
                table: "BookingFeedbacks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceId",
                schema: "feedback",
                table: "BookingFeedbacks");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                schema: "feedback",
                table: "BookingFeedbacks");
        }
    }
}
