namespace SmartLife.Client.Merchant
{
    partial class frmWorkOrderNotify
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkOrderNotify));
            this.lblMessage = new System.Windows.Forms.Label();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.xImage = new System.Windows.Forms.ImageList(this.components);
            this.xIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lbtnIgore = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(107, 42);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(149, 95);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "消息内容";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMessage.Click += new System.EventHandler(this.lblMessage_Click);
            // 
            // pbClose
            // 
            this.pbClose.Image = global::SmartLife.Client.Merchant.Properties.Resources.Close2;
            this.pbClose.Location = new System.Drawing.Point(242, 6);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(17, 17);
            this.pbClose.TabIndex = 3;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            this.pbClose.MouseEnter += new System.EventHandler(this.pbClose_MouseEnter);
            this.pbClose.MouseLeave += new System.EventHandler(this.pbClose_MouseLeave);
            // 
            // xImage
            // 
            this.xImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("xImage.ImageStream")));
            this.xImage.TransparentColor = System.Drawing.Color.Transparent;
            this.xImage.Images.SetKeyName(0, "Close2.bmp");
            this.xImage.Images.SetKeyName(1, "Close1.bmp");
            // 
            // xIcon
            // 
            this.xIcon.Text = "任务栏通知窗口";
            this.xIcon.Visible = true;
            this.xIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.xIcon_MouseDoubleClick);
            // 
            // lbtnIgore
            // 
            this.lbtnIgore.AutoSize = true;
            this.lbtnIgore.BackColor = System.Drawing.Color.White;
            this.lbtnIgore.Location = new System.Drawing.Point(227, 140);
            this.lbtnIgore.Name = "lbtnIgore";
            this.lbtnIgore.Size = new System.Drawing.Size(29, 12);
            this.lbtnIgore.TabIndex = 4;
            this.lbtnIgore.Text = "忽略";
            this.lbtnIgore.Click += new System.EventHandler(this.lbtnIgore_Click);
            // 
            // frmWorkOrderNotify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SmartLife.Client.Merchant.Properties.Resources.remind_bg;
            this.ClientSize = new System.Drawing.Size(268, 161);
            this.Controls.Add(this.lbtnIgore);
            this.Controls.Add(this.pbClose);
            this.Controls.Add(this.lblMessage);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmWorkOrderNotify";
            this.ShowInTaskbar = false;
            this.Text = "frmWorkOrderNotify";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmWorkOrderNotify_Load);
            this.Click += new System.EventHandler(this.frmWorkOrderNotify_Click);
            this.MouseEnter += new System.EventHandler(this.frmWorkOrderNotify_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.frmWorkOrderNotify_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.ImageList xImage;
        private System.Windows.Forms.NotifyIcon xIcon;
        private System.Windows.Forms.Label lbtnIgore;
    }
}