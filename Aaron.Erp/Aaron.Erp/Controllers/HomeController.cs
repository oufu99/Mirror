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
        public HomeController(ILogHelper serService)
        {
            this.logService = serService;
        }

        [IgnoreFilter]
        public ActionResult Index(BaseModel model)
        {
            //通过IOC注入日志组件
            logService.Info("autofac就是吊s2");
            return View();
        }
    }
}

