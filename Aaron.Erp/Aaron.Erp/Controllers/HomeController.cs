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
        public HomeController()
        {
        }

        [IgnoreFilter]
        public ActionResult Index(BaseModel model)
        {
            //通过IOC注入日志组件
            return View();
        }





    }
}

