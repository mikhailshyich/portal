using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Register(RegisterDTO request)
        {
            var result = await userService.RegisterAsync(request);
            if (result.Flag is false)
                return Ok(result);
            return Created("", result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO request)
        {
            if (request is null)
                return BadRequest("Заполните обязательные поля.");

            var user = await userService.LoginAsync(request);
            if (user.Flag is false)
                return NotFound(user);

            return Ok(user);
        }

        [HttpPost("roles")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddUserRole(UserRoleDTO request)
        {
            if (request is null)
                return BadRequest("Заполните обязательные поля.");

            var role = await userService.AddRoleAsync(request);

            return Ok(role);
        }

        [HttpGet]
        [Authorize(Roles = "admin,user,employee_it")]
        public async Task<IActionResult> AllUsers()
        {
            var result = await userService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user,employee_it")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await userService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("roles")]
        [Authorize(Roles = "admin,user,employee_it")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await userService.GetAllUserRolesAsync();
            return Ok(result);
        }

        [HttpPost("sync")]
        [Authorize(Roles = "admin,employee_it")]
        public async Task<IActionResult> SyncUsers()
        {
            var result = await userService.SyncUsersAsync();
            return Ok(result);
        }

        [HttpGet("usernames/{username}")]
        [Authorize(Roles = "admin,user,employee_it")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var result = await userService.GetByUsernameAsync(username);
            return Ok(result);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditUser(UserEdit request)
        {
            var result = await userService.EditUserAsync(request);
            return Ok(result);
        }
    }
}
