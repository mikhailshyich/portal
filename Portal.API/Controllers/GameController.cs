using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Application.Services;
using Portal.Domain.DTOs;
using Portal.Domain.Entities;

namespace Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var games = await gameService.GetAllGamesAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var game = await gameService.GetGameByIdAsync(id);
            return Ok(game);
        }

        [HttpPost("add")]
        [Authorize(Roles = "asd")]
        public async Task<IActionResult> Add(GameDTO request)
        {
            var game = await gameService.AddGameAsync(request);
            return Ok(game);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(Game request)
        {
            var game = await gameService.EditGameAsync(request);
            return Ok(game);
        }
    }
}
