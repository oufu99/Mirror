using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    public class Log4Helper
    {

        public static void WriteInfo(string msg)
        {
            //记录信息
            ILog infoLogger = log4net.LogManager.GetLogger("RollingLogFileAppender");
            infoLogger.Info(msg);
        }
        public static void WriteErr(string msg)
        {
            //记录错误
            ILog errorLogger = log4net.LogManager.GetLogger("RollingLogFileAppender2");
            errorLogger.Error(msg);
        }


    }
}
