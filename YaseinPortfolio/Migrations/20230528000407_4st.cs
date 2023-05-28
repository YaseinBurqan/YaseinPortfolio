using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YaseinPortfolio.Migrations
{
    public partial class _4st : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AboutMeEntryDate",
                table: "AboutMes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutMeEntryDate",
                table: "AboutMes");
        }
    }
}
