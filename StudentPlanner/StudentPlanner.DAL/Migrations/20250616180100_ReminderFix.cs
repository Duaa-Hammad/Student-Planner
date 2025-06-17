using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPlanner.DAL.Migrations
{
    public partial class ReminderFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminders_AspNetUsers_UserId",
                table: "Reminders");

            migrationBuilder.DropIndex(
                name: "IX_Reminders_UserId",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reminders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reminders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_UserId",
                table: "Reminders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminders_AspNetUsers_UserId",
                table: "Reminders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
