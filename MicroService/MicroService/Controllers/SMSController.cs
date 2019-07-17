using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        [HttpGet(nameof(QQSend))]
        public string QQSend([FromQuery]RequestModel model)
        {
            return model.Msg + "发送成功";
        }
        [HttpPost]
        public string QQSend1(RequestModel model)
        {
            return model.Msg + "发送成功";
        }

        [HttpGet(nameof(GetJWTStr))]
        public string GetJWTStr(string username, string password)
        {
            //接收参数验证帐号和密码的合法性
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
            TokenModelJWT tokeModel = new TokenModelJWT();
            tokeModel.Custid = 10010;
            tokeModel.Username = username;
            tokeModel.Role = "Admin";
            tokeModel.ManuId = 200000;
            var jwtStr = JwtHelper.IssueJWT(tokeModel);
            //return Newtonsoft.Json.JsonConvert.SerializeObject(jwtStr);
            return jwtStr;
        }

        [HttpPost(nameof(CheckJWTStr))]
        public string CheckJWTStr(string content)
        {
            var httpContext = HttpContext;
            // 检测是否包含'Authorization'请求头
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return "没有权限访问哦";
            }
            //var tokenHeader = httpContext.Request.Headers["Authorization"].ToString();
            var tokenHeader1 = httpContext.Request.Headers["Authorization"].ToString();
            var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            TokenModelJWT tm = JwtHelper.SerializeJWT(tokenHeader);
            // 授权
            //var claimList = new List<Claim>();
            //var claim = new Claim(ClaimTypes.Role, tm.Role);
            //claimList.Add(claim);
            //claimList.Add(new Claim(JwtRegisteredClaimNames.Jti, tm.Custid.ToString()));
            //var identity = new ClaimsIdentity(claimList);
            //var principal = new ClaimsPrincipal(identity);
            //httpContext.User = principal;
            System.IO.File.AppendAllText(@"d:\jialin.txt", "obj:" + JsonConvert.SerializeObject(tm));
            return $"您的账号是{tm.Username},密码是{tm.ManuId}";
        }
        [HttpPost(nameof(CheckTest))]
        public string CheckTest()
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return $"测试成功";
        }
    }
    public class RequestModel
    {

        public string Msg { get; set; }
    }
}