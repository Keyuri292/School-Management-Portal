using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POC_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class searchProcedureCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string SearchCourses = @"create procedure SearchCourses
                                @SearchString NVARCHAR(100)
                                as
                                begin
                                     set nocount on;
                                     select * from Courses where CourseName like '%' + @SearchString + '%';
                                end";
            migrationBuilder.Sql(SearchCourses);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string StoredProcedure = @"drop procedure Search_Tournaments";
            migrationBuilder.Sql(StoredProcedure);
        }
    }
}
