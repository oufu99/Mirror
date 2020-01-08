
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
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            dic.Add("1001", new List<string>() { "2020-01-18", "2020-01-19", "2020-01-20" });
            dic.Add("1002", new List<string>() { "2020-01-18", "2020-01-20" });

            var js = JsonConvert.SerializeObject(dic);
            
            Console.ReadLine();

        }
    }
}

