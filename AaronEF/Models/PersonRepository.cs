using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PersonRepository
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public static void Insert<T>(T model)
            where T : class
        {
            using (AaronContext context = new AaronContext())
            {
                context.Set<T>().Add(model);
                context.SaveChanges();
            }
        }
        public static void InsertList<T>(List<T> list)
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
       
        public void Delete<T>(Expression<Func<T, bool>> express)
            where T : class
        {
            using (AaronContext context = new AaronContext())
            {
                T v = context.Set<T>().SingleOrDefault(express);
                if (v == null)
                {
                    return;
                }
                context.Set<T>().Remove(v);
                context.SaveChanges();
            }
        }
        public void DeleteList<T>(Expression<Func<T, bool>> express)
            where T : class
        {
            //后面我来实现他
            using (AaronContext context = new AaronContext())
            {
                var fun = new Func<Person, bool>(c => c.Age == 18);
                //context.Set<T>().Delete(express);
            }
        }


        public T Query<T>(Expression<Func<T, bool>> express) where T : class
        {
            using (AaronContext context = new AaronContext())
            {
                return context.Set<T>().SingleOrDefault(express);
            }
        }

        public IEnumerable<T> QueryList<T>(Expression<Func<T, bool>> express)
            where T : class
        {
            using (AaronContext context = new AaronContext())
            {
                return context.Set<T>().Where(express).ToList();
            }
        }

    }
}
