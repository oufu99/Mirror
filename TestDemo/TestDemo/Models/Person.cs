using IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Models
{

    public class MyPerson
    {
        public static void Func()
        {
            Thread.Sleep(2000);
            Console.WriteLine("执行完毕");
        }
    }


  
}
