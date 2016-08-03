namespace SmartLife.Client.SmsTelecom
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtn_frmSetting = new System.Windows.Forms.ToolStripButton();
            this.lblMsg = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rtb_SendSms = new System.Windows.Forms.RichTextBox();
            this.rtb_GetSms = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_frmSetting});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(888, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtn_frmSetting
            // 
            this.tsbtn_frmSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_frmSetting.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_frmSetting.Image")));
            this.tsbtn_frmSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_frmSetting.Name = "tsbtn_frmSetting";
            this.tsbtn_frmSetting.Size = new System.Drawing.Size(45, 22);
            this.tsbtn_frmSetting.Text = "设置...";
            this.tsbtn_frmSetting.Click += new System.EventHandler(this.tsbtn_frmSetting_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMsg.Location = new System.Drawing.Point(413, 274);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(66, 19);
            this.lblMsg.TabIndex = 1;
            this.lblMsg.Text = "验证中";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rtb_SendSms);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtb_GetSms);
            this.splitContainer1.Size = new System.Drawing.Size(888, 590);
            this.splitContainer1.SplitterDistance = 444;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 2;
            // 
            // rtb_SendSms
            // 
            this.rtb_SendSms.BackColor = System.Drawing.SystemColors.ControlDark;
            this.rtb_SendSms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_SendSms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_SendSms.Location = new System.Drawing.Point(0, 0);
            this.rtb_SendSms.Name = "rtb_SendSms";
            this.rtb_SendSms.ReadOnly = true;
            this.rtb_SendSms.RightMargin = 5;
            this.rtb_SendSms.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtb_SendSms.Size = new System.Drawing.Size(444, 590);
            this.rtb_SendSms.TabIndex = 0;
            this.rtb_SendSms.Text = "";
            // 
            // rtb_GetSms
            // 
            this.rtb_GetSms.BackColor = System.Drawing.SystemColors.ControlDark;
            this.rtb_GetSms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtb_GetSms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_GetSms.Location = new System.Drawing.Point(0, 0);
            this.rtb_GetSms.Name = "rtb_GetSms";
            this.rtb_GetSms.ReadOnly = true;
            this.rtb_GetSms.RightMargin = 5;
            this.rtb_GetSms.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtb_GetSms.Size = new System.Drawing.Size(443, 590);
            this.rtb_GetSms.TabIndex = 0;
            this.rtb_GetSms.Text = "";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 615);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电信短信处理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtn_frmSetting;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rtb_SendSms;
        private System.Windows.Forms.RichTextBox rtb_GetSms;
    }
}

