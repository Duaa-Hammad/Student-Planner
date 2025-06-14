using Microsoft.AspNetCore.Identity;
<<<<<<< HEAD
using StudentPlanner.DAL.Entities;
=======
>>>>>>> 114a8fa (Created Registration Post Action)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.DAL.Extends
{
    public class ApplicationUser : IdentityUser
    {
        public bool RememberMe { get; set; }
<<<<<<< HEAD

        //Navigation Property
        public virtual Student Student { get; set; }
=======
>>>>>>> 114a8fa (Created Registration Post Action)
    }
}
