﻿namespace Music4Every1.Shared
{
    public class LeilaoCreateDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public int Duracao { get; set; }
        public double PrecoInicial { get; set; }
        public string Estado { get; set; } = string.Empty;  
        public double? PrecoCompraImediata { get; set; }
        public List<ItemDTO> Itens { get; set; } = new();
    }
}
