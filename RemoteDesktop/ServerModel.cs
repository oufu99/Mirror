using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RemoteDesktop
{
    [XmlRootAttribute("ServerListModel")]
    public class ServerListModel
    {

        [XmlElementAttribute("ServerModel")]
        public List<ServerModel> serverModels { get; set; }

        public ServerListModel()
        {
            serverModels = new List<ServerModel>();
        }
    }

    [XmlRootAttribute("ServerModel")]
    public class ServerModel
    {
        /// <summary>
        /// 服务器IP
        /// </summary>
        [XmlAttribute("Server")]
        public string Server { get; set; }

        /// <summary>
        /// 服务器端口 
        /// </summary>
        [XmlAttribute("Port")]
        public int Port { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [XmlAttribute("UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [XmlAttribute("Password")]
        public string Password { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlAttribute("Remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 远程连接打开方式
        /// </summary>
        [XmlAttribute("Type")]
        public string Type { get; set; }

        public ServerModel()
        {
            Server = "";
            Port = 3389;
            UserName = "";
            Password = "";
            Remark = "";
            Type = "1";
        }
    }

    public enum Type
    {
        //普通远程桌面模式
        KeyboardHookMode = 1,
        //运行远程程序模式
        RemoteProgramMode = 2
    }
}
