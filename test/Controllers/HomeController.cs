using Dopomoga.API.SDK;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using test.Models;
using test.Models.Home;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiClient _apiClient;
        public HomeController(ILogger<HomeController> logger, ApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }


        public async Task<IActionResult> Index(int? category = null, string searchWord = null)
        {


            // Retrieves the requested culture
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf.RequestCulture.Culture;


            var response = await _apiClient.Categories.Get();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Identity");
            }

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }


            var postsResponse = await _apiClient.Posts.Get();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Identity");
            }

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            

            var model = new HomeViewModel();


            model.ChosenCategoryId = category;

            foreach (var post in postsResponse.Content)
            {
                if (category == null && post.ShowOnMainMenu)
                {
                    if(post.Thumbnail != null)
                    {
                        post.ThumbnailBase64 = Convert.ToBase64String(post.Thumbnail);
                        model.Posts.Add(post);
                    }
                }
                else
                {
                    if (post.CategoryId == category)
                    {
                        if (post.Thumbnail != null)
                        {
                            post.ThumbnailBase64 = Convert.ToBase64String(post.Thumbnail);
                            model.Posts.Add(post);
                        }
                    }
                }
            }

            response.Content.ForEach(x =>
            {
                model.Categories.Add(x);
            });

            if(category == null)
            {
                model.Posts = model.Posts.OrderByDescending(x => x.UpdatedAt).Where(x => x.ShowOnMainMenu).ToList();
                model.Culture = culture.IetfLanguageTag;
                return View(model);
            }

            model.ShowCategories = true;
            model.Culture = culture.IetfLanguageTag;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PostInternalPage(int id)
        {
            
            // Retrieves the requested culture
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf.RequestCulture.Culture;

            var response = await _apiClient.Categories.Get();

            var post = await _apiClient.Posts.GetById(id);

            if(!post.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }


            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new SharedViewModel();



            response.Content.ForEach(x =>
            {
                model.Categories.Add(x);
            });

            model.Post = post.Content;
            model.Post.ThumbnailBase64 = Convert.ToBase64String(post.Content.Thumbnail);
            model.Culture = culture.IetfLanguageTag;

            return View(model);
        }


        [HttpPost]
        public IActionResult CultureManagement(string culture)
        {

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30)});

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Privacy(int? category = null)
        {
            // Retrieves the requested culture
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf.RequestCulture.Culture;

            var response = await _apiClient.Categories.Get();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Identity");
            }

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }


            var postsResponse = await _apiClient.Posts.Get();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Identity");
            }

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }



            var model = new HomeViewModel();

            postsResponse.Content.ForEach(x =>
            {
                if (category == null && x.ShowOnMainMenu)
                {
                    x.ThumbnailBase64 = Convert.ToBase64String(x.Thumbnail);
                    model.Posts.Add(x);
                }
                else
                {
                    if (x.CategoryId == category)
                    {
                        x.ThumbnailBase64 = Convert.ToBase64String(x.Thumbnail);
                        model.Posts.Add(x);
                    }
                }

            });

            response.Content.ForEach(x =>
            {
                model.Categories.Add(x);
            });

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public async Task<IActionResult> AboutUs()
        {
            // Retrieves the requested culture
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf.RequestCulture.Culture;

            var model = new SharedViewModel();

            model.Culture = culture.IetfLanguageTag;

            var response = await _apiClient.Categories.Get();

            response.Content.ForEach(x =>
            {
                model.Categories.Add(x);
            });

            var pageInfo = await _apiClient.PageInformation.GetPageInformation("aboutus");

            model.Info = pageInfo;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Contacts()
        {

            // Retrieves the requested culture
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf.RequestCulture.Culture;
            var model = new SharedViewModel();

            model.Culture = culture.IetfLanguageTag;
            var response = await _apiClient.Categories.Get();

            response.Content.ForEach(x =>
            {
                model.Categories.Add(x);
            });

            try
            {
                var pageInfo = await _apiClient.PageInformation.GetPageInformation("contact");

                model.Info = pageInfo;
            }
            catch (Exception ex)
            {
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> FAQ()
        {
            // Retrieves the requested culture
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf.RequestCulture.Culture;


            var model = new SharedViewModel();

            model.Culture = culture.IetfLanguageTag;

            var response = await _apiClient.Categories.Get();

            response.Content.ForEach(x =>
            {
                model.Categories.Add(x);
            });


            try
            {
                var pageInfo = await _apiClient.PageInformation.GetPageInformation("faq");

                model.Info = pageInfo;
            }
            catch (Exception ex)
            {
                return View(model);
            }
            return View(model);
        }
    }
}