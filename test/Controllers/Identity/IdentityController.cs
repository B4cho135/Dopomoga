using Microsoft.AspNetCore.Mvc;

namespace test.Controllers.Identity
{
    public class IdentityController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
