using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music4Every1.Shared
{
    public class ItemDTO
    {
        public string Nome { get; set; } = string.Empty;    
        public string Categoria { get; set; } = string.Empty;
        public int LeilaoId { get; set; }
    }
}
