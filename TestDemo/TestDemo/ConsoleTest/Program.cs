
using Aaron.Common;
using ConsoleTest.Models;
using IModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Models;
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



            Test1 t = new Test1();

            var res = t?.Say2();
 



            Console.ReadLine();
        }


         
    }

    interface ITest
    {

        string Name { get; set; }
        void Say();
        void Say1();
    }

    public abstract class Test : ITest
    {
        public string MyProperty { get; set; }
        private string _name;
        public string Name
        { get => "33"; set => throw new NotImplementedException(); }


        public abstract void Say();
        public void Say1() { }
    }

    public class Test1 : Test 
    {
        public override void Say()
        {
            Console.WriteLine(Name);
        }
        public string Say2()
        {
            return "333";
        }

    }




}
