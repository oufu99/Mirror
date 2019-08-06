namespace RemoteDesktop
{
    partial class AddServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tb_remark = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_ip = new System.Windows.Forms.TextBox();
            this.tb_port = new System.Windows.Forms.TextBox();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.tb_pwd = new System.Windows.Forms.TextBox();
            this.btn_addserver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器备注:";
            // 
            // tb_remark
            // 
            this.tb_remark.Location = new System.Drawing.Point(106, 30);
            this.tb_remark.Name = "tb_remark";
            this.tb_remark.Size = new System.Drawing.Size(167, 21);
            this.tb_remark.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "服务器IP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "服务器端口:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "服务器账号:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "服务器密码:";
            // 
            // tb_ip
            // 
            this.tb_ip.Location = new System.Drawing.Point(106, 68);
            this.tb_ip.Name = "tb_ip";
            this.tb_ip.Size = new System.Drawing.Size(167, 21);
            this.tb_ip.TabIndex = 6;
            // 
            // tb_port
            // 
            this.tb_port.Location = new System.Drawing.Point(106, 103);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(167, 21);
            this.tb_port.TabIndex = 7;
            this.tb_port.Text = "3389";
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(106, 143);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(167, 21);
            this.tb_username.TabIndex = 8;
            // 
            // tb_pwd
            // 
            this.tb_pwd.Location = new System.Drawing.Point(106, 182);
            this.tb_pwd.Name = "tb_pwd";
            this.tb_pwd.Size = new System.Drawing.Size(167, 21);
            this.tb_pwd.TabIndex = 9;
            // 
            // btn_addserver
            // 
            this.btn_addserver.Location = new System.Drawing.Point(93, 233);
            this.btn_addserver.Name = "btn_addserver";
            this.btn_addserver.Size = new System.Drawing.Size(75, 23);
            this.btn_addserver.TabIndex = 10;
            this.btn_addserver.Text = "确认";
            this.btn_addserver.UseVisualStyleBackColor = true;
            this.btn_addserver.Click += new System.EventHandler(this.btn_addserver_Click);
            // 
            // AddServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 284);
            this.Controls.Add(this.btn_addserver);
            this.Controls.Add(this.tb_pwd);
            this.Controls.Add(this.tb_username);
            this.Controls.Add(this.tb_port);
            this.Controls.Add(this.tb_ip);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_remark);
            this.Controls.Add(this.label1);
            this.Name = "AddServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddServer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_remark;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_ip;
        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.TextBox tb_pwd;
        private System.Windows.Forms.Button btn_addserver;
    }
}