using Microsoft.EntityFrameworkCore.Migrations;

namespace BC.API.Services.MastersListService.Data.Migrations
{
    public partial class Change_Avatar_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarUrl",
                schema: "masters",
                table: "Masters",
                newName: "ThumbnailFileName");

            migrationBuilder.AddColumn<string>(
                name: "AvatarSourceFileName",
                schema: "masters",
                table: "Masters",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarSourceFileName",
                schema: "masters",
                table: "Masters");

            migrationBuilder.RenameColumn(
                name: "ThumbnailFileName",
                schema: "masters",
                table: "Masters",
                newName: "AvatarUrl");
        }
    }
}
