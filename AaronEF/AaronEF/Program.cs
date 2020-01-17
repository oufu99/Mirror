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


            var list = new List<Person>();
            Person u = new Person()
            {
                Id = 3,
                Age = 11,
                Name = "testEF616"
            };
            Person u2 = new Person()
            {
                Id = 4,
                Age = 11,
                Name = "testEF611"
            };
            Person u3 = new Person()
            {
                Id = 4,
                Age = 11,
                Name = "ttt"
            };
            list.Add(u);
            list.Add(u);
            list.Add(u);


            PersonRepository.InsertList(list);



            Console.ReadLine();
        }
         
    }
}