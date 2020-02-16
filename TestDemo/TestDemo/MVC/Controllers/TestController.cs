using Aaron.Common;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class TestController : Controller
    {


        public ActionResult Index(string str)
        {
            if (str=="111")
            {
                Thread.Sleep(3000);
            }
            return Content(str);

        }

         
    }
}