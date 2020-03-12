using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Repository
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public static void Add<T>(T model)
            where T : class
        {
            using (AaronContext context = new AaronContext())
            {
                context.Set<T>().Add(model);
                context.SaveChanges();
            }
        }
        public static void AddList<T>(List<T> list)
         where T : class
        {
            using (AaronContext context = new AaronContext())
            {
                context.Set<T>().AddRange(list);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// 修改某一模型数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public static void Update<T>(T model) where T : class
        {
            using (AaronContext context = new AaronContext())
            {
                if (context.Entry<T>(model).State == EntityState.Detached)
                {
                    context.Set<T>().Attach(model);
                    context.Entry<T>(model).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        public static void Delete<T>(T model)
            where T : class
        {
            //用循环,然后最后再SaveChange就可以了
            using (AaronContext context = new AaronContext())
            {
                context.Set<T>().Add(model);
                context.Entry<T>(model).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }


        public static T Query<T>(Expression<Func<T, bool>> express) where T : class
        {
            using (AaronContext context = new AaronContext())
            {
                return context.Set<T>().FirstOrDefault(express);
            }
        }

        public static IEnumerable<T> QueryList<T>(Expression<Func<T, bool>> express)
            where T : class
        {
            using (AaronContext context = new AaronContext())
            {
                return context.Set<T>().Where(express).ToList();
            }
        }

    }
}
