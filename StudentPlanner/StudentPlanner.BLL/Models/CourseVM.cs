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
        [Required]
        public int Hours { get; set; }
    }
}
