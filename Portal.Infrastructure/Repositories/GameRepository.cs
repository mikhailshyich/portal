using Microsoft.EntityFrameworkCore;
using Portal.Domain.DTOs;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Data;

namespace Portal.Infrastructure.Repositories
{
    public class GameRepository : IGame
    {
        private readonly PortalDbContext context;

        public GameRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public async Task<Game> AddGameAsync(GameDTO request)
        {
            if (request is null)
                return null!;

            var game = new Game()
            {
                Name = request.Name,
                Description = request.Description,
            };

            await context.Games.AddAsync(game);
            await context.SaveChangesAsync();

            return game;
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await context.Games.AsNoTracking<Game>().ToListAsync();
        }

        public async Task<Game> GetGameByIdAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                return null!;
            }

            var game = await context.Games.FirstOrDefaultAsync(g => g.Id == id);
            if (game is null)
                return null!;

            return game;
        }
    }
}
