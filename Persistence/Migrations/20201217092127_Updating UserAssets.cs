using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdatingUserAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStaffs_Assets_AssetId",
                table: "UserStaffs");

            migrationBuilder.DropIndex(
                name: "IX_UserStaffs_AssetId",
                table: "UserStaffs");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "UserStaffs");

            migrationBuilder.CreateTable(
                name: "UserAssets",
                columns: table => new
                {
                    AssetsId = table.Column<Guid>(nullable: false),
                    UserStaffId = table.Column<Guid>(nullable: false),
                    IssuedOn = table.Column<DateTime>(nullable: false),
                    ReturnedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssets", x => new { x.AssetsId, x.UserStaffId });
                    table.ForeignKey(
                        name: "FK_UserAssets_Assets_AssetsId",
                        column: x => x.AssetsId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAssets_UserStaffs_UserStaffId",
                        column: x => x.UserStaffId,
                        principalTable: "UserStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAssets_UserStaffId",
                table: "UserAssets",
                column: "UserStaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAssets");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "UserStaffs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStaffs_AssetId",
                table: "UserStaffs",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStaffs_Assets_AssetId",
                table: "UserStaffs",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
