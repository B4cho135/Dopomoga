using Dopomoga.Data.Entities.Categories;
using Dopomoga.Data.Entities.Identity;
using Dopomoga.Data.Entities.PageInformation;
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
    public class DopomogaDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
    {
        public DopomogaDbContext(DbContextOptions<DopomogaDbContext> options) : base(options)
        {

        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<PageInformationEntity> PageInformation { get; set; }
        public DbSet<MainCategoryEntity> MainCategories { get; set; }

    }
}
