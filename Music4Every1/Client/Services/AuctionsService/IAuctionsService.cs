namespace Music4Every1.Client.Services.AuctionsService
{
    public interface IAuctionsService
    {
        List<Leilao> Auctions { get; set; }

        Task GetAuctions();

        Task FilteredSearch(Filter search);

    }
}
