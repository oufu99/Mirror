using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        static void Main(string[] args)
        {
            //string str = Test().Result;
           string rs= Test().Result;
            Console.WriteLine("满天风沙中");
            Console.ReadLine();
        }

        static async Task<string> Test()
        {
            Console.WriteLine("111");
            await Task.Run(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("继续走起");
            });
            Console.WriteLine("333");
            return "你好吗";
        }
    }
}
