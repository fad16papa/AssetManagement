using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdatingUserAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetsLicenses_Assets_AssetId",
                table: "AssetsLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAssets_Assets_AssetsId",
                table: "UserAssets");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserAssets_Id",
                table: "UserAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAssets",
                table: "UserAssets");

            migrationBuilder.DropColumn(
                name: "AssetsId",
                table: "AssetsLicenses");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "UserAssets",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AssetId",
                table: "AssetsLicenses",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAssets",
                table: "UserAssets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssets_AssetId",
                table: "UserAssets",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetsLicenses_Assets_AssetId",
                table: "AssetsLicenses",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssets_Assets_AssetId",
                table: "UserAssets",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetsLicenses_Assets_AssetId",
                table: "AssetsLicenses");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "AssetId",
                table: "AssetsLicenses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "AssetsId",
                table: "AssetsLicenses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserAssets_Id",
                table: "UserAssets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAssets",
                table: "UserAssets",
                columns: new[] { "AssetsId", "UserStaffId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AssetsLicenses_Assets_AssetId",
                table: "AssetsLicenses",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssets_Assets_AssetsId",
                table: "UserAssets",
                column: "AssetsId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
