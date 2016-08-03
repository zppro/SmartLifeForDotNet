namespace SmartLife.Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlReminder = new System.Windows.Forms.Panel();
            this.tlpReminder = new System.Windows.Forms.TableLayoutPanel();
            this.lblFamilyReminderTitle = new System.Windows.Forms.Label();
            this.wbFamily = new System.Windows.Forms.WebBrowser();
            this.wbLife = new System.Windows.Forms.WebBrowser();
            this.lblLifeReminderTitle = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.lblEmergencyReminderTitle = new System.Windows.Forms.Label();
            this.wbEmergency = new System.Windows.Forms.WebBrowser();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tlblProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlblSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlblUserCodeName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssbtnSetBusyFree = new System.Windows.Forms.ToolStripSplitButton();
            this.示闲ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.示忙ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtnTestTC_IPCC = new System.Windows.Forms.ToolStripButton();
            this.tbtnSimulateEmergencyService = new System.Windows.Forms.ToolStripButton();
            this.tbtnSimulateFamilyCallService = new System.Windows.Forms.ToolStripButton();
            this.tbtnSimulateLifeService = new System.Windows.Forms.ToolStripButton();
            this.tbtnSettings = new System.Windows.Forms.ToolStripButton();
            this.tbtnReload = new System.Windows.Forms.ToolStripButton();
            this.pnlMain.SuspendLayout();
            this.pnlReminder.SuspendLayout();
            this.tlpReminder.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlReminder);
            this.pnlMain.Controls.Add(this.webBrowser);
            this.pnlMain.Controls.Add(this.statusStrip1);
            this.pnlMain.Controls.Add(this.toolStrip1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1008, 787);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlReminder
            // 
            this.pnlReminder.Controls.Add(this.tlpReminder);
            this.pnlReminder.Location = new System.Drawing.Point(3, 373);
            this.pnlReminder.Name = "pnlReminder";
            this.pnlReminder.Size = new System.Drawing.Size(246, 388);
            this.pnlReminder.TabIndex = 3;
            this.pnlReminder.Visible = false;
            // 
            // tlpReminder
            // 
            this.tlpReminder.BackColor = System.Drawing.Color.White;
            this.tlpReminder.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpReminder.ColumnCount = 2;
            this.tlpReminder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tlpReminder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tlpReminder.Controls.Add(this.lblFamilyReminderTitle, 1, 3);
            this.tlpReminder.Controls.Add(this.wbFamily, 0, 3);
            this.tlpReminder.Controls.Add(this.wbLife, 0, 2);
            this.tlpReminder.Controls.Add(this.lblLifeReminderTitle, 1, 2);
            this.tlpReminder.Controls.Add(this.pnlTitle, 0, 0);
            this.tlpReminder.Controls.Add(this.lblEmergencyReminderTitle, 1, 1);
            this.tlpReminder.Controls.Add(this.wbEmergency, 0, 1);
            this.tlpReminder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpReminder.Location = new System.Drawing.Point(0, 0);
            this.tlpReminder.Margin = new System.Windows.Forms.Padding(0);
            this.tlpReminder.Name = "tlpReminder";
            this.tlpReminder.RowCount = 4;
            this.tlpReminder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpReminder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tlpReminder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tlpReminder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpReminder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpReminder.Size = new System.Drawing.Size(246, 388);
            this.tlpReminder.TabIndex = 1;
            // 
            // lblFamilyReminderTitle
            // 
            this.lblFamilyReminderTitle.AutoSize = true;
            this.lblFamilyReminderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFamilyReminderTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblFamilyReminderTitle.Location = new System.Drawing.Point(225, 284);
            this.lblFamilyReminderTitle.Name = "lblFamilyReminderTitle";
            this.lblFamilyReminderTitle.Size = new System.Drawing.Size(25, 103);
            this.lblFamilyReminderTitle.TabIndex = 6;
            this.lblFamilyReminderTitle.Text = "亲人通话";
            this.lblFamilyReminderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // wbFamily
            // 
            this.wbFamily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbFamily.Location = new System.Drawing.Point(4, 287);
            this.wbFamily.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbFamily.Name = "wbFamily";
            this.wbFamily.ScrollBarsEnabled = false;
            this.wbFamily.Size = new System.Drawing.Size(214, 97);
            this.wbFamily.TabIndex = 5;
            this.wbFamily.WebBrowserShortcutsEnabled = false;
            // 
            // wbLife
            // 
            this.wbLife.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbLife.Location = new System.Drawing.Point(4, 161);
            this.wbLife.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbLife.Name = "wbLife";
            this.wbLife.ScrollBarsEnabled = false;
            this.wbLife.Size = new System.Drawing.Size(214, 119);
            this.wbLife.TabIndex = 4;
            this.wbLife.WebBrowserShortcutsEnabled = false;
            // 
            // lblLifeReminderTitle
            // 
            this.lblLifeReminderTitle.AutoSize = true;
            this.lblLifeReminderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLifeReminderTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblLifeReminderTitle.Location = new System.Drawing.Point(225, 158);
            this.lblLifeReminderTitle.Name = "lblLifeReminderTitle";
            this.lblLifeReminderTitle.Size = new System.Drawing.Size(25, 125);
            this.lblLifeReminderTitle.TabIndex = 2;
            this.lblLifeReminderTitle.Text = "生活服务";
            this.lblLifeReminderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(216)))), ((int)(((byte)(230)))));
            this.tlpReminder.SetColumnSpan(this.pnlTitle, 2);
            this.pnlTitle.Controls.Add(this.pbIcon);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(1, 1);
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(252, 30);
            this.pnlTitle.TabIndex = 0;
            // 
            // pbIcon
            // 
            this.pbIcon.Image = global::SmartLife.Client.Properties.Resources.left2arrow;
            this.pbIcon.Location = new System.Drawing.Point(223, 5);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(16, 16);
            this.pbIcon.TabIndex = 0;
            this.pbIcon.TabStop = false;
            this.pbIcon.Click += new System.EventHandler(this.pbIcon_Click);
            // 
            // lblEmergencyReminderTitle
            // 
            this.lblEmergencyReminderTitle.AutoSize = true;
            this.lblEmergencyReminderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmergencyReminderTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEmergencyReminderTitle.Location = new System.Drawing.Point(225, 32);
            this.lblEmergencyReminderTitle.Name = "lblEmergencyReminderTitle";
            this.lblEmergencyReminderTitle.Size = new System.Drawing.Size(25, 125);
            this.lblEmergencyReminderTitle.TabIndex = 1;
            this.lblEmergencyReminderTitle.Text = "紧急服务";
            this.lblEmergencyReminderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // wbEmergency
            // 
            this.wbEmergency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbEmergency.Location = new System.Drawing.Point(4, 35);
            this.wbEmergency.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbEmergency.Name = "wbEmergency";
            this.wbEmergency.ScrollBarsEnabled = false;
            this.wbEmergency.Size = new System.Drawing.Size(214, 119);
            this.wbEmergency.TabIndex = 3;
            this.wbEmergency.WebBrowserShortcutsEnabled = false;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(0, 25);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(1008, 739);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.WebBrowserShortcutsEnabled = false;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlblProcess,
            this.tlblSpace,
            this.tlblUserCodeName,
            this.tssbtnSetBusyFree});
            this.statusStrip1.Location = new System.Drawing.Point(0, 764);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 23);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tlblProcess
            // 
            this.tlblProcess.AutoSize = false;
            this.tlblProcess.BackColor = System.Drawing.SystemColors.Control;
            this.tlblProcess.Name = "tlblProcess";
            this.tlblProcess.Size = new System.Drawing.Size(200, 18);
            this.tlblProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlblSpace
            // 
            this.tlblSpace.AutoSize = false;
            this.tlblSpace.Name = "tlblSpace";
            this.tlblSpace.Size = new System.Drawing.Size(400, 18);
            // 
            // tlblUserCodeName
            // 
            this.tlblUserCodeName.AutoSize = false;
            this.tlblUserCodeName.BackColor = System.Drawing.SystemColors.Control;
            this.tlblUserCodeName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlblUserCodeName.ForeColor = System.Drawing.Color.Blue;
            this.tlblUserCodeName.MergeIndex = 999;
            this.tlblUserCodeName.Name = "tlblUserCodeName";
            this.tlblUserCodeName.Size = new System.Drawing.Size(300, 18);
            // 
            // tssbtnSetBusyFree
            // 
            this.tssbtnSetBusyFree.AutoSize = false;
            this.tssbtnSetBusyFree.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tssbtnSetBusyFree.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.示闲ToolStripMenuItem,
            this.示忙ToolStripMenuItem});
            this.tssbtnSetBusyFree.Image = ((System.Drawing.Image)(resources.GetObject("tssbtnSetBusyFree.Image")));
            this.tssbtnSetBusyFree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbtnSetBusyFree.Name = "tssbtnSetBusyFree";
            this.tssbtnSetBusyFree.Size = new System.Drawing.Size(80, 21);
            this.tssbtnSetBusyFree.Text = "空闲";
            // 
            // 示闲ToolStripMenuItem
            // 
            this.示闲ToolStripMenuItem.Name = "示闲ToolStripMenuItem";
            this.示闲ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.示闲ToolStripMenuItem.Text = "示闲";
            this.示闲ToolStripMenuItem.Click += new System.EventHandler(this.示闲ToolStripMenuItem_Click);
            // 
            // 示忙ToolStripMenuItem
            // 
            this.示忙ToolStripMenuItem.Name = "示忙ToolStripMenuItem";
            this.示忙ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.示忙ToolStripMenuItem.Text = "示忙";
            this.示忙ToolStripMenuItem.Click += new System.EventHandler(this.示忙ToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnTestTC_IPCC,
            this.tbtnSimulateEmergencyService,
            this.tbtnSimulateFamilyCallService,
            this.tbtnSimulateLifeService,
            this.tbtnSettings,
            this.tbtnReload});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbtnTestTC_IPCC
            // 
            this.tbtnTestTC_IPCC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbtnTestTC_IPCC.Image = ((System.Drawing.Image)(resources.GetObject("tbtnTestTC_IPCC.Image")));
            this.tbtnTestTC_IPCC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnTestTC_IPCC.Name = "tbtnTestTC_IPCC";
            this.tbtnTestTC_IPCC.Size = new System.Drawing.Size(107, 22);
            this.tbtnTestTC_IPCC.Text = "测试TC-IPCC平台";
            this.tbtnTestTC_IPCC.Visible = false;
            this.tbtnTestTC_IPCC.Click += new System.EventHandler(this.tbtnTestTC_IPCC_Click);
            // 
            // tbtnSimulateEmergencyService
            // 
            this.tbtnSimulateEmergencyService.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbtnSimulateEmergencyService.Image = ((System.Drawing.Image)(resources.GetObject("tbtnSimulateEmergencyService.Image")));
            this.tbtnSimulateEmergencyService.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSimulateEmergencyService.Name = "tbtnSimulateEmergencyService";
            this.tbtnSimulateEmergencyService.Size = new System.Drawing.Size(108, 22);
            this.tbtnSimulateEmergencyService.Text = "模拟紧急服务接入";
            this.tbtnSimulateEmergencyService.Visible = false;
            this.tbtnSimulateEmergencyService.Click += new System.EventHandler(this.tbtnSimulateEmergencyService_Click);
            // 
            // tbtnSimulateFamilyCallService
            // 
            this.tbtnSimulateFamilyCallService.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbtnSimulateFamilyCallService.Image = ((System.Drawing.Image)(resources.GetObject("tbtnSimulateFamilyCallService.Image")));
            this.tbtnSimulateFamilyCallService.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSimulateFamilyCallService.Name = "tbtnSimulateFamilyCallService";
            this.tbtnSimulateFamilyCallService.Size = new System.Drawing.Size(108, 22);
            this.tbtnSimulateFamilyCallService.Text = "模拟亲人通话接入";
            this.tbtnSimulateFamilyCallService.Visible = false;
            this.tbtnSimulateFamilyCallService.Click += new System.EventHandler(this.tbtnSimulateFamilyCallService_Click);
            // 
            // tbtnSimulateLifeService
            // 
            this.tbtnSimulateLifeService.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbtnSimulateLifeService.Image = ((System.Drawing.Image)(resources.GetObject("tbtnSimulateLifeService.Image")));
            this.tbtnSimulateLifeService.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSimulateLifeService.Name = "tbtnSimulateLifeService";
            this.tbtnSimulateLifeService.Size = new System.Drawing.Size(108, 22);
            this.tbtnSimulateLifeService.Text = "模拟生活服务接入";
            this.tbtnSimulateLifeService.Visible = false;
            this.tbtnSimulateLifeService.Click += new System.EventHandler(this.tbtnSimulateLifeService_Click);
            // 
            // tbtnSettings
            // 
            this.tbtnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbtnSettings.Image = ((System.Drawing.Image)(resources.GetObject("tbtnSettings.Image")));
            this.tbtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSettings.Name = "tbtnSettings";
            this.tbtnSettings.Size = new System.Drawing.Size(45, 22);
            this.tbtnSettings.Text = "设置...";
            this.tbtnSettings.Click += new System.EventHandler(this.tbtnSettings_Click);
            // 
            // tbtnReload
            // 
            this.tbtnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbtnReload.Image = ((System.Drawing.Image)(resources.GetObject("tbtnReload.Image")));
            this.tbtnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnReload.Name = "tbtnReload";
            this.tbtnReload.Size = new System.Drawing.Size(36, 22);
            this.tbtnReload.Text = "刷新";
            this.tbtnReload.Click += new System.EventHandler(this.tbtnReload_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 787);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智慧生活";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlReminder.ResumeLayout(false);
            this.tlpReminder.ResumeLayout(false);
            this.tlpReminder.PerformLayout();
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbtnTestTC_IPCC;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tlblUserCodeName;
        private System.Windows.Forms.ToolStripButton tbtnSimulateEmergencyService;
        private System.Windows.Forms.ToolStripButton tbtnSettings;
        private System.Windows.Forms.ToolStripStatusLabel tlblSpace;
        private System.Windows.Forms.ToolStripStatusLabel tlblProcess;
        private System.Windows.Forms.ToolStripButton tbtnSimulateLifeService;
        private System.Windows.Forms.ToolStripButton tbtnReload;
        private System.Windows.Forms.ToolStripSplitButton tssbtnSetBusyFree;
        private System.Windows.Forms.ToolStripMenuItem 示闲ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 示忙ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tbtnSimulateFamilyCallService;
        private System.Windows.Forms.Panel pnlReminder;
        private System.Windows.Forms.TableLayoutPanel tlpReminder;
        private System.Windows.Forms.WebBrowser wbLife;
        private System.Windows.Forms.Label lblLifeReminderTitle;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.WebBrowser wbEmergency;
        private System.Windows.Forms.Label lblFamilyReminderTitle;
        private System.Windows.Forms.WebBrowser wbFamily;
        private System.Windows.Forms.Label lblEmergencyReminderTitle;
    }
}