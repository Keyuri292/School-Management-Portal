using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class storedProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE GetStudentsByGrade
                  @GradeId INT
                AS
                BEGIN
                    SELECT *
                    FROM StudentDetails
                    WHERE GradeId = @GradeId;
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
