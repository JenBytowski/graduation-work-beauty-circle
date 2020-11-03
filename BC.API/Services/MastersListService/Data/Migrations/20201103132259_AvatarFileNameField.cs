using Microsoft.EntityFrameworkCore.Migrations;

namespace BC.API.Services.MastersListService.Data.Migrations
{
    public partial class AvatarFileNameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarFileName",
                schema: "masters",
                table: "Masters",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarFileName",
                schema: "masters",
                table: "Masters");
        }
    }
}
