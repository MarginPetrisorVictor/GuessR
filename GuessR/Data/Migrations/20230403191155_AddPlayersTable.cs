using Microsoft.EntityFrameworkCore.Migrations;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;
namespace GuessR.Data.Migrations 
{ 
	public partial class AddPlayersTable : Migration 
	{ 
		protected override void Up(MigrationBuilder migrationBuilder) 
		{ 
			migrationBuilder.CreateTable(
				name: "Players",
				columns: table => new { Id = table.Column<int>(type: "int", nullable: false) 
				.Annotation("SqlServer:Identity", "1, 1"), 
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: (false)), 
					Name = table.Column<string>(type: "nvarchar(256)", nullable: true), 
					CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true), 
					Score = table.Column<int>(type: "int", nullable: true), 
					Rank = table.Column<int>(type: "int", nullable: true), 
					TotalScore = table.Column<int>(type: "int", nullable: true), 
					TotalPlayedGames = table.Column<int>(type: "int", nullable: true), 
					SuccessPercentage = table.Column<int>(type: "int", nullable: true) },
				
				constraints: table => { table.PrimaryKey("PK_Players", x => x.Id); 
					table.ForeignKey( name: "FK_Players_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});
		}
		protected override void Down(MigrationBuilder migrationBuilder) 
		{ 
			migrationBuilder.DropTable( name: "Players"); 
		} 
	} 
}
