using Dopomoga.Data;
using Dopomoga.Data.Entities.Posts;
using Dopomoga.Models.Requests.Posts;
using Dopomoga.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Dopomoga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PostsController : ControllerBase
    {
        private readonly DopomogaDbContext _context;
        private readonly IEmailService _emailSerivce;
        public PostsController(DopomogaDbContext context, IEmailService emailSerivce)
        {
            _context = context;
            _emailSerivce = emailSerivce;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get(string searchWord = null, int? page = null, int limit = 12, int? category = null)
        {
            try
            {
                var posts = _context.Posts.Include(x => x.Category).OrderByDescending(x => !x.ShowInTheEnd).ThenByDescending(x => x.CreatedAt).Where(x => !x.IsDeleted && x.Thumbnail != null);

                if(!string.IsNullOrEmpty(searchWord))
                {
                    posts = posts.Where(x => (x.GeorgianTitle + x.UkrainianTitle + x.GeorgianDescription + x.UkrainianDescription).Contains(searchWord));
                }

                if(category.HasValue)
                {
                    if(category.Value != 28)
                    {
                        posts = posts.Where(x => x.CategoryId == category.Value);
                    }
                    else
                    {
                        posts = posts.Where(x => x.Category.MainCategoryId == 6);
                    }
                }

                if(page != null && page > 0)
                {
                    posts = posts.Skip(page.Value * limit);
                }

                var a = posts.Take(limit).ToList();

                return Ok(posts.Take(limit).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("Main")]
        [AllowAnonymous]
        public IActionResult GetMainPosts()
        {
            try
            {
                var posts = _context.Posts.Include(x => x.Category).Where(x => !x.IsDeleted && x.ShowOnMainMenu).OrderByDescending(x => x.UpdatedAt).ToList();

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("All")]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            try
            {
                var posts = _context.Posts.Include(x => x.Category).Where(x => !x.IsDeleted).ToList();

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("Quantity")]
        [AllowAnonymous]
        public IActionResult GetQuantity(string searchWord, int? categoryId)
        {
            try
            {
                var posts = _context.Posts.Include(x => x.Category).Where(x => !x.IsDeleted);

                if(!string.IsNullOrEmpty(searchWord))
                {
                    var searchQuantity = posts.Where(x => (x.GeorgianTitle + x.UkrainianTitle + x.UkrainianDescription + x.GeorgianDescription).Contains(searchWord)).Count();

                    return Ok(searchQuantity);
                }

                if(categoryId.HasValue)
                {
                    if(categoryId.Value == 28)
                    {
                        var quantity = posts.Where(x => x.Category.MainCategoryId == 6).Count();
                        return Ok(quantity);
                    }
                    var categoryItemsQuantity = posts.Where(x => x.CategoryId == categoryId.Value).Count();

                    return Ok(categoryItemsQuantity);
                }

                return Ok(posts.Count());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            try
            {
                var post = _context.Posts.Include(x => x.Category).FirstOrDefault(c => c.Id == id && !c.IsDeleted);
                if (post == null)
                {
                    return NotFound();
                }
                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostEntity request)
        {
            try
            {

                if (request.CategoryId == 48)
                {

                    String St = request.RedirectUrl;

                    int pFrom = St.IndexOf("v=") + "v=".Length;
                    int pTo = St.LastIndexOf("&");

                    String result = St.Substring(pFrom, pTo - pFrom);

                    string someUrl = $"https://img.youtube.com/vi/{result}/0.jpg";
                    using (var webClient = new WebClient())
                    {
                        byte[] imageBytes = webClient.DownloadData(someUrl);

                        request.Thumbnail = imageBytes;
                    }
                }

                if(request.MainCategoryId == 6 && request.CategoryId != 28)
                {
                    request.CategoryId = 28;
                    //var newPost = new PostEntity()
                    //{
                    //    CategoryId = 28,
                    //    MainCategoryId = request.CategoryId,
                    //    CreatedAt = DateTime.Now,
                    //    EnglishDescription = request.EnglishDescription,
                    //    EnglishTitle = request.EnglishTitle,
                    //    GeorgianDescription = request.GeorgianDescription,
                    //    GeorgianTitle = request.GeorgianTitle,
                    //    IsDeleted = false,
                    //    RedirectUrl = request.RedirectUrl,
                    //    ShowInTheEnd = request.ShowInTheEnd,
                    //    ShowOnMainMenu = request.ShowOnMainMenu,
                    //    Thumbnail = request.Thumbnail,
                    //    UkrainianDescription = request.UkrainianDescription,
                    //    UkrainianTitle = request.UkrainianTitle,
                    //    UpdatedAt = DateTime.Now
                    //};

                    
                    //_context.Posts.Add(newPost);
                    //_context.SaveChanges();
                }

                _context.Posts.Add(request);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatePostRequest request)
        {
            try
            {
                var existingPost = _context.Posts.Include(x => x.Category).FirstOrDefault(c => c.Id == id);

                if(existingPost == null)
                {
                    return NotFound();
                }

                existingPost.GeorgianTitle = request.GeorgianTitle;
                existingPost.GeorgianDescription = request.GeorgianDescription;
                existingPost.UkrainianTitle = request.UkrainianTitle;
                existingPost.UkrainianDescription = request.UkrainianDescription;
                existingPost.CategoryId = request.CategoryId;
                existingPost.RedirectUrl = request.RedirectUrl;
                existingPost.ShowOnMainMenu = request.ShowOnMainMenu;
                existingPost.UpdatedAt = DateTime.UtcNow;
                existingPost.ShowInTheEnd = request.ShowInTheEnd;

                if (request.Thumbnail != null)
                {
                    existingPost.Thumbnail = request.Thumbnail;
                }


                _context.Posts.Update(existingPost);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var post = _context.Posts.FirstOrDefault(c => c.Id == id);

                if (post == null)
                {
                    return NotFound();
                }

                post.IsDeleted = true;
                _context.Posts.Remove(post);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
