using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AaronEF
{
    class Program
    {
        static void Main(string[] args)
        {
            Person u = new Person()
            {
                Id = 3,
                Age = 11,
                Name = "testEF616"
            };

            //PersonRepository.Insert(u);
            PersonRepository.Update(u);



            Console.ReadLine();
        }
         
    }
}