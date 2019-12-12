using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    public class CMDHelper
    {
        /// <summary>
        /// 获取命令行执行结果
        /// </summary>
        /// <param name="strList"></param>
        /// <returns></returns>
        public static string Excute(List<string> strList)
        {
            Process p = new Process();
            //设置要启动的应用程序
            p.StartInfo.FileName = "cmd.exe";
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //是否在新窗口执行
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.Verb = "runas";

            //启动程序
            p.Start();
            foreach (var item in strList)
            {
                p.StandardInput.WriteLine(item + Environment.NewLine);
            }
            p.StandardInput.AutoFlush = true;
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine("exit");
            //获取输出信息
            string strOuput = p.StandardOutput.ReadToEnd();
            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
            return strOuput;
        }

        /// <summary>
        /// 根据传入的地址去获取最新代码
        /// </summary>
        /// <param name="disk">硬盘符</param>
        /// <param name="dirPath">vs的安装路径</param>
        /// <param name="workArea">工作区</param>
        /// <returns></returns>
        public static string GetProjectNewCode(string disk, string dirPath, string workArea)
        {
            var strCmdList = new List<string>()
            {
                disk,
                string.Format(@"cd {0}\CommonExtensions\Microsoft\TeamFoundation\Team Explorer",dirPath),
                string.Format(@"tf get {0} /recursive",workArea)
            };
            return CMDHelper.Excute(strCmdList);
        }
    }
}
