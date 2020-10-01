using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BC.API.Services.MastersListService.Data.Migrations
{
    public partial class Update_Master : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypes_ServiceTypeGroups_ServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypeSubGroup_ServiceTypeGroups_ServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypeSubGroup");

            migrationBuilder.DropColumn(
                name: "ParentServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypeGroups");

            migrationBuilder.RenameColumn(
                name: "ServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypes",
                newName: "ServiceTypeSubGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceTypes_ServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypes",
                newName: "IX_ServiceTypes_ServiceTypeSubGroupId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypeSubGroup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypes_ServiceTypeSubGroup_ServiceTypeSubGroupId",
                schema: "masters",
                table: "ServiceTypes",
                column: "ServiceTypeSubGroupId",
                principalSchema: "masters",
                principalTable: "ServiceTypeSubGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypeSubGroup_ServiceTypeGroups_ServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypeSubGroup",
                column: "ServiceTypeGroupId",
                principalSchema: "masters",
                principalTable: "ServiceTypeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypes_ServiceTypeSubGroup_ServiceTypeSubGroupId",
                schema: "masters",
                table: "ServiceTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypeSubGroup_ServiceTypeGroups_ServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypeSubGroup");

            migrationBuilder.RenameColumn(
                name: "ServiceTypeSubGroupId",
                schema: "masters",
                table: "ServiceTypes",
                newName: "ServiceTypeGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceTypes_ServiceTypeSubGroupId",
                schema: "masters",
                table: "ServiceTypes",
                newName: "IX_ServiceTypes_ServiceTypeGroupId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypeSubGroup",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypeGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypes_ServiceTypeGroups_ServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypes",
                column: "ServiceTypeGroupId",
                principalSchema: "masters",
                principalTable: "ServiceTypeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypeSubGroup_ServiceTypeGroups_ServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypeSubGroup",
                column: "ServiceTypeGroupId",
                principalSchema: "masters",
                principalTable: "ServiceTypeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
