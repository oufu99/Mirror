
using ConsoleTest.Models;
using IModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Newtonsoft.Json;
using SendLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var total = 120000000;
            //Action action = new Action(Test);
            //Task.Run(action);

            Console.WriteLine("主线程执行！");
            ThreadPool.SetMinThreads(1, 5);
            ThreadPool.SetMaxThreads(5, 5);
            for (int i = 1; i <= total; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Test), i.ToString());
            }

            //var i = 0;
            //while (true)
            //{
            //    i++;
            //    ThreadPool.QueueUserWorkItem(new WaitCallback(Test), i.ToString());
            //}
            Console.WriteLine("主线程结束！");

            
            Console.ReadLine();
        }


        public static void Test(object obj)
        {

            var list = new List<Person>();
            for (int i = 0; i < 100000; i++)
            {

                list.Add(new Person() { Name = "Test" });
            }
            Console.WriteLine("完成了" + obj.ToString());
            //System.IO.File.AppendAllText(@"d:\jialin.txt", "完成了" + obj.ToString());



        }

    }


}
