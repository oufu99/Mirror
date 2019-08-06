namespace RemoteDesktop
{
    partial class ServerList
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
            this.serverdatalist = new System.Windows.Forms.DataGridView();
            this.服务器备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.服务器IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.服务器端口 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.服务器账号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.服务器密码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.打开方式 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.serverdatalist)).BeginInit();
            this.SuspendLayout();
            // 
            // serverdatalist
            // 
            this.serverdatalist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.serverdatalist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.服务器备注,
            this.服务器IP,
            this.服务器端口,
            this.服务器账号,
            this.服务器密码,
            this.打开方式,
            this.btn_edit});
            this.serverdatalist.Location = new System.Drawing.Point(2, 54);
            this.serverdatalist.Name = "serverdatalist";
            this.serverdatalist.RowHeadersVisible = false;
            this.serverdatalist.RowTemplate.Height = 23;
            this.serverdatalist.Size = new System.Drawing.Size(657, 353);
            this.serverdatalist.TabIndex = 3;
            this.serverdatalist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.serverdatalist_CellContentClick);
            this.serverdatalist.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.serverdatalist_CellDoubleClick);
            // 
            // 服务器备注
            // 
            this.服务器备注.DataPropertyName = "Remark";
            this.服务器备注.HeaderText = "服务器备注";
            this.服务器备注.Name = "服务器备注";
            // 
            // 服务器IP
            // 
            this.服务器IP.DataPropertyName = "Server";
            this.服务器IP.HeaderText = "服务器IP";
            this.服务器IP.Name = "服务器IP";
            // 
            // 服务器端口
            // 
            this.服务器端口.DataPropertyName = "Port";
            this.服务器端口.HeaderText = "服务器端口";
            this.服务器端口.Name = "服务器端口";
            // 
            // 服务器账号
            // 
            this.服务器账号.DataPropertyName = "UserName";
            this.服务器账号.HeaderText = "服务器账号";
            this.服务器账号.Name = "服务器账号";
            // 
            // 服务器密码
            // 
            this.服务器密码.DataPropertyName = "Password";
            this.服务器密码.HeaderText = "服务器密码";
            this.服务器密码.Name = "服务器密码";
            this.服务器密码.Width = 140;
            // 
            // 打开方式
            // 
            this.打开方式.DataPropertyName = "Type";
            this.打开方式.HeaderText = "打开方式";
            this.打开方式.Name = "打开方式";
            this.打开方式.Visible = false;
            // 
            // btn_edit
            // 
            this.btn_edit.HeaderText = "操作";
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Text = "编辑";
            this.btn_edit.UseColumnTextForButtonValue = true;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(12, 12);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 4;
            this.btn_add.Text = "新增";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(108, 12);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(75, 23);
            this.btn_del.TabIndex = 5;
            this.btn_del.Text = "删除";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // ServerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 495);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.serverdatalist);
            this.Name = "ServerList";
            this.Text = "远程桌面管理";
            ((System.ComponentModel.ISupportInitialize)(this.serverdatalist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView serverdatalist;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.DataGridViewTextBoxColumn 服务器备注;
        private System.Windows.Forms.DataGridViewTextBoxColumn 服务器IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn 服务器端口;
        private System.Windows.Forms.DataGridViewTextBoxColumn 服务器账号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 服务器密码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 打开方式;
        private System.Windows.Forms.DataGridViewButtonColumn btn_edit;
    }
}

