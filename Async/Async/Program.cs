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

            Fun();
            Fun1();
            Console.ReadLine();
        }

        async static void Fun()
        {
            await Task.Run(() => { Thread.Sleep(3000); Console.WriteLine("异步中.."); });
            Console.WriteLine("哦豁");
        }

        static void Fun1()
        {
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
