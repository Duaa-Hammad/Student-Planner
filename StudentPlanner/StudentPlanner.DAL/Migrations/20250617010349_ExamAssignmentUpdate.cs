using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPlanner.DAL.Migrations
{
    public partial class ExamAssignmentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_StudentId",
                table: "Exams",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_StudentId",
                table: "Assignments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Students_StudentId",
                table: "Assignments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Students_StudentId",
                table: "Exams",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Students_StudentId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Students_StudentId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_StudentId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_StudentId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Assignments");
        }
    }
}
