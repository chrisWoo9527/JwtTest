namespace ASP.NetCore_Jwt.Dto.JwtDto
{
    public class JwtLoginInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Jwt版本号
        /// </summary>
        public long JwtVersion { get; set; }
    }
}
