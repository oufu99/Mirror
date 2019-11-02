using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Person
    {
        public async Task<int> JiSuan()
        {
            Console.WriteLine("开始计算");
            Thread.Sleep(2000);

            return 66;
            //return  JiSuan2();
        }


        public async Task<int> JiSuan2()
        {
            Console.WriteLine("开始计算2");
            Thread.Sleep(2000);

            return 66;

        }
    }
}
