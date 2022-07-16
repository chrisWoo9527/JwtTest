using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Sql.Data.Model
{
    public class User : IdentityUser<long>
    {
        // 登陆的一个版本号
        public long JwtVersion { get; set; }
    }
}
