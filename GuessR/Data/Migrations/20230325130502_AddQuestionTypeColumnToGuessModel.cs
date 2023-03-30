using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessR.Data.Migrations
{
    public partial class AddQuestionTypeColumnToGuessModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionType",
                table: "GuessModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionType",
                table: "GuessModel");
        }
    }
}
