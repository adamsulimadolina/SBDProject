using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class DisplayUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Surface",
                table: "Mieszkanie",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Surface",
                table: "Mieszkanie",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
