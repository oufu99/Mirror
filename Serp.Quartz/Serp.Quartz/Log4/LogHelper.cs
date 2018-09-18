using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"log4net.config", Watch = true)]
namespace Serp.Quartz
{
    public class LogHelper
    {
        private static log4net.ILog Log(string appenderName)
        {
            return log4net.LogManager.GetLogger(appenderName);
        }

        public static void LogInfo(string appenderName, string message)
        {
            var logger = Log(appenderName);
            if (logger.IsInfoEnabled)
                logger.Info(message);
        }

        public static void LogInfo(string appenderName, string message, Exception ex)
        {
            var logger = Log(appenderName);
            if (logger.IsInfoEnabled)
                logger.Info(message, ex);
        }
        public static void ErrorInfo(string appenderName, string message)
        {
            var logger = Log(appenderName);
            if (logger.IsInfoEnabled)
                logger.Error(message);
        }

        public static void ErrorInfo(string appenderName, string message, Exception ex)
        {
            var logger = Log(appenderName);
            if (logger.IsInfoEnabled)
                logger.Error(message, ex);
        }
        //public static void DebugInfo(string appenderName, string Message)
        //{
        //    if (!log(appenderName).IsInfoEnabled)
        //        SetConfig();
        //    log(appenderName).Debug(Message);
        //}

    }
}
