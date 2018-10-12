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
                //Student stu = new Student(10, "aaron", 25, 1);
                //db.Students.Add(stu);
                //stu = new Student(2, "张三", 25, 1);
                //db.Students.Add(stu);

                //Teachers t = new Teachers();
                //t.Id = 1;
                //t.Name = "杨中科";
                //db.Teachers.Add(t);


                //var model = db.Students.Join(db.Teachers, m => m.TeacherId, c => c.Id, (m, c) => new { m.TeacherId, Phone = c.Name });



                //var model1 = db.Students.Join(model, m => m.TeacherId, c => c.TeacherId, (m, c) => new { m.TeacherId, c.Phone }).FirstOrDefault();
                //var phone = model1.Phone;      // db.SaveChanges();a

                var model = from c in db.Students
                            join t in db.Teachers
      on c.TeacherId equals t.Id
                            group c by new { id = c.TeacherId } into g
                            select new { g.Key, cnt = g.Count(), g.Key.id };
                var count = model.Count();
                var phone = model.FirstOrDefault();
            }
            return View();
        }


    }
}