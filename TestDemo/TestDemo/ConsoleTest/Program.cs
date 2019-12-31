
using Aaron.Common;
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
            var te = ConfigHelper.GetAppConfig("test");

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
