using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SortingAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

            CREATE PROCEDURE GetTeachersSorted
                @OrderBy NVARCHAR(50),
                @OrderDirection NVARCHAR(4)
            AS
            BEGIN
                DECLARE @SqlQuery NVARCHAR(MAX)

                SET @SqlQuery = 'SELECT *
                                 FROM Teachers
                                 ORDER BY CASE WHEN @OrderBy = ''name'' THEN Name_Teacher END ' + @OrderDirection + ', 
                                          CASE WHEN @OrderBy = ''id'' THEN Id_Teacher END ' + @OrderDirection + ';'

                EXEC sp_executesql @SqlQuery, N'@OrderBy NVARCHAR(50), @OrderDirection NVARCHAR(4)', @OrderBy, @OrderDirection
            END
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
