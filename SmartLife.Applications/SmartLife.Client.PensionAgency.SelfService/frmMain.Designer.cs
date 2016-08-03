namespace SmartLife.Client.PensionAgency.SelfService
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCheckUp = new System.Windows.Forms.Button();
            this.btnDining = new System.Windows.Forms.Button();
            this.btnCourse = new System.Windows.Forms.Button();
            this.btnAccount = new System.Windows.Forms.Button();
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.xClock = new win.core.controls.SevenSegmentClock();
            this.lblTechSupport = new System.Windows.Forms.Label();
            this.lblStationName = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblVer = new System.Windows.Forms.Label();
            this.lblProductVersionComent = new System.Windows.Forms.Label();
            this.lblSettings = new System.Windows.Forms.Label();
            this.xContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.在线更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlBackground.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.xContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(867, 698);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCheckUp
            // 
            this.btnCheckUp.BackgroundImage = global::SmartLife.Client.PensionAgency.SelfService.Properties.Resources.button_bg;
            this.btnCheckUp.Font = new System.Drawing.Font("华文行楷", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCheckUp.ForeColor = System.Drawing.Color.White;
            this.btnCheckUp.Location = new System.Drawing.Point(26, 31);
            this.btnCheckUp.Name = "btnCheckUp";
            this.btnCheckUp.Size = new System.Drawing.Size(193, 85);
            this.btnCheckUp.TabIndex = 10;
            this.btnCheckUp.Text = "体检";
            this.btnCheckUp.UseVisualStyleBackColor = true;
            this.btnCheckUp.Click += new System.EventHandler(this.btnCheckUp_Click);
            // 
            // btnDining
            // 
            this.btnDining.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDining.BackgroundImage")));
            this.btnDining.Font = new System.Drawing.Font("华文行楷", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDining.ForeColor = System.Drawing.Color.White;
            this.btnDining.Location = new System.Drawing.Point(26, 132);
            this.btnDining.Name = "btnDining";
            this.btnDining.Size = new System.Drawing.Size(193, 85);
            this.btnDining.TabIndex = 11;
            this.btnDining.Text = "用餐";
            this.btnDining.UseVisualStyleBackColor = true;
            this.btnDining.Click += new System.EventHandler(this.btnDining_Click);
            // 
            // btnCourse
            // 
            this.btnCourse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCourse.BackgroundImage")));
            this.btnCourse.Font = new System.Drawing.Font("华文行楷", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCourse.ForeColor = System.Drawing.Color.White;
            this.btnCourse.Location = new System.Drawing.Point(26, 233);
            this.btnCourse.Name = "btnCourse";
            this.btnCourse.Size = new System.Drawing.Size(193, 85);
            this.btnCourse.TabIndex = 12;
            this.btnCourse.Text = "课程";
            this.btnCourse.UseVisualStyleBackColor = true;
            this.btnCourse.Click += new System.EventHandler(this.btnCourse_Click);
            // 
            // btnAccount
            // 
            this.btnAccount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAccount.BackgroundImage")));
            this.btnAccount.Font = new System.Drawing.Font("华文行楷", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAccount.ForeColor = System.Drawing.Color.White;
            this.btnAccount.Location = new System.Drawing.Point(26, 334);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(193, 85);
            this.btnAccount.TabIndex = 13;
            this.btnAccount.Text = "账户";
            this.btnAccount.UseVisualStyleBackColor = true;
            this.btnAccount.Click += new System.EventHandler(this.btnAccount_Click);
            // 
            // pnlBackground
            // 
            this.pnlBackground.BackColor = System.Drawing.Color.Transparent;
            this.pnlBackground.Controls.Add(this.pnlBottom);
            this.pnlBackground.Controls.Add(this.splitContainer1);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBackground.Location = new System.Drawing.Point(0, 118);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(1024, 650);
            this.pnlBackground.TabIndex = 14;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.xClock);
            this.pnlBottom.Controls.Add(this.lblTechSupport);
            this.pnlBottom.Controls.Add(this.lblStationName);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 590);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1024, 60);
            this.pnlBottom.TabIndex = 16;
            // 
            // xClock
            // 
            this.xClock.ClockColor = System.Drawing.Color.Lime;
            this.xClock.DateTime = new System.DateTime(2015, 11, 6, 10, 17, 22, 0);
            this.xClock.IsDrawShadow = true;
            this.xClock.IsTimerEnable = true;
            this.xClock.Location = new System.Drawing.Point(911, 19);
            this.xClock.Name = "xClock";
            this.xClock.SevenSegmentClockStyle = win.core.controls.SevenSegmentClockStyle.TimeOnly;
            this.xClock.Size = new System.Drawing.Size(101, 28);
            this.xClock.TabIndex = 1;
            this.xClock.Timer = null;
            this.xClock.Click += new System.EventHandler(this.xClock_Click);
            // 
            // lblTechSupport
            // 
            this.lblTechSupport.AutoSize = true;
            this.lblTechSupport.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTechSupport.Location = new System.Drawing.Point(12, 27);
            this.lblTechSupport.Name = "lblTechSupport";
            this.lblTechSupport.Size = new System.Drawing.Size(89, 20);
            this.lblTechSupport.TabIndex = 3;
            this.lblTechSupport.Text = "技术支持";
            // 
            // lblStationName
            // 
            this.lblStationName.Font = new System.Drawing.Font("宋体", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStationName.ForeColor = System.Drawing.Color.White;
            this.lblStationName.Location = new System.Drawing.Point(209, 17);
            this.lblStationName.Name = "lblStationName";
            this.lblStationName.Size = new System.Drawing.Size(606, 34);
            this.lblStationName.TabIndex = 2;
            this.lblStationName.Text = "机构名称读取中...";
            this.lblStationName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCheckUp);
            this.splitContainer1.Panel2.Controls.Add(this.btnDining);
            this.splitContainer1.Panel2.Controls.Add(this.btnCourse);
            this.splitContainer1.Panel2.Controls.Add(this.btnAccount);
            this.splitContainer1.Size = new System.Drawing.Size(1024, 650);
            this.splitContainer1.SplitterDistance = 777;
            this.splitContainer1.TabIndex = 15;
            // 
            // lblVer
            // 
            this.lblVer.BackColor = System.Drawing.Color.Transparent;
            this.lblVer.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblVer.Location = new System.Drawing.Point(970, 79);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(42, 18);
            this.lblVer.TabIndex = 5;
            this.lblVer.Text = "版本...";
            this.lblVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProductVersionComent
            // 
            this.lblProductVersionComent.BackColor = System.Drawing.Color.Transparent;
            this.lblProductVersionComent.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblProductVersionComent.Location = new System.Drawing.Point(932, 58);
            this.lblProductVersionComent.Name = "lblProductVersionComent";
            this.lblProductVersionComent.Size = new System.Drawing.Size(80, 18);
            this.lblProductVersionComent.TabIndex = 4;
            this.lblProductVersionComent.Text = "产品版本类型";
            this.lblProductVersionComent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSettings
            // 
            this.lblSettings.AutoSize = true;
            this.lblSettings.BackColor = System.Drawing.Color.Transparent;
            this.lblSettings.ContextMenuStrip = this.xContextMenu;
            this.lblSettings.Font = new System.Drawing.Font("宋体", 9F);
            this.lblSettings.ForeColor = System.Drawing.Color.White;
            this.lblSettings.Location = new System.Drawing.Point(931, 9);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(41, 12);
            this.lblSettings.TabIndex = 21;
            this.lblSettings.Text = "≡设置";
            this.lblSettings.Click += new System.EventHandler(this.lblSettings_Click);
            // 
            // xContextMenu
            // 
            this.xContextMenu.BackColor = System.Drawing.Color.Transparent;
            this.xContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.在线更新ToolStripMenuItem});
            this.xContextMenu.Name = "xContextMenu";
            this.xContextMenu.Size = new System.Drawing.Size(125, 26);
            // 
            // 在线更新ToolStripMenuItem
            // 
            this.在线更新ToolStripMenuItem.Name = "在线更新ToolStripMenuItem";
            this.在线更新ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.在线更新ToolStripMenuItem.Text = "在线更新";
            this.在线更新ToolStripMenuItem.Click += new System.EventHandler(this.在线更新ToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SmartLife.Client.PensionAgency.SelfService.Properties.Resources.main_bg;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.lblSettings);
            this.Controls.Add(this.pnlBackground);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblVer);
            this.Controls.Add(this.lblProductVersionComent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlBackground.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.xContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCheckUp;
        private System.Windows.Forms.Button btnDining;
        private System.Windows.Forms.Button btnCourse;
        private System.Windows.Forms.Button btnAccount;
        private System.Windows.Forms.Panel pnlBackground;
        private win.core.controls.SevenSegmentClock xClock;
        private System.Windows.Forms.Label lblStationName;
        private System.Windows.Forms.Label lblTechSupport;
        private System.Windows.Forms.Label lblProductVersionComent;
        private System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.ContextMenuStrip xContextMenu;
        private System.Windows.Forms.ToolStripMenuItem 在线更新ToolStripMenuItem;
    }
}

