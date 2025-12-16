using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersWarehousesController : ControllerBase
    {
        private readonly IUserWarehouse userWarehouse;

        public UsersWarehousesController(IUserWarehouse userWarehouse)
        {
            this.userWarehouse = userWarehouse;
        }

        [HttpPost]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Add(UserWarehouseDTO request)
        {
            var result = await userWarehouse.AddAsync(request);
            return Ok(result);
        }

        [HttpGet("users/{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAllByUserId(Guid id)
        {
            var result = await userWarehouse.GetAllByUserIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await userWarehouse.GetAllAsync();
            return Ok(result);
        }
    }
}
