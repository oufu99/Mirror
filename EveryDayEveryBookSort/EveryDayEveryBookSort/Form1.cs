using Aaron.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveryDayEveryBookSort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dirPath = "";
            #region 文件弹出选择
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();     //显示选择文件对话框
            //openFileDialog1.InitialDirectory = @"D:\MyLove\VimD\userPlugins\";
            //openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //openFileDialog1.FilterIndex = 2;
            //openFileDialog1.RestoreDirectory = true;

            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    dirPath = openFileDialog1.FileName;          //显示文件路径
            //}
            #endregion


            #region 文件夹选择
            FolderBrowserDialog folder = new FolderBrowserDialog();
            //设置初始路径
            folder.SelectedPath = @"D:\a 认识方法论\";
            folder.ShowDialog();
            dirPath = folder.SelectedPath;
            #endregion

            IOHelper.EveryDayEveryBookSort(dirPath);
            MessageBox.Show("整理完毕");
        }
    }
}
