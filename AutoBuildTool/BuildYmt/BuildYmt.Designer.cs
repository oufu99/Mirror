namespace BuildYmt
{
    partial class BuildYmt
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
            this.postionBoxLeft = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // postionBoxLeft
            // 
            this.postionBoxLeft.AutoSize = true;
            this.postionBoxLeft.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.postionBoxLeft.Location = new System.Drawing.Point(27, 44);
            this.postionBoxLeft.Name = "postionBoxLeft";
            this.postionBoxLeft.Size = new System.Drawing.Size(194, 18);
            this.postionBoxLeft.TabIndex = 0;
            this.postionBoxLeft.Text = "目标,诺森德 冲冲冲冲冲冲";
            this.postionBoxLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.postionBoxLeft.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(339, 450);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 52);
            this.button1.TabIndex = 1;
            this.button1.Text = "生成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BuildYmt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 642);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.postionBoxLeft);
            this.Name = "BuildYmt";
            this.Text = "BuildYmt";
            this.Load += new System.EventHandler(this.BuildYmt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox postionBoxLeft;
        private System.Windows.Forms.Button button1;
    }
}

