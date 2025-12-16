using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryHardware categoryHardware;

        public CategoriesController(ICategoryHardware categoryHardware)
        {
            this.categoryHardware = categoryHardware;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add([FromBody]CategoryHardwareDTO request)
        {
            var result = await categoryHardware.AddAsync(request);
            if (result.Flag)
                return CreatedAtRoute("",result);
            else
                return BadRequest(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAll()
        {
            var result = await categoryHardware.GetAllAsync();
            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await categoryHardware.GetByIdAsync(id);
            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }
    }
}
