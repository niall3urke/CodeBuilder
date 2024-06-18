using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBuilder.Migrations
{
    public partial class BriefCheckModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Briefs_BriefModelId",
                table: "Checks");

            migrationBuilder.DropIndex(
                name: "IX_Checks_BriefModelId",
                table: "Checks");

            migrationBuilder.DropColumn(
                name: "BriefModelId",
                table: "Checks");

            migrationBuilder.CreateTable(
                name: "BriefChecks",
                columns: table => new
                {
                    BriefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BriefChecks", x => new { x.BriefId, x.CheckId });
                    table.ForeignKey(
                        name: "FK_BriefChecks_Briefs_BriefId",
                        column: x => x.BriefId,
                        principalTable: "Briefs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BriefChecks_Checks_CheckId",
                        column: x => x.CheckId,
                        principalTable: "Checks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BriefChecks_CheckId",
                table: "BriefChecks",
                column: "CheckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BriefChecks");

            migrationBuilder.AddColumn<Guid>(
                name: "BriefModelId",
                table: "Checks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checks_BriefModelId",
                table: "Checks",
                column: "BriefModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Briefs_BriefModelId",
                table: "Checks",
                column: "BriefModelId",
                principalTable: "Briefs",
                principalColumn: "Id");
        }
    }
}
