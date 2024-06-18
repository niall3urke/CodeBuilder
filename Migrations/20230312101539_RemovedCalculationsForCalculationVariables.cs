using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBuilder.Migrations
{
    public partial class RemovedCalculationsForCalculationVariables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calculations_Calculations_CalculationModelId",
                table: "Calculations");

            migrationBuilder.DropIndex(
                name: "IX_Calculations_CalculationModelId",
                table: "Calculations");

            migrationBuilder.DropColumn(
                name: "CalculationModelId",
                table: "Calculations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CalculationModelId",
                table: "Calculations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_CalculationModelId",
                table: "Calculations",
                column: "CalculationModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calculations_Calculations_CalculationModelId",
                table: "Calculations",
                column: "CalculationModelId",
                principalTable: "Calculations",
                principalColumn: "Id");
        }
    }
}
