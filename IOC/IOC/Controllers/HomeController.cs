using IOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOC.Controllers
{
    public class HomeController : Controller
    {
        private IStudent _student;
        private ITeacher SmallTeach;


        public HomeController(IStudent student, ITeacher teacher)
        {
            _student = student;
            SmallTeach = teacher;
        }

        public ActionResult Index()
        {
            return Content(SmallTeach.Test());
        }
    }
}