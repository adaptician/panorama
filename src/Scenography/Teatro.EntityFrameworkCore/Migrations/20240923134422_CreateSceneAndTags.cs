using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Teatro.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class CreateSceneAndTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "thes");

            migrationBuilder.CreateTable(
                name: "Scene",
                schema: "thes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ScenographyId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scene", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scene_Scenography_ScenographyId",
                        column: x => x.ScenographyId,
                        principalSchema: "doc",
                        principalTable: "Scenography",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SceneTag",
                schema: "thes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SceneId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SceneTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SceneTag_Scene_SceneId",
                        column: x => x.SceneId,
                        principalSchema: "thes",
                        principalTable: "Scene",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scene_ScenographyId",
                schema: "thes",
                table: "Scene",
                column: "ScenographyId");

            migrationBuilder.CreateIndex(
                name: "IX_SceneTag_Name_SceneId",
                schema: "thes",
                table: "SceneTag",
                columns: new[] { "Name", "SceneId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SceneTag_SceneId",
                schema: "thes",
                table: "SceneTag",
                column: "SceneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SceneTag",
                schema: "thes");

            migrationBuilder.DropTable(
                name: "Scene",
                schema: "thes");
        }
    }
}
