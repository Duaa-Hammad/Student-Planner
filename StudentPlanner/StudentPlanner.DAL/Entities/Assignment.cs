using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.DAL.Entities
{
    public class Assignment
    {
        public Assignment() 
        {
            Title = string.Empty;
            IsDone = false;
        }
        public int Id { get; set; } //PK
        public int CourseId { get; set; } //FK
        public string Title { get; set; } //Like "Doing a Research"
        public string? Note { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
        public int StudentId { get; set; }

        //----------------------------
        // Navigation properties
        //----------------------------
        public virtual Course Course { get; set; } // Navigation property to Course
        public virtual List<Reminder>? Reminders { get; set; } // Navigation property to Reminders
        public virtual Student Student { get; set; }

    }
}
