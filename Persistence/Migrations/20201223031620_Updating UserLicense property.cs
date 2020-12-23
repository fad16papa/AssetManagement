using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdatingUserLicenseproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLicenses_Licenses_LicenseId",
                table: "UserLicenses");

            migrationBuilder.DropColumn(
                name: "AssetsId",
                table: "UserLicenses");

            migrationBuilder.AlterColumn<Guid>(
                name: "LicenseId",
                table: "UserLicenses",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLicenses_Licenses_LicenseId",
                table: "UserLicenses",
                column: "LicenseId",
                principalTable: "Licenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLicenses_Licenses_LicenseId",
                table: "UserLicenses");

            migrationBuilder.AlterColumn<Guid>(
                name: "LicenseId",
                table: "UserLicenses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "AssetsId",
                table: "UserLicenses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_UserLicenses_Licenses_LicenseId",
                table: "UserLicenses",
                column: "LicenseId",
                principalTable: "Licenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
