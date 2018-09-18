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
        //定义一个通用的调度对象   这里不能做成static的,不然后面改了xml就不会再次加载了
        public IScheduler CurrentSched = null;
        public QuartzServiceRunner()
        {
            ISchedulerFactory schedf = new StdSchedulerFactory();
            //获取xml中定义的所有任务
            CurrentSched = schedf.GetScheduler();
            SchedulerListener listener = new SchedulerListener();
            CurrentSched.ListenerManager.AddSchedulerListener(listener);
        }

        public virtual void Start()
        {
            //这个Quartz写好以后就会变成一个exe文件,接着再用shelfTop来用命令行进行开启关闭
            string logMsg = string.Format("window框架开始调度开始之前" + DateTime.Now.ToString());
            LogHelper.LogInfo("ServiceLogger", logMsg);
            CurrentSched.Start();  //开启调度器
            logMsg = string.Format("window框架开始调度完成" + DateTime.Now.ToString());
            LogHelper.LogInfo("ServiceLogger", logMsg);
        }


        public bool Stop()
        {
            string logMsg = string.Format("window框架停止" + DateTime.Now.ToString());
            LogHelper.LogInfo("ServiceLogger", logMsg);
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

        public void Error()
        {
            try
            {
                string logMsg = string.Format("window框架报错11" + DateTime.Now.ToString());
                LogHelper.LogInfo("ServiceLogger", logMsg);
            }
            catch (Exception ex)
            {
                string logMsg = string.Format("window框架报错22" + ex.Message);
                LogHelper.LogInfo("ServiceLogger", logMsg);
                throw;
            }



        }
    }
}
