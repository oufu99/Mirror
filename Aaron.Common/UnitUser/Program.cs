using Aaron.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitUser
{
    class Program
    {

        static void Main(string[] args)
        {

            var projectFullPath = @"D:\Tools\OpenMyTools\OpenMyTools.csproj";
            //输出目录
            //var outPath = @"D:\Tools\OpenMyTools\bin\Debug\";
            ////编译
            var res = AutoBuildHelper.BuildOutBin(projectFullPath);

            Console.WriteLine(res);
            Console.ReadLine();



        }
    }
}
