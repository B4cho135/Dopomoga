using Dopomoga.Data;
using Dopomoga.Data.Entities.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dopomoga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CategoriesController : ControllerBase
    {

        private readonly DopomogaDbContext _context;
        public CategoriesController(DopomogaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            try
            {
                var categories = _context.Categories.Where(x => !x.IsDeleted).OrderBy(x => x.Order).ToList();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("MainCategories")]
        [AllowAnonymous]
        public IActionResult GetMainCategories()
        {
            try
            {
                var mainCategories = _context.MainCategories.Where(x => !x.IsDeleted).OrderBy(x => x.CreatedAt).ToList();

                return Ok(mainCategories);
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
                var category = _context.Categories.FirstOrDefault(c => c.Id == id && !c.IsDeleted);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoryEntity request)
        {
            try
            {
                var largestOrderNumber = _context.Categories.Max(x => x.Order);

                request.Order = largestOrderNumber;
                _context.Categories.Add(request);
                _context.SaveChanges();
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryEntity request)
        {
            try
            {
                _context.Categories.Update(request);
                _context.SaveChanges();

                return Ok(request);
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
                var cateogry = _context.Categories.Where(x => !x.IsDeleted).FirstOrDefault(c => c.Id == id);

                if (cateogry == null)
                {
                    return NotFound();
                }

                cateogry.IsDeleted = true;

                _context.Categories.Update(cateogry);
                _context.SaveChanges();

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
