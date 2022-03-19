using Microsoft.AspNetCore.Mvc.Rendering;

namespace test.Models.Identity
{
    public class InfoViewModel
    {
        public List<SelectListItem> InfoType { get; set; }
        public string InfoGeorgian { get; set; }
    }
}
