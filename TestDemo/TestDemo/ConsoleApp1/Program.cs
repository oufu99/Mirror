using Aaron.Common;
using System;
using System.Collections.Generic;
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
            var url = "http://localhost:61404/HotUpdate/TestDll";
            string[] arr = new string[] { "123", "346" };
            HttpHelper.PostHttpResponse(url, arr);

            Console.ReadLine();
        }

    }
}
