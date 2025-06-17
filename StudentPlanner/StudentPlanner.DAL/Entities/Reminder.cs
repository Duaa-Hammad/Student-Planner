using StudentPlanner.DAL.Extends;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.DAL.Entities
{
    public enum ReminderType
    {
        Exam,
        Assignment
    }
    public class Reminder
    {
        public int Id { get; set; }
        public int? AssignmentId { get; set; } //FK
        public int? ExamId { get; set; } //FK
        public bool IsSent { get; set; }
        [Required]
        public DateTime ReminderDate { get; set; }
        public ReminderType Type { get; set; }
        public int? CourseId { get; set; } //FK to Course
        public string? Note { get; set; }
        public DateTime Deadline { get; set; }
        public int StudentId { get; set; }
        //----------------------------
        // Navigation properties
        //----------------------------
        public virtual Exam? Exam { get; set; } // Navigation property to Exam
        public virtual Assignment? Assignment { get; set; } // Navigation property to Assignment
        public virtual Course? Course { get; set; } // Navigation property to Course
        public virtual Student Student { get; set; }
    }
}
