namespace SmartLife.Client.PensionAgency.SelfService
{
    partial class ucOldManInfoShow
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
            this.pbHeadPortrait = new System.Windows.Forms.PictureBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblOldManName = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblIDNo = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblICNoHead = new System.Windows.Forms.Label();
            this.pnlWin = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeadPortrait)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlWin.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbHeadPortrait
            // 
            this.pbHeadPortrait.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHeadPortrait.Location = new System.Drawing.Point(11, 40);
            this.pbHeadPortrait.Name = "pbHeadPortrait";
            this.pbHeadPortrait.Size = new System.Drawing.Size(150, 150);
            this.pbHeadPortrait.TabIndex = 0;
            this.pbHeadPortrait.TabStop = false;
            // 
            // lblAddress
            // 
            this.lblAddress.Font = new System.Drawing.Font("华文楷体", 16F);
            this.lblAddress.Location = new System.Drawing.Point(10, 204);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(458, 56);
            this.lblAddress.TabIndex = 1;
            this.lblAddress.Text = "-- -- -- -- -- -- -- -- -- -- ";
            // 
            // lblOldManName
            // 
            this.lblOldManName.Font = new System.Drawing.Font("华文楷体", 24F);
            this.lblOldManName.Location = new System.Drawing.Point(196, 70);
            this.lblOldManName.Name = "lblOldManName";
            this.lblOldManName.Size = new System.Drawing.Size(134, 40);
            this.lblOldManName.TabIndex = 2;
            this.lblOldManName.Text = "-- -- --";
            this.lblOldManName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAge
            // 
            this.lblAge.Font = new System.Drawing.Font("宋体", 12F);
            this.lblAge.Location = new System.Drawing.Point(380, 79);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(60, 30);
            this.lblAge.TabIndex = 3;
            this.lblAge.Text = "(--)";
            this.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIDNo
            // 
            this.lblIDNo.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblIDNo.Location = new System.Drawing.Point(197, 110);
            this.lblIDNo.Name = "lblIDNo";
            this.lblIDNo.Size = new System.Drawing.Size(238, 30);
            this.lblIDNo.TabIndex = 4;
            this.lblIDNo.Text = "ID ------------------";
            this.lblIDNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGender
            // 
            this.lblGender.Font = new System.Drawing.Font("华文楷体", 18F);
            this.lblGender.Location = new System.Drawing.Point(336, 77);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(40, 30);
            this.lblGender.TabIndex = 5;
            this.lblGender.Text = "--";
            this.lblGender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Black;
            this.pnlTop.Controls.Add(this.lblICNoHead);
            this.pnlTop.Location = new System.Drawing.Point(-1, 4);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(480, 30);
            this.pnlTop.TabIndex = 6;
            // 
            // lblICNoHead
            // 
            this.lblICNoHead.BackColor = System.Drawing.Color.Transparent;
            this.lblICNoHead.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblICNoHead.ForeColor = System.Drawing.Color.White;
            this.lblICNoHead.Location = new System.Drawing.Point(7, 0);
            this.lblICNoHead.Name = "lblICNoHead";
            this.lblICNoHead.Size = new System.Drawing.Size(213, 30);
            this.lblICNoHead.TabIndex = 7;
            this.lblICNoHead.Text = "No.";
            // 
            // pnlWin
            // 
            this.pnlWin.BackColor = System.Drawing.Color.White;
            this.pnlWin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWin.Controls.Add(this.pnlTop);
            this.pnlWin.Controls.Add(this.lblGender);
            this.pnlWin.Controls.Add(this.lblIDNo);
            this.pnlWin.Controls.Add(this.lblAge);
            this.pnlWin.Controls.Add(this.lblOldManName);
            this.pnlWin.Controls.Add(this.lblAddress);
            this.pnlWin.Controls.Add(this.pbHeadPortrait);
            this.pnlWin.Location = new System.Drawing.Point(148, 60);
            this.pnlWin.Name = "pnlWin";
            this.pnlWin.Size = new System.Drawing.Size(480, 320);
            this.pnlWin.TabIndex = 11;
            // 
            // ucOldManInfoShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlWin);
            this.Name = "ucOldManInfoShow";
            this.Size = new System.Drawing.Size(777, 650);
            ((System.ComponentModel.ISupportInitialize)(this.pbHeadPortrait)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlWin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbHeadPortrait;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblOldManName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblIDNo;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblICNoHead;
        private System.Windows.Forms.Panel pnlWin;
    }
}
