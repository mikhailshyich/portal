using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;
using Portal.Domain.Entities.Users;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO request)
        {
            var user = await userService.RegisterAsync(request);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO request)
        {
            if (request is null)
                return BadRequest("Заполните обязательные поля.");

            var user = await userService.LoginAsync(request);

            return Ok(user);
        }

        [HttpPost("addRole")]
        public async Task<ActionResult> AddUserRole(UserRoleDTO request)
        {
            if (request is null)
                return BadRequest("Заполните обязательные поля.");

            var role = await userService.AddRoleAsync(request);

            return Ok(role);
        }

        [HttpGet("all")]
        public async Task<IActionResult> AllUsers()
        {
            var result = await userService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await userService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await userService.GetAllUserRolesAsync();
            return Ok(result);
        }

        [HttpPost("sync")]
        public async Task<IActionResult> SyncUsers()
        {
            var result = await userService.SyncUsersAsync();
            return Ok(result);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var result = await userService.GetByUsernameAsync(username);
            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditUser(UserView request)
        {
            var result = await userService.EditUserAsync(request);
            return Ok(result);
        }
    }
}
