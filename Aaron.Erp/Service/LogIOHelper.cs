using Aaron.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Service
{
    public class LogIOHelper : ILogHelper
    {

        /// <summary>
        /// 一般性日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="appenderName"></param>
        public void Info(string msg)
        {
            System.IO.File.AppendAllText(@"d:\jialin.txt", msg);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="appenderName"></param>

        public void Error(string msg)
        {
            System.IO.File.AppendAllText(@"d:\jialin.txt", msg);
        }
    }
}