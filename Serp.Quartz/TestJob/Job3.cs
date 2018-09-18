using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestJob
{
    public class Job3 : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            System.IO.File.AppendAllText(@"d:\test33.txt", "333");
            //业务逻辑处理
            Console.WriteLine("逻辑处理中433");
        }
    }
}
