namespace Music4Every1.Client.Services.SingleAuctionService
{
    public interface ISingleAuctionService
    {
        Leilao Leilao { get; set; }
        Task GetAuction(int id);
        Task PlaceBid(Bid bid);
        Task GetBids(int id);
    }
}
