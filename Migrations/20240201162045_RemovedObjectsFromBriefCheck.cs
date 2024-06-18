using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBuilder.Migrations
{
    public partial class RemovedObjectsFromBriefCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BriefChecks_Briefs_BriefId",
                table: "BriefChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_BriefChecks_Checks_CheckId",
                table: "BriefChecks");

            migrationBuilder.DropIndex(
                name: "IX_BriefChecks_BriefId",
                table: "BriefChecks");

            migrationBuilder.DropIndex(
                name: "IX_BriefChecks_CheckId",
                table: "BriefChecks");

            migrationBuilder.AddColumn<Guid>(
                name: "BriefModelId",
                table: "BriefChecks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BriefChecks_BriefModelId",
                table: "BriefChecks",
                column: "BriefModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_BriefChecks_Briefs_BriefModelId",
                table: "BriefChecks",
                column: "BriefModelId",
                principalTable: "Briefs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BriefChecks_Briefs_BriefModelId",
                table: "BriefChecks");

            migrationBuilder.DropIndex(
                name: "IX_BriefChecks_BriefModelId",
                table: "BriefChecks");

            migrationBuilder.DropColumn(
                name: "BriefModelId",
                table: "BriefChecks");

            migrationBuilder.CreateIndex(
                name: "IX_BriefChecks_BriefId",
                table: "BriefChecks",
                column: "BriefId");

            migrationBuilder.CreateIndex(
                name: "IX_BriefChecks_CheckId",
                table: "BriefChecks",
                column: "CheckId");

            migrationBuilder.AddForeignKey(
                name: "FK_BriefChecks_Briefs_BriefId",
                table: "BriefChecks",
                column: "BriefId",
                principalTable: "Briefs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BriefChecks_Checks_CheckId",
                table: "BriefChecks",
                column: "CheckId",
                principalTable: "Checks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
