using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.Data.Entities.Categories
{
    public class MainCategoryEntity : BaseEntity<int>
    {
        public string GeorgianName { get; set; }
        public string EnglishName { get; set; }
        public string UkrainianName { get; set; }
    }
}
