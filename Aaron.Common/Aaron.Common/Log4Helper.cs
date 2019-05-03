using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    public static class Log4Helper
    {
        static ILog logger = log4net.LogManager.GetLogger("errLog4");
        static ILog log = log4net.LogManager.GetLogger("infoLog4");
        public static void WriteInfo(string msg)
        {
            //记录信息
            log.Info(msg);
        }
        public static void WriteErr(string msg)
        {
            //记录错误
            logger.Error(msg);
        }

        public static void WriteFatal(string msg)
        {
            //记录严重错误
            logger.Fatal(msg);
        }
    }
}
