namespace SmartLife.Client.Gov
{
    partial class frmLogin
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
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblProductVersionComment = new System.Windows.Forms.Label();
            this.ctvObjectNodes = new win.core.controls.ComboTreeView();
            this.llblSettings = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.ForeColor = System.Drawing.Color.White;
            this.lblMsg.Location = new System.Drawing.Point(3, 235);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(144, 22);
            this.lblMsg.TabIndex = 14;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOk.Location = new System.Drawing.Point(202, 200);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 28);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "登录";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(288, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 28);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtPassword.Location = new System.Drawing.Point(202, 140);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(156, 21);
            this.txtPassword.TabIndex = 11;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserName.Location = new System.Drawing.Point(202, 108);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(156, 21);
            this.txtUserName.TabIndex = 10;
            // 
            // lblProductName
            // 
            this.lblProductName.BackColor = System.Drawing.Color.Transparent;
            this.lblProductName.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProductName.ForeColor = System.Drawing.Color.White;
            this.lblProductName.Location = new System.Drawing.Point(18, 47);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(211, 31);
            this.lblProductName.TabIndex = 16;
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblProductVersionComment
            // 
            this.lblProductVersionComment.BackColor = System.Drawing.Color.Transparent;
            this.lblProductVersionComment.ForeColor = System.Drawing.Color.Red;
            this.lblProductVersionComment.Location = new System.Drawing.Point(223, 56);
            this.lblProductVersionComment.Name = "lblProductVersionComment";
            this.lblProductVersionComment.Size = new System.Drawing.Size(70, 22);
            this.lblProductVersionComment.TabIndex = 17;
            this.lblProductVersionComment.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // ctvObjectNodes
            // 
            this.ctvObjectNodes.FormattingEnabled = true;
            this.ctvObjectNodes.Location = new System.Drawing.Point(202, 174);
            this.ctvObjectNodes.Name = "ctvObjectNodes";
            this.ctvObjectNodes.Size = new System.Drawing.Size(156, 20);
            this.ctvObjectNodes.TabIndex = 18;
            this.ctvObjectNodes.TreeViewHeight = 0;
            this.ctvObjectNodes.TreeViewWidth = 0;
            // 
            // llblSettings
            // 
            this.llblSettings.AutoSize = true;
            this.llblSettings.BackColor = System.Drawing.Color.Transparent;
            this.llblSettings.Location = new System.Drawing.Point(337, 4);
            this.llblSettings.Name = "llblSettings";
            this.llblSettings.Size = new System.Drawing.Size(29, 12);
            this.llblSettings.TabIndex = 22;
            this.llblSettings.TabStop = true;
            this.llblSettings.Text = "设置";
            this.llblSettings.Click += new System.EventHandler(this.llblSettings_Click);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SmartLife.Client.Gov.Properties.Resources.loginBG;
            this.ClientSize = new System.Drawing.Size(380, 255);
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
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.Shown += new System.EventHandler(this.frmLogin_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblProductVersionComment;
        private win.core.controls.ComboTreeView ctvObjectNodes;
        private System.Windows.Forms.LinkLabel llblSettings;
    }
}