namespace SmartLife.Client.Merchant
{
    partial class frmSettings
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
            this.gbWeb = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAuthEndPoint = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.gbBiz = new System.Windows.Forms.GroupBox();
            this.chkPlayYY = new System.Windows.Forms.CheckBox();
            this.cbRemindType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAreaSync = new System.Windows.Forms.Button();
            this.gbWeb.SuspendLayout();
            this.gbBiz.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbWeb
            // 
            this.gbWeb.Controls.Add(this.label4);
            this.gbWeb.Controls.Add(this.txtAuthEndPoint);
            this.gbWeb.Location = new System.Drawing.Point(12, 12);
            this.gbWeb.Name = "gbWeb";
            this.gbWeb.Size = new System.Drawing.Size(479, 61);
            this.gbWeb.TabIndex = 4;
            this.gbWeb.TabStop = false;
            this.gbWeb.Text = "认证节点";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "http://";
            // 
            // txtAuthEndPoint
            // 
            this.txtAuthEndPoint.Location = new System.Drawing.Point(66, 25);
            this.txtAuthEndPoint.Name = "txtAuthEndPoint";
            this.txtAuthEndPoint.Size = new System.Drawing.Size(395, 21);
            this.txtAuthEndPoint.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(416, 146);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(314, 146);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gbBiz
            // 
            this.gbBiz.Controls.Add(this.chkPlayYY);
            this.gbBiz.Controls.Add(this.cbRemindType);
            this.gbBiz.Controls.Add(this.label1);
            this.gbBiz.Location = new System.Drawing.Point(12, 79);
            this.gbBiz.Name = "gbBiz";
            this.gbBiz.Size = new System.Drawing.Size(479, 61);
            this.gbBiz.TabIndex = 7;
            this.gbBiz.TabStop = false;
            this.gbBiz.Text = "偏好设置";
            // 
            // chkPlayYY
            // 
            this.chkPlayYY.AutoSize = true;
            this.chkPlayYY.Location = new System.Drawing.Point(210, 27);
            this.chkPlayYY.Name = "chkPlayYY";
            this.chkPlayYY.Size = new System.Drawing.Size(96, 16);
            this.chkPlayYY.TabIndex = 3;
            this.chkPlayYY.Text = "开启语音提醒";
            this.chkPlayYY.UseVisualStyleBackColor = true;
            // 
            // cbRemindType
            // 
            this.cbRemindType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRemindType.FormattingEnabled = true;
            this.cbRemindType.Location = new System.Drawing.Point(66, 25);
            this.cbRemindType.Name = "cbRemindType";
            this.cbRemindType.Size = new System.Drawing.Size(121, 20);
            this.cbRemindType.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "提醒方式";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(21, 146);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(99, 23);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "版本检查";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAreaSync
            // 
            this.btnAreaSync.Location = new System.Drawing.Point(126, 146);
            this.btnAreaSync.Name = "btnAreaSync";
            this.btnAreaSync.Size = new System.Drawing.Size(99, 23);
            this.btnAreaSync.TabIndex = 14;
            this.btnAreaSync.Text = "区域同步";
            this.btnAreaSync.UseVisualStyleBackColor = true;
            this.btnAreaSync.Click += new System.EventHandler(this.btnAreaSync_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 181);
            this.ControlBox = false;
            this.Controls.Add(this.btnAreaSync);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.gbBiz);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbWeb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.gbWeb.ResumeLayout(false);
            this.gbWeb.PerformLayout();
            this.gbBiz.ResumeLayout(false);
            this.gbBiz.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbWeb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAuthEndPoint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox gbBiz;
        private System.Windows.Forms.ComboBox cbRemindType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.CheckBox chkPlayYY;
        private System.Windows.Forms.Button btnAreaSync;
    }
}