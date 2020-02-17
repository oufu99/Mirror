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
            try
            {
                if (str == "Test")
                {
                    throw new Exception("服务器内部coco");
                }
                return Content(str);
            }
            catch (Exception ex)
            {

                return Content("我捕获到了");
            }
            
        }


    }
}