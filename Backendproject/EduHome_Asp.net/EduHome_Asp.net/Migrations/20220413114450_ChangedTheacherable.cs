using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome_Asp.net.Migrations
{
    public partial class ChangedTheacherable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courseses_CoursesFeatures_CoursesFeaturesId",
                table: "Courseses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courseses",
                table: "Courseses");

            migrationBuilder.RenameTable(
                name: "Courseses",
                newName: "Courses");

            migrationBuilder.RenameIndex(
                name: "IX_Courseses_CoursesFeaturesId",
                table: "Courses",
                newName: "IX_Courses_CoursesFeaturesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(nullable: true),
                    Hidden = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CoursesFeatures_CoursesFeaturesId",
                table: "Courses",
                column: "CoursesFeaturesId",
                principalTable: "CoursesFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CoursesFeatures_CoursesFeaturesId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Courseses");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CoursesFeaturesId",
                table: "Courseses",
                newName: "IX_Courseses_CoursesFeaturesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courseses",
                table: "Courseses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courseses_CoursesFeatures_CoursesFeaturesId",
                table: "Courseses",
                column: "CoursesFeaturesId",
                principalTable: "CoursesFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
