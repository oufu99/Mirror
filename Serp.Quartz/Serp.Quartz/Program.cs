using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
