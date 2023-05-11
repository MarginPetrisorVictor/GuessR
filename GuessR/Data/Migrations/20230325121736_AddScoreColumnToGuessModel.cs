using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessR.Data.Migrations
{
    public partial class AddScoreColumnToGuessModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "GuessModel",
                type: "string",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Question",
                table: "GuessModel");
        }
    }
}
