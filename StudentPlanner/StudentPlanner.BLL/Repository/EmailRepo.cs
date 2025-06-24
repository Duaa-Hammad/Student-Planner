using Microsoft.Extensions.Configuration;
using StudentPlanner.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static StudentPlanner.BLL.Repository.EmailRepo;

namespace StudentPlanner.BLL.Repository
{
    public class EmailRepo : IEmail
    {
            private readonly IConfiguration config;

            public EmailRepo(IConfiguration config)
            {
                this.config = config;
            }

            public async Task SendEmailAsync(string toEmail, string subject, string body)
            {
                var smtpClient = new SmtpClient(config["EmailSettings:SmtpServer"])
                {
                    Port = int.Parse(config["EmailSettings:Port"]),
                    Credentials = new NetworkCredential(
                        config["EmailSettings:SenderEmail"],
                        config["EmailSettings:SenderPassword"]
                    ),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(config["EmailSettings:SenderEmail"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
    }

}