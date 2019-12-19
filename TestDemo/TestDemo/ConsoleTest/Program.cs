
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
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
             

            Console.ReadLine();
        }




    }

    public class Person
    {
        public Person(string name)
        {

        }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
