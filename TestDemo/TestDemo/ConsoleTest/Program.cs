
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
            Person p = new Person2("aaa", "bbb");
            p.Say("test");
            p = new Person3();
            p.Say("test");
            Console.ReadLine();

        }
    }



    public abstract class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public abstract void Say(string str);

        public void Func()
        {
            Console.WriteLine("哈哈");
        }

    }

    public class Person2 : Person
    {
        string a;
        string b;
        public Person2(string s, string s2)
        {
            a = s;
            b = s2;
        }
        public override void Say(string str)
        {
            Console.WriteLine(a + "===" + b);
        }
    }
    public class Person3 : Person
    {

        public override void Say(string str)
        {
            Console.WriteLine("我只是一个弟弟");
        }
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

