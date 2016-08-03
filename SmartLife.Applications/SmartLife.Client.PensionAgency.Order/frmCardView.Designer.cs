namespace SmartLife.Client.PensionAgency.Order
{
    partial class frmCardView
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
            this.pnlWin = new System.Windows.Forms.Panel();
            this.lblICNo = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.btnMakeCard = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblICNoHead = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblIDNo = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblOldManName = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.pbHeadPortrait = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlWin.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeadPortrait)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlWin
            // 
            this.pnlWin.BackColor = System.Drawing.Color.White;
            this.pnlWin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWin.Controls.Add(this.lblICNo);
            this.pnlWin.Controls.Add(this.lblTip);
            this.pnlWin.Controls.Add(this.btnMakeCard);
            this.pnlWin.Controls.Add(this.pnlTop);
            this.pnlWin.Controls.Add(this.lblGender);
            this.pnlWin.Controls.Add(this.lblIDNo);
            this.pnlWin.Controls.Add(this.lblAge);
            this.pnlWin.Controls.Add(this.lblOldManName);
            this.pnlWin.Controls.Add(this.lblAddress);
            this.pnlWin.Controls.Add(this.pbHeadPortrait);
            this.pnlWin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWin.Location = new System.Drawing.Point(0, 0);
            this.pnlWin.Name = "pnlWin";
            this.pnlWin.Size = new System.Drawing.Size(480, 320);
            this.pnlWin.TabIndex = 0;
            // 
            // lblICNo
            // 
            this.lblICNo.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblICNo.Location = new System.Drawing.Point(197, 140);
            this.lblICNo.Name = "lblICNo";
            this.lblICNo.Size = new System.Drawing.Size(238, 30);
            this.lblICNo.TabIndex = 10;
            this.lblICNo.Text = "IC ------------------";
            this.lblICNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTip
            // 
            this.lblTip.ForeColor = System.Drawing.Color.Red;
            this.lblTip.Location = new System.Drawing.Point(13, 284);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(293, 20);
            this.lblTip.TabIndex = 9;
            this.lblTip.Text = "请将卡放在读卡器上";
            // 
            // btnMakeCard
            // 
            this.btnMakeCard.BackColor = System.Drawing.Color.Silver;
            this.btnMakeCard.Enabled = false;
            this.btnMakeCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMakeCard.Font = new System.Drawing.Font("宋体", 16F);
            this.btnMakeCard.Location = new System.Drawing.Point(341, 269);
            this.btnMakeCard.Name = "btnMakeCard";
            this.btnMakeCard.Size = new System.Drawing.Size(100, 35);
            this.btnMakeCard.TabIndex = 8;
            this.btnMakeCard.Text = "制卡";
            this.btnMakeCard.UseVisualStyleBackColor = false;
            this.btnMakeCard.Click += new System.EventHandler(this.btnMakeCard_Click);
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
            // lblAddress
            // 
            this.lblAddress.Font = new System.Drawing.Font("华文楷体", 16F);
            this.lblAddress.Location = new System.Drawing.Point(10, 204);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(458, 56);
            this.lblAddress.TabIndex = 1;
            this.lblAddress.Text = "-- -- -- -- -- -- -- -- -- -- ";
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
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(384, 285);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmCardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(480, 320);
            this.Controls.Add(this.pnlWin);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCardView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmCardView_Load);
            this.pnlWin.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbHeadPortrait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlWin;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbHeadPortrait;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblOldManName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblIDNo;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblICNoHead;
        private System.Windows.Forms.Button btnMakeCard;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Label lblICNo;
    }
}