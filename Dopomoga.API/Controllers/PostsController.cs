using Dopomoga.Data;
using Dopomoga.Data.Entities.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dopomoga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PostsController : ControllerBase
    {
        private readonly DopomogaDbContext _context;
        public PostsController(DopomogaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get(string searchWord = null)
        {
            try
            {

                var posts = _context.Posts.Include(x => x.Category).Where(x => !x.IsDeleted).OrderByDescending(x => x.UpdatedAt).ToList();

                if(!string.IsNullOrEmpty(searchWord))
                {

                }

                return Ok(posts);
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
        public IActionResult Put(int id, [FromBody] PostEntity request)
        {
            try
            {
                _context.Posts.Update(request);
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
