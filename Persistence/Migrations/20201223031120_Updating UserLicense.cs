using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdatingUserLicense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLicenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetsId = table.Column<Guid>(nullable: false),
                    LicenseId = table.Column<Guid>(nullable: true),
                    UserStaffId = table.Column<Guid>(nullable: false),
                    IssuedOn = table.Column<DateTime>(nullable: false),
                    ReturnedOn = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLicenses_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLicenses_UserStaffs_UserStaffId",
                        column: x => x.UserStaffId,
                        principalTable: "UserStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLicenses_LicenseId",
                table: "UserLicenses",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLicenses_UserStaffId",
                table: "UserLicenses",
                column: "UserStaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLicenses");
        }
    }
}
