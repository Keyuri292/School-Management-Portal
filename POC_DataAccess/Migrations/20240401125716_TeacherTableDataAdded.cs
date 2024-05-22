using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace POC_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TeacherTableDataAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email_Student", "Name_Student", "Phone_Student" },
                values: new object[] { 2, "2@email.com", "Student 2", "9874563210" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id_Teacher", "Email_Teacher", "Name_Teacher", "Phone_Teacher" },
                values: new object[,]
                {
                    { 1, "1@email.com", "Teacher 1", "985632147" },
                    { 2, "2@email.com", "Teacher 2", "9512364780" }
                });

            migrationBuilder.InsertData(
                table: "StudentDetails",
                columns: new[] { "Id_Student", "Email_Student", "FatherName_Student", "GradeId", "MotherName_Student", "Phone_Student", "StudentId", "firstName_Student", "lastName_Student" },
                values: new object[] { 2, "2@email.com", "F2", 1, "M2", "9874563210", 2, "Student", "2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudentDetails",
                keyColumn: "Id_Student",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id_Teacher",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id_Teacher",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
