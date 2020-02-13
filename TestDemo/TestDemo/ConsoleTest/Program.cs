
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

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD

            string rs = JsonConvert.DeserializeObject<string>("111");

            


            Console.ReadLine();

=======
<<<<<<< HEAD
=======
           
>>>>>>> 74bfa244d7cbff86913ba7973c9bdb2a49a10882
        }
>>>>>>> ad142855ee01b689ff9897edf695fae8234606cf


<<<<<<< HEAD
=======
        }
        public class Person
        {
            public string Name { get; set; }
            public bool CHECK { get; set; }
>>>>>>> 74bfa244d7cbff86913ba7973c9bdb2a49a10882


    }



    public class VersionAttribute : Attribute
    {
        public VersionAttribute()
        {
            Name = "Test3";
        }
        public string Name { get; set; }
        public string Data { get; set; }
        public string Describtion { get; set; }
    }


    public class Person
    {
        [Version]
        public string Name { get; set; }
    }
}

