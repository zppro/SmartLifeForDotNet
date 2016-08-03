namespace SmartLife.Client.Seat
{
    partial class frmInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInfo));
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.pnlConference = new System.Windows.Forms.Panel();
            this.lblConferenceName = new System.Windows.Forms.Label();
            this.lbConferenceMember = new System.Windows.Forms.ListBox();
            this.cmenuConference = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmenuitemKick = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuitemMute = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuitemUnMute = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuitemDeaf = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuitemUnDeaf = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlConference.SuspendLayout();
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
            this.webBrowser.TabIndex = 3;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // pnlConference
            // 
            this.pnlConference.BackColor = System.Drawing.Color.White;
            this.pnlConference.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlConference.Controls.Add(this.lblConferenceName);
            this.pnlConference.Controls.Add(this.lbConferenceMember);
            this.pnlConference.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlConference.Location = new System.Drawing.Point(1021, 0);
            this.pnlConference.Name = "pnlConference";
            this.pnlConference.Size = new System.Drawing.Size(200, 768);
            this.pnlConference.TabIndex = 11;
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
            this.lbConferenceMember.Size = new System.Drawing.Size(156, 664);
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
            // frmInfo
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
            this.Name = "frmInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "对象重打开";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInfo_FormClosed);
            this.Load += new System.EventHandler(this.frmInfo_Load);
            this.pnlConference.ResumeLayout(false);
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
    }
}