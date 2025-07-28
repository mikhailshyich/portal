using Portal.Domain.DTOs;
using Portal.Domain.Entities;

namespace Portal.Domain.Interfaces
{
    public interface IGame
    {
        Task<List<Game>> GetAllGamesAsync();
        Task<Game> AddGameAsync(GameDTO game);
        Task<Game> GetGameByIdAsync(Guid id);
    }
}
