namespace RemoteRunClient
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toggleBtn = new System.Windows.Forms.Button();
            this.HideBtn = new System.Windows.Forms.Button();
            this.selectExeBtn = new System.Windows.Forms.Button();
            this.exeLabel = new System.Windows.Forms.Label();
            this.IPtextBox = new System.Windows.Forms.TextBox();
            this.serverCheckBox = new System.Windows.Forms.CheckBox();
            this.argBox = new System.Windows.Forms.TextBox();
            this.argLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // toggleBtn
            // 
            this.toggleBtn.Location = new System.Drawing.Point(613, 74);
            this.toggleBtn.Margin = new System.Windows.Forms.Padding(4);
            this.toggleBtn.Name = "toggleBtn";
            this.toggleBtn.Size = new System.Drawing.Size(152, 57);
            this.toggleBtn.TabIndex = 0;
            this.toggleBtn.Text = "启动服务器";
            this.toggleBtn.UseVisualStyleBackColor = true;
            this.toggleBtn.Click += new System.EventHandler(this.ToggleBtn_Click);
            // 
            // HideBtn
            // 
            this.HideBtn.Location = new System.Drawing.Point(453, 74);
            this.HideBtn.Margin = new System.Windows.Forms.Padding(4);
            this.HideBtn.Name = "HideBtn";
            this.HideBtn.Size = new System.Drawing.Size(152, 57);
            this.HideBtn.TabIndex = 1;
            this.HideBtn.Text = "隐藏窗口";
            this.HideBtn.UseVisualStyleBackColor = true;
            this.HideBtn.Click += new System.EventHandler(this.HideBtn_Click);
            // 
            // selectExeBtn
            // 
            this.selectExeBtn.Location = new System.Drawing.Point(293, 74);
            this.selectExeBtn.Margin = new System.Windows.Forms.Padding(4);
            this.selectExeBtn.Name = "selectExeBtn";
            this.selectExeBtn.Size = new System.Drawing.Size(152, 57);
            this.selectExeBtn.TabIndex = 2;
            this.selectExeBtn.Text = "选择程序";
            this.selectExeBtn.UseVisualStyleBackColor = true;
            this.selectExeBtn.Click += new System.EventHandler(this.selectExeBtn_Click);
            // 
            // exeLabel
            // 
            this.exeLabel.AutoSize = true;
            this.exeLabel.Location = new System.Drawing.Point(13, 13);
            this.exeLabel.Name = "exeLabel";
            this.exeLabel.Size = new System.Drawing.Size(109, 24);
            this.exeLabel.TabIndex = 3;
            this.exeLabel.Text = "程序: 未设置";
            // 
            // IPtextBox
            // 
            this.IPtextBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IPtextBox.Location = new System.Drawing.Point(12, 81);
            this.IPtextBox.Name = "IPtextBox";
            this.IPtextBox.Size = new System.Drawing.Size(259, 39);
            this.IPtextBox.TabIndex = 4;
            this.IPtextBox.Text = "127.0.0.1:8088";
            // 
            // serverCheckBox
            // 
            this.serverCheckBox.AutoSize = true;
            this.serverCheckBox.Location = new System.Drawing.Point(12, 47);
            this.serverCheckBox.Name = "serverCheckBox";
            this.serverCheckBox.Size = new System.Drawing.Size(154, 28);
            this.serverCheckBox.TabIndex = 5;
            this.serverCheckBox.Text = "WS客户端模式";
            this.serverCheckBox.UseVisualStyleBackColor = true;
            this.serverCheckBox.CheckedChanged += new System.EventHandler(this.serverCheckBox_CheckedChanged);
            // 
            // argBox
            // 
            this.argBox.Location = new System.Drawing.Point(293, 36);
            this.argBox.Name = "argBox";
            this.argBox.Size = new System.Drawing.Size(152, 31);
            this.argBox.TabIndex = 6;
            this.argBox.TextChanged += new System.EventHandler(this.argBox_TextChanged);
            // 
            // argLabel
            // 
            this.argLabel.AutoSize = true;
            this.argLabel.Location = new System.Drawing.Point(205, 39);
            this.argLabel.Name = "argLabel";
            this.argLabel.Size = new System.Drawing.Size(82, 24);
            this.argLabel.TabIndex = 7;
            this.argLabel.Text = "启动参数";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 144);
            this.Controls.Add(this.argLabel);
            this.Controls.Add(this.argBox);
            this.Controls.Add(this.serverCheckBox);
            this.Controls.Add(this.IPtextBox);
            this.Controls.Add(this.exeLabel);
            this.Controls.Add(this.selectExeBtn);
            this.Controls.Add(this.HideBtn);
            this.Controls.Add(this.toggleBtn);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "远程执行";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button toggleBtn;
        private System.Windows.Forms.Button HideBtn;
        private System.Windows.Forms.Button selectExeBtn;
        private System.Windows.Forms.Label exeLabel;
        private System.Windows.Forms.TextBox IPtextBox;
        private System.Windows.Forms.CheckBox serverCheckBox;
        private System.Windows.Forms.TextBox argBox;
        private System.Windows.Forms.Label argLabel;
    }
}

