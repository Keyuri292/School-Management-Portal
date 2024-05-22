using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class searchStudentDetailsProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE SearchStudentDetails
                    @searchKeyword NVARCHAR(100)
                AS
                BEGIN
                    SELECT *
                    FROM StudentDetails
                    WHERE firstName_Student LIKE '%' + @searchKeyword + '%'
                    OR lastName_Student LIKE '%' + @searchKeyword + '%'
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SearchStudentDetails");
        }
    }
}
