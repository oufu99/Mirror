using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
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

            //LogHelper.LogInfo("ServiceLogger", "任务服务已经开启:" + DateTime.Now.ToString());


            #region 直接调度
            //把下面的调度服务region注释掉,把这个放出来 然后去xml中配置自己的执行时间直接执行就可以了

            QuartzServiceRunner run = new QuartzServiceRunner();
            run.Start();
            #endregion


            #region 调度服务

            HostFactory.Run(x =>
 {
     x.Service<QuartzServiceRunner>(s =>
     {
         s.ConstructUsing(name => new QuartzServiceRunner());//用反射传进来
         s.WhenStarted(c => c.Start()); //启动时调用传入这个类的方法
         s.WhenStopped(c => c.Stop());
         s.WhenShutdown(c => c.Error());
     });
     x.RunAsLocalSystem();
     x.StartAutomatically();
     x.SetDescription("Serp调度服务");
     x.SetDisplayName("Serp调度服务");
     x.SetServiceName("Serp调度服务");
 });
            #endregion
        }
    }
}

