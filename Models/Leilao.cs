namespace Music4Every1.Models
{
    public class Leilao
    {
        public int Id { get; set; }
        public double PrecoInicial { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public TimeOnly Duracao { get; set; }
        public string Descricao { get; set; }
        public double? PrecoCompraImediata { get; set; }

        public Leilao()
        {
            Id = 0;
            PrecoInicial = 0;
            DataHoraInicio = DateTime.MinValue;
            Duracao = new TimeOnly();
            Descricao = string.Empty;
        }

        public Leilao(int id, double precoinicial, DateTime datahorainicio, TimeOnly duracao, string descricao)
        {
            Id = id;
            PrecoInicial = precoinicial;
            DataHoraInicio = datahorainicio;
            Duracao = duracao;
            Descricao = descricao;
        }

        public Leilao(int id, double precoinicial, DateTime datahorainicio, TimeOnly duracao, string descricao, double precocompraimediata)
        {
            Id = id;
            PrecoInicial = precoinicial;
            DataHoraInicio = datahorainicio;
            Duracao = duracao;
            Descricao = descricao;
            PrecoCompraImediata = precocompraimediata;
        }
    }
}

