using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBuilder.Migrations
{
    public partial class IndexAddedToCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "BriefChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "BriefChecks");
        }
    }
}
