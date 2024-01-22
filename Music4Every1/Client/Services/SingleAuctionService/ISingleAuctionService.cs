namespace Music4Every1.Client.Services.SingleAuctionService
{
    public interface ISingleAuctionService
    {
        Leilao? Auction { get; set; }
        Task GetAuction(int id);
        Task PlaceBid(Bid bid);
    }
}
