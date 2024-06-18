using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBuilder.Migrations
{
    public partial class PKAddedToBriefCheckModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BriefChecks",
                table: "BriefChecks");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BriefChecks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BriefChecks",
                table: "BriefChecks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BriefChecks_BriefId",
                table: "BriefChecks",
                column: "BriefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BriefChecks",
                table: "BriefChecks");

            migrationBuilder.DropIndex(
                name: "IX_BriefChecks_BriefId",
                table: "BriefChecks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BriefChecks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BriefChecks",
                table: "BriefChecks",
                columns: new[] { "BriefId", "CheckId" });
        }
    }
}
