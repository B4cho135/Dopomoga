using Dopomoga.API.SDK;
using Dopomoga.Data.Entities.Categories;
using Dopomoga.Data.Entities.PageInformation;
using Dopomoga.Data.Entities.Posts;
using Dopomoga.Models.Requests.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Categories()
        {
            var model = new CategoryViewModel();

            var response = await _client.Categories.Get();

            if(response.IsSuccessStatusCode)
            {
                foreach(var category in response.Content)
                model.Categories.Add(new SelectListItem()
                {
                    Value = category.Id.ToString(),
                    Text = category.CategoryGeorgianName
                });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(CategoryViewModel model)
        {
            try
            {
                var response = await _client.Categories.Delete(model.CategoryId);

                return RedirectToAction("Categories", "Identity");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Categories", "Identity");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryViewModel model)
        {
            var entity = new CategoryEntity()
            {
                CategoryGeorgianName = model.GeorgianTitle,
                CategoryUkrainianName = model.UkrainianTitle,
                Order = model.Order,
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


        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel model)
        {
            var entity = new CategoryEntity()
            {
                Id = model.CategoryId,
                CategoryGeorgianName = model.GeorgianTitle,
                CategoryUkrainianName = model.UkrainianTitle,
                Order = model.Order,
            };

            var response = await _client.Categories.Update(model.CategoryId,entity);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login");
            }

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Categories");
        }

        [HttpGet]
        public async Task<IActionResult> Posts()
        {
            var postViewModel = new PostViewModel();

            var posts = await _client.Posts.Get();

            if(posts.IsSuccessStatusCode)
            {
                foreach(var post in posts.Content)
                {
                    postViewModel.Posts.Add(new SelectListItem()
                    {
                        Value = post.Id.ToString(),
                        Text = post.GeorgianTitle
                    });
                }    
            }

            var categories = await _client.Categories.Get();

            if (categories.IsSuccessStatusCode)
            {
                foreach (var category in categories.Content)
                {
                    postViewModel.Categories.Add(new SelectListItem()
                    {
                        Value = category.Id.ToString(),
                        Text = category.CategoryGeorgianName
                    });
                }
            }

            return View(postViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostViewModel model, IFormFile Image)
        {
            var entity = new PostEntity()
            {
                GeorgianTitle = model.TitleGeorgian,
                GeorgianDescription = model.DescriptionGeorgian,
                UkrainianTitle = model.TitleUkrainian,
                UkrainianDescription = model.DescriptionUkrainian,
                CategoryId = model.CategoryId,
                Thumbnail = model.Image,
                RedirectUrl =model.RedirectUrl,
                ShowOnMainMenu = model.ShowOnMainMenu,
            };
            if (Image != null)
            {
                using (var stream = new MemoryStream())
                {
                    await Image.CopyToAsync(stream);
                    entity.Thumbnail = stream.ToArray();


                }
            }

            var response = await _client.Posts.Create(entity);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login");
            }

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Categories");
        }

        [HttpGet]
        public IActionResult Info()
        {
            var model = new InfoViewModel();
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> CreateInfo(InfoViewModel model)
        {

            var response = await _client.PageInformation.CreatePageInfo(new Dopomoga.Models.Requests.PageInformation.CreatePageInfoRequest()
            {
                PageTextInGeorgian = model.InfoGeorgian,
                PageTextInUkrainian = model.InfoUkrainian,
                PageType = model.PageType
            });

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login");
            }

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Info", "Identity");
        }


        [HttpGet]
        public async Task<IActionResult> EditPost(int Id)
        {
            var post = await _client.Posts.GetById(Id);

            var postCategories = await _client.Categories.Get();
            var model = new PostViewModel();
            if(post.IsSuccessStatusCode)
            {
                model.TitleGeorgian = post.Content.GeorgianTitle;
                model.TitleUkrainian = post.Content.UkrainianTitle;
                model.DescriptionGeorgian = post.Content.GeorgianDescription;
                model.DescriptionUkrainian = post.Content.UkrainianDescription;
                model.RedirectUrl = post.Content.RedirectUrl;
                model.CategoryName = post.Content.Category.CategoryGeorgianName;
                model.CategoryId = post.Content.CategoryId;
                model.ShowOnMainMenu = post.Content.ShowOnMainMenu;
            }

            if(postCategories.IsSuccessStatusCode)
            {
                foreach(var category in postCategories.Content)
                {
                    model.Categories.Add(new SelectListItem()
                    {
                        Value = category.Id.ToString(),
                        Text = category.CategoryGeorgianName
                    });
                }
            }
           
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(PostViewModel model, IFormFile Image)
        {
            var entity = new PostEntity()
            {
                Id = model.Id,
                GeorgianTitle = model.TitleGeorgian,
                GeorgianDescription = model.DescriptionGeorgian,
                UkrainianTitle = model.TitleUkrainian,
                UkrainianDescription = model.DescriptionUkrainian,
                CategoryId = model.CategoryId,
                Thumbnail = model.Image,
                RedirectUrl = model.RedirectUrl,
                ShowOnMainMenu=model.ShowOnMainMenu,
            };
            if (Image != null)
            {
                using (var stream = new MemoryStream())
                {
                    await Image.CopyToAsync(stream);
                    entity.Thumbnail = stream.ToArray();


                }
            }

            var response = await _client.Posts.Update(model.Id,entity);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login");
            }

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Categories");
        }


        [HttpPost]
        public async Task<IActionResult> DeletePost(PostViewModel model)
        {

            try
            {
                var response = await _client.Posts.Delete(model.Id);


                return RedirectToAction("Posts");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Posts");
            }
        }


    }
}
