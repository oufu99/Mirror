using IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Person3 : IPerson
    {
        public void Say()
        {
            Console.WriteLine("Person3");
        }
    }
}
