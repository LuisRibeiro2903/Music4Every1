using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music4Every1.Shared
{
    public class Leilao
    {
        public int Id { get; set; }
        public Utilizador Vendedor { get; set; }
        public int VendedorId { get; set; }
        public Utilizador? Comprador { get; set; }
        public int? CompradorId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public TimeSpan Duracao { get; set; }
        public double PrecoAtual { get; set; }
        public double? PrecoCompraImediata { get; set; }
        public List<Item> Itens { get; set; } = new List<Item>();

        public TimeSpan TimeLeft ()
        {
            return DateTime.Now - DataInicio;
        }
    }
}
