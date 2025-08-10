using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryHardwareController : ControllerBase
    {
        private readonly ICategoryHardware categoryHardware;

        public CategoryHardwareController(ICategoryHardware categoryHardware)
        {
            this.categoryHardware = categoryHardware;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CategoryHardwareDTO request)
        {
            var result = await categoryHardware.AddAsync(request);
            return Ok(result);
        }
    }
}
