using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreIOC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreIOC.Contrlloers
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
            return Content("11" + _student.Test());
        }
        public IActionResult Index1()
        {
            return View();
        }
    }
}