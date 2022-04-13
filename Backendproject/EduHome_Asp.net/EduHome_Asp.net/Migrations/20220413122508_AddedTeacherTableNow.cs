using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHome_Asp.net.Migrations
{
    public partial class AddedTeacherTableNow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {

                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    count = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TeacherDetailId = table.Column<int>(nullable: false),
                    SliderDetailId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_SliderDetails_SliderDetailId",
                        column: x => x.SliderDetailId,
                        principalTable: "SliderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeachersDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Proffesor = table.Column<string>(nullable: true),
                    Sosicals = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SliderDetailId",
                table: "Teachers",
                column: "SliderDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "TeachersDetails");
        }
    }
}
