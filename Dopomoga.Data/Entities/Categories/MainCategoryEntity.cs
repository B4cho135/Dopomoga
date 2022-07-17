using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.Data.Entities.Categories
{
    public class MainCategoryEntity : BaseEntity<int>
    {
        public string NameGeorgian { get; set; }
        public string NameEnglish { get; set; }
        public string NameUkrainian { get; set; }
    }
}
