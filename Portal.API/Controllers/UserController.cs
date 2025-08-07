using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;

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

        [Authorize(Roles = "admin")]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO request)
        {
            if (request is null)
            {
                return BadRequest("пользователь null");
            }
            var user = await userService.RegisterAsync(request);
            if (user.Flag == false)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO request)
        {
            if (request is null)
                return BadRequest("Заполните обязательные поля.");

            var user = await userService.LoginAsync(request);
            if (user is null)
            {
                return BadRequest("Проверьте введённые данные.");
            }

            return Ok(user);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("addRole")]
        public async Task<ActionResult> AddUserRole(UserRoleDTO request)
        {
            if (request is null)
                return BadRequest("Заполните обязательные поля.");

            var role = await userService.AddRoleAsync(request);

            return Ok(role);
        }
    }
}
