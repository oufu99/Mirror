
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
            Console.WriteLine("主线程1");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            var task = Asy();
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("主线程2");

            Console.WriteLine("主线程3");

            Console.ReadLine();


        }


        static async Task<string> Asy()
        {
            Console.WriteLine("内部1");
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Run(() => {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(5000); });
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

