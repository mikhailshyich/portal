using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainWarehousesController : ControllerBase
    {
        private readonly IMainWarehouse mainWarehouseService;

        public MainWarehousesController(IMainWarehouse mainWarehouseService)
        {
            this.mainWarehouseService = mainWarehouseService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add(MainWarehouseDTO request)
        {
            if (request is null)
                return BadRequest();
            var result = await mainWarehouseService.AddAsync(request);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin,user,employee_it")]
        public async Task<IActionResult> GetAll()
        {
            var result = await mainWarehouseService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user,employee_it")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await mainWarehouseService.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
