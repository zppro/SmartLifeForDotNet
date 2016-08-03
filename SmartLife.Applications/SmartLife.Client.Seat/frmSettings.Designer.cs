namespace SmartLife.Client.Seat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlatformAddress = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.chkServiceOnlineFlag = new System.Windows.Forms.CheckBox();
            this.cbxPlayTone = new System.Windows.Forms.CheckBox();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnTestSpeed = new System.Windows.Forms.Button();
            this.btnCloud = new System.Windows.Forms.Button();
            this.btnSpare = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "平台地址";
            // 
            // txtPlatformAddress
            // 
            this.txtPlatformAddress.Location = new System.Drawing.Point(84, 61);
            this.txtPlatformAddress.Name = "txtPlatformAddress";
            this.txtPlatformAddress.Size = new System.Drawing.Size(408, 21);
            this.txtPlatformAddress.TabIndex = 2;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(234, 144);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(99, 23);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "测试连接";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(507, 144);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(405, 144);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(24, 144);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(99, 23);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "版本检查";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // chkServiceOnlineFlag
            // 
            this.chkServiceOnlineFlag.AutoSize = true;
            this.chkServiceOnlineFlag.Location = new System.Drawing.Point(510, 63);
            this.chkServiceOnlineFlag.Name = "chkServiceOnlineFlag";
            this.chkServiceOnlineFlag.Size = new System.Drawing.Size(72, 16);
            this.chkServiceOnlineFlag.TabIndex = 12;
            this.chkServiceOnlineFlag.Text = "在线客服";
            this.chkServiceOnlineFlag.UseVisualStyleBackColor = true;
            // 
            // cbxPlayTone
            // 
            this.cbxPlayTone.AutoSize = true;
            this.cbxPlayTone.Location = new System.Drawing.Point(27, 103);
            this.cbxPlayTone.Name = "cbxPlayTone";
            this.cbxPlayTone.Size = new System.Drawing.Size(132, 16);
            this.cbxPlayTone.TabIndex = 14;
            this.cbxPlayTone.Text = "开启示忙下来电提醒";
            this.cbxPlayTone.UseVisualStyleBackColor = true;
            this.cbxPlayTone.CheckedChanged += new System.EventHandler(this.cbxPlayTone_CheckedChanged);
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "HH:mm";
            this.dtpStart.Enabled = false;
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(165, 98);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.ShowUpDown = true;
            this.dtpStart.Size = new System.Drawing.Size(56, 21);
            this.dtpStart.TabIndex = 15;
            this.dtpStart.Value = new System.DateTime(2014, 10, 17, 11, 1, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "-";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "HH:mm";
            this.dtpEnd.Enabled = false;
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(246, 98);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.ShowUpDown = true;
            this.dtpEnd.Size = new System.Drawing.Size(56, 21);
            this.dtpEnd.TabIndex = 17;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            // 
            // btnTestSpeed
            // 
            this.btnTestSpeed.Location = new System.Drawing.Point(129, 144);
            this.btnTestSpeed.Name = "btnTestSpeed";
            this.btnTestSpeed.Size = new System.Drawing.Size(99, 23);
            this.btnTestSpeed.TabIndex = 18;
            this.btnTestSpeed.Text = "测试网速";
            this.btnTestSpeed.UseVisualStyleBackColor = true;
            this.btnTestSpeed.Click += new System.EventHandler(this.btnTestSpeed_Click);
            // 
            // btnCloud
            // 
            this.btnCloud.Location = new System.Drawing.Point(24, 19);
            this.btnCloud.Name = "btnCloud";
            this.btnCloud.Size = new System.Drawing.Size(75, 23);
            this.btnCloud.TabIndex = 19;
            this.btnCloud.Text = "云地址";
            this.btnCloud.UseVisualStyleBackColor = true;
            this.btnCloud.Click += new System.EventHandler(this.btnCloud_Click);
            // 
            // btnSpare
            // 
            this.btnSpare.Location = new System.Drawing.Point(129, 19);
            this.btnSpare.Name = "btnSpare";
            this.btnSpare.Size = new System.Drawing.Size(75, 23);
            this.btnSpare.TabIndex = 19;
            this.btnSpare.Text = "备用地址";
            this.btnSpare.UseVisualStyleBackColor = true;
            this.btnSpare.Click += new System.EventHandler(this.btnSpare_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 207);
            this.Controls.Add(this.btnSpare);
            this.Controls.Add(this.btnCloud);
            this.Controls.Add(this.btnTestSpeed);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.cbxPlayTone);
            this.Controls.Add(this.chkServiceOnlineFlag);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPlatformAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置对话框";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSettings_FormClosed);
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlatformAddress;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.CheckBox chkServiceOnlineFlag;
        private System.Windows.Forms.CheckBox cbxPlayTone;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnTestSpeed;
        private System.Windows.Forms.Button btnCloud;
        private System.Windows.Forms.Button btnSpare;
    }
}