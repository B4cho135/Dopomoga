using Dopomoga.Models.Dtos.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.Models.Requests.Identity
{
    internal class LoginResponse
    {
        public User User { get; set; }
        public string JWT { get; set; }
    }
}
