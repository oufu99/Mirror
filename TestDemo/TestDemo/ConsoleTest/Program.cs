
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
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var list = new List<Person>();
                list.Add(new Person() { Name = "Aaron" });
                var res = list.Where(c => c.Name == "123").ToList();
                Console.WriteLine(res);

            }
            catch (Exception ex)
            {

                throw;
            }
            Console.ReadLine();



        }




    }
}

