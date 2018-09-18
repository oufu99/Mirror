using Quartz;
using Quartz.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serp.Quartz
{
    /// <summary>
    /// 监听类
    /// </summary>
    public class SchedulerListener : SchedulerListenerSupport
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        public override void SchedulerError(string msg, SchedulerException cause)
        {
            string logMsg = string.Format("服务报错,信息{0},{1},{2},{3},{4}",
                msg, cause.Message, cause.Source, cause.StackTrace, cause.InnerException.Message);
            LogHelper.LogInfo("ServiceLogger", logMsg);
            base.SchedulerError(msg, cause);
        }

        public override void JobScheduled(ITrigger trigger)
        {
            string logMsg = string.Format("服务开始调度,{0},trigger名字:{1}",
              trigger.JobKey.Name, trigger.Key.Name);
            LogHelper.LogInfo("ServiceLogger", logMsg);
            //log.DebugFormat("服务启动,服务名称.{0}.TriggerName.{1}.JobGroup.{2}.JobName.{3}", trigger.Key.Group, trigger.Key.Name, trigger.JobKey.Group, trigger.JobKey.Name);
            base.JobScheduled(trigger);
        }

        public override void JobPaused(JobKey jobKey)
        {
            string logMsg = string.Format("服务停止,{0},名字:{1}}",
        jobKey.Group, jobKey.Name);
            LogHelper.LogInfo("ServiceLogger", logMsg);
            //log.DebugFormat("JobPaused.Group.{0}.Name.{1}", jobKey.Group, jobKey.Name);
            base.JobPaused(jobKey);
        }



        public override void SchedulerStarted()
        {
            //log.Warn("SchedulerStarted");
            string logMsg = string.Format("调度器启动");
            LogHelper.LogInfo("ServiceLogger", logMsg);
            base.SchedulerStarted();
        }


    }
}
