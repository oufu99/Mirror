﻿
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
using ConsoleTest.Models;

namespace ConsoleTest
{
    delegate void WriteLineDelegate(string line);

    class Program
    {
        static void Main(string[] args)
        {



            WriteLineDelegate readLine = new WriteLineDelegate(Console.WriteLine);
            readLine("Try!");

            Console.ReadLine();



        }




    }
}

