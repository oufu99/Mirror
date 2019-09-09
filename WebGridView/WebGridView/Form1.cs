using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebGridView
{
    public partial class Form1 : Form
    {
        int count = 0;
        public Form1()
        {
            InitializeComponent();

        }



        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (count > 0)
            {
                return;
            }
            count++;
            HtmlDocument doc = webBrowser1.Document;
            var js = @"document.getElementById('source').value='123';";
            ExecJS(doc, "function sayHello(){" + js + "}", "sayHello");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new Uri("https://www.sojson.com/jsobfuscator.html");

        }

        public string ExecJS(HtmlDocument Doc, string JsFun, string FunNanme)
        {
            HtmlElement ele = Doc.CreateElement("script");
            ele.SetAttribute("type", "text/javascript");
            ele.SetAttribute("text", JsFun);
            Doc.Body.AppendChild(ele);
            return Doc.InvokeScript(FunNanme).ToString();
        }
    }
}
