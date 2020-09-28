using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BC.API.Services.MastersListService.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "masters");

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MapProviderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypeGroups",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentServiceTypeGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypeGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialities",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceTypeGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceTypes_ServiceTypeGroups_ServiceTypeGroupId",
                        column: x => x.ServiceTypeGroupId,
                        principalSchema: "masters",
                        principalTable: "ServiceTypeGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypeSubGroup",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceTypeGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypeSubGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceTypeSubGroup_ServiceTypeGroups_ServiceTypeGroupId",
                        column: x => x.ServiceTypeGroupId,
                        principalSchema: "masters",
                        principalTable: "ServiceTypeGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Masters",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VkProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Viber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stars = table.Column<double>(type: "float", nullable: false),
                    ReviewsCount = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Masters_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "masters",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Masters_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalSchema: "masters",
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Masters_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalSchema: "masters",
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceLists",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceLists_Masters_MasterId",
                        column: x => x.MasterId,
                        principalSchema: "masters",
                        principalTable: "Masters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Masters_MasterId",
                        column: x => x.MasterId,
                        principalSchema: "masters",
                        principalTable: "Masters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceListItems",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceMin = table.Column<int>(type: "int", nullable: false),
                    PriceMax = table.Column<int>(type: "int", nullable: false),
                    DurationInMinutesMax = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceListItems_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalSchema: "masters",
                        principalTable: "PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceListItems_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalSchema: "masters",
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleDays",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleDays_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "masters",
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ScheduleDayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Pauses",
                schema: "masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleDayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pauses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pauses_ScheduleDays_ScheduleDayId",
                        column: x => x.ScheduleDayId,
                        principalSchema: "masters",
                        principalTable: "ScheduleDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ScheduleDayId",
                schema: "masters",
                table: "Bookings",
                column: "ScheduleDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Masters_CityId",
                schema: "masters",
                table: "Masters",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Masters_ServiceTypeId",
                schema: "masters",
                table: "Masters",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Masters_SpecialityId",
                schema: "masters",
                table: "Masters",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Pauses_ScheduleDayId",
                schema: "masters",
                table: "Pauses",
                column: "ScheduleDayId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItems_PriceListId",
                schema: "masters",
                table: "PriceListItems",
                column: "PriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItems_ServiceTypeId",
                schema: "masters",
                table: "PriceListItems",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_MasterId",
                schema: "masters",
                table: "PriceLists",
                column: "MasterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDays_ScheduleId",
                schema: "masters",
                table: "ScheduleDays",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_MasterId",
                schema: "masters",
                table: "Schedules",
                column: "MasterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_ServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypes",
                column: "ServiceTypeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypeSubGroup_ServiceTypeGroupId",
                schema: "masters",
                table: "ServiceTypeSubGroup",
                column: "ServiceTypeGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "Pauses",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "PriceListItems",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "ServiceTypeSubGroup",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "ScheduleDays",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "PriceLists",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "Masters",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "ServiceTypes",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "Specialities",
                schema: "masters");

            migrationBuilder.DropTable(
                name: "ServiceTypeGroups",
                schema: "masters");
        }
    }
}
