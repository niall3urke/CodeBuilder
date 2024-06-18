using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeBuilder.Migrations
{
    public partial class RenamedCalculationToVariable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Calculations_CalculationId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Conditions_Calculations_LhsId",
                table: "Conditions");

            migrationBuilder.DropForeignKey(
                name: "FK_Conditions_Calculations_RhsId",
                table: "Conditions");

            migrationBuilder.DropForeignKey(
                name: "FK_VariableGroupVariables_Calculations_VariableId",
                table: "VariableGroupVariables");

            migrationBuilder.DropTable(
                name: "CalculationVariables");

            migrationBuilder.DropTable(
                name: "Calculations");

            migrationBuilder.CreateTable(
                name: "Variables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StandardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Storage = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodeCalculation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MathCalculation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Math = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variables_Standards_StandardId",
                        column: x => x.StandardId,
                        principalTable: "Standards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VariableInputs",
                columns: table => new
                {
                    CalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableInputs", x => new { x.CalculationId, x.InputId });
                    table.ForeignKey(
                        name: "FK_VariableInputs_Variables_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Variables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariableInputs_Variables_InputId",
                        column: x => x.InputId,
                        principalTable: "Variables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VariableInputs_InputId",
                table: "VariableInputs",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_Variables_StandardId",
                table: "Variables",
                column: "StandardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_Variables_CalculationId",
                table: "Blocks",
                column: "CalculationId",
                principalTable: "Variables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conditions_Variables_LhsId",
                table: "Conditions",
                column: "LhsId",
                principalTable: "Variables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conditions_Variables_RhsId",
                table: "Conditions",
                column: "RhsId",
                principalTable: "Variables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VariableGroupVariables_Variables_VariableId",
                table: "VariableGroupVariables",
                column: "VariableId",
                principalTable: "Variables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Variables_CalculationId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Conditions_Variables_LhsId",
                table: "Conditions");

            migrationBuilder.DropForeignKey(
                name: "FK_Conditions_Variables_RhsId",
                table: "Conditions");

            migrationBuilder.DropForeignKey(
                name: "FK_VariableGroupVariables_Variables_VariableId",
                table: "VariableGroupVariables");

            migrationBuilder.DropTable(
                name: "VariableInputs");

            migrationBuilder.DropTable(
                name: "Variables");

            migrationBuilder.CreateTable(
                name: "Calculations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StandardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeCalculation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Math = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MathCalculation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Storage = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calculations_Standards_StandardId",
                        column: x => x.StandardId,
                        principalTable: "Standards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CalculationVariables",
                columns: table => new
                {
                    CalculationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationVariables", x => new { x.CalculationId, x.VariableId });
                    table.ForeignKey(
                        name: "FK_CalculationVariables_Calculations_CalculationId",
                        column: x => x.CalculationId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalculationVariables_Calculations_VariableId",
                        column: x => x.VariableId,
                        principalTable: "Calculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calculations_StandardId",
                table: "Calculations",
                column: "StandardId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculationVariables_VariableId",
                table: "CalculationVariables",
                column: "VariableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_Calculations_CalculationId",
                table: "Blocks",
                column: "CalculationId",
                principalTable: "Calculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conditions_Calculations_LhsId",
                table: "Conditions",
                column: "LhsId",
                principalTable: "Calculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conditions_Calculations_RhsId",
                table: "Conditions",
                column: "RhsId",
                principalTable: "Calculations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VariableGroupVariables_Calculations_VariableId",
                table: "VariableGroupVariables",
                column: "VariableId",
                principalTable: "Calculations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
