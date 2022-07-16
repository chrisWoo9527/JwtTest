using ASP.NetCore_Jwt.Dto.JwtDto;
using ASP.NetCore_Jwt.Dto.LoginDto;
using Identity.Sql.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace ASP.NetCore_Jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IOptionsSnapshot<JwtOptions> optionsSnapshot;

        public LoginController(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptionsSnapshot<JwtOptions> optionsSnapshot)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.optionsSnapshot = optionsSnapshot;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginInputDto input)
        {
            User user = await userManager.FindByNameAsync(input.UserName);

            if (user == null)
            {
                return BadRequest("用户名或密码错误");
            }
            else
            {
                bool authorizes = await userManager.CheckPasswordAsync(user, input.PassWord);
                if (!authorizes)
                {
                    return BadRequest("用户名或密码错误");
                }
            }

            user.JwtVersion ++;
            IdentityResult identityResult = await userManager.UpdateAsync(user);
            if (!identityResult.Succeeded)
            {
                return BadRequest();
            }

            input.JwtVersion = user.JwtVersion;

            List<Claim> claims = new();
            Type type = input.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (var item in propertyInfos)
            {
                claims.Add(new Claim(item.Name, item.GetValue(input, null).ToString()));
            }

            byte[] secKeyBytes = Encoding.UTF8.GetBytes(optionsSnapshot.Value.SecKey);
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(secKeyBytes);
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey,
                        SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(claims: claims,
                        expires: DateTime.Now.AddSeconds(optionsSnapshot.Value.ExpireSeconds),
                        signingCredentials: signingCredentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return jwt;
        }
    }
}
