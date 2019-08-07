using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCore.Model;
using WebCore.Services;

namespace WebCore.Controllers
{
    public class TestController : Controller
    {
        public Person _p;
        public TestController(Person p, Service ser)
        {
            _p = p;
            _p.Age = 19;

        }
        public IActionResult Index()
        {
            _p.Age = 19;
            return Content(_p.SayHi());
        }


        public IActionResult Index1(Service ser)
        {
            return Content(_p.GetAge().ToString());

        }
    }
}