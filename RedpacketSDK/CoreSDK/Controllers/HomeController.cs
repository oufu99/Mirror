using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZP.Framework.Pay.SDK;

namespace CoreSDK.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string SendRedpacket()
        {
            //System.IO.File.AppendAllText(@"d:\jialin.txt", "开始");
            //RedpacketSDKModel model = new RedpacketSDKModel();
            //model.AppId = "wx64f8d4739ddd0818";
            //model.ActivityName = "测试活动";
            //model.MchId = "1497305122";
            //model.Money = 0.34M;
            //model.OrderNum = "121451420";
            //model.OpenID = "oVmZN1l7536LZ8WaBcDXHNr07keM";
            //model.Secret = "wx64f8d4739ddd0818wx64f8d4739ddd";
            //model.SenderName = "正品科技";
            //model.Wishing = "恭喜你发财";
            //model.Remark = "活动备注";
            //model.CertCustId = "200063";
            //var res = PaySDK.SendRedPackage("http://PayApi.onlyId.cn", model);
            //return res;


            var url = "";
            return url;
        }
    }
}
