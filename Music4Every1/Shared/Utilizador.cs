using System.ComponentModel.DataAnnotations;

namespace Music4Every1.Shared
{
    public class Utilizador
    {
        [Key]
        public string Email { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public double Saldo { get; set; }

    }
}