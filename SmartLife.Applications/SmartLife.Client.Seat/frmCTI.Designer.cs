namespace SmartLife.Client.Seat
{
    partial class frmCTI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCTI));
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.pnlConference = new System.Windows.Forms.Panel();
            this.pnlCountdown = new System.Windows.Forms.Panel();
            this.lblCountdown = new System.Windows.Forms.Label();
            this.lblSec = new System.Windows.Forms.Label();
            this.lblCountdownFoot = new System.Windows.Forms.Label();
            this.lblCountdownHead = new System.Windows.Forms.Label();
            this.lblConferenceName = new System.Windows.Forms.Label();
            this.lbConferenceMember = new System.Windows.Forms.ListBox();
            this.cmenuConference = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmenuitemKick = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuitemMute = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuitemUnMute = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuitemDeaf = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuitemUnDeaf = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlConference.SuspendLayout();
            this.pnlCountdown.SuspendLayout();
            this.cmenuConference.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScrollBarsEnabled = false;
            this.webBrowser.Size = new System.Drawing.Size(1024, 768);
            this.webBrowser.TabIndex = 2;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // pnlConference
            // 
            this.pnlConference.BackColor = System.Drawing.Color.White;
            this.pnlConference.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlConference.Controls.Add(this.pnlCountdown);
            this.pnlConference.Controls.Add(this.lblConferenceName);
            this.pnlConference.Controls.Add(this.lbConferenceMember);
            this.pnlConference.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlConference.Location = new System.Drawing.Point(1021, 0);
            this.pnlConference.Name = "pnlConference";
            this.pnlConference.Size = new System.Drawing.Size(200, 768);
            this.pnlConference.TabIndex = 10;
            // 
            // pnlCountdown
            // 
            this.pnlCountdown.BackgroundImage = global::SmartLife.Client.Seat.Properties.Resources.BG_Countdown;
            this.pnlCountdown.Controls.Add(this.lblCountdown);
            this.pnlCountdown.Controls.Add(this.lblSec);
            this.pnlCountdown.Controls.Add(this.lblCountdownFoot);
            this.pnlCountdown.Controls.Add(this.lblCountdownHead);
            this.pnlCountdown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCountdown.Location = new System.Drawing.Point(0, 541);
            this.pnlCountdown.Name = "pnlCountdown";
            this.pnlCountdown.Size = new System.Drawing.Size(196, 223);
            this.pnlCountdown.TabIndex = 10;
            this.pnlCountdown.Visible = false;
            // 
            // lblCountdown
            // 
            this.lblCountdown.BackColor = System.Drawing.Color.Transparent;
            this.lblCountdown.Font = new System.Drawing.Font("宋体", 80F);
            this.lblCountdown.Location = new System.Drawing.Point(51, 66);
            this.lblCountdown.Name = "lblCountdown";
            this.lblCountdown.Size = new System.Drawing.Size(85, 102);
            this.lblCountdown.TabIndex = 3;
            this.lblCountdown.Text = "8";
            // 
            // lblSec
            // 
            this.lblSec.AutoSize = true;
            this.lblSec.BackColor = System.Drawing.Color.Transparent;
            this.lblSec.Font = new System.Drawing.Font("宋体", 20F);
            this.lblSec.Location = new System.Drawing.Point(131, 135);
            this.lblSec.Name = "lblSec";
            this.lblSec.Size = new System.Drawing.Size(39, 27);
            this.lblSec.TabIndex = 2;
            this.lblSec.Text = "秒";
            // 
            // lblCountdownFoot
            // 
            this.lblCountdownFoot.AutoSize = true;
            this.lblCountdownFoot.BackColor = System.Drawing.Color.Transparent;
            this.lblCountdownFoot.Font = new System.Drawing.Font("宋体", 20F);
            this.lblCountdownFoot.Location = new System.Drawing.Point(25, 178);
            this.lblCountdownFoot.Name = "lblCountdownFoot";
            this.lblCountdownFoot.Size = new System.Drawing.Size(147, 27);
            this.lblCountdownFoot.TabIndex = 1;
            this.lblCountdownFoot.Text = "后关闭窗口";
            // 
            // lblCountdownHead
            // 
            this.lblCountdownHead.AutoSize = true;
            this.lblCountdownHead.BackColor = System.Drawing.Color.Transparent;
            this.lblCountdownHead.Font = new System.Drawing.Font("宋体", 12F);
            this.lblCountdownHead.Location = new System.Drawing.Point(32, 16);
            this.lblCountdownHead.Name = "lblCountdownHead";
            this.lblCountdownHead.Size = new System.Drawing.Size(120, 16);
            this.lblCountdownHead.TabIndex = 0;
            this.lblCountdownHead.Text = "跳屏关闭倒计时";
            // 
            // lblConferenceName
            // 
            this.lblConferenceName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblConferenceName.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblConferenceName.ForeColor = System.Drawing.Color.Blue;
            this.lblConferenceName.Location = new System.Drawing.Point(0, 0);
            this.lblConferenceName.Name = "lblConferenceName";
            this.lblConferenceName.Size = new System.Drawing.Size(196, 46);
            this.lblConferenceName.TabIndex = 9;
            this.lblConferenceName.Text = "多方通话";
            this.lblConferenceName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbConferenceMember
            // 
            this.lbConferenceMember.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbConferenceMember.FormattingEnabled = true;
            this.lbConferenceMember.ItemHeight = 12;
            this.lbConferenceMember.Location = new System.Drawing.Point(21, 60);
            this.lbConferenceMember.Name = "lbConferenceMember";
            this.lbConferenceMember.Size = new System.Drawing.Size(156, 448);
            this.lbConferenceMember.TabIndex = 0;
            this.lbConferenceMember.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbConferenceMember_MouseDown);
            // 
            // cmenuConference
            // 
            this.cmenuConference.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmenuitemKick,
            this.cmenuitemMute,
            this.cmenuitemUnMute,
            this.cmenuitemDeaf,
            this.cmenuitemUnDeaf});
            this.cmenuConference.Name = "cmenuConference";
            this.cmenuConference.Size = new System.Drawing.Size(149, 114);
            // 
            // cmenuitemKick
            // 
            this.cmenuitemKick.Name = "cmenuitemKick";
            this.cmenuitemKick.Size = new System.Drawing.Size(148, 22);
            this.cmenuitemKick.Text = "踢出多方通话";
            this.cmenuitemKick.Click += new System.EventHandler(this.cmenuitemKick_Click);
            // 
            // cmenuitemMute
            // 
            this.cmenuitemMute.Name = "cmenuitemMute";
            this.cmenuitemMute.Size = new System.Drawing.Size(148, 22);
            this.cmenuitemMute.Text = "禁麦";
            this.cmenuitemMute.Click += new System.EventHandler(this.cmenuitemMute_Click);
            // 
            // cmenuitemUnMute
            // 
            this.cmenuitemUnMute.Name = "cmenuitemUnMute";
            this.cmenuitemUnMute.Size = new System.Drawing.Size(148, 22);
            this.cmenuitemUnMute.Text = "取消禁麦";
            this.cmenuitemUnMute.Click += new System.EventHandler(this.cmenuitemUnMute_Click);
            // 
            // cmenuitemDeaf
            // 
            this.cmenuitemDeaf.Name = "cmenuitemDeaf";
            this.cmenuitemDeaf.Size = new System.Drawing.Size(148, 22);
            this.cmenuitemDeaf.Text = "静音";
            this.cmenuitemDeaf.Click += new System.EventHandler(this.cmenuitemDeaf_Click);
            // 
            // cmenuitemUnDeaf
            // 
            this.cmenuitemUnDeaf.Name = "cmenuitemUnDeaf";
            this.cmenuitemUnDeaf.Size = new System.Drawing.Size(148, 22);
            this.cmenuitemUnDeaf.Text = "取消静音";
            this.cmenuitemUnDeaf.Click += new System.EventHandler(this.cmenuitemUnDeaf_Click);
            // 
            // frmCTI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 768);
            this.Controls.Add(this.pnlConference);
            this.Controls.Add(this.webBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCTI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "弹屏处理";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCTI_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCTI_FormClosed);
            this.Load += new System.EventHandler(this.frmCTI_Load);
            this.pnlConference.ResumeLayout(false);
            this.pnlCountdown.ResumeLayout(false);
            this.pnlCountdown.PerformLayout();
            this.cmenuConference.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Panel pnlConference;
        private System.Windows.Forms.Label lblConferenceName;
        private System.Windows.Forms.ListBox lbConferenceMember;
        private System.Windows.Forms.ContextMenuStrip cmenuConference;
        private System.Windows.Forms.ToolStripMenuItem cmenuitemKick;
        private System.Windows.Forms.ToolStripMenuItem cmenuitemMute;
        private System.Windows.Forms.ToolStripMenuItem cmenuitemUnMute;
        private System.Windows.Forms.ToolStripMenuItem cmenuitemDeaf;
        private System.Windows.Forms.ToolStripMenuItem cmenuitemUnDeaf;
        private System.Windows.Forms.Panel pnlCountdown;
        private System.Windows.Forms.Label lblCountdown;
        private System.Windows.Forms.Label lblSec;
        private System.Windows.Forms.Label lblCountdownFoot;
        private System.Windows.Forms.Label lblCountdownHead;
    }
}