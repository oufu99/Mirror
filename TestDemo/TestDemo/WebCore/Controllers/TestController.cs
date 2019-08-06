using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCore.Model;

namespace WebCore.Controllers
{
    public class TestController : Controller
    {
        public Person _p;
        public TestController(Person p)
        {
            _p = p;
        }
        public IActionResult Index()
        {
          
            return Content(_p.SayHi());
        }


        public IActionResult Index1()
        {
            return Content(_p.GetAge().ToString());

        }
    }
}