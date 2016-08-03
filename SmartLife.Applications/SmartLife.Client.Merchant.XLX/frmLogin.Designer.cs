namespace SmartLife.Client.Merchant
{
    partial class frmLogin
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblProductVersionComment = new System.Windows.Forms.Label();
            this.ctvObjectNodes = new win.core.controls.ComboTreeView();
            this.llblSettings = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserName.Location = new System.Drawing.Point(154, 71);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(174, 21);
            this.txtUserName.TabIndex = 10;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtPassword.Location = new System.Drawing.Point(154, 101);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(174, 21);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOk.Location = new System.Drawing.Point(162, 180);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 28);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "登录";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(248, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.ForeColor = System.Drawing.Color.White;
            this.lblMsg.Location = new System.Drawing.Point(12, 181);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(144, 22);
            this.lblMsg.TabIndex = 8;
            // 
            // lblProductName
            // 
            this.lblProductName.BackColor = System.Drawing.Color.Transparent;
            this.lblProductName.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProductName.ForeColor = System.Drawing.Color.White;
            this.lblProductName.Location = new System.Drawing.Point(82, 221);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(211, 31);
            this.lblProductName.TabIndex = 18;
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblProductVersionComment
            // 
            this.lblProductVersionComment.BackColor = System.Drawing.Color.Transparent;
            this.lblProductVersionComment.ForeColor = System.Drawing.Color.Red;
            this.lblProductVersionComment.Location = new System.Drawing.Point(287, 230);
            this.lblProductVersionComment.Name = "lblProductVersionComment";
            this.lblProductVersionComment.Size = new System.Drawing.Size(70, 22);
            this.lblProductVersionComment.TabIndex = 19;
            this.lblProductVersionComment.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // ctvObjectNodes
            // 
            this.ctvObjectNodes.FormattingEnabled = true;
            this.ctvObjectNodes.Location = new System.Drawing.Point(154, 131);
            this.ctvObjectNodes.Name = "ctvObjectNodes";
            this.ctvObjectNodes.Size = new System.Drawing.Size(174, 20);
            this.ctvObjectNodes.TabIndex = 20;
            this.ctvObjectNodes.TreeViewHeight = 0;
            this.ctvObjectNodes.TreeViewWidth = 0;
            this.ctvObjectNodes.Visible = false;
            // 
            // llblSettings
            // 
            this.llblSettings.AutoSize = true;
            this.llblSettings.BackColor = System.Drawing.Color.Transparent;
            this.llblSettings.Location = new System.Drawing.Point(377, 239);
            this.llblSettings.Name = "llblSettings";
            this.llblSettings.Size = new System.Drawing.Size(29, 12);
            this.llblSettings.TabIndex = 21;
            this.llblSettings.TabStop = true;
            this.llblSettings.Text = "设置";
            this.llblSettings.Click += new System.EventHandler(this.llblSettings_Click);
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::SmartLife.Client.Merchant.Properties.Resources.loginBG;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(418, 262);
            this.ControlBox = false;
            this.Controls.Add(this.llblSettings);
            this.Controls.Add(this.ctvObjectNodes);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.lblProductVersionComment);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLogin";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "商家登录";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblProductVersionComment;
        private win.core.controls.ComboTreeView ctvObjectNodes;
        private System.Windows.Forms.LinkLabel llblSettings;
    }
}

