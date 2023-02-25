using Dopomoga.Data.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.Services.Abstractions
{
    public interface IEmailService
    {
        void SendEmailsToSubscribers(PostEntity post);
    }
}
