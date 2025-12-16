using Microsoft.AspNetCore.Identity;
using StudentPlanner.DAL.Entities;
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
        //Navigation Property
        public virtual Student Student { get; set; }
    }
}
