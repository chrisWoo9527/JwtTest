using ASP.NetCore_Jwt.Dto.JwtDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASP.NetCore_Jwt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IOptionsSnapshot<JwtOptions> _snapshot;

        public TestController(IOptions<JwtOptions> options, IOptionsSnapshot<JwtOptions> snapshot)
        {
            this._jwtOptions = options.Value;
            _snapshot = snapshot;
        }

        [HttpGet]
        public string test1()
        {
            return _jwtOptions.SecKey;
        }

        [Authorize]
        [CheckJwtVersion]
        [HttpGet]
        public string test2()
        {
            return _snapshot.Value.SecKey;
        }
    }
}
