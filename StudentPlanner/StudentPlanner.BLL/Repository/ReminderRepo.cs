using Microsoft.EntityFrameworkCore;
using StudentPlanner.BLL.Interfaces;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Database;
using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Repository
{
    public class ReminderRepo : IReminder
    {
        private readonly StPlannerContext data;
        public ReminderRepo(StPlannerContext data)
        {
            this.data = data;
        }
        public async Task AddReminder(Reminder model)
        {
            var reminder = await data.Reminders.AddAsync(model);
            await data.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reminder>> GetRemindersByUserId(int Id)
        {
           return await data.Reminders.Where(r => r.StudentId==Id).ToListAsync();          
        }

    }
}
