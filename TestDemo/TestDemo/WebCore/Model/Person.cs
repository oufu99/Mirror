using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Model
{
    public class Person
    {
        public int Age { get; set; }
        public string SayHi()
        {
            return "are you ok";
        }

        public int GetAge()
        {
            return Age;
        }

    }
}
