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
        public Task DeleteReminderByCourseId(int Id);

        public Task<IEnumerable<Reminder>> GetRemindersByUserId(int Id);

        public Task<IEnumerable<Reminder>> GetDueRemindersAsync(CancellationToken cancellationToken);

        // هذه الدالة ترجع كل التذكيرات المستحقة للإرسال وتقوم بإرسالها
        public Task SendDueRemindersAsync(CancellationToken cancellationToken);
        public Task UpdateAsync(Reminder reminder, CancellationToken cancellationToken);
    }
}
