
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


using System.Collections;
using System.Globalization;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person2();
            Console.WriteLine(p.Name);


            Console.ReadLine();

        }
    }

    public class Person
    {
        public Person()
        {
            this.Name = "Test";
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public void Say()
        {
            Console.WriteLine(Name);
        }
    }

    public class Person2 : Person
    {
        public new string Name { get; set; }
    }

    public class MyComparer<T> : IEqualityComparer<T>
        where T : Person
    {
        public bool Equals(T x, T y)
        {
           return (x.Name == y.Name) && x.Name == "222";
         
        }

        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}

