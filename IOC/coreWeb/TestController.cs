using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreIOC
{
    public class TestController : Controller
    {
        public IStudent _student { get; set; }


        public TestController(IStudent student)
        {
            _student = student;
        }
        public IActionResult Index()
        {
            return Content("11");
        }
        public IActionResult Index1()
        {
            return View();
        }
    }
}
