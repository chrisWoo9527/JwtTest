using ASP.NetCore_Jwt.Dto.JwtDto;

namespace ASP.NetCore_Jwt.Dto.LoginDto
{
    public class LoginInputDto : JwtLoginInfo
    {
        public string PassWord { get; set;  }
    }
}
