using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessR.Data.Migrations
{
    public partial class AddNewQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "GuessModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentUrl",
                table: "GuessModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "GuessModel",
                columns: new[] { "GuessRiddle", "GuessAnswer", "QuestionType", "ContentUrl" },
                values: new object[] { "From which game is this image?", "answer", "Image", "/images/metin.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "GuessModel");

            migrationBuilder.DropColumn(
                name: "ContentUrl",
                table: "GuessModel");
        }
    }
}
