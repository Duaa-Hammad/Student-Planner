using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Models
{
    //A list of named options(like "Exam", "Assignment")
    public enum ReminderType
    {
        Exam,
        Assignment
    }
    public class ReminderVM
    {
        public int Id { get; set; }
        public int? AssignmentId { get; set; } //FK
        public int? ExamId { get; set; } //FK
        public bool IsSent { get; set; }
        [Required]
        public DateTime ReminderDate { get; set; }
        [Required (ErrorMessage="You have to input the deadline date")]
        public DateTime Deadline { get; set; }
        [Required(ErrorMessage = "You have to choose the type")]
        public ReminderType Type { get; set; }
        [Required(ErrorMessage = "You have to input when you want to be reminded")]
        public int ReminderOffsetDays { get; set; }
        [Required]
        public int CourseId { get; set; }
        //[Required(ErrorMessage = "Add note or title for your task")]
        public string? Note { get; set; }
        public int StudentId {get; set;}
        //----------------------------
        // Navigation properties
        public virtual Exam? Exam { get; set; } // Navigation property to Exam
        public virtual Assignment? Assignment { get; set; } // Navigation property to Assignment
        public virtual Course? Course { get; set; } // Navigation property to Course
    }
}
