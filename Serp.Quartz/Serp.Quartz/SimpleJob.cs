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
    /// 简单任务
    /// </summary>
    public class SimpleJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine(string.Format("[{0}] 任务处理中...", DateTime.Now.ToString("HH:mm:ss")));
            //业务逻辑处理
            Console.WriteLine("逻辑处理中");
             string json=  ConfigurationManager.AppSettings["JsonData"];
        }
    }
}
