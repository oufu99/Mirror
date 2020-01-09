
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

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<string> list = new List<string>() { "123", "234" };
            list.Count();
            Console.ReadLine();

        }
    }
}

