using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Models
{
    public class CourseWithRemindersVM
    {
        public CourseVM Course { get; set; }
        public List<ReminderVM> Reminders { get; set; }
    }
}
