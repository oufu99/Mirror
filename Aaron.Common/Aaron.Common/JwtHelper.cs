using JWT;
using JWT.Algorithms;
using JWT.Serializers;
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
        public static string IssueJwt()
        {
            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow();
            //过期时间
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);

            var payload = new Dictionary<string, object>
{
        { "name", "MrBug" },
        {"exp",secondsSinceEpoch+100 },
        {"jti","luozhipeng" }
};
            var p = new Person() { Name = "Aaron", Age = 18 };


            Console.WriteLine(secondsSinceEpoch);

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(p, secret);
            return token;
        }

        public static void SerializeJWT(string token)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var json = decoder.Decode(token, secret, verify: true);//token为之前生成的字符串
                Console.WriteLine(json);
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
