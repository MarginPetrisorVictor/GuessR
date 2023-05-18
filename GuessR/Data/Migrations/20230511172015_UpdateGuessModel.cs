using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessR.Data.Migrations
{
    public partial class UpdateGuessModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "GuessModels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Question",
                table: "GuessModels");
        }
    }
}
