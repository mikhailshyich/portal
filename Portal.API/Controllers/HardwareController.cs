using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HardwareController : ControllerBase
    {
        private readonly IHardware hardwareInterface;

        public HardwareController(IHardware hardwareInterface)
        {
            this.hardwareInterface = hardwareInterface;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(HardwareDTO request)
        {
            var result = await hardwareInterface.AddAsync(request);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await hardwareInterface.GetAllAsync();
            return Ok(result);
        }
    }
}
