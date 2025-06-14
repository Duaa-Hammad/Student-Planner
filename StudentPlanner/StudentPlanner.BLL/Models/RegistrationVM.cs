using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Models
{
    public class RegistrationVM
    {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            [Range(1, 12)]    
            public int Semester { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Compare("Password")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
    }
}
