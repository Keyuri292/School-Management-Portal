using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class StudentCourseTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    SId = table.Column<int>(type: "int", nullable: false),
                    Cid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => new { x.SId, x.Cid });
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_Cid",
                        column: x => x.Cid,
                        principalTable: "Courses",
                        principalColumn: "Id_Courses",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_StudentDetails_SId",
                        column: x => x.SId,
                        principalTable: "StudentDetails",
                        principalColumn: "Id_Student",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_Cid",
                table: "StudentCourses",
                column: "Cid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourses");
        }
    }
}
