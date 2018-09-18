using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;

namespace Serp.Quartz
{
    /// <summary>
    /// 测试任务
    /// </summary>
    public class SimpleJob : IJob
    {
        public  void Execute(IJobExecutionContext context)
        {
            System.IO.File.AppendAllText(@"d:\test.txt", "111111");
            //业务逻辑处理
            Console.WriteLine("逻辑处理中");
        }
    }
}
