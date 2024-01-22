namespace Music4Every1.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utilizador>().HasData(
                new Utilizador { Id = 1, Nome = "João", Password = "123", Saldo = 1000 },
                new Utilizador { Id = 2, Nome = "Maria", Password = "123", Saldo = 1000 },
                new Utilizador { Id = 3, Nome = "José", Password = "123", Saldo = 1000 },
                new Utilizador { Id = 4, Nome = "Ana", Password = "123", Saldo = 1000 },
                new Utilizador { Id = 5, Nome = "Carlos", Password = "123", Saldo = 1000 }
            );
            modelBuilder.Entity<Leilao>().HasMany(l => l.Itens).WithOne(i => i.Leilao).HasForeignKey(i => i.LeilaoId);

            modelBuilder.Entity<Leilao>().HasData(
                new Leilao { Id = 1, VendedorId = 1, Descricao = "Guitarra", DataInicio = new DateTime(2021, 1, 1), Duracao = new TimeSpan(1, 0, 0, 0), PrecoAtual = 100, PrecoCompraImediata = 200 },
                new Leilao { Id = 2, VendedorId = 2, Descricao = "Bateria", DataInicio = new DateTime(2021, 1, 1), Duracao = new TimeSpan(1, 0, 0, 0), PrecoAtual = 100, PrecoCompraImediata = 200 },
                new Leilao { Id = 3, VendedorId = 3, Descricao = "Piano", DataInicio = new DateTime(2021, 1, 1), Duracao = new TimeSpan(1, 0, 0, 0), PrecoAtual = 100, PrecoCompraImediata = 200 },
                new Leilao { Id = 4, VendedorId = 4, Descricao = "Violino", DataInicio = new DateTime(2021, 1, 1), Duracao = new TimeSpan(1, 0, 0, 0), PrecoAtual = 100, PrecoCompraImediata = 200 },
                new Leilao { Id = 5, VendedorId = 5, Descricao = "Saxofone", DataInicio = new DateTime(2021, 1, 1), Duracao = new TimeSpan(1, 0, 0, 0), PrecoAtual = 100, PrecoCompraImediata = 200 }
            );

            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Nome = "Guitarra Elétrica", Categoria = "Cordas", LeilaoId = 1 },
                new Item { Id = 2, Nome = "Bateria", Categoria = "Percussão", LeilaoId = 2 },
                new Item { Id = 3, Nome = "Piano", Categoria = "Teclas", LeilaoId = 3 },
                new Item { Id = 4, Nome = "Violino", Categoria = "Cordas", LeilaoId = 4 },
                new Item { Id = 5, Nome = "Saxofone", Categoria = "Sopro", LeilaoId = 5 }
            );
        }

        public DbSet<Utilizador> Utilizadores { get; set; }

        public DbSet<Leilao> Leiloes { get; set; }

    }
}
