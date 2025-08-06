using Portal.Domain.DTOs;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;

namespace Portal.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGame gameInterface;

        public GameService(IGame gameInterface)
        {
            this.gameInterface = gameInterface;
        }


        public async Task<Game> AddGameAsync(GameDTO game)
        {
            return await gameInterface.AddGameAsync(game);
        }

        public async Task<Game> EditGameAsync(Game request)
        {
            return await gameInterface.EditGameAsync(request);
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await gameInterface.GetAllGamesAsync();
        }

        public async Task<Game> GetGameByIdAsync(Guid id)
        {
            return await gameInterface.GetGameByIdAsync(id);
        }
    }
}
