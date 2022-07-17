using Dopomoga.Data.Entities.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.Data.Entities.Posts
{
    public class PostEntity : BaseEntity<int>
    {
        public string GeorgianTitle { get; set; }
        public string GeorgianDescription { get; set; }
        public string UkrainianTitle { get; set; }
        public string UkrainianDescription { get; set; }
        public byte[] Thumbnail { get; set; }
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
        public string RedirectUrl { get; set; }
        public bool ShowOnMainMenu { get; set; }
        public string EnglishDescription { get; set; }
        public string EnglishTitle { get; set; }
        public int? MainCategoryId { get; set; }
        public MainCategoryEntity MainCategory { get; set; }

        [NotMapped]
        public string ThumbnailBase64 { get; set; }
        public bool ShowInTheEnd { get; set; }
    }
}
