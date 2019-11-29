using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Models
{
    public interface IStudent
    {
        void Say();
    }



    public class Student : IStudent
    {
        public void Say()
        {
            Console.WriteLine("我是Student");
        }
    }
}
