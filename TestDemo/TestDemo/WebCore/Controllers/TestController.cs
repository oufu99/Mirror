using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return Content("你好呀");
        }


        public IActionResult Index1()
        {
            return View();
        }
    }
}