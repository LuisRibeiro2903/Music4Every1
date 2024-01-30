namespace Music4Every1.Client.Services.UsersService
{
    public interface IUsersService
    {
        Task<double> UpdateWallet (string amount);

        Task AddWatchlist(int leilaoId);

        Task RemoveWatchlist(int leilaoId);

        Task<bool> IsWatchlisted(int leilaoId);

        Task<double> GetWallet();
    }
}
