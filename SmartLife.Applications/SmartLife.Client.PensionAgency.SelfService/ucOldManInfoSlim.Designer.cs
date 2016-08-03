namespace SmartLife.Client.PensionAgency.SelfService
{
    partial class ucOldManInfoSlim
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
            this.pnlOldManInfo = new System.Windows.Forms.Panel();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblIDNo = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblOldManName = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.pbHeadPortrait = new System.Windows.Forms.PictureBox();
            this.pnlOldManInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeadPortrait)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlOldManInfo
            // 
            this.pnlOldManInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlOldManInfo.Controls.Add(this.lblGender);
            this.pnlOldManInfo.Controls.Add(this.lblIDNo);
            this.pnlOldManInfo.Controls.Add(this.lblAge);
            this.pnlOldManInfo.Controls.Add(this.lblOldManName);
            this.pnlOldManInfo.Controls.Add(this.lblAddress);
            this.pnlOldManInfo.Controls.Add(this.pbHeadPortrait);
            this.pnlOldManInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOldManInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlOldManInfo.Name = "pnlOldManInfo";
            this.pnlOldManInfo.Size = new System.Drawing.Size(777, 150);
            this.pnlOldManInfo.TabIndex = 3;
            // 
            // lblGender
            // 
            this.lblGender.Font = new System.Drawing.Font("华文楷体", 18F);
            this.lblGender.ForeColor = System.Drawing.Color.White;
            this.lblGender.Location = new System.Drawing.Point(299, 17);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(40, 30);
            this.lblGender.TabIndex = 11;
            this.lblGender.Text = "--";
            this.lblGender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIDNo
            // 
            this.lblIDNo.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblIDNo.ForeColor = System.Drawing.Color.White;
            this.lblIDNo.Location = new System.Drawing.Point(468, 17);
            this.lblIDNo.Name = "lblIDNo";
            this.lblIDNo.Size = new System.Drawing.Size(238, 30);
            this.lblIDNo.TabIndex = 10;
            this.lblIDNo.Text = "ID ------------------";
            this.lblIDNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAge
            // 
            this.lblAge.Font = new System.Drawing.Font("宋体", 12F);
            this.lblAge.ForeColor = System.Drawing.Color.White;
            this.lblAge.Location = new System.Drawing.Point(343, 19);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(60, 30);
            this.lblAge.TabIndex = 9;
            this.lblAge.Text = "(--)";
            this.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOldManName
            // 
            this.lblOldManName.Font = new System.Drawing.Font("华文楷体", 24F);
            this.lblOldManName.ForeColor = System.Drawing.Color.White;
            this.lblOldManName.Location = new System.Drawing.Point(159, 10);
            this.lblOldManName.Name = "lblOldManName";
            this.lblOldManName.Size = new System.Drawing.Size(134, 40);
            this.lblOldManName.TabIndex = 8;
            this.lblOldManName.Text = "-- -- --";
            this.lblOldManName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAddress
            // 
            this.lblAddress.Font = new System.Drawing.Font("华文楷体", 12F);
            this.lblAddress.ForeColor = System.Drawing.Color.White;
            this.lblAddress.Location = new System.Drawing.Point(162, 57);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(594, 69);
            this.lblAddress.TabIndex = 7;
            this.lblAddress.Text = "-- -- -- -- -- -- -- -- -- -- ";
            // 
            // pbHeadPortrait
            // 
            this.pbHeadPortrait.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHeadPortrait.Location = new System.Drawing.Point(20, 10);
            this.pbHeadPortrait.Name = "pbHeadPortrait";
            this.pbHeadPortrait.Size = new System.Drawing.Size(120, 120);
            this.pbHeadPortrait.TabIndex = 6;
            this.pbHeadPortrait.TabStop = false;
            // 
            // ucOldManInfoSlim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlOldManInfo);
            this.Name = "ucOldManInfoSlim";
            this.Size = new System.Drawing.Size(777, 150);
            this.pnlOldManInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbHeadPortrait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOldManInfo;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblIDNo;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblOldManName;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.PictureBox pbHeadPortrait;
    }
}
