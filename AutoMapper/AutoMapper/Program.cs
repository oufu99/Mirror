using AutoMapper;
using System;

namespace AutoMapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            var p = new Person() { Name = "Aaron", Age = 18, Email = "qq.com" };
            var p2 = Mapper.Map<Person, Person>(p);










            Console.ReadLine();
        }
    }

    public class Person
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
