using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC 
{

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Son MySon { get; set; }

    }

    public class Son
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}