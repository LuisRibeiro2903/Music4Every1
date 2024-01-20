
using System.Net.Http.Json;

namespace Music4Every1.Client.Services.AuctionsService
{
    public class AuctionsService : IAuctionsService
    {
        private readonly HttpClient _http;

        public List<Leilao> Auctions { get; set; } = new List<Leilao>();

        public AuctionsService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetAuctions()
        {
            var result = await _http.GetFromJsonAsync<List<Leilao>>("api/auctions");
            if (result != null)
            {
                Auctions = result;
            }
        }

        public async Task FilteredSearch(Filter search)
        {
            var result = await _http.PostAsJsonAsync("api/auctions/search", search);
            await SetAuctions(result);
        }

        private async Task SetAuctions (HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Leilao>>();
            Auctions = response;
        }
    }
}
