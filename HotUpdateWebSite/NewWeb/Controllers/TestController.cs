using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IServices;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class TestController : Controller
    {
        IProductService _service;
        public TestController(IProductService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var name = _service.Test2();
            return Content(name);
        }

    }
}