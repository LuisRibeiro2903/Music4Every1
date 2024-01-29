using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music4Every1.Shared;
using System.Net;
using System.Security.Claims;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<LeilaoDetailsDTO>> GetAuctionById(int id)
        {
            var response = await _context.Leiloes.Include(l => l.Itens).Include(l => l.Imagens).FirstOrDefaultAsync(x => x.Id == id);
            LeilaoDetailsDTO result = new LeilaoDetailsDTO
            {
                Id = response.Id,
                VendedorId = response.VendedorId,
                Descricao = response.Descricao,
                DataInicio = response.DataInicio,
                Duracao = response.Duracao,
                PrecoInicial = response.PrecoInicial,
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

        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateAuction(LeilaoCreateDTO leilao)
        {
            Console.WriteLine("ola");
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

    }
}
