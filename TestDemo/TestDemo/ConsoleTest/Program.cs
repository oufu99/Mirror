using IModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IStudent s = new Student();
            s.Say();

            Console.ReadLine();
        }
    }

    public interface IStudent
    {
        void Say();
    }


    public abstract class StudentBase : IStudent
    {
        public void Say()
        {
            Console.WriteLine("我是base");
        }
    }

    public class Student : StudentBase
    {
        public void Say()
        {
            Console.WriteLine("我是Student");
        }
    }

}
