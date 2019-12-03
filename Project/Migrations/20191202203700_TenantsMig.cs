using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class TenantsMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Opinie",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Opinie",
                nullable: true,
                oldClrType: typeof(int));

        }
    }
}
