using Dopomoga.Data.Entities.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace test.Models.Identity
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string TitleGeorgian { get; set; }
        public string TitleUkrainian { get; set; }
        public string DescriptionGeorgian { get; set; }
        public string DescriptionUkrainian { get; set; }
        public string RedirectUrl { get; set; }
        public string CategoryName { get; set; }
        public byte[] Image { get; set; }
        public int CategoryId { get; set; }
        public bool ShowOnMainMenu { get; set; }
        public CategoryEntity Category { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Posts { get; set; } = new List<SelectListItem>();
    }
}
