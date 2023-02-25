using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dopomoga.Data.Entities.Posts;
using Dopomoga.Data;
using Dopomoga.Data.Entities.EmailSubsctiption;

namespace Dopomoga.Services.Abstractions
{
    public class EmailService : IEmailService
    {
        private readonly DopomogaDbContext _dbContext;
        public EmailService(DopomogaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SendEmailsToSubscribers(PostEntity post)
        {
            try
            {
                var subject = "На Dopomoga.ge опубліковано нову інформацію";

                var recipients = _dbContext.Subscriptions.Where(x => !x.IsDeleted).Select(x => x.Email).ToList();

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("badri.tatarashvili@gmail.com", "davocrahoijiouyj"),
                    EnableSsl = true,
                };

                foreach (var recipient in recipients)
                {
                    try
                    {

                        var mailMessage = new MailMessage("badri.tatarashvili@gmail.com", recipient, subject, post.UkrainianDescription);

                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);

                        var newSentEmail = new SentEmailEntity()
                        {
                            SentTo = recipient,
                            SentAt = DateTime.Now
                        };

                        _dbContext.SentEmails.Add(newSentEmail);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Could not send email");
                    }
                }

                _dbContext.SaveChanges();
            }
            catch 
            {
                Console.WriteLine("Something went wrong!");
            }
        }
    }
}
