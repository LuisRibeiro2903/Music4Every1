using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Music4Every1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeilaoController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public LeilaoController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
