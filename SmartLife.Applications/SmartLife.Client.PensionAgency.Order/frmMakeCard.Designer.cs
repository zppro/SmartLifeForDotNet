namespace SmartLife.Client.PensionAgency.Order
{
    partial class frmMakeCard
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
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlWin = new System.Windows.Forms.Panel();
            this.panel62 = new System.Windows.Forms.Panel();
            this.lblPageNo = new System.Windows.Forms.Label();
            this.lblPageAll = new System.Windows.Forms.Label();
            this.txtInputCode = new System.Windows.Forms.TextBox();
            this.panel41 = new System.Windows.Forms.Panel();
            this.panel40 = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.xTab = new System.Windows.Forms.TabControl();
            this.pnlWin.SuspendLayout();
            this.panel62.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(809, 605);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pnlWin
            // 
            this.pnlWin.Controls.Add(this.panel62);
            this.pnlWin.Controls.Add(this.panel41);
            this.pnlWin.Controls.Add(this.panel40);
            this.pnlWin.Controls.Add(this.btnNext);
            this.pnlWin.Controls.Add(this.btnLeft);
            this.pnlWin.Controls.Add(this.xTab);
            this.pnlWin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWin.Location = new System.Drawing.Point(0, 0);
            this.pnlWin.Name = "pnlWin";
            this.pnlWin.Size = new System.Drawing.Size(896, 720);
            this.pnlWin.TabIndex = 10;
            // 
            // panel62
            // 
            this.panel62.Controls.Add(this.lblPageNo);
            this.panel62.Controls.Add(this.lblPageAll);
            this.panel62.Controls.Add(this.txtInputCode);
            this.panel62.Location = new System.Drawing.Point(55, 5);
            this.panel62.Name = "panel62";
            this.panel62.Size = new System.Drawing.Size(794, 30);
            this.panel62.TabIndex = 6;
            // 
            // lblPageNo
            // 
            this.lblPageNo.ForeColor = System.Drawing.Color.White;
            this.lblPageNo.Location = new System.Drawing.Point(724, 9);
            this.lblPageNo.Name = "lblPageNo";
            this.lblPageNo.Size = new System.Drawing.Size(20, 12);
            this.lblPageNo.TabIndex = 2;
            this.lblPageNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPageAll
            // 
            this.lblPageAll.ForeColor = System.Drawing.Color.White;
            this.lblPageAll.Location = new System.Drawing.Point(744, 9);
            this.lblPageAll.Name = "lblPageAll";
            this.lblPageAll.Size = new System.Drawing.Size(40, 12);
            this.lblPageAll.TabIndex = 1;
            this.lblPageAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInputCode
            // 
            this.txtInputCode.Location = new System.Drawing.Point(19, 5);
            this.txtInputCode.Name = "txtInputCode";
            this.txtInputCode.Size = new System.Drawing.Size(100, 21);
            this.txtInputCode.TabIndex = 0;
            // 
            // panel41
            // 
            this.panel41.Location = new System.Drawing.Point(45, 0);
            this.panel41.Name = "panel41";
            this.panel41.Size = new System.Drawing.Size(10, 790);
            this.panel41.TabIndex = 5;
            // 
            // panel40
            // 
            this.panel40.Location = new System.Drawing.Point(848, 0);
            this.panel40.Name = "panel40";
            this.panel40.Size = new System.Drawing.Size(10, 790);
            this.panel40.TabIndex = 4;
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Location = new System.Drawing.Point(860, 278);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(29, 134);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLeft.Location = new System.Drawing.Point(11, 278);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(29, 134);
            this.btnLeft.TabIndex = 2;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // xTab
            // 
            this.xTab.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xTab.Location = new System.Drawing.Point(48, 8);
            this.xTab.Name = "xTab";
            this.xTab.SelectedIndex = 0;
            this.xTab.Size = new System.Drawing.Size(807, 716);
            this.xTab.TabIndex = 1;
            // 
            // frmMakeCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(61)))), ((int)(((byte)(222)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(896, 720);
            this.Controls.Add(this.pnlWin);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMakeCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMakeCard";
            this.Load += new System.EventHandler(this.frmMakeCard_Load);
            this.pnlWin.ResumeLayout(false);
            this.panel62.ResumeLayout(false);
            this.panel62.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlWin;
        private System.Windows.Forms.TabControl xTab;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Panel panel40;
        private System.Windows.Forms.Panel panel41;
        private System.Windows.Forms.Panel panel62;
        private System.Windows.Forms.TextBox txtInputCode;
        private System.Windows.Forms.Label lblPageAll;
        private System.Windows.Forms.Label lblPageNo;
    }
}