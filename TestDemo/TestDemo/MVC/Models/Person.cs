using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Person
    {

        [ActionTrack]
        public void SayHi()
        {
            Console.WriteLine("11");

        }
    }
}