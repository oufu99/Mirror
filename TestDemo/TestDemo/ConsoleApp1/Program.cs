using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        #region 加了await
        //delegate string test();
        //static void Main(string[] args)
        //{

        //    //string resultstr = ts.EndInvoke(result);
        //    //Console.WriteLine(resultstr);

        //    TestDelegate();
        //    Console.WriteLine("i主线程" + Thread.CurrentThread.ManagedThreadId);

        //    Console.WriteLine(321);


        //    Console.ReadLine();
        //}

        //internal static async Task<string> TestDelegate()
        //{

        //    await Task.Run(() =>
        //    {
        //        Thread.Sleep(3000);
        //        Console.WriteLine("次线程" + Thread.CurrentThread.ManagedThreadId);
        //    });
        //    Console.WriteLine("次线程1" + Thread.CurrentThread.ManagedThreadId);
        //    return "hello";
        //}
        //static async void Async3()
        //{
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        Console.WriteLine("异步");

        //    }
        //}
        #endregion

        static void Main(string[] args)
        {
            Async3();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("主线程");
            }
            Console.WriteLine("同步完成");
            Console.ReadLine();
        }
        static async void Async3()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 30; i++)
                {
                    Console.WriteLine("异步");

                }
            });
            Console.WriteLine("异步完成");
        }
       
    }
}
