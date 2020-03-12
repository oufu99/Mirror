using Aaron.Common;
using MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class TestController : Controller
    {

        [OTARoomStateAuthorizeAttribute]
        public ActionResult Index(List<string> list)
        {
            var t = new { list = new List<string>() { "111", "222" } };
            var json = JsonConvert.SerializeObject(t);

            HttpContext.Request.InputStream.Position = 0;
            var stream = HttpContext.Request.InputStream;
            StreamReader reader = new StreamReader(stream);
            var test = reader.ReadToEnd();
            return Content(test);
        }

    }

    public class OTARoomStateAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //var stream = filterContext.HttpContext.Request.InputStream;
            //StreamReader reader = new StreamReader(stream);
            //var test = reader.ReadToEnd();
            base.OnAuthorization(filterContext);
        }
    }


}