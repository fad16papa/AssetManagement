using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CreatingAssetsLicenseDoman : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetsLicenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetsId = table.Column<Guid>(nullable: false),
                    AssetId = table.Column<Guid>(nullable: true),
                    LicenseId = table.Column<Guid>(nullable: false),
                    IssuedOn = table.Column<DateTime>(nullable: false),
                    ReturnedOn = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetsLicenses_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetsLicenses_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetsLicenses_AssetId",
                table: "AssetsLicenses",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetsLicenses_LicenseId",
                table: "AssetsLicenses",
                column: "LicenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetsLicenses");
        }
    }
}
