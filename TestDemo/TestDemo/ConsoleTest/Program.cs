using CommonServiceLocator;
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
            IServiceLocator locator = new MockServiceLocator(new object[] { new Student(),
                                             new NullReferenceException() });

            //IStudent instance = locator.GetInstance<IStudent>();
            //instance.Say();

            ServiceLocatorProvider provider = new ServiceLocatorProvider(() => locator);

            ServiceLocator.SetLocatorProvider(provider);
            var student = ServiceLocator.Current.GetInstance<IStudent>();
            student.Say();
            Console.ReadLine();
        }
    }

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
