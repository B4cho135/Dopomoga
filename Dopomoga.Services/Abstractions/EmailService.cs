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
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Cms;

namespace Dopomoga.Services.Abstractions
{
    public class EmailService : IEmailService
    {
        private readonly DopomogaDbContext _dbContext;
        public EmailService(DopomogaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SendEmailsToSubscribers()
        {
            try
            {
                var subject = "На Dopomoga.ge опубліковано нову інформацію";

                var todayPosts = _dbContext.Posts.Where(x => !x.IsDeleted && x.CreatedAt.Date == DateTime.Today).ToList();

                if(!todayPosts.Any()) {
                    return;
                }

                var emailBody = "";

                todayPosts.ForEach(post =>
                {
                    emailBody += post.UkrainianDescription + "<br />";
                });

                var recipients = _dbContext.Subscriptions.Where(x => !x.IsDeleted).Select(x => x.Email).ToList();

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("Dopomogage@gmail.com", "jblukjogewpgducj"),
                    EnableSsl = true,
                };

                

                foreach (var recipient in recipients)
                {
                    try
                    {
                        var emailBodyWithUnsubscribeButton = emailBody + "<br />  " +
                               "<div class=\"d-flex justify-content-center\"> " +
                               $"<a href=\"https://dopomoga.ge/Home/Unsubscribe?={recipient}\" style=\"margin-top:10px;background-color:darkblue; color:white; width:150px;height:40px;\" id=\"SubscriberSubmit\">Відписатися</a> </div>";

                        var mailMessage = new MailMessage("Dopomogage@gmail.com", recipient, subject, emailBodyWithUnsubscribeButton);

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
