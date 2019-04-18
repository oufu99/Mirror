using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aaron.OA.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index(int Pame)
        {
            var x = Request.RequestContext.RouteData.Values["id"];
            return View();
        }
    }
}