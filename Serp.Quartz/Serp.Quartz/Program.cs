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
using Topshelf;

namespace Serp.Quartz
{
    class Program
    {

        static void Main(string[] args)
        {
            //明天研究一下这个core时间定义  看一下多任务怎么生效  指定时间生效
            QuartzServiceRunner run = new SimpleJob();
            run.Start();


            #region 可以用的调度服务
            //HostFactory.Run(x =>
            //{
            //    x.Service<QuartzServiceRunner>(s =>
            //    {
            //        s.ConstructUsing(name => new SimpleJob());
            //        s.WhenStarted(c => c.Start()); //启动时调用传入这个类的方法
            //        s.WhenStopped(c => c.Stop());
            //    });
            //    x.RunAsLocalSystem();
            //    x.StartAutomatically();
            //    x.SetDescription("Serp调度服务");
            //    x.SetDisplayName("Serp调度服务");
            //    x.SetServiceName("Serp调度服务");
            //});

            //Console.ReadLine(); 
            #endregion


        }


        static void Main1(string[] args)
        {

            #region 网上的,试了下不行
            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler();
            scheduler.Start();//启动调度器

            Console.WriteLine("IsStarted={0}", scheduler.IsStarted);
            Console.WriteLine("SchedulerName={0}", scheduler.SchedulerName);
            Console.WriteLine("The scheduler is running. Press any key to stop");
            Console.ReadKey();
            Console.WriteLine("Shutting down scheduler");
            scheduler.Shutdown(false);
            while (!scheduler.IsShutdown)
            {
                Console.WriteLine("Waiting for scheduler to shutdown.");
                Thread.Sleep(1000);
            }
            Console.WriteLine("IsShutdown={0}", scheduler.IsShutdown);
            Console.WriteLine("The scheduler has been shutdown.");
            #endregion
        }
    }
}

