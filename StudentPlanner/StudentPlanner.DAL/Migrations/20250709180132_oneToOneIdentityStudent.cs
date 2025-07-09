using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPlanner.DAL.Migrations
{
    public partial class oneToOneIdentityStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_IdentityUserId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_IdentityUserId",
                table: "Students",
                column: "IdentityUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_IdentityUserId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Reminders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Students_IdentityUserId",
                table: "Students",
                column: "IdentityUserId");
        }
    }
}
