
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
using System.Collections;
using System.Globalization;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person() { Name = "123" };



            var p2 = JsonConvert.DeserializeObject<Person>(JsonConvert.SerializeObject(p));
            Console.WriteLine(p2.Name);
            p2.Name = "222";
            Console.WriteLine(p.Name);
            Console.ReadLine();

        }
    }
}

