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
        public string Titulo { get; set; } = string.Empty;
        public Utilizador Vendedor { get; set; }
        public string VendedorId { get; set; }
        public Utilizador? Comprador { get; set; }
        public string? CompradorId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public int Duracao { get; set; }
        public double PrecoInicial { get; set; }
        public double? PrecoCompraImediata { get; set; }
        public string Estado { get; set; } = string.Empty;  
        public List<Item> Itens { get; set; } = new List<Item>();
        public List<Imagem> Imagens { get; set; } = new List<Imagem>();

    }
}
