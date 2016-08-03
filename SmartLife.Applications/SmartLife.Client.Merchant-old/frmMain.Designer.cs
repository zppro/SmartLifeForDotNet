namespace SmartLife.Client.Merchant
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("待响应的工单");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("处理中的工单");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("处理完成的工单");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("已完成工单查询");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("工单处理", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tlblUserCodeName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlblSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlblProcess = new System.Windows.Forms.ToolStripStatusLabel();
            this.spcMain = new System.Windows.Forms.SplitContainer();
            this.tvNav = new System.Windows.Forms.TreeView();
            this.pbRight = new System.Windows.Forms.PictureBox();
            this.pbTop = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiServiceSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiNodeSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).BeginInit();
            this.spcMain.Panel1.SuspendLayout();
            this.spcMain.Panel2.SuspendLayout();
            this.spcMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTop)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlblUserCodeName,
            this.tlblSpace,
            this.tlblProcess});
            this.statusStrip1.Location = new System.Drawing.Point(0, 717);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1018, 23);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tlblUserCodeName
            // 
            this.tlblUserCodeName.BackColor = System.Drawing.SystemColors.Control;
            this.tlblUserCodeName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlblUserCodeName.ForeColor = System.Drawing.Color.Blue;
            this.tlblUserCodeName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tlblUserCodeName.MergeIndex = 999;
            this.tlblUserCodeName.Name = "tlblUserCodeName";
            this.tlblUserCodeName.Size = new System.Drawing.Size(0, 18);
            // 
            // tlblSpace
            // 
            this.tlblSpace.AutoSize = false;
            this.tlblSpace.Name = "tlblSpace";
            this.tlblSpace.Size = new System.Drawing.Size(400, 18);
            // 
            // tlblProcess
            // 
            this.tlblProcess.AutoSize = false;
            this.tlblProcess.BackColor = System.Drawing.SystemColors.Control;
            this.tlblProcess.Name = "tlblProcess";
            this.tlblProcess.Size = new System.Drawing.Size(200, 18);
            this.tlblProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // spcMain
            // 
            this.spcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcMain.IsSplitterFixed = true;
            this.spcMain.Location = new System.Drawing.Point(0, 81);
            this.spcMain.Name = "spcMain";
            // 
            // spcMain.Panel1
            // 
            this.spcMain.Panel1.Controls.Add(this.tvNav);
            // 
            // spcMain.Panel2
            // 
            this.spcMain.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(236)))), ((int)(((byte)(250)))));
            this.spcMain.Panel2.Controls.Add(this.pbRight);
            this.spcMain.Size = new System.Drawing.Size(1018, 636);
            this.spcMain.SplitterDistance = 200;
            this.spcMain.TabIndex = 5;
            // 
            // tvNav
            // 
            this.tvNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvNav.ItemHeight = 20;
            this.tvNav.Location = new System.Drawing.Point(0, 0);
            this.tvNav.Name = "tvNav";
            treeNode1.Name = "A0101";
            treeNode1.Text = "待响应的工单";
            treeNode2.Name = "A0102";
            treeNode2.Text = "处理中的工单";
            treeNode3.Name = "A0103";
            treeNode3.Text = "处理完成的工单";
            treeNode4.Name = "A0104";
            treeNode4.Text = "已完成工单查询";
            treeNode5.Name = "A01";
            treeNode5.Text = "工单处理";
            this.tvNav.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.tvNav.Size = new System.Drawing.Size(200, 636);
            this.tvNav.TabIndex = 0;
            this.tvNav.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvNav_AfterSelect);
            // 
            // pbRight
            // 
            this.pbRight.BackgroundImage = global::SmartLife.Client.Merchant.Properties.Resources.right;
            this.pbRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbRight.Location = new System.Drawing.Point(0, 0);
            this.pbRight.Name = "pbRight";
            this.pbRight.Size = new System.Drawing.Size(581, 416);
            this.pbRight.TabIndex = 7;
            this.pbRight.TabStop = false;
            // 
            // pbTop
            // 
            this.pbTop.BackgroundImage = global::SmartLife.Client.Merchant.Properties.Resources.top;
            this.pbTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbTop.Location = new System.Drawing.Point(0, 25);
            this.pbTop.Name = "pbTop";
            this.pbTop.Size = new System.Drawing.Size(1018, 56);
            this.pbTop.TabIndex = 6;
            this.pbTop.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(236)))), ((int)(((byte)(250)))));
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSetting});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1018, 25);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiSetting
            // 
            this.tsmiSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiServiceSetting,
            this.toolStripMenuItem1,
            this.tsmiNodeSetting});
            this.tsmiSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiSetting.Name = "tsmiSetting";
            this.tsmiSetting.Size = new System.Drawing.Size(53, 21);
            this.tsmiSetting.Text = "设置...";
            // 
            // tsmiServiceSetting
            // 
            this.tsmiServiceSetting.Name = "tsmiServiceSetting";
            this.tsmiServiceSetting.Size = new System.Drawing.Size(152, 22);
            this.tsmiServiceSetting.Text = "配置服务";
            this.tsmiServiceSetting.Click += new System.EventHandler(this.tsmiServiceSetting_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiNodeSetting
            // 
            this.tsmiNodeSetting.Name = "tsmiNodeSetting";
            this.tsmiNodeSetting.Size = new System.Drawing.Size(152, 22);
            this.tsmiNodeSetting.Text = "配置节点";
            this.tsmiNodeSetting.Click += new System.EventHandler(this.tsmiNodeSetting_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 740);
            this.Controls.Add(this.spcMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pbTop);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.spcMain.Panel1.ResumeLayout(false);
            this.spcMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).EndInit();
            this.spcMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTop)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tlblProcess;
        private System.Windows.Forms.ToolStripStatusLabel tlblSpace;
        private System.Windows.Forms.ToolStripStatusLabel tlblUserCodeName;
        private System.Windows.Forms.SplitContainer spcMain;
        private System.Windows.Forms.TreeView tvNav;
        private System.Windows.Forms.PictureBox pbTop;
        private System.Windows.Forms.PictureBox pbRight;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmiServiceSetting;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiNodeSetting;
    }
}