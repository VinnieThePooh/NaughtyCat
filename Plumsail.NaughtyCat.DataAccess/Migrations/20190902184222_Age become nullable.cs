using Microsoft.EntityFrameworkCore.Migrations;

namespace Plumsail.NaughtyCat.DataAccess.Migrations
{
    public partial class Agebecomenullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Rabbits",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Rabbits",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
