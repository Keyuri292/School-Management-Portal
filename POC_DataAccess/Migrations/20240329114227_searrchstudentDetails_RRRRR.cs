using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class searrchstudentDetails_RRRRR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            string sP = @"CREATE PROCEDURE SearchStudent
            @SearchString NVARCHAR(100)
        AS
        BEGIN
            SET NOCOUNT ON;
            SELECT * FROM StudentDetails
            WHERE firstName_Student LIKE '%' + @SearchString + '%'
               ;
        END";
            migrationBuilder.Sql(sP);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sP = @"Drop procedure SearchStudent";
            migrationBuilder.Sql(sP);
        }
    }
}
