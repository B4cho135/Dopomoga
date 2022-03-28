using Dopomoga.Data.Entities.Categories;
using Dopomoga.Data.Entities.PageInformation;

namespace test.Models.Home
{
    public class SharedViewModel
    {
        public PageInformationEntity Info { get; set; } = new PageInformationEntity() {  PageTextInGeorgian = "ინფორმაცია ვერ მოიძებნა"};
        public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
    }
}
