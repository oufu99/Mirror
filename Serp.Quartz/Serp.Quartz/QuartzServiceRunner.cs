using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serp.Quartz
{


    public abstract class QuartzServiceRunner : IJob
    {
        static QuartzServiceRunner()
        {
            CurrentSched = StdSchedulerFactory.GetDefaultScheduler();   //从工厂中获取一个调度器实例化
        }
        public static IScheduler CurrentSched { get; private set; }
        public virtual void Start()
        {

            //流程  先创建一个调度器,然后给这个调度器设置属性, 
            //这个属性包括  传入实现了IJob的类进去执行里面的execute方法,还有一个 Trigger用来标记多久执行一次
            //然后这个Quartz写好以后就会变成一个exe文件,接着再用shelfTop来用命令行进行开启关闭

            //时间设置那些都是在xml中实现
            CurrentSched.Start();  //开启调度器

        }
        /// <summary>
        /// 用自己的类实现这个方法
        /// </summary>
        /// <param name="context"></param>
        public abstract void Execute(IJobExecutionContext context);



        /// <summary>
        /// 后面的重写这个方法 自定义执行时间就可以了 现在更方便了
        /// </summary>
        public virtual void JobWithSimpleTriggerDemo()
        {
            //明天研究一下这个core时间定义 
            IJobDetail simpleJob = JobBuilder.Create<SimpleJob>().WithIdentity("renwu1", "renwuGroup").Build();    //创建一个任务
            ITrigger simpleTrigger = TriggerBuilder.Create().WithIdentity("chufa1", "chufaGroup").StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever()).Build();   //创建一个简单触发器，每隔5秒执行一次
            CurrentSched.ScheduleJob(simpleJob, simpleTrigger);    //把任务、简单触发器加入调度器
        }



        public bool Stop()
        {
            CurrentSched.Shutdown(false);
            return true;
        }

        public bool Continue()
        {
            CurrentSched.ResumeAll();
            return true;
        }

        public bool Pause()
        {
            CurrentSched.PauseAll();
            return true;
        }


    }
}
