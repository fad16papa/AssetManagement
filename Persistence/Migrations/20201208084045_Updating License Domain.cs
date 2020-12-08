using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdatingLicenseDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Expiration",
                table: "Licenses",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Expiration",
                table: "Licenses",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
