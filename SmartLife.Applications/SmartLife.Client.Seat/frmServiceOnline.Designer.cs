namespace SmartLife.Client.Seat
{
    partial class frmServiceOnline
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
            this.pbPresent = new System.Windows.Forms.PictureBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.pbSendPic = new System.Windows.Forms.PictureBox();
            this.pbCapture = new System.Windows.Forms.PictureBox();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.pbOK = new System.Windows.Forms.PictureBox();
            this.pbEmo = new System.Windows.Forms.PictureBox();
            this.pbText = new System.Windows.Forms.PictureBox();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.rtxtInputBox = new System.Windows.Forms.RichTextBox();
            this.lblNickName = new System.Windows.Forms.Label();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.lblVer = new System.Windows.Forms.Label();
            this.pbMin = new System.Windows.Forms.PictureBox();
            this.pbMax = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbPresent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSendPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCapture)).BeginInit();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbText)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPresent
            // 
            this.pbPresent.Image = global::SmartLife.Client.Seat.Properties.Resources.图标礼物;
            this.pbPresent.Location = new System.Drawing.Point(296, 3);
            this.pbPresent.Name = "pbPresent";
            this.pbPresent.Size = new System.Drawing.Size(50, 50);
            this.pbPresent.TabIndex = 4;
            this.pbPresent.TabStop = false;
            this.pbPresent.Click += new System.EventHandler(this.pbPresent_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(0, 57);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScrollBarsEnabled = false;
            this.webBrowser.Size = new System.Drawing.Size(756, 279);
            this.webBrowser.TabIndex = 23;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // pbSendPic
            // 
            this.pbSendPic.Image = global::SmartLife.Client.Seat.Properties.Resources.图标图片;
            this.pbSendPic.Location = new System.Drawing.Point(225, 3);
            this.pbSendPic.Name = "pbSendPic";
            this.pbSendPic.Size = new System.Drawing.Size(50, 50);
            this.pbSendPic.TabIndex = 3;
            this.pbSendPic.TabStop = false;
            this.pbSendPic.Click += new System.EventHandler(this.pbSendPic_Click);
            // 
            // pbCapture
            // 
            this.pbCapture.Image = global::SmartLife.Client.Seat.Properties.Resources.图标裁剪;
            this.pbCapture.Location = new System.Drawing.Point(154, 3);
            this.pbCapture.Name = "pbCapture";
            this.pbCapture.Size = new System.Drawing.Size(50, 50);
            this.pbCapture.TabIndex = 2;
            this.pbCapture.TabStop = false;
            this.pbCapture.Click += new System.EventHandler(this.pbCapture_Click);
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.pbOK);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(0, 492);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(756, 42);
            this.pnlButton.TabIndex = 22;
            // 
            // pbOK
            // 
            this.pbOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.pbOK.BackgroundImage = global::SmartLife.Client.Seat.Properties.Resources.ok;
            this.pbOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbOK.Location = new System.Drawing.Point(653, 5);
            this.pbOK.Name = "pbOK";
            this.pbOK.Size = new System.Drawing.Size(100, 32);
            this.pbOK.TabIndex = 0;
            this.pbOK.TabStop = false;
            this.pbOK.Click += new System.EventHandler(this.pbOK_Click);
            this.pbOK.MouseEnter += new System.EventHandler(this.pbOK_MouseEnter);
            this.pbOK.MouseLeave += new System.EventHandler(this.pbOK_MouseLeave);
            // 
            // pbEmo
            // 
            this.pbEmo.Image = global::SmartLife.Client.Seat.Properties.Resources.图标笑脸;
            this.pbEmo.Location = new System.Drawing.Point(83, 3);
            this.pbEmo.Name = "pbEmo";
            this.pbEmo.Size = new System.Drawing.Size(50, 50);
            this.pbEmo.TabIndex = 1;
            this.pbEmo.TabStop = false;
            this.pbEmo.Click += new System.EventHandler(this.pbEmo_Click);
            // 
            // pbText
            // 
            this.pbText.Image = global::SmartLife.Client.Seat.Properties.Resources.图标文字;
            this.pbText.Location = new System.Drawing.Point(12, 3);
            this.pbText.Name = "pbText";
            this.pbText.Size = new System.Drawing.Size(50, 50);
            this.pbText.TabIndex = 0;
            this.pbText.TabStop = false;
            this.pbText.Click += new System.EventHandler(this.pbText_Click);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(222)))));
            this.pnlToolbar.Controls.Add(this.pbPresent);
            this.pnlToolbar.Controls.Add(this.pbSendPic);
            this.pnlToolbar.Controls.Add(this.pbCapture);
            this.pnlToolbar.Controls.Add(this.pbEmo);
            this.pnlToolbar.Controls.Add(this.pbText);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 336);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(756, 57);
            this.pnlToolbar.TabIndex = 21;
            // 
            // rtxtInputBox
            // 
            this.rtxtInputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtxtInputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtInputBox.Font = new System.Drawing.Font("宋体", 12F);
            this.rtxtInputBox.Location = new System.Drawing.Point(0, 0);
            this.rtxtInputBox.Name = "rtxtInputBox";
            this.rtxtInputBox.Size = new System.Drawing.Size(756, 99);
            this.rtxtInputBox.TabIndex = 1;
            this.rtxtInputBox.Text = "";
            // 
            // lblNickName
            // 
            this.lblNickName.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNickName.ForeColor = System.Drawing.Color.White;
            this.lblNickName.Location = new System.Drawing.Point(338, 18);
            this.lblNickName.Name = "lblNickName";
            this.lblNickName.Size = new System.Drawing.Size(100, 23);
            this.lblNickName.TabIndex = 4;
            this.lblNickName.Text = "--";
            this.lblNickName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.rtxtInputBox);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInput.Location = new System.Drawing.Point(0, 393);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(756, 99);
            this.pnlInput.TabIndex = 20;
            // 
            // pnlHead
            // 
            this.pnlHead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(172)))), ((int)(((byte)(184)))));
            this.pnlHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlHead.Controls.Add(this.lblNickName);
            this.pnlHead.Controls.Add(this.lblVer);
            this.pnlHead.Controls.Add(this.pbMin);
            this.pnlHead.Controls.Add(this.pbMax);
            this.pnlHead.Controls.Add(this.pbClose);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(756, 57);
            this.pnlHead.TabIndex = 19;
            this.pnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.pnlHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            // 
            // lblVer
            // 
            this.lblVer.AutoSize = true;
            this.lblVer.Location = new System.Drawing.Point(60, 5);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(0, 12);
            this.lblVer.TabIndex = 3;
            // 
            // pbMin
            // 
            this.pbMin.BackColor = System.Drawing.Color.Transparent;
            this.pbMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbMin.Image = global::SmartLife.Client.Seat.Properties.Resources.min;
            this.pbMin.Location = new System.Drawing.Point(664, 0);
            this.pbMin.Name = "pbMin";
            this.pbMin.Size = new System.Drawing.Size(26, 57);
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
            this.pbMax.Location = new System.Drawing.Point(690, 0);
            this.pbMax.Name = "pbMax";
            this.pbMax.Size = new System.Drawing.Size(25, 57);
            this.pbMax.TabIndex = 2;
            this.pbMax.TabStop = false;
            this.pbMax.Visible = false;
            // 
            // pbClose
            // 
            this.pbClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbClose.Image = global::SmartLife.Client.Seat.Properties.Resources.close;
            this.pbClose.Location = new System.Drawing.Point(715, 0);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(41, 57);
            this.pbClose.TabIndex = 0;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            this.pbClose.MouseEnter += new System.EventHandler(this.pbClose_MouseEnter);
            this.pbClose.MouseLeave += new System.EventHandler(this.pbClose_MouseLeave);
            // 
            // frmServiceOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 534);
            this.ControlBox = false;
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlHead);
            this.Controls.Add(this.pnlButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmServiceOnline";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmServiceOnline_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPresent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSendPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCapture)).EndInit();
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbText)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPresent;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.PictureBox pbSendPic;
        private System.Windows.Forms.PictureBox pbCapture;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.PictureBox pbOK;
        private System.Windows.Forms.PictureBox pbEmo;
        private System.Windows.Forms.PictureBox pbText;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.RichTextBox rtxtInputBox;
        private System.Windows.Forms.Label lblNickName;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.PictureBox pbMin;
        private System.Windows.Forms.PictureBox pbMax;
        private System.Windows.Forms.PictureBox pbClose;
    }
}