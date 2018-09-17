using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Quartz;
using Quartz.Impl;


namespace Serp.Quartz
{
    class Program
    {

        static void Main(string[] args)
        {
            //明天研究一下这个core时间定义  看一下多任务怎么生效  立刻生效
            QuartzServiceRunner run = new SimpleJob();
            run.Start();

            Console.ReadLine();

        }


        static void Main1(string[] args)
        {

            #region 网上的,试了下不行
            //ISchedulerFactory factory = new StdSchedulerFactory();
            //IScheduler scheduler = factory.GetScheduler();
            //scheduler.Start();//启动调度器


            //Console.WriteLine("IsStarted={0}", scheduler.IsStarted);
            //Console.WriteLine("SchedulerName={0}", scheduler.SchedulerName);
            //Console.WriteLine("The scheduler is running. Press any key to stop");
            //Console.ReadKey();
            //Console.WriteLine("Shutting down scheduler");
            //scheduler.Shutdown(false);
            //while (!scheduler.IsShutdown)
            //{
            //    Console.WriteLine("Waiting for scheduler to shutdown.");
            //    Thread.Sleep(1000);
            //}
            //Console.WriteLine("IsShutdown={0}", scheduler.IsShutdown);
            //Console.WriteLine("The scheduler has been shutdown.");

            #endregion



        }
    }
}

