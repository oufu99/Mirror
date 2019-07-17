using Aaron.Common;
using Aaron.Erp.App_Start.Filter;
using Aaron.Models;
using Aaron.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aaron.Erp.Controllers
{
    public class HomeController : Controller
    {
        private ILogHelper logService;

        public static int Age = 1;
        public HomeController(ILogHelper serService)
        {
            logService = serService;
        }

        [IgnoreFilter]
        public ActionResult Index(BaseModel model)
        {
            logService.Info("haha");
            return View();
        }

        public string TestFunc(string name)
        {

            return name;
        }

        //该Index的Action加缓存
        [OutputCache(CacheProfile = "TestConfigCache")]
        public ActionResult Test()
        {
            ViewBag.CurrentTime = System.DateTime.Now;
            return View();
        }

        //该Action不加缓存
        public ActionResult Index1()
        {
            ViewBag.CurrentTime = System.DateTime.Now;
            return View();
        }
    }
}

