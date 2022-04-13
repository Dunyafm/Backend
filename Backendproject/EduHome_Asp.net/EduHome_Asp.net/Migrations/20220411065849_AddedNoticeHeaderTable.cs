using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome_Asp.net.Migrations
{
    public partial class AddedNoticeHeaderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Notices");

            migrationBuilder.AddColumn<string>(
                name: "Header",
                table: "Notices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Header",
                table: "Notices");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Notices",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
