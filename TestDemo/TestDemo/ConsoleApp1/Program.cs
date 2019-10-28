using Aaron.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var id = 2;
            var obj = new[] { "Apple", "Orange", "Peach" }[id > 2 ? 0 : id];
            Console.WriteLine();
        }



    }
}
