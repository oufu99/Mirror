using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    public class EmailHelper
    {
        public static void SendEmail(string smtpCode, string toEmail, string fromEmail, string subject, string content)
        {
            //实例化一个发送邮件类。
            MailMessage mailMessage = new MailMessage();
            //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
            mailMessage.From = new MailAddress(fromEmail);
            //收件人邮箱地址。
            mailMessage.To.Add(new MailAddress(toEmail));
            //邮件标题。
            mailMessage.Subject = subject;
            //邮件内容。
            mailMessage.Body = content;
            //实例化一个SmtpClient类。
            SmtpClient client = new SmtpClient();
            //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
            client.Host = "smtp.163.com";
            //使用安全加密连接。
            client.EnableSsl = true;
            //不和请求一块发送。
            client.UseDefaultCredentials = false;
            //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
            client.Credentials = new NetworkCredential(fromEmail, smtpCode);
            //发送
            client.Send(mailMessage);
        }

        //获取自己的所有邮件
        public static void ReadPop3()
        {
            List<string> fileList = new List<string>();
            using (Pop3Client client = new Pop3Client())
            {
                if (client.Connected)
                {
                    client.Disconnect();
                }
                // Connect to the server, false means don't use ssl
                client.Connect("pop.qq.com", 995, true);
                // Authenticate ourselves towards the server by email account and password
                client.Authenticate("qq邮箱", "授权码");
                var infos = client.GetMessageInfos();
                //email count
                int messageCount = client.GetMessageCount();

                //i = 1 is the first email; 1 is the oldest email
                for (int i = messageCount; i >= messageCount - 2; i--)
                {
                    Message message = client.GetMessage(i);
                    var date = DateTime.Parse(message.Headers.Date);

                    string sender = message.Headers.From.DisplayName;
                    string from = message.Headers.From.Address;
                    string subject = message.Headers.Subject;
                    DateTime Datesent = message.Headers.DateSent;

                    MessagePart messagePart = message.MessagePart;

                    //email body, 
                    string body = " ";
                    if (messagePart.IsText)
                    {
                        body = messagePart.GetBodyAsText();
                    }
                    else if (messagePart.IsMultiPart)
                    {
                        MessagePart plainTextPart = message.FindFirstPlainTextVersion();
                        if (plainTextPart != null)
                        {
                            // The message had a text/plain version - show that one
                            body = plainTextPart.GetBodyAsText();
                        }
                        else
                        {
                            // Try to find a body to show in some of the other text versions
                            List<MessagePart> textVersions = message.FindAllTextVersions();
                            if (textVersions.Count >= 1)
                                body = textVersions[0].GetBodyAsText();
                            else
                                body = "<<OpenPop>> Cannot find a text version body in this message.";
                        }
                    }
                    Console.WriteLine(body);
                    Console.WriteLine("----------------");
                    fileList.Add(body);

                }
                Console.WriteLine("读取结束");
                foreach (var item in fileList)
                {
                    System.IO.File.AppendAllText(@"d:\jialin.txt", item + "\n");
                }
                Console.WriteLine("写入完毕");
                Console.ReadLine();
            }
        }
    }
}
