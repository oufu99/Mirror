using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;


namespace Serp.Quartz
{
    class Program
    {
        public static IScheduler CurrentSched { get; private set; }

        static void Main(string[] args)
        {
            //调度器构造工厂
            ISchedulerFactory factory = new StdSchedulerFactory();

            //第一步：构造调度器
            IScheduler scheduler = factory.GetScheduler();
            scheduler.Start();//启动调度器

            while (!scheduler.IsStarted)
            {
                Console.WriteLine("Waiting for scheduler to start.");
                Thread.Sleep(1000);
            }
            Console.WriteLine("IsStarted={0}", scheduler.IsStarted);
            Console.WriteLine("SchedulerInstanceId={0}", scheduler.SchedulerInstanceId);
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

        }





        #region 我的
        static void Main1(string[] args)
        {

            //流程  先创建一个调度器,然后给这个调度器设置属性, 
            // 这个属性包括  传入实现了IJob的类进去执行里面的execute方法,还有一个 Trigger用来标记多久执行一次

            //然后这个Quartz写好以后就会变成一个exe文件,接着再用shelfTop来用命令行进行开启关闭

            CurrentSched = StdSchedulerFactory.GetDefaultScheduler();   //从工厂中获取一个调度器实例化

            JobWithSimpleTriggerDemo();

            CurrentSched.Start();  //开启调度器
            Console.WriteLine("开始调度");
            Console.ReadLine();
        }
        private static void JobWithSimpleTriggerDemo()
        {
            IJobDetail simpleJob = JobBuilder.Create<SimpleJob>().WithIdentity("renwu1", "renwuGroup").Build();    //创建一个任务
            ITrigger simpleTrigger = TriggerBuilder.Create().WithIdentity("chufa1", "chufaGroup").StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever()).Build();   //创建一个简单触发器，每隔5秒执行一次
            CurrentSched.ScheduleJob(simpleJob, simpleTrigger);    //把任务、简单触发器加入调度器
        } 
        #endregion


    }
}
