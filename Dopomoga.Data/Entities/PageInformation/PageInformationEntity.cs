using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.Data.Entities.PageInformation
{
    public class PageInformationEntity : BaseEntity<int>
    {
        public string PageType { get; set; }
        public string PageTextInGeorgian { get; set; }
        public string PageTextInUkrainian { get; set; }
        public string PageTextInEnglish { get; set; }
    }
}
