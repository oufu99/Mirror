using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HotUpdateController : Controller
    {
        // GET: HotUpdate
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 是可以直接绑定的  我要做一个前台没有传参自动给他补上
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Bind(Person model)
        {

            return "ok";
        }

     


    }

  
}