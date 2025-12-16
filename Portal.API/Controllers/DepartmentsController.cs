using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IUserDepartment userDepartment;

        public DepartmentsController(IUserDepartment userDepartment)
        {
            this.userDepartment = userDepartment;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add([FromBody]UserDepartmentDTO request)
        {
            var result = await userDepartment.AddAsync(request);
            return Ok(result);
        }

        [HttpPatch]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(UserDepartmentUpdateDTO request)
        {
            var result = await userDepartment.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await userDepartment.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetAll()
        {
            var result = await userDepartment.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await userDepartment.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
