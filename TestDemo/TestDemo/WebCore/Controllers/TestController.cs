using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Mvc;
using WebCore.Model;

namespace WebCore.Controllers
{
    public class TestController : Controller
    {
        public IProductService _service;
        public TestController(IProductService service)
        {
            _service = service;

        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Index1()
        {
            _service.Test();
            return Content("123木头人");

        }
    }
}