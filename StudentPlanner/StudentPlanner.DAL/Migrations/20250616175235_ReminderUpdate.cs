using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPlanner.DAL.Migrations
{
    public partial class ReminderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Courses_CourseId",
                table: "Exams");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Assignments",
                newName: "Note");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Reminders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reminders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_CourseId",
                table: "Reminders",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_UserId",
                table: "Reminders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Courses_CourseId",
                table: "Exams",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_AspNetUsers_UserId",
                table: "Reminders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Courses_CourseId",
                table: "Reminders",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Courses_CourseId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_AspNetUsers_UserId",
                table: "Reminders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Courses_CourseId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_CourseId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_UserId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Exams");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Assignments",
                newName: "Description");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_CourseId",
                table: "Assignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Courses_CourseId",
                table: "Exams",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
