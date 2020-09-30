using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BC.API.Services.MastersListService.Data.Migrations
{
    public partial class Change_Schedule_Day : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pauses_ScheduleDays_ScheduleDayId",
                schema: "masters",
                table: "Pauses");

            migrationBuilder.DropTable(
                name: "Bookings",
                schema: "masters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pauses",
                schema: "masters",
                table: "Pauses");

            migrationBuilder.RenameTable(
                name: "Pauses",
                schema: "masters",
                newName: "ScheduleDayItem",
                newSchema: "masters");

            migrationBuilder.RenameIndex(
                name: "IX_Pauses_ScheduleDayId",
                schema: "masters",
                table: "ScheduleDayItem",
                newName: "IX_ScheduleDayItem_ScheduleDayId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "masters",
                table: "ScheduleDayItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleDayItem",
                schema: "masters",
                table: "ScheduleDayItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDayItem_ScheduleDays_ScheduleDayId",
                schema: "masters",
                table: "ScheduleDayItem",
                column: "ScheduleDayId",
                principalSchema: "masters",
                principalTable: "ScheduleDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDayItem_ScheduleDays_ScheduleDayId",
                schema: "masters",
                table: "ScheduleDayItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleDayItem",
                schema: "masters",
                table: "ScheduleDayItem");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "masters",
                table: "ScheduleDayItem");

            migrationBuilder.RenameTable(
                name: "ScheduleDayItem",
                schema: "masters",
                newName: "Pauses",
                newSchema: "masters");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleDayItem_ScheduleDayId",
                schema: "masters",
                table: "Pauses",
                newName: "IX_Pauses_ScheduleDayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pauses",
                schema: "masters",
                table: "Pauses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Bookings",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduleDayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_ScheduleDays_ScheduleDayId",
                        column: x => x.ScheduleDayId,
                        principalSchema: "masters",
                        principalTable: "ScheduleDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ScheduleDayId",
                schema: "masters",
                table: "Bookings",
                column: "ScheduleDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pauses_ScheduleDays_ScheduleDayId",
                schema: "masters",
                table: "Pauses",
                column: "ScheduleDayId",
                principalSchema: "masters",
                principalTable: "ScheduleDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
