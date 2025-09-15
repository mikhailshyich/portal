using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWarehouseController : ControllerBase
    {
        private readonly IUserWarehouse userWarehouse;

        public UserWarehouseController(IUserWarehouse userWarehouse)
        {
            this.userWarehouse = userWarehouse;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(UserWarehouseDTO request)
        {
            var result = await userWarehouse.AddAsync(request);
            return Ok(result);
        }

        [HttpGet("get/user/{id}")]
        public async Task<IActionResult> GetAllByUserId(Guid id)
        {
            var result = await userWarehouse.GetAllByUserIdAsync(id);
            return Ok(result);
        }
    }
}
