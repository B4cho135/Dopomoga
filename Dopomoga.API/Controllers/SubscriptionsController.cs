using Dopomoga.Data;
using Dopomoga.Data.Entities.EmailSubsctiption;
using Dopomoga.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace Dopomoga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly DopomogaDbContext _dbContext;
        private readonly IEmailService _emailService;

        public SubscriptionsController(DopomogaDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult GetSubscriptions()
        {
            var subscriptions = _dbContext.Subscriptions.Where(x => !x.IsDeleted).ToList();

            return Ok(subscriptions);
        }

        [HttpPost("{emailAddress}/Unsubscribe")]
        public IActionResult RemoveSubscription(string emailAddress)
        {
            var subscriber = _dbContext.Subscriptions.FirstOrDefault(x => x.Email == emailAddress);

            if(subscriber == null)
            {
                return NotFound();
            }

            subscriber.IsDeleted= true;
            subscriber.UpdatedAt= DateTime.UtcNow;

            _dbContext.Subscriptions.Update(subscriber);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult CreateSubscription(string email)
        {
            if(!IsValid(email))
            {
                return BadRequest("Email not valid!");
            }

            var emailExists = _dbContext.Subscriptions.FirstOrDefault(x => !x.IsDeleted && x.Email == email) != null;

            if(emailExists)
            {
                return BadRequest("Email already exists");
            }

            var newSubscription = new SubsctiptionEntity()
            {
                Email = email
            };

            _dbContext.Subscriptions.Add(newSubscription);
            _dbContext.SaveChanges();

            return Ok("Subscribed");
        }


        private bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
