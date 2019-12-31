
using Aaron.Common;
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
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Perosn p = new Student();

            p.wanju.Introduce();

            Console.ReadLine();
        }




    }

    public abstract class Perosn
    {
        internal Wanju wanju;
        public Perosn(string str)
        {
            Console.WriteLine("11111");
            wanju = new Wanju();
            wanju.Name = "玩具1";
        }

        public Perosn()
        {
            Console.WriteLine("333");
            wanju = new Wanju();
            wanju.Name = "玩具1";
        }
    }

    public class Wanju
    {
        public string Name { get; set; }
        public void Introduce()
        {
            Console.WriteLine(Name);
        }
    }

    public class Student : Perosn
    {
        public Student()
        {
            Console.WriteLine("222");

            wanju.Name = "玩具2";
        }
        public string Name { get; set; }
    }

}
