using Dopomoga.API.SDK;
using Dopomoga.Data.Entities.Categories;
using Dopomoga.Models.Requests.Identity;
using Microsoft.AspNetCore.Mvc;
using test.Models.Identity;

namespace test.Controllers.Identity
{
    public class IdentityController : Controller
    {
        private readonly ApiClient _client;
        public IdentityController(ApiClient client)
        {
            _client = client;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            var loginResponse = await _client.Account.Login(new LoginRequest()
            {
                Email = model.Email,
                Password = model.Password
            });
            if (loginResponse.IsSuccessStatusCode)
            {
                HttpContext.Response.Cookies.Append("access_token", loginResponse.Content.JWT, new CookieOptions());
                return RedirectToAction("Categories");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Categories()
        {
            var model = new CategoryViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryViewModel model)
        {
            var entity = new CategoryEntity()
            {
                CategoryGeorgianName = model.GeorgianTitle,
                CategoryUkrainianName = model.UkrainianTitle
            };

            var response = await _client.Categories.Create(entity);

            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login");
            }

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Categories");
        }
    }
}
