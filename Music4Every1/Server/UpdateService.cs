using System.Net;
using System.Net.Mail;
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
                    .Where(l => l.Estado != "FINISHED")
                    .ToList();

                    foreach (var leilao in leilaosToUpdate)
                    {
                        if(leilao.Estado == "STARTED")
                        {
                            // Calculate the expected end date based on starting date and duration
                            var expectedEndDate = leilao.DataInicio.AddMinutes(leilao.Duracao);

                            // If the expected end date is in the past, update the status to "FINISHED"
                            if (expectedEndDate < DateTime.Now)
                            {
                                leilao.Estado = "FINISHED";
                                var vendedor = dbContext.Utilizadores.FirstOrDefault(u => u.Email == leilao.VendedorId);
                                vendedor.Saldo += leilao.PrecoAtual;
                                using (MailMessage mail = new MailMessage())
                                {
                                    mail.From = new MailAddress("secomarmelo@gmail.com");
                                    mail.To.Add(vendedor.Email);
                                    mail.Subject = "Leilão terminado";
                                    mail.Body = "O seu leilão " + leilao.Titulo + " terminou com sucesso. Entre na sua conta para verificar como correu!";
                                    mail.IsBodyHtml = false;
                                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                    {
                                        smtp.Credentials = new NetworkCredential("secomarmelo@gmail.com", "woio mapm hyjt ebsr");
                                        smtp.EnableSsl = true;
                                        smtp.Send(mail);
                                    }
                                }
                                if (leilao.CompradorId != null)
                                {
                                    using (MailMessage mail = new MailMessage())
                                    {
                                        mail.From = new MailAddress("secomarmelo@gmail.com");
                                        mail.To.Add(leilao.CompradorId);
                                        mail.Subject = "Leilão terminado";
                                        mail.Body = "O leilão " + leilao.Titulo + " é seu!";
                                        mail.IsBodyHtml = false;
                                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                        {
                                            smtp.Credentials = new NetworkCredential("secomarmelo@gmail.com", "woio mapm hyjt ebsr");
                                            smtp.EnableSsl = true;
                                            smtp.Send(mail);
                                        }
                                    }
                                }
                            }
                        } 
                        else
                        {
                            if(leilao.DataInicio < DateTime.Now)
                            {
                                leilao.Estado = "STARTED";
                            }
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
