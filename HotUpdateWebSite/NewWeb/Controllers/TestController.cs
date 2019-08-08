using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    //public class TestController : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }
    //}
    public class TestController : Controller
    {
        public IActionResult Index()
        {

            return Content("哈哈哈");
        }

    }
}