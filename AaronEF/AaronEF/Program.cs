using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AaronEF
{
    class Program
    {
        static void Main(string[] args)
        {



            //var t = new Users() { Name = "测试添加", Age = 19 };

            AaronContext context = new AaronContext();
            //context.Set<Users>().Attach(t);
            //context.Entry<Users>(t).State = EntityState.Added;
            //context.SaveChanges();

            var model = context.Users.FirstOrDefault(c => c.Name == "测试添加");
            Console.WriteLine(model.Age);

            Console.ReadLine();
        }

    }
}