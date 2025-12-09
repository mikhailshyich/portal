using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDepartmentController : ControllerBase
    {
        private readonly IUserDepartment userDepartment;

        public UserDepartmentController(IUserDepartment userDepartment)
        {
            this.userDepartment = userDepartment;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(UserDepartmentDTO request)
        {
            var result = await userDepartment.AddAsync(request);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserDepartment request)
        {
            var result = await userDepartment.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await userDepartment.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await userDepartment.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await userDepartment.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
