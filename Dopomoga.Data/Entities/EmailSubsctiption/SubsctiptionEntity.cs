using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.Data.Entities.EmailSubsctiption
{
    public class SubsctiptionEntity : BaseEntity<int>
    {
        public string Email { get; set; }
    }
}
