using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Models
{
    public class ExamVM
    {
        public int Id { get; set; } //PK
        public int CourseId { get; set; } //FK
        [Required]
        public DateTime Date { get; set; }
    }
}
