using System;
using System.Runtime.InteropServices; // 勿忘这句

namespace ConsoleApplication1
{
    class Program
    {
        [DllImport("user32.dll", EntryPoint = "FindWindowA", CharSet = CharSet.Ansi)]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SetWindowText", CharSet = CharSet.Ansi)]
        public static extern int SetWindowText(int hwnd, string lpString);

        static void Main(string[] args)
        {
            int lHwnd = FindWindow(null, "无标题 - 记事本");
            while (lHwnd != 0)
            {
                SetWindowText(lHwnd, "有标题");
                lHwnd = FindWindow(null, "无标题 - 记事本");
            }
        }
    }
}