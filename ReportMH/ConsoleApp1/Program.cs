using Microsoft.Exchange.WebServices.Data;
using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
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

            //SmtpClient client = new System.Net.Imap4.Imap4Client("ExampleHost", port, ssl);
            //client.DefaultMailbox = "[Gmail]/Sent Mail";
            //try
            //{
            //    client.Login("ExampleEmail", "ExamplePass", AuthMethod.Login);
            //    IEnumerable<uint> units = client.Search(SearchCondition.Seen());
            //    DataTable TempTaskTable = new DataTable();
            //    TempTaskTable.Columns.Add("FromEmail", typeof(string));
            //    TempTaskTable.Columns.Add("ToEmail", typeof(string));
            //    TempTaskTable.Columns.Add("Subject", typeof(string));
            //    foreach (var uid in units)
            //    {
            //        System.Net.Mail.MailMessage email = client.GetMessage(uid, true, "[Gmail]/Sent Mail");
            //        DataRow TempTaskRow2 = TempTaskTable.NewRow();
            //        TempTaskRow2["FromEmail"] = email.Sender;
            //        TempTaskRow2["ToEmail"] = email.From;
            //        TempTaskRow2["Subject"] = email.Subject;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string exceptionCheck = ex.Message;
            //}

            #region 暂时注释
            //FactoryPop3 factory = new FactoryPop3();
            //Pop3 pop = factory.CreatePop3();
            //pop.Pop3Port = 110;
            //pop.Pop3Address = "pop3.163.com";
            //pop.EmailAddress = "q51758018@163.com";
            ////pop.EmailPassword = "ou7225822";
            //pop.EmailPassword = "shouquan163";

            //pop.Authenticate();
            //if (pop.ExitsError)
            //{
            //    List<string> nameList = new List<string>();
            //    var emailCount = pop.GetMailCount();

            //    //获取第一行
            //    for (int i = 1; i <= emailCount; i++)
            //    {
            //        var content = pop.GetMailBodyAsText(i);
            //    }


            //    //备用的方法
            //    //MessageBox.Show(pop.GetMailCount().Tostring());
            //    //MessageBox.Show(pop.GetSendMialAddress(1).Tostring());
            //    //MessageBox.Show(pop.GetMailUID(1).Tostring());
            //    //MessageBox.Show(pop.GetMailSubject(2).Tostring());
            //    //MessageBox.Show(pop.GetMailBodyAsText(1).Tostring());
            //}
            //else
            //{
            //    //MessageBox.Show(pop.ErrorMessage);
            //} 
            #endregion



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
        /// <summary>
        /// 是否存在错误
        /// </summary>
        public abstract bool ExitsError { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public abstract string ErrorMessage { get; set; }
        /// <summary>
        /// POP3端口号
        /// </summary>
        public abstract int Pop3Port { set; get; }
        /// <summary>
        /// POP3地址
        /// </summary>
        public abstract string Pop3Address { set; get; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public abstract string EmailAddress { set; get; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public abstract string EmailPassword { set; get; }

        /// <summary>
        /// 链接至服务器并读取邮件集合
        /// </summary>
        public abstract bool Authenticate();

        /// <summary>
        /// 获取邮件数量
        /// </summary>
        /// <returns></returns>
        public abstract int GetMailCount();

        /// <summary>
        /// 获取发件人
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public abstract string GetSendMialAddress(int mailIndex);

        /// <summary>
        /// 获取邮件的主题
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public abstract string GetMailUID(int mailIndex);

        /// <summary>
        /// 获取邮件发送时间
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public abstract DateTime GetEmailDate(int mailIndex);

        /// <summary>
        /// 获取邮件的UID
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public abstract string GetMailSubject(int mailIndex);

        /// <summary>
        /// 获取邮件正文
        /// </summary>
        /// <param name="mailIndex">邮件顺序</param>
        /// <returns></returns>
        public abstract string GetMailBodyAsText(int mailIndex);

        public abstract bool GetMailAttachment(int mailIndex, string receiveBackpath);


        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailIndex"></param>
        public abstract void DeleteMail(int mailIndex);

        public abstract void Pop3Close();
    }


    /// <summary>
    /// 用于多态,不同的邮箱可能需要不同实现 
    /// </summary>
    public class OpenPopPop3 : Pop3
    {
        /// <summary>
        /// 是否存在错误
        /// </summary>
        public override bool ExitsError { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public override string ErrorMessage { get; set; }
        /// <summary>
        /// POP3端口号
        /// </summary>
        public override int Pop3Port { set; get; }
        /// <summary>
        /// POP3地址
        /// </summary>
        public override string Pop3Address { set; get; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public override string EmailAddress { set; get; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public override string EmailPassword { set; get; }


        private Pop3Client pop3Client;

        private int mailTotalCount;


        /// <summary>
        /// 链接至服务器并读取邮件集合
        /// </summary>
        public override bool Authenticate()
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

        /// <summary>
        /// 获取邮件数量
        /// </summary>
        /// <returns></returns>
        public override int GetMailCount()
        {
            return mailTotalCount;
        }

        /// <summary>
        /// 获取发件人
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override string GetSendMialAddress(int mailIndex)
        {
            RfcMailAddress address = pop3Client.GetMessageHeaders(mailIndex).From;
            return address.Address;
        }

        /// <summary>
        /// 获取收到邮件的日期
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override DateTime GetEmailDate(int mailIndex)
        {
            return pop3Client.GetMessageHeaders(mailIndex).DateSent;
        }


        /// <summary>
        /// 获取邮件的主题
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override string GetMailUID(int mailIndex)
        {
            return pop3Client.GetMessageUid(mailIndex);

        }

        /// <summary>
        /// 获取邮件的UID
        /// </summary>
        /// <param name="mailIndex"></param>
        /// <returns></returns>
        public override string GetMailSubject(int mailIndex)
        {
            return pop3Client.GetMessageHeaders(mailIndex).Subject;
        }

        /// <summary>
        /// 获取邮件正文
        /// </summary>
        /// <param name="mailIndex">邮件顺序</param>
        /// <returns></returns>
        public override string GetMailBodyAsText(int mailIndex)
        {
            Message message = pop3Client.GetMessage(mailIndex);
            MessagePart selectedMessagePart = message.MessagePart;
            if (selectedMessagePart.Body != null)
            {
                return selectedMessagePart.GetBodyAsText();

            }
            //经过查看数据结构发现163的好像不一样,需要重写一下
            return selectedMessagePart.MessageParts[0].GetBodyAsText();
        }

        public override bool GetMailAttachment(int mailIndex, string receiveBackpath)
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

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailIndex"></param>
        public override void DeleteMail(int mailIndex)
        {
            pop3Client.DeleteMessage(mailIndex);
        }

        public override void Pop3Close()
        {
            pop3Client.Disconnect();
            pop3Client.Dispose();
        }
    }

    public class FactoryPop3
    {
        public string Pop3Type = "OpenPop";

        public Pop3 CreatePop3()
        {
            Pop3 pop = null;
            if (Pop3Type == "OpenPop")
            {
                return pop = new OpenPopPop3();
            }
            else
            {
                return null;
            }
        }
    }
}
