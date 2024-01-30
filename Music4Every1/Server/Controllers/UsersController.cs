using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Music4Every1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("wallet")]
        public async Task<ActionResult<double>> UpdateWallet(WalletDTO wallet)
        {
            double amount = double.Parse(wallet.Ammount);
            Request.Headers.TryGetValue("Authorization", out var token);
            var token_str = token.ToString().Replace("Bearer ", "").Trim();
            var claims = JwtParser.ParseClaimsFromJwt(token_str);
            string email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _context.Utilizadores.FindAsync(email);
            user.Saldo += amount;
            await _context.SaveChangesAsync();
            return Ok(user.Saldo);
        }

        [HttpPost("watchlist")]
        public async Task<ActionResult> AddWatchlist(WatchlistDTO watchlist)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            var token_str = token.ToString().Replace("Bearer ", "").Trim();
            var claims = JwtParser.ParseClaimsFromJwt(token_str);
            string email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            await _context.Watchlists.AddAsync(new Watchlist { AuctionId = watchlist.LeilaoId, UserId = email });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("watchlist/{leilaoId}")]
        public async Task<ActionResult> RemoveWatchlist(int leilaoId)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            var token_str = token.ToString().Replace("Bearer ", "").Trim();
            var claims = JwtParser.ParseClaimsFromJwt(token_str);
            string email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var watchlist = await _context.Watchlists.FirstOrDefaultAsync(x => x.AuctionId == leilaoId && x.UserId == email);
            _context.Watchlists.Remove(watchlist);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("watchlist/{leilaoId}")]
        public async Task<ActionResult> IsWatchlisted(int leilaoId)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            var token_str = token.ToString().Replace("Bearer ", "").Trim();
            var claims = JwtParser.ParseClaimsFromJwt(token_str);
            string email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var watchlist = await _context.Watchlists.FirstOrDefaultAsync(x => x.AuctionId == leilaoId && x.UserId == email);
            if (watchlist == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("wallet")]
        public async Task<ActionResult<double>> GetWallet()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            var token_str = token.ToString().Replace("Bearer ", "").Trim();
            var claims = JwtParser.ParseClaimsFromJwt(token_str);
            string email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _context.Utilizadores.FindAsync(email);
            return Ok(user.Saldo);
        }
    }
}
