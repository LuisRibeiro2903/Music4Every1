
using System.Net.Http.Json;

namespace Music4Every1.Client.Services.UsersService
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _http;

        public UsersService(HttpClient http)
        {
            _http = http;
        }
        public async Task<double> UpdateWallet(string ammount)
        {
            await _http.PostAsJsonAsync("api/users/wallet", new WalletDTO { Ammount = ammount});
            return 10;
        }

        public async Task AddWatchlist(int leilaoId)
        {
            await _http.PostAsJsonAsync("api/users/watchlist", new WatchlistDTO { LeilaoId = leilaoId });
        }

        public async Task RemoveWatchlist(int leilaoId)
        {
            await _http.DeleteAsync($"api/users/watchlist/{leilaoId}");
        }

        public async Task<bool> IsWatchlisted(int leilaoId)
        {
            var result = await _http.GetAsync($"api/users/watchlist/{leilaoId}");
            return result.IsSuccessStatusCode;
        }
    }
}
