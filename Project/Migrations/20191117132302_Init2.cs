using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verified",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "VerifyPassword",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerifyPassword",
                table: "User");

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "User",
                nullable: false,
                defaultValue: false);
        }
    }
}
