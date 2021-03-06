

using Microsoft.AspNetCore.Mvc.Rendering;

namespace test.Models.Identity
{
    public class CategoryViewModel
    {
        public string GeorgianTitle { get; set; }
        public string UkrainianTitle { get; set; }
        public string EnglishTitle { get; set; }
        public int CategoryId { get; set; }
        public int Order { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
