using Aaron.Common;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class TestController : Controller
    {


        public ActionResult Index()
        {

            return View();

        }


        public ActionResult Index2(Person list)
        {

            Person p = new Person();
            return Content("suc");

        }
    }
}