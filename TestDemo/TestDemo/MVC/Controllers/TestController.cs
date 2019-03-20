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

            Person p = new Person();
            p.SayHi();
            return View();

        }
    }
}