namespace AutoBuildTool
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.suyaBtn = new System.Windows.Forms.Button();
            this.wsBgBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // suyaBtn
            // 
            this.suyaBtn.Location = new System.Drawing.Point(389, 111);
            this.suyaBtn.Name = "suyaBtn";
            this.suyaBtn.Size = new System.Drawing.Size(144, 69);
            this.suyaBtn.TabIndex = 0;
            this.suyaBtn.Text = "生成新版素雅";
            this.suyaBtn.UseVisualStyleBackColor = true;
            this.suyaBtn.Click += new System.EventHandler(this.suyaBuild);
            // 
            // wsBgBtn
            // 
            this.wsBgBtn.Location = new System.Drawing.Point(86, 111);
            this.wsBgBtn.Name = "wsBgBtn";
            this.wsBgBtn.Size = new System.Drawing.Size(144, 69);
            this.wsBgBtn.TabIndex = 1;
            this.wsBgBtn.Text = "生成WsBg";
            this.wsBgBtn.UseVisualStyleBackColor = true;
            this.wsBgBtn.Click += new System.EventHandler(this.wsBgBuild);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(86, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 69);
            this.button1.TabIndex = 2;
            this.button1.Text = "生成新版米菲";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.mifeiBuild);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(111, 59);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "简易生成";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 357);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.wsBgBtn);
            this.Controls.Add(this.suyaBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button suyaBtn;
        private System.Windows.Forms.Button wsBgBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

