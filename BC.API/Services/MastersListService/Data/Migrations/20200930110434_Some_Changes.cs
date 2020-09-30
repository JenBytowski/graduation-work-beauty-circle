using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BC.API.Services.MastersListService.Data.Migrations
{
    public partial class Some_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Masters_Cities_CityId",
                schema: "masters",
                table: "Masters");

            migrationBuilder.DropForeignKey(
                name: "FK_Masters_Specialities_SpecialityId",
                schema: "masters",
                table: "Masters");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceLists_Masters_MasterId",
                schema: "masters",
                table: "PriceLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Masters_MasterId",
                schema: "masters",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_MasterId",
                schema: "masters",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_PriceLists_MasterId",
                schema: "masters",
                table: "PriceLists");

            migrationBuilder.DropColumn(
                name: "MasterId",
                schema: "masters",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "MasterId",
                schema: "masters",
                table: "PriceLists");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpecialityId",
                schema: "masters",
                table: "Masters",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CityId",
                schema: "masters",
                table: "Masters",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PriceListId",
                schema: "masters",
                table: "Masters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                schema: "masters",
                table: "Masters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Masters_PriceListId",
                schema: "masters",
                table: "Masters",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_Masters_ScheduleId",
                schema: "masters",
                table: "Masters",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Masters_Cities_CityId",
                schema: "masters",
                table: "Masters",
                column: "CityId",
                principalSchema: "masters",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Masters_PriceLists_PriceListId",
                schema: "masters",
                table: "Masters",
                column: "PriceListId",
                principalSchema: "masters",
                principalTable: "PriceLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Masters_Schedules_ScheduleId",
                schema: "masters",
                table: "Masters",
                column: "ScheduleId",
                principalSchema: "masters",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Masters_Specialities_SpecialityId",
                schema: "masters",
                table: "Masters",
                column: "SpecialityId",
                principalSchema: "masters",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Masters_Cities_CityId",
                schema: "masters",
                table: "Masters");

            migrationBuilder.DropForeignKey(
                name: "FK_Masters_PriceLists_PriceListId",
                schema: "masters",
                table: "Masters");

            migrationBuilder.DropForeignKey(
                name: "FK_Masters_Schedules_ScheduleId",
                schema: "masters",
                table: "Masters");

            migrationBuilder.DropForeignKey(
                name: "FK_Masters_Specialities_SpecialityId",
                schema: "masters",
                table: "Masters");

            migrationBuilder.DropIndex(
                name: "IX_Masters_PriceListId",
                schema: "masters",
                table: "Masters");

            migrationBuilder.DropIndex(
                name: "IX_Masters_ScheduleId",
                schema: "masters",
                table: "Masters");

            migrationBuilder.DropColumn(
                name: "PriceListId",
                schema: "masters",
                table: "Masters");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                schema: "masters",
                table: "Masters");

            migrationBuilder.AddColumn<Guid>(
                name: "MasterId",
                schema: "masters",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MasterId",
                schema: "masters",
                table: "PriceLists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SpecialityId",
                schema: "masters",
                table: "Masters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CityId",
                schema: "masters",
                table: "Masters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_MasterId",
                schema: "masters",
                table: "Schedules",
                column: "MasterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_MasterId",
                schema: "masters",
                table: "PriceLists",
                column: "MasterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Masters_Cities_CityId",
                schema: "masters",
                table: "Masters",
                column: "CityId",
                principalSchema: "masters",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Masters_Specialities_SpecialityId",
                schema: "masters",
                table: "Masters",
                column: "SpecialityId",
                principalSchema: "masters",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceLists_Masters_MasterId",
                schema: "masters",
                table: "PriceLists",
                column: "MasterId",
                principalSchema: "masters",
                principalTable: "Masters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Masters_MasterId",
                schema: "masters",
                table: "Schedules",
                column: "MasterId",
                principalSchema: "masters",
                principalTable: "Masters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
