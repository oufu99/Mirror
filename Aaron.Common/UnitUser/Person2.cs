using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitUser
{
    public class Person2 : IPerson
    {
        public void SayHi()
        {
            Console.WriteLine("hello world  我是重载");
        }
    }
}
