using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.Models.Requests.PageInformation
{
    public class CreatePageInfoRequest
    {
        public string PageTextInGeorgian { get; set; }
        public string PageTextInUkrainian { get; set; }
        public string PageType { get; set; }
    }
}
