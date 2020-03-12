using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Service
    {


        public static T Query<T>(Expression<Func<T, bool>> express)
             where T : class
        {
            var res = Repository.Query(express);
            return res;
        }

        public static IEnumerable<T> QueryList<T>(Expression<Func<T, bool>> express)
            where T : class
        {
            var res = Repository.QueryList(express);
            return res;
        }
        public static void Add<T>(T model)
             where T : class
        {
            Repository.Add(model);
        }
        public static void Update<T>(T model)
           where T : class
        {
            Repository.Update(model);
        }
        public static void Delete<T>(T model)
          where T : class
        {
            Repository.Delete(model);
        }
    }
}

