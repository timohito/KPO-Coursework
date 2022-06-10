using Microsoft.EntityFrameworkCore.Migrations;

namespace TimetableDatabaseImplement.Migrations
{
    public partial class timetable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LectorSubject_LectorId",
                table: "Timetables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LectorSubject_SubjectId",
                table: "Timetables",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LectorSubject_LectorId",
                table: "Timetables");

            migrationBuilder.DropColumn(
                name: "LectorSubject_SubjectId",
                table: "Timetables");
        }
    }
}
