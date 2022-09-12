using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies_API.Migrations
{
    public partial class MoviePosterChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosterURl",
                table: "Movies",
                newName: "PosterURL");

            migrationBuilder.AlterColumn<string>(
                name: "PosterURL",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosterURL",
                table: "Movies",
                newName: "PosterURl");

            migrationBuilder.AlterColumn<string>(
                name: "PosterURl",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
