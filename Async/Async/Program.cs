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
            string rs = Test().Result;
            Console.WriteLine("满天风沙中");
            Console.ReadLine();
        }

        static async Task<string> Test()
        {
            Console.WriteLine("111");
            //加了await就会变成同步 执行顺序111- 继续走起 -333 如果不加就是 111-333-等待5秒再打印-继续走起
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
