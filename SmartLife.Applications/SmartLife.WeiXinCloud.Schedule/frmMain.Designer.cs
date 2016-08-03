namespace SmartLife.WeiXinCloud.Schedule
{
    partial class frmMain
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.txtObjectId = new System.Windows.Forms.TextBox();
            this.lblObjectIdTitle = new System.Windows.Forms.Label();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbRequestLog = new System.Windows.Forms.ListBox();
            this.lbResponseLog = new System.Windows.Forms.ListBox();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.txtObjectId);
            this.pnlTop.Controls.Add(this.lblObjectIdTitle);
            this.pnlTop.Controls.Add(this.btnStartStop);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(994, 67);
            this.pnlTop.TabIndex = 0;
            // 
            // txtObjectId
            // 
            this.txtObjectId.Location = new System.Drawing.Point(87, 12);
            this.txtObjectId.Multiline = true;
            this.txtObjectId.Name = "txtObjectId";
            this.txtObjectId.Size = new System.Drawing.Size(572, 39);
            this.txtObjectId.TabIndex = 2;
            // 
            // lblObjectIdTitle
            // 
            this.lblObjectIdTitle.AutoSize = true;
            this.lblObjectIdTitle.Location = new System.Drawing.Point(22, 24);
            this.lblObjectIdTitle.Name = "lblObjectIdTitle";
            this.lblObjectIdTitle.Size = new System.Drawing.Size(59, 12);
            this.lblObjectIdTitle.TabIndex = 1;
            this.lblObjectIdTitle.Text = "ObjectId:";
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(785, 12);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(176, 39);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "开始";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 67);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbRequestLog);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbResponseLog);
            this.splitContainer1.Size = new System.Drawing.Size(994, 505);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 1;
            // 
            // lbRequestLog
            // 
            this.lbRequestLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRequestLog.FormattingEnabled = true;
            this.lbRequestLog.ItemHeight = 12;
            this.lbRequestLog.Location = new System.Drawing.Point(0, 0);
            this.lbRequestLog.Name = "lbRequestLog";
            this.lbRequestLog.Size = new System.Drawing.Size(500, 505);
            this.lbRequestLog.TabIndex = 0;
            // 
            // lbResponseLog
            // 
            this.lbResponseLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResponseLog.FormattingEnabled = true;
            this.lbResponseLog.ItemHeight = 12;
            this.lbResponseLog.Location = new System.Drawing.Point(0, 0);
            this.lbResponseLog.Name = "lbResponseLog";
            this.lbResponseLog.Size = new System.Drawing.Size(492, 505);
            this.lbResponseLog.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 572);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbRequestLog;
        private System.Windows.Forms.ListBox lbResponseLog;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label lblObjectIdTitle;
        private System.Windows.Forms.TextBox txtObjectId;
    }
}