using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panorama.Migrations
{
    /// <inheritdoc />
    public partial class CreateSimulationRunParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimulationRunParticipant",
                schema: "pano",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SimulationRunId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    EntryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationRunParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulationRunParticipant_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimulationRunParticipant_SimulationRun_SimulationRunId",
                        column: x => x.SimulationRunId,
                        principalSchema: "pano",
                        principalTable: "SimulationRun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimulationRunParticipant_SimulationRunId",
                schema: "pano",
                table: "SimulationRunParticipant",
                column: "SimulationRunId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationRunParticipant_UserId",
                schema: "pano",
                table: "SimulationRunParticipant",
                column: "UserId",
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimulationRunParticipant",
                schema: "pano");
        }
    }
}
