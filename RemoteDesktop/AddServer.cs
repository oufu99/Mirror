using Aaron.Common;
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
    public partial class AddServer : Form
    {
        public static readonly string ServerXmlUrl = System.Configuration.ConfigurationManager.AppSettings["ServerXmlUrl"];
        public ServerListModel serverListModel = new ServerListModel();

        public AddServer()
        {
            InitializeComponent();

            serverListModel = XMLHelper.LoadFromXml(ServerXmlUrl, typeof(ServerListModel)) as ServerListModel;
        }

        private void btn_addserver_Click(object sender, EventArgs e)
        {
            ServerModel serverModel = new ServerModel();
            serverModel.Remark = tb_remark.Text;
            serverModel.Server = tb_ip.Text;
            serverModel.Port = Convert.ToInt32(tb_port.Text);
            serverModel.UserName = tb_username.Text;
            serverModel.Password = tb_pwd.Text;

            serverListModel.serverModels.Add(serverModel);

            XMLHelper.SaveToXml(ServerXmlUrl, serverListModel, typeof(ServerListModel), "");

            MessageBoxButtons mess = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("继续添加？", "提示", mess);
            if (dr == DialogResult.OK)
            {
                tb_remark.Text = "";
                tb_ip.Text = "";
                tb_port.Text = "3389";
                tb_username.Text = "";
                tb_pwd.Text = "";
            }
            else
            {
                DialogResult = DialogResult.OK;   ///设置对话框的值 
                this.Close();
            }

        }
    }
}
