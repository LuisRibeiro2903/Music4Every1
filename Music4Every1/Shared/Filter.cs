using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music4Every1.Shared
{
    public class Filter
    {
        public string? Term { get; set; } = string.Empty;
        public int? PrecoMax { get; set; }
        public int? PrecoMin { get; set; }
        public string? Categoria { get; set; } = string.Empty;

    }
}
