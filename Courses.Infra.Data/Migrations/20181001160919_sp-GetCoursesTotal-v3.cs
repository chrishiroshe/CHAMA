using Microsoft.EntityFrameworkCore.Migrations;

namespace Courses.Infra.Data.Migrations
{
    public partial class spGetCoursesTotalv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"ALTER PROCEDURE [dbo].[GetCoursesTotal]
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT 
                         c.Id as Id,
                         c.IsActive as IsActive,
                         c.Created as Created,
                         c.TeacherId as TeacherId,
                         MaxAvailability AS MaxAvailability, 
                         (MaxAvailability - count(s.id)) AS ActualAvalability, 
                         min(s.age) MinAge, 
                         max(s.age) MaxAge, 
                         sum(s.age)/count(s.id) AverageAge, 
                         c.name
                    FROM course c with (nolock)
                    INNER JOIN student s with (nolock)
                    on c.id = s.courseid 
                    GROUP BY c.id, MaxAvailability, c.name, c.IsActive, c.Created, c.TeacherId
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
