
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace Music4Every1.Client.Services.AuctionsService
{
    public class AuctionsService : IAuctionsService
    {
        private readonly HttpClient _http;

        public List<Leilao> Auctions { get; set; } = new List<Leilao>();
        public Boolean IsLoading { get; set; } = true;

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
            IsLoading = false;
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

        public async Task<int> CreateAuction (LeilaoCreateDTO leilao)
        {
            var result = await _http.PostAsJsonAsync("api/auctions/create", leilao);
            int id = await result.Content.ReadFromJsonAsync<int>();
            return id;

        }

        public async Task<LeilaoDetailsDTO> GetAuctionById (int id)
        {
            var result = await _http.GetFromJsonAsync<LeilaoDetailsDTO>($"api/auctions/{id}");
            return result;
        }

        public async Task UploadImages (MultipartFormDataContent files, int id)
        {
            await _http.PostAsync($"api/auctions/upload/{id}", files);
        }
    }
}
