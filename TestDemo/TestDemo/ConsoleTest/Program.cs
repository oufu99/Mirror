
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


<<<<<<< HEAD
            Operate op = new Operate();
            op.UpdatePersonName("haha");
            op.PersonSay();
             
=======

            Test1 t = new Test1();

          




>>>>>>> 6f012f6380523815a43e761f03e574df607d123a
            Console.ReadLine();
        }



<<<<<<< HEAD
        public void TestFun()
        {
            int a = 1;
            Console.WriteLine(a);
=======
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

    public class Test1
    {
        static Test1()
        {
            Console.WriteLine("静态构造");
        }

        public Test1()
        {
            Console.WriteLine("普通构造");
        }
        public void Say2()
        {
        }
>>>>>>> 6f012f6380523815a43e761f03e574df607d123a

        }
    }




}
