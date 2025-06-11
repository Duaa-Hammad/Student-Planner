using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Models
{
    public class ReminderVM
    {
        public int Id { get; set; }
        public int? AssignmentId { get; set; } //FK
        public int? ExamId { get; set; } //FK
        public bool IsSent { get; set; }
        [Required]
        public DateTime ReminderTime { get; set; }
    }
}
