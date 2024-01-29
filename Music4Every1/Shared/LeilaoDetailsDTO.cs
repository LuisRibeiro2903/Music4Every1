using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music4Every1.Shared
{
    public class LeilaoDetailsDTO
    {
        public int Id { get; set; }
        public string VendedorId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public int Duracao { get; set; }
        public double PrecoInicial { get; set; }
        public double? PrecoCompraImediata { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
