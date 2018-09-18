using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serp.Quartz
{


    public class QuartzServiceRunner
    {
        //定义一个通用的调度对象
        public static IScheduler CurrentSched = null;
        static QuartzServiceRunner()
        {
            ISchedulerFactory schedf = new StdSchedulerFactory();
            //获取xml中定义的所有任务
            CurrentSched = schedf.GetScheduler();
           
        }

        public virtual void Start()
        {
            //这个Quartz写好以后就会变成一个exe文件,接着再用shelfTop来用命令行进行开启关闭
            CurrentSched.Start();  //开启调度器
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
