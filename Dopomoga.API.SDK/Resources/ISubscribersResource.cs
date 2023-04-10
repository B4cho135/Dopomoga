using Dopomoga.Data.Entities.Posts;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.API.SDK.Resources
{
    public interface ISubscribersResource
    {

        [Post("/api/Subscriptions")]
        public Task<string> AddSubscriber(string email);
        [Post("/api/Subscriptions/{emailAddress}/Unsubscribe")]
        public Task<string> RemoveSubscriber(string emailAddress);
    }
}
