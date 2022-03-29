﻿using Dopomoga.Data.Entities.Categories;
using Dopomoga.Data.Entities.Posts;

namespace test.Models.Home
{
    public class HomeViewModel
    {
        public bool ShowCategories { get; set; } = false;
        public int? ChosenCategoryId { get; set; }
        public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
        public List<PostEntity> Posts { get; set; } = new List<PostEntity>();
    }
}
