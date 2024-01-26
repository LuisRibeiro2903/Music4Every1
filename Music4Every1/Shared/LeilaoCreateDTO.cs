namespace Music4Every1.Shared
{
    public class LeilaoCreateDTO
    {
        public string Vendedor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public TimeSpan Duracao { get; set; }
        public double PrecoInicial { get; set; }
        public double? PrecoCompraImediata { get; set; }
        public List<Item> Itens { get; set; } = new List<Item>();
        public List<MultipartFormDataContent> Imagens = new();
    }
}
