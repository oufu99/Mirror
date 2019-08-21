using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class GongSu : Form
    {
        public GongSu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double gs = double.Parse(textBox1.Text);
            double jg = double.Parse(textBox2.Text);
            var result = ((100 + gs) * 0.01) / jg;
            textBox3.Text = result.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            double jg = double.Parse(textBox2.Text);
            var result = double.Parse(textBox3.Text);
            var gs = (result * jg) * 100 - 100;
            textBox1.Text = gs.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
        }
    }
}
