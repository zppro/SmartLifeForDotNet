namespace SmartLife.Client.PensionAgency.SelfService
{
    partial class ucCourseInfo
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlAM = new System.Windows.Forms.Panel();
            this.pnlPM = new System.Windows.Forms.Panel();
            this.lblAM = new System.Windows.Forms.Label();
            this.pnlTopAM = new System.Windows.Forms.Panel();
            this.pnlTopPM = new System.Windows.Forms.Panel();
            this.lblPM = new System.Windows.Forms.Label();
            this.lbAM = new System.Windows.Forms.ListBox();
            this.lbPM = new System.Windows.Forms.ListBox();
            this.pnlAM.SuspendLayout();
            this.pnlPM.SuspendLayout();
            this.pnlTopAM.SuspendLayout();
            this.pnlTopPM.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAM
            // 
            this.pnlAM.BackColor = System.Drawing.Color.White;
            this.pnlAM.Controls.Add(this.lbAM);
            this.pnlAM.Controls.Add(this.pnlTopAM);
            this.pnlAM.Location = new System.Drawing.Point(26, 0);
            this.pnlAM.Name = "pnlAM";
            this.pnlAM.Size = new System.Drawing.Size(350, 500);
            this.pnlAM.TabIndex = 0;
            // 
            // pnlPM
            // 
            this.pnlPM.BackColor = System.Drawing.Color.White;
            this.pnlPM.Controls.Add(this.lbPM);
            this.pnlPM.Controls.Add(this.pnlTopPM);
            this.pnlPM.Location = new System.Drawing.Point(404, 0);
            this.pnlPM.Name = "pnlPM";
            this.pnlPM.Size = new System.Drawing.Size(350, 500);
            this.pnlPM.TabIndex = 1;
            // 
            // lblAM
            // 
            this.lblAM.BackColor = System.Drawing.Color.Transparent;
            this.lblAM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAM.Font = new System.Drawing.Font("幼圆", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAM.ForeColor = System.Drawing.Color.White;
            this.lblAM.Location = new System.Drawing.Point(0, 0);
            this.lblAM.Name = "lblAM";
            this.lblAM.Size = new System.Drawing.Size(350, 50);
            this.lblAM.TabIndex = 1;
            this.lblAM.Text = "上 午";
            this.lblAM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTopAM
            // 
            this.pnlTopAM.BackgroundImage = global::SmartLife.Client.PensionAgency.SelfService.Properties.Resources.ap_bg;
            this.pnlTopAM.Controls.Add(this.lblAM);
            this.pnlTopAM.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopAM.Location = new System.Drawing.Point(0, 0);
            this.pnlTopAM.Name = "pnlTopAM";
            this.pnlTopAM.Size = new System.Drawing.Size(350, 50);
            this.pnlTopAM.TabIndex = 2;
            // 
            // pnlTopPM
            // 
            this.pnlTopPM.BackgroundImage = global::SmartLife.Client.PensionAgency.SelfService.Properties.Resources.ap_bg;
            this.pnlTopPM.Controls.Add(this.lblPM);
            this.pnlTopPM.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopPM.Location = new System.Drawing.Point(0, 0);
            this.pnlTopPM.Name = "pnlTopPM";
            this.pnlTopPM.Size = new System.Drawing.Size(350, 50);
            this.pnlTopPM.TabIndex = 3;
            // 
            // lblPM
            // 
            this.lblPM.BackColor = System.Drawing.Color.Transparent;
            this.lblPM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPM.Font = new System.Drawing.Font("幼圆", 30F, System.Drawing.FontStyle.Bold);
            this.lblPM.ForeColor = System.Drawing.Color.White;
            this.lblPM.Location = new System.Drawing.Point(0, 0);
            this.lblPM.Name = "lblPM";
            this.lblPM.Size = new System.Drawing.Size(350, 50);
            this.lblPM.TabIndex = 1;
            this.lblPM.Text = "下 午";
            this.lblPM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbAM
            // 
            this.lbAM.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbAM.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbAM.FormattingEnabled = true;
            this.lbAM.ItemHeight = 30;
            this.lbAM.Location = new System.Drawing.Point(0, 60);
            this.lbAM.Name = "lbAM";
            this.lbAM.Size = new System.Drawing.Size(350, 420);
            this.lbAM.TabIndex = 3;
            this.lbAM.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItem);
            // 
            // lbPM
            // 
            this.lbPM.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbPM.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbPM.FormattingEnabled = true;
            this.lbPM.ItemHeight = 30;
            this.lbPM.Location = new System.Drawing.Point(0, 60);
            this.lbPM.Name = "lbPM";
            this.lbPM.Size = new System.Drawing.Size(350, 420);
            this.lbPM.TabIndex = 4;
            this.lbPM.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItem);
            // 
            // ucCourseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlPM);
            this.Controls.Add(this.pnlAM);
            this.Name = "ucCourseInfo";
            this.Size = new System.Drawing.Size(777, 500);
            this.pnlAM.ResumeLayout(false);
            this.pnlPM.ResumeLayout(false);
            this.pnlTopAM.ResumeLayout(false);
            this.pnlTopPM.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAM;
        private System.Windows.Forms.Panel pnlPM;
        private System.Windows.Forms.Label lblAM;
        private System.Windows.Forms.Panel pnlTopAM;
        private System.Windows.Forms.Panel pnlTopPM;
        private System.Windows.Forms.Label lblPM;
        private System.Windows.Forms.ListBox lbAM;
        private System.Windows.Forms.ListBox lbPM;
    }
}
