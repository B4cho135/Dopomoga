﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace test.Models.Identity
{
    public class InfoViewModel
    {
        public List<SelectListItem> InfoType { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "ჩვენს შესახებ", Value = "AboutUs"},
            new SelectListItem() { Text = "საკონტაქტო", Value = "Contact"},
            new SelectListItem(){ Text = "FAQ", Value = "FAQ"}
        };
        public string InfoGeorgian { get; set; }
        public string InfoUkrainian { get; set; }
        public string PageType { get; set; }
    }
}
