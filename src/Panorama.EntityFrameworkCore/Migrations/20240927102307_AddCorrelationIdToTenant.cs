using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panorama.Migrations
{
    /// <inheritdoc />
    public partial class AddCorrelationIdToTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorrelationId",
                table: "AbpTenants",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_CorrelationId",
                table: "AbpTenants",
                column: "CorrelationId",
                unique: true,
                filter: "[CorrelationId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AbpTenants_CorrelationId",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "CorrelationId",
                table: "AbpTenants");
        }
    }
}
