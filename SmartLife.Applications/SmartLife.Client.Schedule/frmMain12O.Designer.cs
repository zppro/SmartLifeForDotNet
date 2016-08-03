namespace SmartLife.Client.Schedule
{
    partial class frmMain12O
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
            this.spLeftRight = new System.Windows.Forms.SplitContainer();
            this.lbRequestLog = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbResponseLog = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spLeftRight)).BeginInit();
            this.spLeftRight.Panel1.SuspendLayout();
            this.spLeftRight.Panel2.SuspendLayout();
            this.spLeftRight.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // spLeftRight
            // 
            this.spLeftRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spLeftRight.Location = new System.Drawing.Point(0, 0);
            this.spLeftRight.Name = "spLeftRight";
            // 
            // spLeftRight.Panel1
            // 
            this.spLeftRight.Panel1.Controls.Add(this.lbRequestLog);
            this.spLeftRight.Panel1.Controls.Add(this.panel1);
            // 
            // spLeftRight.Panel2
            // 
            this.spLeftRight.Panel2.Controls.Add(this.lbResponseLog);
            this.spLeftRight.Panel2.Controls.Add(this.panel2);
            this.spLeftRight.Size = new System.Drawing.Size(860, 543);
            this.spLeftRight.SplitterDistance = 440;
            this.spLeftRight.SplitterWidth = 1;
            this.spLeftRight.TabIndex = 0;
            // 
            // lbRequestLog
            // 
            this.lbRequestLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRequestLog.FormattingEnabled = true;
            this.lbRequestLog.ItemHeight = 12;
            this.lbRequestLog.Location = new System.Drawing.Point(0, 31);
            this.lbRequestLog.Name = "lbRequestLog";
            this.lbRequestLog.Size = new System.Drawing.Size(440, 512);
            this.lbRequestLog.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 31);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "访问节点：";
            // 
            // lbResponseLog
            // 
            this.lbResponseLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResponseLog.FormattingEnabled = true;
            this.lbResponseLog.ItemHeight = 12;
            this.lbResponseLog.Location = new System.Drawing.Point(0, 31);
            this.lbResponseLog.Name = "lbResponseLog";
            this.lbResponseLog.Size = new System.Drawing.Size(419, 512);
            this.lbResponseLog.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(419, 31);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "发送日志：";
            // 
            // frmMain12O
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 543);
            this.Controls.Add(this.spLeftRight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain12O";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主窗口";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain12O_FormClosed);
            this.Load += new System.EventHandler(this.frmMain12O_Load);
            this.Shown += new System.EventHandler(this.frmMain12O_Shown);
            this.spLeftRight.Panel1.ResumeLayout(false);
            this.spLeftRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spLeftRight)).EndInit();
            this.spLeftRight.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spLeftRight;
        private System.Windows.Forms.ListBox lbRequestLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbResponseLog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
    }
}