using Dopomoga.Data.Entities.Categories;
using Dopomoga.Data.Entities.Posts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.Data
{
    public class DopomogaDbContext : DbContext
    {
        public DopomogaDbContext(DbContextOptions<DopomogaDbContext> options) : base(options)
        {

        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<PostEntity> Posts { get; set; }

    }
}
