using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitUser
{
    public class Person : IPerson
    {
        public void SayHi()
        {
            Console.WriteLine("hello world");
        }

        public void Fly()
        {
            Console.WriteLine("我膨胀了");
        }
    }
}
