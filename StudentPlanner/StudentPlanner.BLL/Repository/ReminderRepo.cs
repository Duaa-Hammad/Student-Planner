using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentPlanner.BLL.Interfaces;
using StudentPlanner.DAL.Database;
using StudentPlanner.DAL.Entities;

namespace StudentPlanner.BLL.Repository
{
    public class ReminderRepo : IReminder
    {
        private readonly StPlannerContext data;
        private readonly IEmail email;
        private readonly ICourse course;
        private readonly ILogger<ReminderBackgroundService> logger;
        public ReminderRepo(StPlannerContext data, IEmail email, ILogger<ReminderBackgroundService> logger, ICourse course)
        {
            this.data = data;
            this.email = email;
            this.logger = logger;
            this.course = course;
        }
        public async Task AddReminder(Reminder model)
        {
            var reminder = await data.Reminders.AddAsync(model);
            await data.SaveChangesAsync();
        }
        public Task UpdateAsync(Reminder reminder, CancellationToken cancellationToken)
        {
            data.Reminders.Update(reminder);
            return data.SaveChangesAsync();
        }
        public async Task<IEnumerable<Reminder>> GetRemindersByUserId(int Id)
        {
           return await data.Reminders.Include("Course").Where(r => r.StudentId==Id).ToListAsync();          
        }
        public async Task<IEnumerable<Reminder>> GetDueRemindersAsync(CancellationToken cancellationToken)
        {
            return await data.Reminders
                .Where(r => r.ReminderDate <= DateTime.UtcNow && !r.IsSent)
                .ToListAsync(cancellationToken);
        }
        public async Task SendDueRemindersAsync(CancellationToken cancellationToken)
        {
            // 1. جلب كل التذكيرات التي لم تُرسل بعد وموعدها وصل

            var reminders = await GetDueRemindersAsync(cancellationToken);

            // 2. لو مفيش تنبيهات، نخرج من الدالة مباشرة
            if (reminders == null || !reminders.Any())
                return;

            // 3. نرسل كل تذكير واحد واحد
            foreach (var reminder in reminders)
            {
                if (cancellationToken.IsCancellationRequested)
                    break; // لو تم طلب الإلغاء، نوقف التنفيذ

                try
                {

                   if(reminder.StudentId == null)
                        continue; // لو مفيش طالب مرتبط بالتذكير، نتخطاه

                    var st = await data.Students.FindAsync(reminder.StudentId);
                    if (st == null)
                        continue; // لو الطالب مش موجود، نتخطى التذكير

                    string studentName = st.Name;
                    var daysLeft = reminder.Deadline - reminder.ReminderDate;
                    string userEmail = st.Email;
                    DAL.Entities.ReminderType type = reminder.Type;
                    // تحديد نوع التذكير
                    string Remindertype="";

                    if (type == DAL.Entities.ReminderType.Exam)
                          Remindertype = "exam";
                    else if (type == DAL.Entities.ReminderType.Assignment)
                        Remindertype = "assignment";

                    var courseData = await course.GetCourseByIdAsync(reminder.CourseId);

                    string timeFrame = daysLeft.Days switch
                    {
                        1 => "tomorrow",
                        3 => "in 3 days",
                        7 => "in a week",
                        30 => "in a month",
                        _ => $"in {daysLeft} days"
                    };

                    string message = $"Hey {studentName}, you have {Remindertype} in {courseData.Name} {timeFrame} <br/>Details:<br/>{reminder.Note}<br/>Be ready for it!<br/>Break a leg buddy<br/>I know you can do it <3";

                    await email.SendEmailAsync(userEmail, "Reminder", message);

                    // 5. تحديث حالة التنبيه إلى 'تم الإرسال'
                    reminder.IsSent = true;
                    await UpdateAsync(reminder, cancellationToken);
                }
                catch (Exception ex)
                {
                    // 6. سجل الخطأ لكن استمر في إرسال بقية التنبيهات
                    logger.LogError(ex, $"Failed to send reminder to {reminder.Student.Email}");
                }
            }
        }
        public async Task<Reminder> FindCourseReminder (int CourseId)
        {
            var reminder = await data.Reminders.Include("Course").Where(e => e.CourseId == CourseId).FirstOrDefaultAsync();
            return reminder;
        }
        public async Task DeleteReminderByCourseId(int Id)
        {
            var reminders = await data.Reminders
              .Where(r => r.CourseId == Id)
              .ToListAsync();

            if (reminders.Any())
            {
                data.Reminders.RemoveRange(reminders);
                await data.SaveChangesAsync();
            }
        }

        public async Task<Reminder> GetReminderById(int Id)
        {
            return await data.Reminders.Include("Course").Where(r => r.Id == Id).FirstOrDefaultAsync();
        }

        public async Task DeleteReminderAsync(Reminder reminder)
        {
            data.Reminders.Remove(reminder);
            await data.SaveChangesAsync();
        }

        public async Task UpdateReminderAsync(Reminder reminder)
        {
            data.Reminders.Update(reminder);
            await data.SaveChangesAsync();
        }

    }
}
