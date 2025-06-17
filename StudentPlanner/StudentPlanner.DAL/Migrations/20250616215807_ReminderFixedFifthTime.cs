using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPlanner.DAL.Migrations
{
    public partial class ReminderFixedFifthTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_AspNetUsers_IdentityUserId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_IdentityUserId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "IdentiyUser",
                table: "Reminders");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_StudentId",
                table: "Reminders",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_Students_StudentId",
                table: "Reminders",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_Students_StudentId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_StudentId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Reminders");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Reminders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentiyUser",
                table: "Reminders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_IdentityUserId",
                table: "Reminders",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_AspNetUsers_IdentityUserId",
                table: "Reminders",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
