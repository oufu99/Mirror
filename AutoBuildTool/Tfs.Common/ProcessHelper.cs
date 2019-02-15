using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tfs.Common
{
    public class ProcessHelper
    {
        /// <summary>
        /// 获取软件的安装目录
        /// </summary>
        /// <returns></returns>
        public static string GetInstallDirName(string softwareName)
        {
            var proList = Process.GetProcesses();
            var fileName = Process.GetProcessesByName(softwareName)[0].MainModule.FileName;
            return Path.GetDirectoryName(fileName);
        }
    }
}
