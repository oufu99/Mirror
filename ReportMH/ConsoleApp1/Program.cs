using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            FactoryPop3 factory = new FactoryPop3();
            Pop3 pop = factory.CreatePop3();
            pop.Pop3Port = 110;
            pop.Pop3Address = "pop3.163.com";
            pop.EmailAddress = "q51758018@163.com";
            //pop.EmailPassword = "ou7225822";
            pop.EmailPassword = "shouquan163";

            pop.Authenticate();
            if (pop.ExitsError)
            {
                DataTable aa = new DataTable();
                aa.Columns.Add("MailCount");
                aa.Columns.Add("SendMialAddress");
                aa.Columns.Add("MailUID");
                aa.Columns.Add("MailSubject");
                aa.Columns.Add("MailBodyAsText");

                DataRow dr = aa.NewRow();
                dr["MailCount"] = pop.GetMailCount().ToString();
                dr["SendMialAddress"] = pop.GetSendMialAddress(1).ToString();
                dr["MailUID"] = pop.GetSendMialAddress(1).ToString();
                dr["MailSubject"] = pop.GetSendMialAddress(1).ToString();
                dr["MailBodyAsText"] = pop.GetSendMialAddress(1).ToString();
                aa.Rows.Add(dr);

                //MessageBox.Show(pop.GetMailCount().ToString());
                //MessageBox.Show(pop.GetSendMialAddress(1).ToString());
                //MessageBox.Show(pop.GetMailUID(1).ToString());
                //MessageBox.Show(pop.GetMailSubject(2).ToString());
                //MessageBox.Show(pop.GetMailBodyAsText(1).ToString());
            }
            else
            {
                //MessageBox.Show(pop.ErrorMessage);
            }

            Console.ReadLine();
        }
        //public static void ReadPop3()
        //{
        //    List<string> fileList = new List<string>();
        //    using (Pop3Client client = new Pop3Client())
        //    {
        //        if (client.Connected)
        //        {
        //            client.Disconnect();
        //        }

        //        // Connect to the server, false means don't use ssl
        //        client.Connect("pop.qq.com", 995, true);
        //        // Authenticate ourselves towards the server by email account and password
        //        client.Authenticate("qq邮箱", "授权码");
        //        var infos = client.GetMessageInfos();
        //        //email count
        //        int messageCount = client.GetMessageCount();

        //        //i = 1 is the first email; 1 is the oldest email
        //        for (int i = messageCount; i >= messageCount - 2; i--)
        //        {
        //            Message message = client.GetMessage(i);
        //            var date = DateTime.Parse(message.Headers.Date);

        //            string sender = message.Headers.From.DisplayName;
        //            string from = message.Headers.From.Address;
        //            string subject = message.Headers.Subject;
        //            DateTime Datesent = message.Headers.DateSent;

        //            MessagePart messagePart = message.MessagePart;

        //            //email body, 
        //            string body = " ";
        //            if (messagePart.IsText)
        //            {
        //                body = messagePart.GetBodyAsText();
        //            }
        //            else if (messagePart.IsMultiPart)
        //            {
        //                MessagePart plainTextPart = message.FindFirstPlainTextVersion();
        //                if (plainTextPart != null)
        //                {
        //                    // The message had a text/plain version - show that one
        //                    body = plainTextPart.GetBodyAsText();
        //                }
        //                else
        //                {
        //                    // Try to find a body to show in some of the other text versions
        //                    List<MessagePart> textVersions = message.FindAllTextVersions();
        //                    if (textVersions.Count >= 1)
        //                        body = textVersions[0].GetBodyAsText();
        //                    else
        //                        body = "<<OpenPop>> Cannot find a text version body in this message.";
        //                }
        //            }
        //            Console.WriteLine(body);
        //            Console.WriteLine("----------------");
        //            fileList.Add(body);

        //        }
        //        Console.WriteLine("读取结束");
        //        foreach (var item in fileList)
        //        {
        //            System.IO.File.AppendAllText(@"d:\jialin.txt", item + "\n");
        //        }
        //        Console.WriteLine("写入完毕");
        //        Console.ReadLine();
        //    }
        //}
    }

    public abstract class Pop3
    {
        #region 窗体变量
        /// <summary>
        /// 是否存在错误
        /// </summary>
        public abstract Boolean ExitsError { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public abstract String ErrorMessage { get; set; }
        /// <summary>
        /// POP3端口号
        /// </summary>
        public abstract Int32 Pop3Port { set; get; }
        /// <summary>
        /// POP3地址
        /// </summary>
        public abstract String Pop3Address { set; get; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public abstract String EmailAddress { set; get; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public abstract String EmailPassword { set; get; }

        #endregion

        #region 链接至服务器并读取邮件集合
        /// <summary>
        /// 链接至服务器并读取邮件集合
        /// </summary>
        public abstract Boolean Authenticate();

        #endregion

        #region 获取邮件数量
        /// <summary>
        /// 获取邮件数量
        /// </summary>
        /// <returns></returns>
        public abstract Int32 GetMailCount();

        #endregion

        #region 获取发件人
        /// <summary>
        /// 获取发件人
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public abstract String GetSendMialAddress(Int32 mailIndex);

        #endregion

        #region 获取邮件的主题
        /// <summary>
        /// 获取邮件的主题
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public abstract String GetMailUID(Int32 mailIndex);

        #endregion

        #region 取邮件的UID
        /// <summary>
        /// 获取邮件的UID
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public abstract String GetMailSubject(Int32 mailIndex);
        #endregion

        #region 获取邮件正文
        /// <summary>
        /// 获取邮件正文
        /// </summary>
        /// <param name="mailIndex">邮件顺序</param>
        /// <returns></returns>
        public abstract String GetMailBodyAsText(Int32 mailIndex);
        #endregion

        #region 获取邮件的附件
        public abstract Boolean GetMailAttachment(Int32 mailIndex, String receiveBackpath);

        #endregion

        #region 删除邮件
        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailIndex"></param>
        public abstract void DeleteMail(Int32 mailIndex);
        #endregion

        #region 关闭邮件服务器
        public abstract void Pop3Close();
        #endregion
    }


    /// <summary>
    /// 自己多封装了一层  继承dll中的Pop3类
    /// </summary>
    public class OpenPopPop3 : Pop3
    {

        public OpenPopPop3() { }

        #region 窗体变量
        /// <summary>
        /// 是否存在错误
        /// </summary>
        public override Boolean ExitsError { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public override String ErrorMessage { get; set; }
        /// <summary>
        /// POP3端口号
        /// </summary>
        public override Int32 Pop3Port { set; get; }
        /// <summary>
        /// POP3地址
        /// </summary>
        public override String Pop3Address { set; get; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public override String EmailAddress { set; get; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public override String EmailPassword { set; get; }

        #endregion

        #region 私有变量
        private Pop3Client pop3Client;

        // private List<POP3_ClientMessage> pop3MessageList = new List<POP3_ClientMessage>();

        private Int32 mailTotalCount;
        #endregion


        #region 链接至服务器并读取邮件集合
        /// <summary>
        /// 链接至服务器并读取邮件集合
        /// </summary>
        public override Boolean Authenticate()
        {
            try
            {
                pop3Client = new Pop3Client();
                if (pop3Client.Connected)
                    pop3Client.Disconnect();
                pop3Client.Connect(Pop3Address, Pop3Port, false);
                pop3Client.Authenticate(EmailAddress, EmailPassword, AuthenticationMethod.UsernameAndPassword);
                mailTotalCount = pop3Client.GetMessageCount();

                return ExitsError = true;
            }
            catch (Exception ex) { ErrorMessage = ex.Message; return ExitsError = false; }
        }
        #endregion

        #region 获取邮件数量
        /// <summary>
        /// 获取邮件数量
        /// </summary>
        /// <returns></returns>
        public override Int32 GetMailCount()
        {
            return mailTotalCount;
        }
        #endregion

        #region 获取发件人
        /// <summary>
        /// 获取发件人
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override String GetSendMialAddress(Int32 mailIndex)
        {
            RfcMailAddress address = pop3Client.GetMessageHeaders(mailIndex).From;
            return address.Address;
        }
        #endregion

        #region 获取邮件的主题
        /// <summary>
        /// 获取邮件的主题
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override String GetMailUID(Int32 mailIndex)
        {
            return pop3Client.GetMessageUid(mailIndex);

        }
        #endregion

        #region 获取邮件的UID
        /// <summary>
        /// 获取邮件的UID
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override String GetMailSubject(Int32 mailIndex)
        {
            return pop3Client.GetMessageHeaders(mailIndex).Subject;
        }
        #endregion

        #region 获取邮件正文
        /// <summary>
        /// 获取邮件正文
        /// </summary>
        /// <param name="mailIndex">邮件顺序</param>
        /// <returns></returns>
        public override String GetMailBodyAsText(Int32 mailIndex)
        {
            Message message = pop3Client.GetMessage(mailIndex);
            MessagePart selectedMessagePart = message.MessagePart;
            return selectedMessagePart.GetBodyAsText();
        }
        #endregion

        #region 获取邮件的附件
        public override Boolean GetMailAttachment(Int32 mailIndex, String receiveBackpath)
        {
            if (mailIndex == 0)
                return false;
            else if (mailIndex > mailTotalCount)
                return false;
            else
            {
                try
                {
                    Message message = pop3Client.GetMessage(mailIndex);
                    //邮件的全部附件.
                    List<MessagePart> attachments = message.FindAllAttachments();
                    foreach (MessagePart attachment in attachments)
                    {
                        string fileName = attachment.FileName;
                        string fileFullName = receiveBackpath + "\\" + fileName;
                        FileInfo fileInfo = new FileInfo(fileFullName);
                        if (fileInfo.Exists) fileInfo.Delete();
                        attachment.Save(fileInfo);
                    }
                    pop3Client.DeleteMessage(mailIndex);
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 删除邮件
        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailIndex"></param>
        public override void DeleteMail(Int32 mailIndex)
        {
            pop3Client.DeleteMessage(mailIndex);
        }
        #endregion

        #region 关闭邮件服务器
        public override void Pop3Close()
        {
            pop3Client.Disconnect();
            pop3Client.Dispose();
        }
        #endregion

    }

    public class FactoryPop3
    {
        public String Pop3Type = "OpenPop";

        public Pop3 CreatePop3()
        {
            Pop3 pop = null;
            if (Pop3Type == "OpenPop")
            {
                return pop = new OpenPopPop3();
            }
            //else if (Pop3Type == "LumiSoft")
            //{
            //    return pop = new LumiSoftPop3();
            //}
            else
            {
                return null;
            }
        }
    }
}
