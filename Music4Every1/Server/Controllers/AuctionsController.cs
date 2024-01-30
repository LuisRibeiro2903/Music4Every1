using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music4Every1.Shared;
using System.Net;
using System.Security.Claims;
using System.IO;
using System.Net.Mail;

namespace Music4Every1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public AuctionsController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        [HttpGet]
        public async Task<ActionResult<List<Leilao>>> GetAuctions()
        {
            var results = await _context.Leiloes.ToListAsync();
            return Ok(results);
        }

        [HttpGet("watchlist")]
        public async Task<ActionResult<List<Leilao>>> GetAuctionsWatchlist()
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            var token_str = token.ToString().Replace("Bearer ", "").Trim();
            var claims = JwtParser.ParseClaimsFromJwt(token_str);
            string email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var results = await _context.Watchlists.Include(x => x.Auction).Where(x => x.UserId == email).Select(x => x.Auction).ToListAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeilaoDetailsDTO>> GetAuctionById(int id)
        {
            var response = await _context.Leiloes.Include(l => l.Itens).Include(l => l.Imagens).FirstOrDefaultAsync(x => x.Id == id);
            LeilaoDetailsDTO result = new LeilaoDetailsDTO
            {
                Id = response.Id,
                Titulo = response.Titulo,
                VendedorId = response.VendedorId,
                Descricao = response.Descricao,
                DataInicio = response.DataInicio,
                Duracao = response.Duracao,
                PrecoInicial = response.PrecoInicial,
                PrecoAtual = response.PrecoAtual,
                PrecoCompraImediata = response.PrecoCompraImediata,
                Estado = response.Estado
            };
            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<Leilao>>> FilteredSearch(Filter search)
        {
            IQueryable<Leilao> query = _context.Leiloes.Include(l => l.Itens);
            if (!string.IsNullOrEmpty(search.Term))
            {
                query = query.Where(x => x.Descricao.Contains(search.Term));
            }
            if (!string.IsNullOrEmpty(search.Categoria))
            {
                query = query.Where(x => x.Itens.Any(item => item.Categoria.Equals(search.Categoria)));
            }
            if (search.PrecoMin != null)
            {
                query = query.Where(x => x.PrecoInicial >= search.PrecoMin);
            }
            if (search.PrecoMax != null)
            {
                query = query.Where(x => x.PrecoInicial <= search.PrecoMax);
            }
            var results = await query
                .Select(x => new Leilao
                {
                    Id = x.Id,
                    Descricao = x.Descricao,
                    DataInicio = x.DataInicio,
                    PrecoInicial = x.PrecoInicial,
                }).ToListAsync();
            return Ok(results);
        }

        [HttpPost("watchlist/filter")]
        public async Task<ActionResult<List<Leilao>>> FilteredSearchWatchlist(Filter search)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            var token_str = token.ToString().Replace("Bearer ", "").Trim();
            var claims = JwtParser.ParseClaimsFromJwt(token_str);
            string email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            IQueryable<Leilao> query = _context.Watchlists.Include(x => x.Auction).ThenInclude(l => l.Itens).Where(x => x.UserId == email).Select(x => x.Auction);
            if (!string.IsNullOrEmpty(search.Term))
            {
                query = query.Where(x => x.Descricao.Contains(search.Term) || x.Titulo.Contains(search.Term) || x.Itens.Any(item => item.Nome.Contains(search.Term)));
            }
            if (!string.IsNullOrEmpty(search.Categoria))
            {
                query = query.Where(x => x.Itens.Any(item => item.Categoria.Equals(search.Categoria)));
            }
            if (search.PrecoMin != null)
            {
                query = query.Where(x => x.PrecoInicial >= search.PrecoMin);
            }
            if (search.PrecoMax != null)
            {
                query = query.Where(x => x.PrecoInicial <= search.PrecoMax);
            }
            var results = await query
                .Select(x => new Leilao
                {
                    Id = x.Id,
                    Descricao = x.Descricao,
                    DataInicio = x.DataInicio,
                    PrecoInicial = x.PrecoInicial,
                }).ToListAsync();
            return Ok(results);
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateAuction(LeilaoCreateDTO leilao)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            var token_str = token.ToString().Replace("Bearer ", "").Trim();
            var claims = JwtParser.ParseClaimsFromJwt(token_str);
            string email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            Leilao res = new Leilao
            {
                VendedorId = email,
                Titulo = leilao.Titulo,
                Descricao = leilao.Descricao,
                DataInicio = leilao.DataInicio,
                Duracao = leilao.Duracao,
                Estado = leilao.Estado,
                PrecoInicial = leilao.PrecoInicial,
                PrecoCompraImediata = leilao.PrecoCompraImediata,
            };
            var result = await _context.Leiloes.AddAsync(res);
            await _context.SaveChangesAsync();
            int id = result.Entity.Id;
            foreach(var item in leilao.Itens)
            {
                await _context.Itens.AddAsync(new Item { Categoria = item.Categoria, Nome = item.Nome, LeilaoId = id});
            }
            await _context.SaveChangesAsync();
            return Ok(id);

        }

        [HttpPost("upload/{id}")]
        public async Task<ActionResult> UploadImages(int id, List<IFormFile> files)
        {
            foreach (var file in files)
            { 
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);
                trustedFileNameForFileStorage = Path.GetRandomFileName();
                var path = Path.Combine(_env.ContentRootPath, "uploads", "images", trustedFileNameForFileStorage);

                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);
                await _context.Imagens.AddAsync(new Imagem { LeilaoId = id,FileName= untrustedFileName  ,StoredFileName = trustedFileNameForFileStorage , ContentType = file.ContentType});
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("bid")]
        public async Task<ActionResult> PlaceBid(Bid bid)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            var token_str = token.ToString().Replace("Bearer ", "").Trim();
            var claims = JwtParser.ParseClaimsFromJwt(token_str);
            string email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var auction = await _context.Leiloes.Include(x => x.Itens).FirstOrDefaultAsync(x => x.Id == bid.LeilaoId);
            var possivel_comprador = await _context.Utilizadores.FirstOrDefaultAsync(x => x.Email == email); 
            var comprador_atual = await _context.Utilizadores.FirstOrDefaultAsync(x => x.Email == auction.CompradorId);
            if (auction == null)
            {
                return BadRequest();
            }
            if (auction.VendedorId == email)
            {
                return BadRequest();
            }
            if (auction.PrecoAtual >= bid.Ammount)
            {
                return BadRequest();
            }
            if (possivel_comprador.Saldo < bid.Ammount)
            {
                return BadRequest();
            }
            if (comprador_atual != null)
            {
                comprador_atual.Saldo += auction.PrecoAtual;
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("secomarmelo@gmail.com");
                    mail.To.Add(comprador_atual.Email);
                    mail.Subject = "Licitação ultrapassada";
                    mail.Body = "A sua licitação no leilão " + auction.Titulo + " foi ultrapassada! Volte rápido para reaver o seu item!";
                    mail.IsBodyHtml = false;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("secomarmelo@gmail.com", "woio mapm hyjt ebsr");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            var watchlist = _context.Watchlists.FirstOrDefault(x => x.AuctionId == auction.Id && x.UserId == email);
            if (watchlist == null)
            {
                await _context.Watchlists.AddAsync(new Watchlist { AuctionId = auction.Id, UserId = email });
            }
            auction.PrecoAtual = bid.Ammount;
            auction.CompradorId = email;
            possivel_comprador.Saldo -= bid.Ammount;
            var time_left = auction.DataInicio.AddMinutes(auction.Duracao) - DateTime.Now;
            if(time_left.TotalMinutes < 5)
            {
                auction.Duracao += 5;
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("images/{id}")]
        public async Task<ActionResult<List<string>>> GetImages(int id)
        {
            var results = await _context.Imagens.Where(x => x.LeilaoId == id).Select(x => x.StoredFileName).ToListAsync();
            List<string> returns = new List<string>();
            foreach(var result in results)
            {
                var path = Path.Combine(_env.ContentRootPath, "uploads", "images", result);
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    byte[] imageBytes;
                    imageBytes = new byte[fileStream.Length];
                    fileStream.Read(imageBytes, 0, (int)fileStream.Length);
                    returns.Add(Convert.ToBase64String(imageBytes));
                }

            }
            return Ok(returns);
        }
    }
}
