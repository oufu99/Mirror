using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace AHKPrompt
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        [DllImport("User32.dll ")]
        public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childe, string strclass, string FrmText);


        [DllImport("User32.dll ")]
        public static extern long SetWindowText(IntPtr parent, string lpString);

        const int WM_SETTEXT = 0x000C;



        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //这个句柄可以用ahk的Spy去发现出来
            //IntPtr hwnd = FindWindow("Notepad", null);

            IntPtr hwnd = FindWindow("TNavicatMainForm", null);
            if (hwnd != IntPtr.Zero)
            {
                //首先得到第一个窗口,然后发送文本
                IntPtr htextbox = FindWindowEx(hwnd, IntPtr.Zero, null, null);
                SendMessage(htextbox, WM_SETTEXT, (IntPtr)0, "Hello world");

            }

        }


    }

}
