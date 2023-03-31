using Dopomoga.Data;
using Dopomoga.Services.Abstractions;

namespace Dopomoga.API.Jobs
{
    public class EmailJobs : IEmailJobs
    {
        private readonly IEmailService _emailService;

        public EmailJobs(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendEmailsToSubscribers()
        {
            try
            {
                _emailService.SendEmailsToSubscribers();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }

    public interface IEmailJobs
    {
        Task SendEmailsToSubscribers();
    }
}
