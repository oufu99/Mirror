using Aaron.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aaron.Erp.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login()
        {
            //写入cookie
            CookieHelper.SetCookie("IsLogin", "1");
            var jwtJson = JwtHelper.IssueJwt(new Models.BaseModel() { ManuId = 10036, Name = "Aaron", UserId = 520 });

            CookieHelper.SetCookie("Authorization", jwtJson);
            return View();
        }
    }
}