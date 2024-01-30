namespace Music4Every1.Server
{
    public class UpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public UpdateService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Perform database update logic here
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                    var leilaosToUpdate = dbContext.Leiloes
                    .Where(l => l.Estado == "STARTED")
                    .ToList();

                    foreach (var leilao in leilaosToUpdate)
                    {
                        // Calculate the expected end date based on starting date and duration
                        var expectedEndDate = leilao.DataInicio.AddHours(leilao.Duracao);

                        // If the expected end date is in the past, update the status to "FINISHED"
                        if (expectedEndDate < DateTime.Now)
                        {
                            leilao.Estado = "FINISHED";
                            var vendedor = dbContext.Utilizadores.FirstOrDefault(u => u.Email == leilao.VendedorId);
                            vendedor.Saldo += leilao.PrecoAtual;
                        }
                    }

                    // Save changes to the database
                    await dbContext.SaveChangesAsync(stoppingToken);
                }

                // Wait for the specified interval before the next execution
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Adjust the interval as needed
            }
        }
    }
}
