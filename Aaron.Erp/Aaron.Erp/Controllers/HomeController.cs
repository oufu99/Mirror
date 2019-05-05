using Aaron.Common;
using Aaron.Erp.App_Start.Filter;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aaron.Erp.Controllers
{
    public class HomeController : Controller
    {
        [IgnoreFilter]
        public ActionResult Index(BaseModel model)
        {


            Log4Helper.InfoLog("执行中22...");
            Log4Helper.ErrorInfo("错误执行中22...");

            return View();
        }
    }
}

