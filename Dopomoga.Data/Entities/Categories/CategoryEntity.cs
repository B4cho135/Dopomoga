﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.Data.Entities.Categories
{
    public class CategoryEntity : BaseEntity<int>
    {
        public string CategoryGeorgianName { get; set; }
        public string CategoryUkrainianName { get; set; }
    }
}
