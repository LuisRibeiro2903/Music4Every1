namespace Music4Every1.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utilizador>();
            modelBuilder.Entity<Leilao>().HasMany(l => l.Itens).WithOne(i => i.Leilao).HasForeignKey(i => i.LeilaoId);
            modelBuilder.Entity<Leilao>().HasMany(l => l.Imagens).WithOne(i => i.Leilao).HasForeignKey(i => i.LeilaoId);

            modelBuilder.Entity<Leilao>();

            modelBuilder.Entity<Item>();

            modelBuilder.Entity<Watchlist>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Watchlist>()
                .HasOne(w => w.Auction)
                .WithMany()
                .HasForeignKey(w => w.AuctionId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Utilizador> Utilizadores { get; set; }

        public DbSet<Leilao> Leiloes { get; set; }

        public DbSet<Imagem> Imagens { get; set; }

        public DbSet<Item> Itens { get; set; }

        public DbSet<Watchlist> Watchlists { get; set; }
    }
}
