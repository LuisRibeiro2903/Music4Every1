using Music4Every1.Client.Pages;
using static System.Net.WebRequestMethods;
using System.Net.Http.Json;

namespace Music4Every1.Client.Services.SingleAuctionService
{
    public class SingleAuctionService : ISingleAuctionService
    {
        private readonly HttpClient _http;
        public Leilao? Auction { get; set; }
        public Boolean IsLoading { get; set; } = true;
        
        public SingleAuctionService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetAuction(int id)
        {
            Auction = await _http.GetFromJsonAsync<Leilao>($"api/auctions/{id}");
            IsLoading = false;
        }
        public async Task PlaceBid(Bid bid)
        {
            if (Auction != null)
            {
                var result= _http.PostAsJsonAsync($"api/auctions/{Auction.Id}/bid", bid);
                if (result!= null)
                {
                    Console.WriteLine("Licitação submetida.");
                    return;
                }
            }
            Console.WriteLine("Licitação não submetida.");
        }
    }
}