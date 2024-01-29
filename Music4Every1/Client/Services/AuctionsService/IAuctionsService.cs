namespace Music4Every1.Client.Services.AuctionsService
{
    public interface IAuctionsService
    {
        List<Leilao> Auctions { get; set; }
        public Boolean IsLoading { get; set; }

        Task GetAuctions();

        Task FilteredSearch(Filter search);

        Task<int> CreateAuction(LeilaoCreateDTO leilao);

        Task UploadImages(MultipartFormDataContent files, int id);
        
        Task<LeilaoDetailsDTO> GetAuctionById(int id);

    }
}
