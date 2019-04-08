using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            var person = new Person() { Name = "Aaron", Age = 18, Email = null };
            var person2 = new Person() { Name = "张三" };

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, Person>());


            var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, Person>()
    .ForMember("", opt => opt.NullSubstitute("")));

            var mapper = config.CreateMapper();
            mapper.Map(person, person2, typeof(Person), typeof(Person));


            Console.WriteLine(person2.Email);



            Console.ReadLine();
        }

        //public static T Update<T>(T person)
        //{
        //    var config = new MapperConfiguration(cfg => cfg.CreateMap<T, T>());
        //    var mapper = config.CreateMapper();
        //    var dto = mapper.Map<T>(person);
        //    return dto;

        //}

    }




    public class Person
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
