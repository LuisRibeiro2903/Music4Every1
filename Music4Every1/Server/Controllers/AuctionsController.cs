﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Music4Every1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly DataContext _context;

        public AuctionsController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Leilao>>> GetAuctions()
        {
            var results = await _context.Leiloes.ToListAsync();
            return Ok(results);
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
                query = query.Where(x => x.PrecoAtual >= search.PrecoMin);
            }
            if (search.PrecoMax != null)
            {
                query = query.Where(x => x.PrecoAtual <= search.PrecoMax);
            }
            var results = await query
                .Select(x => new Leilao
                {
                    Id = x.Id,
                    Descricao = x.Descricao,
                    DataInicio = x.DataInicio,
                    PrecoAtual = x.PrecoAtual,
                }).ToListAsync();
            return Ok(results);
        }
    }
}
