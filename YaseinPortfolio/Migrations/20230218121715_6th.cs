using Microsoft.EntityFrameworkCore.Migrations;

namespace YaseinPortfolio.Migrations
{
    public partial class _6th : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutMes",
                columns: table => new
                {
                    AboutMeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutMeMyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutMeSubtitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutMeParagraph = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutMeImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutMes", x => x.AboutMeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutMes");
        }
    }
}
