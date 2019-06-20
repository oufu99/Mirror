using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<string> list = new List<string>() { "111", "2222", "333" };
            foreach (var item in list)
            {
                list.Remove(item);
            }
            Console.WriteLine("我要这铁棒有何用");
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.ReadLine();
        }

        async static void Fun()
        {
            Thread.Sleep(3000);
            Console.WriteLine("哦豁");
        }

        async static void Fun1()
        {
            Thread.Sleep(2000);
            Console.WriteLine("完蛋");
        }



        static async Task<string> Test()
        {
            Console.WriteLine("111");
            //加了await就会变成同步 执行顺序111- 继续走起 -333 如果不加就是 111-333-等待5秒再打印-继续走起
            return await Task.Run(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("继续走");
                return "完毕";
            });

        }

        static string Say()
        {

            return "World";


        }

    }

}
