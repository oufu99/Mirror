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
        
        public TestController()
        {
          

        }
        public IActionResult Index()
        {
            return View();
        }


    }
}