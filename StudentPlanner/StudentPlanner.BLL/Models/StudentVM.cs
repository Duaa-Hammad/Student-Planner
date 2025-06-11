using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Models
{
    public class StudentVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public int? Semester { get; set; }
    }
}
