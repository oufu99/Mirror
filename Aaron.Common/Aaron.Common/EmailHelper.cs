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
    }
}
