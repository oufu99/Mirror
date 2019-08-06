using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    public class HostHelper
    {
        public static void AddHost(string ip, string domain)
        {
            string path = XMLHelper.GetNodeText(XMLPath.Host);
            //通常情况下这个文件是只读的，所以写入之前要取消只读
            File.SetAttributes(path, File.GetAttributes(path) & (~FileAttributes.ReadOnly));//取消只读
            //1.创建文件流
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    //127.0.0.1       shulunduo.web.cn
                    StringBuilder sb = new StringBuilder();
                    sb.Append("\r\n" + ip);
                    sb.Append("\t\t" + domain + XMLHelper.GetNodeText(XMLPath.IISWebUrl));
                    sb.Append("\r\n" + ip);
                    sb.Append("\t\t" + domain + XMLHelper.GetNodeText(XMLPath.IISMobileUrl));
                    sw.WriteLine(sb.ToString());
                    //如果报redirection.config 错误
                    //到C:\inetpub\history中找到最近一次的【CFGHISTORY_00000000XX】文件，点击进去找到applicationHost.config文件，用其覆盖C:\Windows\system32\inetsrv\config\applicationHost.config。
                }
            }
        }
    }
}
