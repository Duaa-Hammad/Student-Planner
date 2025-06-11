using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.DAL.Entities
{
    public class Exam
    {
        public int Id { get; set; } //PK
        public int CourseId { get; set; } //FK
        [Required]
        public DateTime Date { get; set; }
        //----------------------------
        // Navigation properties
        //----------------------------
        public virtual Course Course { get; set; } // Navigation property to Course
        public virtual List<Reminder> Reminders { get; set; } // Navigation property to Reminders
    }
}
