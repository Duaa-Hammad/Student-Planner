using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Interfaces
{
    public interface IReminder
    {
        public Task AddReminder(Reminder model);
        public Task<IEnumerable<Reminder>> GetRemindersByUserId(int Id);
    }
}
