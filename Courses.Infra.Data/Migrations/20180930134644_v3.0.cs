using Microsoft.EntityFrameworkCore.Migrations;

namespace Courses.Infra.Data.Migrations
{
    public partial class v30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualAvalability",
                table: "Course");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "ActualAvalability",
                table: "Course",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
