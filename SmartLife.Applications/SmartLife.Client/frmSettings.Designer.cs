namespace SmartLife.Client
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
            this.gbSmartCare = new System.Windows.Forms.GroupBox();
            this.cbbRunMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVirPath = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRemoteHost = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbVOIP = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCallCenterNodeAddress = new System.Windows.Forms.TextBox();
            this.gbSmartCare.SuspendLayout();
            this.gbVOIP.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSmartCare
            // 
            this.gbSmartCare.Controls.Add(this.cbbRunMode);
            this.gbSmartCare.Controls.Add(this.label3);
            this.gbSmartCare.Controls.Add(this.label2);
            this.gbSmartCare.Controls.Add(this.txtVirPath);
            this.gbSmartCare.Controls.Add(this.btnTest);
            this.gbSmartCare.Controls.Add(this.label1);
            this.gbSmartCare.Controls.Add(this.txtRemoteHost);
            this.gbSmartCare.Location = new System.Drawing.Point(12, 12);
            this.gbSmartCare.Name = "gbSmartCare";
            this.gbSmartCare.Size = new System.Drawing.Size(531, 117);
            this.gbSmartCare.TabIndex = 0;
            this.gbSmartCare.TabStop = false;
            this.gbSmartCare.Text = "智慧养老主机";
            // 
            // cbbRunMode
            // 
            this.cbbRunMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbRunMode.FormattingEnabled = true;
            this.cbbRunMode.Location = new System.Drawing.Point(72, 31);
            this.cbbRunMode.Name = "cbbRunMode";
            this.cbbRunMode.Size = new System.Drawing.Size(121, 20);
            this.cbbRunMode.TabIndex = 6;
            this.cbbRunMode.SelectedIndexChanged += new System.EventHandler(this.cbbRunMode_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "运行模式:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "/";
            // 
            // txtVirPath
            // 
            this.txtVirPath.Location = new System.Drawing.Point(268, 71);
            this.txtVirPath.Name = "txtVirPath";
            this.txtVirPath.Size = new System.Drawing.Size(166, 21);
            this.txtVirPath.TabIndex = 3;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(440, 69);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "测试连接";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "http://";
            // 
            // txtRemoteHost
            // 
            this.txtRemoteHost.Location = new System.Drawing.Point(54, 71);
            this.txtRemoteHost.Name = "txtRemoteHost";
            this.txtRemoteHost.Size = new System.Drawing.Size(191, 21);
            this.txtRemoteHost.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(178, 299);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(280, 299);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbVOIP
            // 
            this.gbVOIP.Controls.Add(this.label4);
            this.gbVOIP.Controls.Add(this.txtCallCenterNodeAddress);
            this.gbVOIP.Location = new System.Drawing.Point(12, 146);
            this.gbVOIP.Name = "gbVOIP";
            this.gbVOIP.Size = new System.Drawing.Size(531, 61);
            this.gbVOIP.TabIndex = 3;
            this.gbVOIP.TabStop = false;
            this.gbVOIP.Text = "语音通话";
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
            // txtCallCenterNodeAddress
            // 
            this.txtCallCenterNodeAddress.Location = new System.Drawing.Point(54, 25);
            this.txtCallCenterNodeAddress.Name = "txtCallCenterNodeAddress";
            this.txtCallCenterNodeAddress.Size = new System.Drawing.Size(380, 21);
            this.txtCallCenterNodeAddress.TabIndex = 0;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 345);
            this.Controls.Add(this.gbVOIP);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbSmartCare);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.gbSmartCare.ResumeLayout(false);
            this.gbSmartCare.PerformLayout();
            this.gbVOIP.ResumeLayout(false);
            this.gbVOIP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSmartCare;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRemoteHost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVirPath;
        private System.Windows.Forms.GroupBox gbVOIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCallCenterNodeAddress;
        private System.Windows.Forms.ComboBox cbbRunMode;
        private System.Windows.Forms.Label label3;
    }
}