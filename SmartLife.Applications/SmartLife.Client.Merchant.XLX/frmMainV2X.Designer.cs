namespace SmartLife.Client.Merchant
{
    partial class frmMainV2X
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
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.xTabs = new System.Windows.Forms.TabControl();
            this.Default = new System.Windows.Forms.TabPage();
            this.wbWorkArea = new System.Windows.Forms.WebBrowser();
            this.pnlAssistToolbarContainer = new System.Windows.Forms.Panel();
            this.pnlAssistToolbar = new win.core.controls.shapes.RectangleShape();
            this.pnlAssistToolbarTitle = new System.Windows.Forms.Panel();
            this.btnESC = new System.Windows.Forms.Button();
            this.wbProvinceSum = new System.Windows.Forms.WebBrowser();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.pbdActions = new win.core.controls.PictureDropDownButton(this.components);
            this.lblVer = new System.Windows.Forms.Label();
            this.pbMin = new System.Windows.Forms.PictureBox();
            this.pbMax = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.xTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlMain.SuspendLayout();
            this.xTabs.SuspendLayout();
            this.Default.SuspendLayout();
            this.pnlAssistToolbarContainer.SuspendLayout();
            this.pnlHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbdActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 508);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1198, 134);
            this.pnlBottom.TabIndex = 6;
            // 
            // pnlRight
            // 
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(1064, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(134, 508);
            this.pnlRight.TabIndex = 7;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.Controls.Add(this.xTabs);
            this.pnlMain.Controls.Add(this.pnlAssistToolbarContainer);
            this.pnlMain.Controls.Add(this.pnlRight);
            this.pnlMain.Controls.Add(this.btnESC);
            this.pnlMain.Controls.Add(this.wbProvinceSum);
            this.pnlMain.Controls.Add(this.pnlBottom);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 159);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1198, 642);
            this.pnlMain.TabIndex = 8;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(144, 508);
            this.pnlLeft.TabIndex = 8;
            // 
            // xTabs
            // 
            this.xTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xTabs.Controls.Add(this.Default);
            this.xTabs.Location = new System.Drawing.Point(144, 0);
            this.xTabs.Name = "xTabs";
            this.xTabs.SelectedIndex = 0;
            this.xTabs.Size = new System.Drawing.Size(932, 508);
            this.xTabs.TabIndex = 14;
            // 
            // Default
            // 
            this.Default.Controls.Add(this.wbWorkArea);
            this.Default.Location = new System.Drawing.Point(4, 22);
            this.Default.Margin = new System.Windows.Forms.Padding(0);
            this.Default.Name = "Default";
            this.Default.Size = new System.Drawing.Size(924, 482);
            this.Default.TabIndex = 0;
            this.Default.Text = "规章制度";
            this.Default.UseVisualStyleBackColor = true;
            // 
            // wbWorkArea
            // 
            this.wbWorkArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbWorkArea.Location = new System.Drawing.Point(0, 0);
            this.wbWorkArea.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbWorkArea.Name = "wbWorkArea";
            this.wbWorkArea.Size = new System.Drawing.Size(924, 482);
            this.wbWorkArea.TabIndex = 4;
            this.wbWorkArea.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbWorkArea_DocumentCompleted);
            // 
            // pnlAssistToolbarContainer
            // 
            this.pnlAssistToolbarContainer.BackColor = System.Drawing.SystemColors.Control;
            this.pnlAssistToolbarContainer.Controls.Add(this.pnlAssistToolbar);
            this.pnlAssistToolbarContainer.Controls.Add(this.pnlAssistToolbarTitle);
            this.pnlAssistToolbarContainer.Location = new System.Drawing.Point(832, 50);
            this.pnlAssistToolbarContainer.Name = "pnlAssistToolbarContainer";
            this.pnlAssistToolbarContainer.Size = new System.Drawing.Size(100, 168);
            this.pnlAssistToolbarContainer.TabIndex = 11;
            // 
            // pnlAssistToolbar
            // 
            this.pnlAssistToolbar.BackColor = System.Drawing.Color.Blue;
            this.pnlAssistToolbar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAssistToolbar.BorderThickness = 1F;
            this.pnlAssistToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAssistToolbar.Location = new System.Drawing.Point(0, 20);
            this.pnlAssistToolbar.Name = "pnlAssistToolbar";
            this.pnlAssistToolbar.Opacity = 0.2F;
            this.pnlAssistToolbar.RotateAngle = 2F;
            this.pnlAssistToolbar.Size = new System.Drawing.Size(100, 148);
            this.pnlAssistToolbar.TabIndex = 999;
            // 
            // pnlAssistToolbarTitle
            // 
            this.pnlAssistToolbarTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(135)))), ((int)(((byte)(197)))));
            this.pnlAssistToolbarTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlAssistToolbarTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAssistToolbarTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlAssistToolbarTitle.Name = "pnlAssistToolbarTitle";
            this.pnlAssistToolbarTitle.Size = new System.Drawing.Size(100, 20);
            this.pnlAssistToolbarTitle.TabIndex = 1000;
            this.pnlAssistToolbarTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlAssistToolbarTitle_MouseDown);
            this.pnlAssistToolbarTitle.MouseEnter += new System.EventHandler(this.pnlAssistToolbarTitle_MouseEnter);
            this.pnlAssistToolbarTitle.MouseLeave += new System.EventHandler(this.pnlAssistToolbarTitle_MouseLeave);
            this.pnlAssistToolbarTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlAssistToolbarTitle_MouseMove);
            this.pnlAssistToolbarTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlAssistToolbarTitle_MouseUp);
            // 
            // btnESC
            // 
            this.btnESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnESC.Location = new System.Drawing.Point(957, 567);
            this.btnESC.Name = "btnESC";
            this.btnESC.Size = new System.Drawing.Size(75, 23);
            this.btnESC.TabIndex = 10;
            this.btnESC.Text = "btnESC";
            this.btnESC.UseVisualStyleBackColor = true;
            this.btnESC.Click += new System.EventHandler(this.btnESC_Click);
            // 
            // wbProvinceSum
            // 
            this.wbProvinceSum.IsWebBrowserContextMenuEnabled = false;
            this.wbProvinceSum.Location = new System.Drawing.Point(8, 8);
            this.wbProvinceSum.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbProvinceSum.Name = "wbProvinceSum";
            this.wbProvinceSum.ScriptErrorsSuppressed = true;
            this.wbProvinceSum.Size = new System.Drawing.Size(920, 553);
            this.wbProvinceSum.TabIndex = 19;
            this.wbProvinceSum.Visible = false;
            this.wbProvinceSum.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbProvinceSum_DocumentCompleted);
            // 
            // pnlTop
            // 
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 25);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1198, 134);
            this.pnlTop.TabIndex = 9;
            // 
            // pnlHead
            // 
            this.pnlHead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(135)))), ((int)(((byte)(197)))));
            this.pnlHead.BackgroundImage = global::SmartLife.Client.Merchant.Properties.Resources.titleBG;
            this.pnlHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlHead.Controls.Add(this.pbdActions);
            this.pnlHead.Controls.Add(this.lblVer);
            this.pnlHead.Controls.Add(this.pbMin);
            this.pnlHead.Controls.Add(this.pbMax);
            this.pnlHead.Controls.Add(this.pbClose);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(1198, 25);
            this.pnlHead.TabIndex = 5;
            this.pnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.pnlHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            // 
            // pbdActions
            // 
            this.pbdActions.BackColor = System.Drawing.Color.Transparent;
            this.pbdActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbdActions.Image = global::SmartLife.Client.Merchant.Properties.Resources.normal;
            this.pbdActions.Location = new System.Drawing.Point(1074, 0);
            this.pbdActions.Name = "pbdActions";
            this.pbdActions.Size = new System.Drawing.Size(26, 25);
            this.pbdActions.TabIndex = 6;
            this.pbdActions.TabStop = false;
            // 
            // lblVer
            // 
            this.lblVer.AutoSize = true;
            this.lblVer.ForeColor = System.Drawing.Color.White;
            this.lblVer.Location = new System.Drawing.Point(30, 5);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(0, 12);
            this.lblVer.TabIndex = 3;
            // 
            // pbMin
            // 
            this.pbMin.BackColor = System.Drawing.Color.Transparent;
            this.pbMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbMin.Image = global::SmartLife.Client.Merchant.Properties.Resources.min;
            this.pbMin.Location = new System.Drawing.Point(1100, 0);
            this.pbMin.Name = "pbMin";
            this.pbMin.Size = new System.Drawing.Size(26, 25);
            this.pbMin.TabIndex = 1;
            this.pbMin.TabStop = false;
            this.pbMin.Click += new System.EventHandler(this.pbMin_Click);
            this.pbMin.MouseEnter += new System.EventHandler(this.pbMin_MouseEnter);
            this.pbMin.MouseLeave += new System.EventHandler(this.pbMin_MouseLeave);
            // 
            // pbMax
            // 
            this.pbMax.BackColor = System.Drawing.Color.Transparent;
            this.pbMax.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbMax.Image = global::SmartLife.Client.Merchant.Properties.Resources.max;
            this.pbMax.Location = new System.Drawing.Point(1126, 0);
            this.pbMax.Name = "pbMax";
            this.pbMax.Size = new System.Drawing.Size(25, 25);
            this.pbMax.TabIndex = 2;
            this.pbMax.TabStop = false;
            this.pbMax.Click += new System.EventHandler(this.pbMax_Click);
            this.pbMax.MouseEnter += new System.EventHandler(this.pbMax_MouseEnter);
            this.pbMax.MouseLeave += new System.EventHandler(this.pbMax_MouseLeave);
            // 
            // pbClose
            // 
            this.pbClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbClose.Image = global::SmartLife.Client.Merchant.Properties.Resources.close;
            this.pbClose.Location = new System.Drawing.Point(1151, 0);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(47, 25);
            this.pbClose.TabIndex = 0;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            this.pbClose.MouseEnter += new System.EventHandler(this.pbClose_MouseEnter);
            this.pbClose.MouseLeave += new System.EventHandler(this.pbClose_MouseLeave);
            // 
            // frmMainV2X
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.CancelButton = this.btnESC;
            this.ClientSize = new System.Drawing.Size(1198, 801);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMainV2X";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMainV2X_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlMain.ResumeLayout(false);
            this.xTabs.ResumeLayout(false);
            this.Default.ResumeLayout(false);
            this.pnlAssistToolbarContainer.ResumeLayout(false);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbdActions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.PictureBox pbMin;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.PictureBox pbMax;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.Button btnESC;
        private win.core.controls.PictureDropDownButton pbdActions;
        private System.Windows.Forms.Panel pnlAssistToolbarContainer;
        private System.Windows.Forms.Panel pnlAssistToolbarTitle;
        private win.core.controls.shapes.RectangleShape pnlAssistToolbar;
        private System.Windows.Forms.ToolTip xTooltip;
        private System.Windows.Forms.TabControl xTabs;
        private System.Windows.Forms.TabPage Default;
        private System.Windows.Forms.WebBrowser wbWorkArea;
        private System.Windows.Forms.WebBrowser wbProvinceSum;
    }
}

