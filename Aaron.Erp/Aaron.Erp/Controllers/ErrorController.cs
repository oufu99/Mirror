using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aaron.Erp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(ResponseModel model)
        {
            System.IO.File.AppendAllText(@"d:\jialin.txt", "进入eroor");
            ViewBag.Msg = model.Msg;
            return View();
        }
    }
}