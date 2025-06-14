using Microsoft.AspNetCore.Identity;
using StudentPlanner.DAL.Extends;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.DAL.Entities
{
    public class Student
    {
        public Student()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public int? Semester { get; set; }
        //--------------------------------------------------
        //Authentication properties
        public string IdentityUserId { get; set; } //FK
        public DateTime CreatedAt { get; set; }
        //----------------------------
        // Navigation properties
        //----------------------------
        public virtual List<Course> Courses { get; set; }
        public ApplicationUser IdentityUser { get; set; }
    }
}
