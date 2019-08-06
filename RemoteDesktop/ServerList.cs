using Aaron.Common;
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
using System.Xml;
using System.Xml.Serialization;

namespace RemoteDesktop
{
    public partial class ServerList : Form
    {
        public static readonly string ServerXmlUrl = System.Configuration.ConfigurationManager.AppSettings["ServerXmlUrl"];
        public ServerListModel serverListModel = new ServerListModel();
      

        public ServerList()
        {
            InitializeComponent();
            //设置控件选中选中的模式
            //单击单元格或行标题可以选中整行
            this.serverdatalist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            serverdatalist.AutoGenerateColumns = false;
            serverListModel = XMLHelper.LoadFromXml(ServerXmlUrl, typeof(ServerListModel)) as ServerListModel;

            BindListLogin();
        }


        private void btn_add_Click(object sender, EventArgs e)
        {
            AddServer addServer = new AddServer();

            addServer.ShowDialog();

            if (addServer.DialogResult == DialogResult.OK)
            {
                BindListLogin();    //DataGridView控件的值
            }

        }

        public void BindListLogin()
        {
            serverListModel = XMLHelper.LoadFromXml(ServerXmlUrl, typeof(ServerListModel)) as ServerListModel;
            serverdatalist.DataSource = serverListModel.serverModels;
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            MessageBoxButtons mess = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确认删除？", "提示", mess);
            if (dr == DialogResult.OK)
            {
                ServerListModel DelModel = new ServerListModel();
                for (int i = serverdatalist.RowCount - 1; i > 0; i--)
                {
                    if (serverdatalist.Rows[i].Selected == true)
                    {
                        string tmp = serverdatalist.Rows[i].Cells[1].Value.ToString();


                        ServerModel[] serverModels = serverListModel.serverModels.ToArray<ServerModel>();
                        foreach (ServerModel model in serverModels)
                        {
                            if (model.Server.Equals(tmp))
                            {
                                DelModel.serverModels.Add(model);
                            }
                        }
                    }
                }
                foreach (ServerModel model in DelModel.serverModels)
                {
                    serverListModel.serverModels.Remove(model);
                    XMLHelper.SaveToXml(ServerXmlUrl, serverListModel, typeof(ServerListModel), "");
                }
            }
            //刷新DatagradView的值
            BindListLogin();
        }

        private void serverdatalist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (serverdatalist.Columns[e.ColumnIndex].Name == "btn_edit")
            {
                XMLHelper.SaveToXml(ServerXmlUrl, serverListModel, typeof(ServerListModel), "");
            }
        }

        private void serverdatalist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn column = serverdatalist.Columns[e.ColumnIndex];
            ServerModel serverModel = new ServerModel();
            serverModel.Remark = Convert.ToString(serverdatalist.CurrentRow.Cells[0].Value);
            serverModel.Server = Convert.ToString(serverdatalist.CurrentRow.Cells[1].Value);
            serverModel.Port = Convert.ToInt32(serverdatalist.CurrentRow.Cells[2].Value);
            serverModel.UserName = Convert.ToString(serverdatalist.CurrentRow.Cells[3].Value);
            serverModel.Password = Convert.ToString(serverdatalist.CurrentRow.Cells[4].Value);

            Server server = new Server(serverModel);
            server.Show();

        }
    }
}
