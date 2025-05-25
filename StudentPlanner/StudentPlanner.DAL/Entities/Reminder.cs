using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.DAL.Entities
{
    public class Reminder
    {
        public int Id { get; set; }
        public int? AssignmentId { get; set; } //FK
        public int? ExamId { get; set; } //FK
        public bool IsSent { get; set; }
        public DateTime ReminderTime { get; set; }
    }
}
