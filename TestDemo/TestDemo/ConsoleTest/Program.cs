
using Aaron.Common;
using IModels;

using SendLib;

using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using System.Collections;
using System.Globalization;
using Newtonsoft.Json;
using System;
using ConsoleTest.Models;

namespace ConsoleTest
{
    delegate void WriteLineDelegate(string line);

    class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine(ConfigHelper.GetAppConfig("test"));
            Console.ReadLine();


        }


        static async Task<string> Asy()
        {
            Console.WriteLine("内部1");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            try
            {
                await Task.Run(() => {
                    throw new Exception("发生错误");
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(5000);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                 
            }
            
            Console.WriteLine("内部2");
            await Task.Run(() => {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(5000); });
            Console.WriteLine("内部3");

            return "99";
        }


        static async Task<string> Asy2()
        {
            Console.WriteLine("内部11");

            await Task.Run(() => { Thread.Sleep(5000); });
            Console.WriteLine("内部22");
            return "99";
        }


    }
}

