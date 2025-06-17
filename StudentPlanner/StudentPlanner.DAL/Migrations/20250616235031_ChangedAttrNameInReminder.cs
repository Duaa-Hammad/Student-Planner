using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPlanner.DAL.Migrations
{
    public partial class ChangedAttrNameInReminder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReminderTime",
                table: "Reminders",
                newName: "ReminderDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReminderDate",
                table: "Reminders",
                newName: "ReminderTime");
        }
    }
}
