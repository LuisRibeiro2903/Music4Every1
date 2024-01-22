using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music4Every1.Shared
{
    public class Item
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;    
        public string Categoria { get; set; } = string.Empty;
        public Leilao Leilao { get; set; }
        public int LeilaoId { get; set; }

    }
}
