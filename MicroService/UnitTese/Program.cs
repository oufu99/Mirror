using System;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Test1<object>();
            Console.ReadLine();
        }

        public static void Test1<T>()
        {
            Console.WriteLine(111);
        }

        public static void Test1()
        {
            Console.WriteLine(222);
        }
    }
}
