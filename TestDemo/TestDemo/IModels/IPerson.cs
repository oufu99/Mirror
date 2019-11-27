using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IModels
{
    public interface IPerson
    {
        void Say();
    }

    public class Person1 : IPerson
    {
        public void Say()
        {
            Console.WriteLine("Person1");
        }
    }

    public class Person2 : IPerson
    {
        public void Say()
        {
            Console.WriteLine("Person2");
        }
    }
}
