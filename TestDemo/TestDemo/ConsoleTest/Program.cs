
using Aaron.Common;
using IModels;

using SendLib;

using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using System.Collections;
using System.Globalization;
using Newtonsoft.Json;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var res = HttpHelper.GetHttpResponse("http://localhost:8088/Test/Index?str=Test");
                Console.WriteLine(res);

            }
            catch (Exception ex)
            {

                throw;
            }
            Console.ReadLine();



        }




    }
}

