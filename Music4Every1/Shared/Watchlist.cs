using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music4Every1.Shared
{
    public class Watchlist
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public Utilizador User { get; set; }
        public int AuctionId { get; set; }
        public Leilao Auction { get; set; }
    }
}
