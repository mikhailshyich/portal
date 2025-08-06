using Portal.Domain.DTOs;
using Portal.Domain.Entities;

namespace Portal.Application.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAllGamesAsync();
        Task<Game> AddGameAsync(GameDTO game);
        Task<Game> GetGameByIdAsync(Guid id);
        Task<Game> EditGameAsync(Game request);
    }
}
