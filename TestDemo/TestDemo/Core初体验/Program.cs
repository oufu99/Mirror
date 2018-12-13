using SqlSugar;
using System;
using System.Linq;

namespace Core初体验
{
    class Program
    {
        static void Main(string[] args)
        {
            var wei = 129 & 129;
            Console.WriteLine(wei);
            Console.ReadLine();

        }
    }

    class Users
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
