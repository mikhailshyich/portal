using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainWarehouseController : ControllerBase
    {
        private readonly IMainWarehouse mainWarehouseService;

        public MainWarehouseController(IMainWarehouse mainWarehouseService)
        {
            this.mainWarehouseService = mainWarehouseService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(MainWarehouseDTO request)
        {
            if (request is null)
                return BadRequest();
            var result = await mainWarehouseService.AddAsync(request);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await mainWarehouseService.GetAllAsync();
            return Ok(result);
        }
    }
}
