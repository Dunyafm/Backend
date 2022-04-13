using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome_Asp.net.Migrations
{
    public partial class ChangeAboutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Header",
                table: "Abouts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Header",
                table: "Abouts");
        }
    }
}
