using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OORM
{
    using Models;
    using System.Configuration;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            //利用反射创建对象
            var nameSpace = ConfigurationManager.AppSettings["nameSpace"];
            var className = ConfigurationManager.AppSettings["className"];
            var classFullName = string.Format($"{nameSpace}.{className}");

            var fullAss = @"C:\MyDemo\Mirror\ORMDemo\Models\bin\Debug\" + nameSpace + ".dll";

            Assembly assembly = Assembly.LoadFile(fullAss); // 加载程序集（EXE 或 DLL） 
            var t = assembly.GetType(classFullName);
            var obj = Activator.CreateInstance(t);





            //赋值

            //传入ORM方法,反射出对象的属性,基于约定ID为主字段 

            Console.ReadLine();
        }
    }
}
