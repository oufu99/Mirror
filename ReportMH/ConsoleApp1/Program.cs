using OpenPop.Mime;
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

            ReadPop3();
        }


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
