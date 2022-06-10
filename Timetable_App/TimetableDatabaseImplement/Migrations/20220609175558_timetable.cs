using Microsoft.EntityFrameworkCore.Migrations;

namespace TimetableDatabaseImplement.Migrations
{
    public partial class timetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Class",
                table: "Timetables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Timetables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Denearies",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Timetables");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Timetables");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Denearies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
