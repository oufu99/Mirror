using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    public class JwtHelper
    {
        public static string secret = "Aaron{520}$345qwer";
        public static string IssueJwt(BaseModel model)
        {
            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow();
            //过期时间
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds) + 100;

            //把用户的基本信息填到jwt荷载中,后面要具体的信息再去数据库获取
            var userJson = JsonConvert.SerializeObject(model);
            var payload = new Dictionary<string, object>
{
        { "userInfo",userJson },
        {"exp",secondsSinceEpoch }

};
            Console.WriteLine(secondsSinceEpoch);

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            return token;
        }

        /// <summary>
        /// 解密jwt 返回json字符串,自己再去解析成想要的数据
        /// </summary>
        /// <param name="token">前面的Issue颁发的jwt字符串</param>
        /// <returns></returns>
        public static string SerializeJWT(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            var json = decoder.Decode(token, secret, verify: true);
            return json;
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
