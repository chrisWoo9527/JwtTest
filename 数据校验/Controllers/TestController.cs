using Identity.Sql.Data;
using Identity.Sql.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace 数据校验.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly MirDbContext _context;

        public TestController(MirDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostDb(UserSomething input)
        {
            await _context.AddAsync(input);
            await _context.SaveChangesAsync();
            return "ok";
        }
    }
}
