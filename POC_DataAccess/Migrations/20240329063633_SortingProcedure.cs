using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SortingProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string SP_Teacher_Sort = @"CREATE PROCEDURE SortTeachers
                            @SortColumn NVARCHAR(50),
                            @SortOrder NVARCHAR(4)
                        AS
                        BEGIN
                            SET NOCOUNT ON;
 
                            DECLARE @Query NVARCHAR(MAX)
 
                            SET @Query = 'SELECT * FROM Teachers ORDER BY ' + @SortColumn + ' ' + @SortOrder
 
                            EXEC sp_executesql @Query
                        END";
            migrationBuilder.Sql(SP_Teacher_Sort);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string SP_Teacher_Sort = @"Drop procedure SortTeachers";
            migrationBuilder.Sql(SP_Teacher_Sort);
        }
    }
}
