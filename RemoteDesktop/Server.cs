using AxMSTSCLib;
using MSTSCLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteDesktop
{
    public partial class Server : Form
    {
        public Server(ServerModel serverModel)
        {

            var rdpClient = new AxMsRdpClient9NotSafeForScripting
            {
                Dock = DockStyle.None,
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.WorkingArea.Height
            };
            ((System.ComponentModel.ISupportInitialize)(rdpClient)).BeginInit();
            this.Controls.Add(rdpClient);
            ((System.ComponentModel.ISupportInitialize)(rdpClient)).EndInit();

            rdpClient.Server = serverModel.Server;

            rdpClient.AdvancedSettings2.RDPPort = serverModel.Port;

            rdpClient.UserName = serverModel.UserName;
            rdpClient.AdvancedSettings2.ClearTextPassword = serverModel.Password;


            if (serverModel.Type == "1") //普通远程桌面模式
            {
                //映射键盘
                rdpClient.SecuredSettings3.KeyboardHookMode = 1;
            }
            else //运行远程程序模式
            {
                rdpClient.RemoteProgram2.RemoteProgramMode = true;

                rdpClient.OnLoginComplete += (o, args) =>
                {
                    rdpClient.RemoteProgram2.ServerStartProgram("123", "", "%SYSTEMROOT%", false, "", false);
                    rdpClient.OnRemoteProgramResult += (o1, args1) =>
                    {
                        if (args1.lError != RemoteProgramResult.remoteAppResultOk)
                        {
                            rdpClient.Dispose();
                        }
                    };
                };
            }

         //偏好设置
         ((IMsRdpClientNonScriptable5)rdpClient.GetOcx()).PromptForCredentials = false;
            rdpClient.AdvancedSettings9.EnableCredSspSupport = true;
            rdpClient.ColorDepth = 16;
            rdpClient.AdvancedSettings9.RedirectDrives = true; //共享本地磁盘
            rdpClient.ConnectingText = $"正在连接";
            //连接远程桌面
            rdpClient.Connect();


            InitializeComponent();
        }

        private void Server_Load(object sender, EventArgs e)
        {
            //this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            //必加，不加也不会实现
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //this.Height = this.Height - 30;
        }
    }
}
