using Dopomoga.Data.Entities.Categories;
using Dopomoga.Data.Entities.PageInformation;
using Dopomoga.Data.Entities.Posts;

namespace test.Models.Home
{
    public class SharedViewModel
    {
        public string Culture { get; set; }
        public PostEntity Post { get; set; }
        public PageInformationEntity Info { get; set; } = new PageInformationEntity() {  PageTextInGeorgian = "ინფორმაცია ვერ მოიძებნა"};
        public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
    }
}
