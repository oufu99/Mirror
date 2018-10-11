using EFAndVue.DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFAndVue.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //using能及时释放资源,例如数据库连接异常，可以即使将上下文释放
            using (var db = new EFCodeFirstDbContext())
            {
                //Student stu = new Student("0002", "cuiyanwei", 25);
                //db.Students.Add(stu);
                //db.SaveChanges();
                //string name = db.Students.Select(p => p.Name).FirstOrDefault().ToString();


                Student stu1 = db.Students.Where(p => p.StuId == "0001").FirstOrDefault();
                stu1.Name = "CYW";
                db.SaveChanges();

                string name = db.Students.Select(p => p.Name).FirstOrDefault().ToString();

            }
            return View();
        }


    }
}