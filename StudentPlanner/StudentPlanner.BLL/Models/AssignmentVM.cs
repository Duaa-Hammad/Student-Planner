using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Models
{
   public class AssignmentVM
    {
        public int Id { get; set; } //PK
        public int CourseId { get; set; } //FK
        [Required]
        public string Title { get; set; } //Like "Doing a Research"
        public string? Note { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
    }
}
