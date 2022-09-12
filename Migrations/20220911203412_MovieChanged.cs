using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies_API.Migrations
{
    public partial class MovieChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Collections_CollectionId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CollectionId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Movies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                columns: new[] { "Id", "CollectionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CollectionId",
                table: "Movies",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Collections_CollectionId",
                table: "Movies",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
