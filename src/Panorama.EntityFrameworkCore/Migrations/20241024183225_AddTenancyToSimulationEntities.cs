using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panorama.Migrations
{
    /// <inheritdoc />
    public partial class AddTenancyToSimulationEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                schema: "pano",
                table: "SimulationRunParticipant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                schema: "pano",
                table: "SimulationRun",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                schema: "pano",
                table: "Simulation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "pano",
                table: "SimulationRunParticipant");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "pano",
                table: "SimulationRun");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "pano",
                table: "Simulation");
        }
    }
}
