using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tfs.Common;

namespace BuildYmt
{
    public partial class BuildYmt : Form
    {
        //项目的根文件夹
        string projectBasePath = @"E:\ZPCode\zp.ymt\";
        //编译工具MsBuild的路径
        string dirkPath = ConfigurationManager.AppSettings["dirkPath"];



        public BuildYmt()
        {
            InitializeComponent();
            //生成checkBox
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Project.txt");
            //要更新的项目
            var fileStr = File.ReadAllLines(filePath);
            var leftList = fileStr.ToList();

            //自动生成checkBox控件
            int leftX = 0;
            int leftY = 0;
            int rigthX = 0;
            int rigthY = 0;
            //生成左边的 
            for (int i = 0; i < leftList.Count; i++)
            {
                if (i == 0)
                {
                    leftX = this.postionBoxLeft.Location.X;
                    leftY = this.postionBoxLeft.Location.Y + 40;
                }
                else
                {
                    leftY = leftY + 40;
                    rigthY = rigthY + 40;
                }
                CheckBox cBox = new CheckBox();
                cBox.Text = leftList[i];
                cBox.Location = new Point(leftX, leftY);
                cBox.Width = this.postionBoxLeft.Width;
                this.Controls.Add(cBox);
            }

        }
        private void BuildYmt_Load(object sender, EventArgs e)
        {
            this.postionBoxLeft.Visible = false;
            this.postionBoxLeft.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //foreach (var item in fileStr)
            //{
            //    var projectFullPath = projectBasePath + item + @"\" + item + ".csproj";
            //    //输出目录
            //    var outPath = projectBasePath + item + @"\" + item + @"\bin\Debug\netcoreapp2.1\";
            //    var buildRstStr = TfHelper.Build(dirkPath, projectFullPath, outPath);
            //    Regex reg = new Regex(@"[0-9]+ 个错误", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //    var rst = reg.Match(buildRstStr);

            //}



            //var checkEd = checkBox1.Checked;
            //if (checkEd)
            //{
            //    var outPath = @"E:\ZPCode\zp.ymt\ZP.YMT.ActivityAdmin.Basics\bin\Debug\netcoreapp2.1\";
            //    var buildRstStr = TfHelper.Build(dirkPath, path, outPath);
            //    Regex reg = new Regex(@"[0-9]+ 个错误", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //    var rst = reg.Match(buildRstStr);
            //}
        }


    }
}
