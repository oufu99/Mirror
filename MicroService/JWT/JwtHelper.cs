using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JWT
{
    public class JwtHelper
    {
        /// <summary>
        /// Gets or sets the secret key.
        /// </summary>
        /// <value>The secret key.</value>
        public static string secretKey { get; set; } = "a5fw7fsray45p34kch9lght3gdfms345t67kfs";

        /// <summary>
        /// 颁发 JWT 字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJWT(TokenModelJWT tokenModel)
        {
            var dateTime = DateTime.UtcNow;


            var claims = new Claim[]
                {
                    // 下边为 Claim 的默认配置
                new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Custid.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                // 这个就是过期时间，目前是过期 100 秒，可自定义，注意 JWT 有自己的缓冲过期时间
                 new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(100)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,"Aaron.Service"),
                new Claim(JwtRegisteredClaimNames.Aud,"wr"),
                // 这个 Role 是官方 UseAuthentication 要要验证的 Role，我们就不用手动设置 Role 这个属性了
                 new Claim(ClaimTypes.Role,tokenModel.Role),
                 new Claim(ClaimTypes.GroupSid,tokenModel.ManuId.ToString()),
                 new Claim(ClaimTypes.Surname,tokenModel.Username)
               };


            // 秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtHelper.secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: "ZP.YMT.Api",
                claims: claims,
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModelJWT SerializeJWT(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role = new object();
            object group = new object();
            object userName = new object();
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
                jwtToken.Payload.TryGetValue(ClaimTypes.GroupSid, out group);
                jwtToken.Payload.TryGetValue(ClaimTypes.Surname, out userName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModelJWT
            {
                Custid = int.Parse(jwtToken.Id),
                Role = role != null ? role.ToString() : "",
                ManuId = group != null ? int.Parse(group.ToString()) : 0,
                Username = userName != null ? userName.ToString() : ""
            };
            return tm;

        }
    }
    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelJWT
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Custid { get; set; }

        /// <summary>
        /// domain 二级域名
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }

        /// <summary>
        /// 厂商ID
        /// </summary>
        public int ManuId { get; set; }
    }
}
