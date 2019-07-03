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
        //编译工具MsBuild的路径  这里不写在配置文件中是因为后面会被引用
        string dirkPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin";
        List<CheckBox> checkBoxList = new List<CheckBox>();

        public BuildYmt()
        {
            InitializeComponent();


        }
        private void BuildYmt_Load(object sender, EventArgs e)
        {
            #region 生成CheckBox
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
                    //这里只写了左边部分,所以不用家  X坐标
                    leftY = leftY + 40;
                }
                CheckBox cBox = new CheckBox();
                cBox.Text = leftList[i];
                cBox.Location = new Point(leftX, leftY);
                cBox.Width = this.postionBoxLeft.Width + 80;
                this.Controls.Add(cBox);
                checkBoxList.Add(cBox);
            }
            this.postionBoxLeft.Visible = false;
            this.postionBoxLeft.Checked = true;
            #endregion


            var defaultFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DefaultCheck.txt");
            //要更新的项目
            var defaultList = File.ReadAllLines(defaultFilePath).ToList();
            checkBoxList.Where(c => defaultList.Contains(c.Text)).ToList().ForEach(c => c.Checked = true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //判断是否选中
            List<string> checkList = new List<string>();

            foreach (var item in checkBoxList)
            {
                if (item.Checked)
                {
                    checkList.Add(item.Text);
                }
            }
            List<Task> taskList = new List<Task>();
            foreach (var item in checkList)
            {
                var projectFullPath = projectBasePath + item + @"\" + item + ".csproj";
                //输出目录
                var outPath = projectBasePath + item + @"\" + item + @"\bin\Debug\netcoreapp2.1\";
                //编译
                TfHelper.Build(dirkPath, projectFullPath, outPath);
                Regex reg = new Regex(@"[0-9]+ 个错误", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            }
            MessageBox.Show("全部生成完毕");
        }
    }
}
