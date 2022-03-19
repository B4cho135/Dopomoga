using Dopomoga.Data;
using Dopomoga.Data.Entities.PageInformation;
using Dopomoga.Models.Requests.PageInformation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dopomoga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class InfoController : ControllerBase
    {
        private readonly DopomogaDbContext _context;
        public InfoController(DopomogaDbContext context)
        {
            _context = context;
        }
        [HttpGet("{pageType}")]
        [AllowAnonymous]
        public IActionResult Get(string pageType)
        {
            var infoEntity = _context.PageInformation.FirstOrDefault(x => x.PageType == pageType);

            if(infoEntity == null)
            {
                return NotFound();
            }
            return Ok(infoEntity);
        }

        [HttpPost]
        public IActionResult CreatePageInfo(CreatePageInfoRequest model)
        {
            var entity = new PageInformationEntity()
            {
                PageType = model.PageType,
                PageTextInGeorgian = model.PageTextInGeorgian,
                PageTextInUkrainian = model.PageTextInUkrainian,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            try
            {
                _context.PageInformation.Add(entity);
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
