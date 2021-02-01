using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RevertUserAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssets_Assets_AssetId",
                table: "UserAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAssets",
                table: "UserAssets");

            migrationBuilder.DropIndex(
                name: "IX_UserAssets_AssetId",
                table: "UserAssets");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "UserAssets");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserAssets_Id",
                table: "UserAssets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAssets",
                table: "UserAssets",
                columns: new[] { "AssetsId", "UserStaffId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssets_Assets_AssetsId",
                table: "UserAssets",
                column: "AssetsId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssets_Assets_AssetsId",
                table: "UserAssets");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserAssets_Id",
                table: "UserAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAssets",
                table: "UserAssets");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "UserAssets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAssets",
                table: "UserAssets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssets_AssetId",
                table: "UserAssets",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssets_Assets_AssetId",
                table: "UserAssets",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
