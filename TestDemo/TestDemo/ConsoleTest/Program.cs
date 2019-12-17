
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
             

            //Person p1 = new Person() { Name = "111", Age = 18 };
            //Person p2 = new Person() { Name = "222", Age = 18 };
            //Person p3 = new Person() { Name = "333", Age = 18 };

            //var list = new List<Person>() { p1, p2, p3 };
            //var res = list.Distinct<Person, int>(b => b.Age).ToList();
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
