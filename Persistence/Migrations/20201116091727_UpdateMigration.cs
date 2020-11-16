using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetHistories");

            migrationBuilder.DropTable(
                name: "LicenseHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "UserAssetsId",
                table: "Assets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAssets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_UserAssetsId",
                table: "Assets",
                column: "UserAssetsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_UserAssets_UserAssetsId",
                table: "Assets",
                column: "UserAssetsId",
                principalTable: "UserAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_UserAssets_UserAssetsId",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "UserAssets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_UserAssetsId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "UserAssetsId",
                table: "Assets");

            migrationBuilder.CreateTable(
                name: "AssetHistories",
                columns: table => new
                {
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetHistories", x => new { x.AppUserId, x.AssetId });
                    table.ForeignKey(
                        name: "FK_AssetHistories_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetHistories_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LicenseHistories",
                columns: table => new
                {
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LicenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseHistories", x => new { x.AppUserId, x.LicenseId });
                    table.ForeignKey(
                        name: "FK_LicenseHistories_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LicenseHistories_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetHistories_AssetId",
                table: "AssetHistories",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseHistories_LicenseId",
                table: "LicenseHistories",
                column: "LicenseId");
        }
    }
}
