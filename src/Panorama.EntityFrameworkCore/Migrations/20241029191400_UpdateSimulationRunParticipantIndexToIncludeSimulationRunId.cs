using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panorama.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSimulationRunParticipantIndexToIncludeSimulationRunId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SimulationRunParticipant_SimulationRunId",
                schema: "pano",
                table: "SimulationRunParticipant");

            migrationBuilder.DropIndex(
                name: "IX_SimulationRunParticipant_UserId",
                schema: "pano",
                table: "SimulationRunParticipant");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationRunParticipant_SimulationRunId_UserId",
                schema: "pano",
                table: "SimulationRunParticipant",
                columns: new[] { "SimulationRunId", "UserId" },
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationRunParticipant_UserId",
                schema: "pano",
                table: "SimulationRunParticipant",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SimulationRunParticipant_SimulationRunId_UserId",
                schema: "pano",
                table: "SimulationRunParticipant");

            migrationBuilder.DropIndex(
                name: "IX_SimulationRunParticipant_UserId",
                schema: "pano",
                table: "SimulationRunParticipant");

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
    }
}
