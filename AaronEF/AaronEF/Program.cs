using Models;
using Services;
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
            var t = new Users() { Name = "测试添加", Age = 19 };
            Service.Add(t);
            var model = Service.Query<Users>(c => c.Name == "测试添加");
            Console.WriteLine(model.Name + "=====" + model.Age);

            Console.ReadLine();
        }

    }
}