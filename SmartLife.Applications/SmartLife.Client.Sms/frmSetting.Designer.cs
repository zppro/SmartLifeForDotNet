namespace SmartLife.Client.Sms
{
    partial class frmSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_Txt_Password = new System.Windows.Forms.TextBox();
            this.tb_Txt_LoginName = new System.Windows.Forms.TextBox();
            this.tb_Txt_SpCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_Digital_Password = new System.Windows.Forms.TextBox();
            this.tb_Digital_LoginName = new System.Windows.Forms.TextBox();
            this.tb_Digital_SpCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtAuthEndPoint = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tb_SmsEndPoint = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAuthSmsEndPoint = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "企业编号：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_Txt_Password);
            this.groupBox1.Controls.Add(this.tb_Txt_LoginName);
            this.groupBox1.Controls.Add(this.tb_Txt_SpCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 207);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文本节点";
            // 
            // tb_Txt_Password
            // 
            this.tb_Txt_Password.Location = new System.Drawing.Point(95, 149);
            this.tb_Txt_Password.Name = "tb_Txt_Password";
            this.tb_Txt_Password.PasswordChar = '*';
            this.tb_Txt_Password.Size = new System.Drawing.Size(177, 21);
            this.tb_Txt_Password.TabIndex = 5;
            // 
            // tb_Txt_LoginName
            // 
            this.tb_Txt_LoginName.Location = new System.Drawing.Point(95, 93);
            this.tb_Txt_LoginName.Name = "tb_Txt_LoginName";
            this.tb_Txt_LoginName.Size = new System.Drawing.Size(177, 21);
            this.tb_Txt_LoginName.TabIndex = 4;
            // 
            // tb_Txt_SpCode
            // 
            this.tb_Txt_SpCode.Location = new System.Drawing.Point(95, 37);
            this.tb_Txt_SpCode.Name = "tb_Txt_SpCode";
            this.tb_Txt_SpCode.Size = new System.Drawing.Size(177, 21);
            this.tb_Txt_SpCode.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "密    码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用 户 名：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_Digital_Password);
            this.groupBox2.Controls.Add(this.tb_Digital_LoginName);
            this.groupBox2.Controls.Add(this.tb_Digital_SpCode);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(323, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(297, 207);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "指令节点";
            // 
            // tb_Digital_Password
            // 
            this.tb_Digital_Password.Location = new System.Drawing.Point(99, 149);
            this.tb_Digital_Password.Name = "tb_Digital_Password";
            this.tb_Digital_Password.PasswordChar = '*';
            this.tb_Digital_Password.Size = new System.Drawing.Size(177, 21);
            this.tb_Digital_Password.TabIndex = 6;
            // 
            // tb_Digital_LoginName
            // 
            this.tb_Digital_LoginName.Location = new System.Drawing.Point(99, 93);
            this.tb_Digital_LoginName.Name = "tb_Digital_LoginName";
            this.tb_Digital_LoginName.Size = new System.Drawing.Size(177, 21);
            this.tb_Digital_LoginName.TabIndex = 5;
            // 
            // tb_Digital_SpCode
            // 
            this.tb_Digital_SpCode.Location = new System.Drawing.Point(99, 37);
            this.tb_Digital_SpCode.Name = "tb_Digital_SpCode";
            this.tb_Digital_SpCode.Size = new System.Drawing.Size(177, 21);
            this.tb_Digital_SpCode.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "密    码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "用 户 名：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "企业编号：";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(148, 471);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(485, 471);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtAuthEndPoint);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(34, 312);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(626, 70);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "认证节点";
            // 
            // txtAuthEndPoint
            // 
            this.txtAuthEndPoint.Location = new System.Drawing.Point(71, 29);
            this.txtAuthEndPoint.Name = "txtAuthEndPoint";
            this.txtAuthEndPoint.Size = new System.Drawing.Size(534, 21);
            this.txtAuthEndPoint.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "http://";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tb_SmsEndPoint);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Location = new System.Drawing.Point(34, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(626, 293);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "短信接口节点";
            // 
            // tb_SmsEndPoint
            // 
            this.tb_SmsEndPoint.Location = new System.Drawing.Point(101, 31);
            this.tb_SmsEndPoint.Name = "tb_SmsEndPoint";
            this.tb_SmsEndPoint.Size = new System.Drawing.Size(498, 21);
            this.tb_SmsEndPoint.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "短信节点：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtAuthSmsEndPoint);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(34, 388);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(626, 67);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "推送短信节点";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "http://";
            // 
            // txtAuthSmsEndPoint
            // 
            this.txtAuthSmsEndPoint.Location = new System.Drawing.Point(71, 27);
            this.txtAuthSmsEndPoint.Name = "txtAuthSmsEndPoint";
            this.txtAuthSmsEndPoint.Size = new System.Drawing.Size(534, 21);
            this.txtAuthSmsEndPoint.TabIndex = 1;
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 506);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置节点设置";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_Txt_Password;
        private System.Windows.Forms.TextBox tb_Txt_LoginName;
        private System.Windows.Forms.TextBox tb_Txt_SpCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_Digital_Password;
        private System.Windows.Forms.TextBox tb_Digital_LoginName;
        private System.Windows.Forms.TextBox tb_Digital_SpCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtAuthEndPoint;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tb_SmsEndPoint;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtAuthSmsEndPoint;
        private System.Windows.Forms.Label label9;
    }
}

