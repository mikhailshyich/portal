using Portal.Domain.DTOs;
using Portal.Domain.Entities;

namespace Portal.WEB.Services
{
    public class GameService : IGameService
    {
        private readonly HttpClient httpClient;

        public GameService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private readonly string BaseURI = "api/Game";

        public async Task<List<Game>> GetAllGamesAsync()
        {
            var game = await httpClient.GetAsync($"{BaseURI}");
            var response = await game.Content.ReadFromJsonAsync<List<Game>>();
            return response!;
        }

        public Task<Game> AddGameAsync(GameDTO game)
        {
            throw new NotImplementedException();
        }

        public async Task<Game> GetGameByIdAsync(Guid id)
        {
            var game = await httpClient.GetAsync($"{BaseURI}/{id}");
            var response = await game.Content.ReadFromJsonAsync<Game>();
            return response!;
        }

        public async Task<Game> EditGameAsync(Game request)
        {
            var game = await httpClient.PutAsJsonAsync($"{BaseURI}/edit", request);
            var response = await game.Content.ReadFromJsonAsync<Game>();
            return response!;
        }
    }
}
