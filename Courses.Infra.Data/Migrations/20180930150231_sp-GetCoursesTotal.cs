using Microsoft.EntityFrameworkCore.Migrations;

namespace Courses.Infra.Data.Migrations
{
    public partial class spGetCoursesTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetCoursesTotal]
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT 
                         MaxAvailability AS MaxAvailability, 
                         (MaxAvailability - count(s.id)) AS ActualAvalability, 
                         min(s.age) MinAge, 
                         max(s.age) MaxAge, 
                         sum(s.age)/count(s.id) AverageAge, 
                         c.name
                    FROM course c with (nolock)
                    INNER JOIN student s with (nolock)
                    on c.id = s.courseid 
                    GROUP BY c.id, MaxAvailability, c.name
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
