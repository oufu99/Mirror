
using Aaron.Common;
using ConsoleTest.Models;
using IModels;

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

using Models;
using Newtonsoft.Json;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {


            Operate op = new Operate();
            op.UpdatePersonName("haha");
            op.PersonSay();
             
            Console.ReadLine();
        }



        public void TestFun()
        {
            int a = 1;
            Console.WriteLine(a);

        }
    }




}
