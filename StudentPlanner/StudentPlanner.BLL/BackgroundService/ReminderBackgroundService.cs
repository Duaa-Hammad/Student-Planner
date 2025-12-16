using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentPlanner.BLL.Interfaces;

namespace StudentPlanner.BLL.Repository
{
    public class ReminderBackgroundService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<ReminderBackgroundService> logger;

        public ReminderBackgroundService(IServiceProvider serviceProvider, ILogger<ReminderBackgroundService> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Reminder Background Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Checking for reminders to send at: {time}", DateTimeOffset.Now);

                try
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var reminderService = scope.ServiceProvider.GetRequiredService<IReminder>();
                        await reminderService.SendDueRemindersAsync(stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error occurred while sending reminders.");
                }
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
            logger.LogInformation("Reminder Background Service is stopping.");
        }

    }

}
