using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Models
{
    public class CourseVM
    {
        public int Id { get; set; }
        public int StudentId { get; set; } //FK to Student
        [Required]
        public string Name { get; set; }
        public string Code { get; set; }
        [Required]
        public int Hours { get; set; }
        //---------------------------------------------
        // Navigation properties
        public virtual Student Student { get; set; } // Navigation property to Student
        public virtual List<Assignment> Assignments { get; set; } // Navigation property to Assignments
        public virtual List<Exam> Exams { get; set; } // Navigation property to Exams
        public virtual List<Reminder> Reminders { get; set; }
    }
}
