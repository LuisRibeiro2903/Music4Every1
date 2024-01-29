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
                new Utilizador {Nome = "João",Email = "joao@gmail.com" ,PasswordHash = System.Text.Encoding.UTF8.GetBytes("123"),  PasswordSalt = System.Text.Encoding.UTF8.GetBytes("123"), Saldo = 1000 },
                new Utilizador {Nome = "Maria",Email = "maria@gmail.com", PasswordHash = System.Text.Encoding.UTF8.GetBytes("123"), PasswordSalt = System.Text.Encoding.UTF8.GetBytes("123"), Saldo = 1000 },
                new Utilizador {Nome = "José",Email = "jose@gmail.com" ,PasswordHash = System.Text.Encoding.UTF8.GetBytes("123"), PasswordSalt = System.Text.Encoding.UTF8.GetBytes("123"), Saldo = 1000 },
                new Utilizador {Nome = "Ana",Email = "ana@gmail.com", PasswordHash = System.Text.Encoding.UTF8.GetBytes("123"), PasswordSalt = System.Text.Encoding.UTF8.GetBytes("123"), Saldo = 1000 },
                new Utilizador {Nome = "Carlos", Email = "carlos@gmail.com", PasswordHash = System.Text.Encoding.UTF8.GetBytes("123"), PasswordSalt = System.Text.Encoding.UTF8.GetBytes("123"), Saldo = 1000 }
            );
            modelBuilder.Entity<Leilao>().HasMany(l => l.Itens).WithOne(i => i.Leilao).HasForeignKey(i => i.LeilaoId);
            modelBuilder.Entity<Leilao>().HasMany(l => l.Imagens).WithOne(i => i.Leilao).HasForeignKey(i => i.LeilaoId);

            modelBuilder.Entity<Leilao>().HasData(
                new Leilao { Id = 1, VendedorId = "joao@gmail.com", Estado = "FINISHED" ,Descricao = "Guitarra", DataInicio = new DateTime(2021, 1, 1), Duracao = 1, PrecoInicial = 100, PrecoCompraImediata = 200 },
                new Leilao { Id = 2, VendedorId = "maria@gmail.com", Estado = "FINISHED" ,Descricao = "Bateria", DataInicio = new DateTime(2021, 1, 1), Duracao = 1, PrecoInicial = 100, PrecoCompraImediata = 200 },
                new Leilao { Id = 3, VendedorId = "jose@gmail.com", Estado = "FINISHED" ,Descricao = "Piano", DataInicio = new DateTime(2021, 1, 1), Duracao = 1, PrecoInicial = 100, PrecoCompraImediata = 200 },
                new Leilao { Id = 4, VendedorId = "ana@gmail.com", Estado = "FINISHED" ,Descricao = "Violino", DataInicio = new DateTime(2021, 1, 1), Duracao = 1, PrecoInicial = 100, PrecoCompraImediata = 200 },
                new Leilao { Id = 5, VendedorId = "carlos@gmail.com", Estado = "FINISHED" ,Descricao = "Saxofone", DataInicio = new DateTime(2021, 1, 1), Duracao = 1, PrecoInicial = 100, PrecoCompraImediata = 200 }
            );

            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Nome = "Guitarra Elétrica", Categoria = "electric", LeilaoId = 1 },
                new Item { Id = 2, Nome = "Bateria", Categoria = "percussion", LeilaoId = 2 },
                new Item { Id = 3, Nome = "Piano", Categoria = "strings", LeilaoId = 3 },
                new Item { Id = 4, Nome = "Violino", Categoria = "strings", LeilaoId = 4 },
                new Item { Id = 5, Nome = "Saxofone", Categoria = "wind", LeilaoId = 5 }
            );

        }

        public DbSet<Utilizador> Utilizadores { get; set; }

        public DbSet<Leilao> Leiloes { get; set; }

        public DbSet<Imagem> Imagens { get; set; }
    }
}
