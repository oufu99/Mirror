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
            var list = Process.GetProcesses();
            var name = list.First(c => c.ProcessName.Contains("AutoHotkey"));

            Console.ReadLine();
        }
    }




}
