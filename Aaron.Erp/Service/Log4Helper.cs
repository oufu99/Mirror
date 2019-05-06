using IService;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"Config\log4.config", Watch = true)]
namespace Aaron.Service
{
    public class Log4Helper : ILogHelper
    {

        /// <summary>
        /// 生成对应的Logger 根据配置文件的<logger name="InfoLogger">节点的name 然后通过ref去找到对应的文件位置记录
        /// </summary>
        /// <param name="appenderName"></param>
        /// <returns></returns>
        private ILog log(string appenderName)
        {
            return LogManager.GetLogger(appenderName);
        }

        /// <summary>
        /// 一般性日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="appenderName"></param>
        public void Info(string msg)
        {
            ILog logger = log("InfoLogger");
            logger.Info(msg);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="appenderName"></param>

        public void Error(string msg)
        {
            ILog logger = log("ErrorLogger");
            //if (logger.IsInfoEnabled)
            logger.Error(msg);
        }


    }
}