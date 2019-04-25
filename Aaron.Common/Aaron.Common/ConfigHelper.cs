using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Aaron.Common
{
    using System.Configuration;

    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 获取appConfig节点的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获取连接字符串节点的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionConfig(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

    }

}
