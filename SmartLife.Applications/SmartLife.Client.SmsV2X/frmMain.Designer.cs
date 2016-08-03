namespace SmartLife.Client.SmsV2X
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
            this.ms_Tool = new System.Windows.Forms.MenuStrip();
            this.tsmi_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_config = new System.Windows.Forms.ToolStripMenuItem();
            this.sc_Main = new System.Windows.Forms.SplitContainer();
            this.lb_Send = new System.Windows.Forms.ListBox();
            this.pnl_Left = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_Get = new System.Windows.Forms.ListBox();
            this.pnl_Right = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.ms_Tool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sc_Main)).BeginInit();
            this.sc_Main.Panel1.SuspendLayout();
            this.sc_Main.Panel2.SuspendLayout();
            this.sc_Main.SuspendLayout();
            this.pnl_Left.SuspendLayout();
            this.pnl_Right.SuspendLayout();
            this.SuspendLayout();
            // 
            // ms_Tool
            // 
            this.ms_Tool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_menu});
            this.ms_Tool.Location = new System.Drawing.Point(0, 0);
            this.ms_Tool.Name = "ms_Tool";
            this.ms_Tool.Size = new System.Drawing.Size(794, 25);
            this.ms_Tool.TabIndex = 0;
            this.ms_Tool.Text = "menuStrip1";
            // 
            // tsmi_menu
            // 
            this.tsmi_menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_config});
            this.tsmi_menu.Name = "tsmi_menu";
            this.tsmi_menu.Size = new System.Drawing.Size(59, 21);
            this.tsmi_menu.Text = "设置(&S)";
            // 
            // tsmi_config
            // 
            this.tsmi_config.Name = "tsmi_config";
            this.tsmi_config.Size = new System.Drawing.Size(152, 22);
            this.tsmi_config.Text = "配置(&T)";
            this.tsmi_config.Click += new System.EventHandler(this.tsmi_config_Click);
            // 
            // sc_Main
            // 
            this.sc_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sc_Main.Location = new System.Drawing.Point(0, 25);
            this.sc_Main.Name = "sc_Main";
            // 
            // sc_Main.Panel1
            // 
            this.sc_Main.Panel1.Controls.Add(this.lb_Send);
            this.sc_Main.Panel1.Controls.Add(this.pnl_Left);
            // 
            // sc_Main.Panel2
            // 
            this.sc_Main.Panel2.Controls.Add(this.lb_Get);
            this.sc_Main.Panel2.Controls.Add(this.pnl_Right);
            this.sc_Main.Size = new System.Drawing.Size(794, 547);
            this.sc_Main.SplitterDistance = 395;
            this.sc_Main.SplitterWidth = 1;
            this.sc_Main.TabIndex = 1;
            // 
            // lb_Send
            // 
            this.lb_Send.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Send.FormattingEnabled = true;
            this.lb_Send.ItemHeight = 12;
            this.lb_Send.Location = new System.Drawing.Point(0, 33);
            this.lb_Send.Name = "lb_Send";
            this.lb_Send.Size = new System.Drawing.Size(395, 514);
            this.lb_Send.TabIndex = 1;
            // 
            // pnl_Left
            // 
            this.pnl_Left.Controls.Add(this.label1);
            this.pnl_Left.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Left.Location = new System.Drawing.Point(0, 0);
            this.pnl_Left.Name = "pnl_Left";
            this.pnl_Left.Size = new System.Drawing.Size(395, 33);
            this.pnl_Left.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "发送短信：";
            // 
            // lb_Get
            // 
            this.lb_Get.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Get.FormattingEnabled = true;
            this.lb_Get.ItemHeight = 12;
            this.lb_Get.Location = new System.Drawing.Point(0, 33);
            this.lb_Get.Name = "lb_Get";
            this.lb_Get.Size = new System.Drawing.Size(398, 514);
            this.lb_Get.TabIndex = 1;
            // 
            // pnl_Right
            // 
            this.pnl_Right.Controls.Add(this.label2);
            this.pnl_Right.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Right.Location = new System.Drawing.Point(0, 0);
            this.pnl_Right.Name = "pnl_Right";
            this.pnl_Right.Size = new System.Drawing.Size(398, 33);
            this.pnl_Right.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "接收短信：";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("宋体", 15F);
            this.lblMsg.Location = new System.Drawing.Point(360, 280);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(69, 20);
            this.lblMsg.TabIndex = 2;
            this.lblMsg.Text = "验证中";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(794, 572);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.sc_Main);
            this.Controls.Add(this.ms_Tool);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ms_Tool;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "短信管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ms_Tool.ResumeLayout(false);
            this.ms_Tool.PerformLayout();
            this.sc_Main.Panel1.ResumeLayout(false);
            this.sc_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sc_Main)).EndInit();
            this.sc_Main.ResumeLayout(false);
            this.pnl_Left.ResumeLayout(false);
            this.pnl_Left.PerformLayout();
            this.pnl_Right.ResumeLayout(false);
            this.pnl_Right.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip ms_Tool;
        private System.Windows.Forms.ToolStripMenuItem tsmi_menu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_config;
        private System.Windows.Forms.SplitContainer sc_Main;
        private System.Windows.Forms.ListBox lb_Send;
        private System.Windows.Forms.Panel pnl_Left;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lb_Get;
        private System.Windows.Forms.Panel pnl_Right;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMsg;
    }
}

