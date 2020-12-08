using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdatingUserStaffDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Licenses_LicenseId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LicenseId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "IsActive",
                table: "UserStaffs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserStaffs");

            migrationBuilder.AddColumn<Guid>(
                name: "LicenseId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LicenseId",
                table: "AspNetUsers",
                column: "LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Licenses_LicenseId",
                table: "AspNetUsers",
                column: "LicenseId",
                principalTable: "Licenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
