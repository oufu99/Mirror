using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    public class ProcessHelper
    {
        /// <summary>
        /// 根据传入Name杀死进程
        /// </summary>
        /// <param name="killName"></param>
        public static void KillProgramByName(string killName)
        {

            var ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if (killName.ToLower().Contains(p.ProcessName.ToLower()))
                {
                    p.Kill();
                    p.WaitForExit();
                }
            }
        }

        /// <summary>
        /// 根据传入数组杀死进程
        /// </summary>
        /// <param name="killList"></param>
        public static void KillProgramByList(List<string> killList)
        {
            //直接杀死进程
            for (int i = 0; i < killList.Count; i++)
            {
                killList[i] = killList[i].ToLower();
            }
            var ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                if (killList.Contains(p.ProcessName.ToLower()))
                {
                    p.Kill();
                    p.WaitForExit();
                }
            }
        }
    }
}
