using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace EFAndVue.Controllers
{
    public class MovieController : Controller
    {
        public ActionResult Index()
        {
            //using能及时释放资源,例如数据库连接异常，可以即使将上下文释放
            using (var db = new MovieDbContext())
            {
                //var movie = new Movies();
                //movie.MovieId = 1;
                //movie.EpisodeID = 0;
                //movie.MovieName = "军师联盟";
                //movie.IsDelete = 1;
                //db.Movies.Add(movie);
                //var movie1 = db.Movies.Where(c => c.EpisodeID == 1).First();
                //movie1.FilePath = @"E:\迅雷下载\军师联盟\1.mp4";

                //db.SaveChanges();
            }
            return View();
        }
        public string LoadMovies(PageInfo page)
        {
            //using (var db = new MovieDbContext())
            //{
            //    var movies = db.Movies.Where(c => true).ToList();
            //    var Data = new { Data = movies };
            //    //return JsonConvert.SerializeObject(Data);
            //}



            var pageInfo = new { page = 1, recTotal = 1, recPerPage = 10 };

            //WsBg的外面是再封了一层Data,我们不需要
            var data = new List<dynamic>();
            data.Add(new { ID = "99", MovieId = "1", MovieName = "军师联盟", TotalJiShu = "40", Operation = "123" });
            var result = new { data = data, result = "success", message = "", pager = pageInfo };
            return JsonConvert.SerializeObject(result);

        }

        public ActionResult MovieDetail()
        {
            return View();
        }

    }
}