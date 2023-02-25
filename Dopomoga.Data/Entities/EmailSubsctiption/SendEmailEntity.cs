using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.Data.Entities.EmailSubsctiption
{
    public class SentEmailEntity : BaseEntity<int>
    {
        public DateTime SentAt { get; set; }
        public string SentTo { get; set; }
    }
}
