namespace SmartLife.Client.Sms
{
    partial class frmSendSms
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
            this.tb_SendSms = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tb_SendSms
            // 
            this.tb_SendSms.BackColor = System.Drawing.Color.Black;
            this.tb_SendSms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_SendSms.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_SendSms.ForeColor = System.Drawing.Color.White;
            this.tb_SendSms.Location = new System.Drawing.Point(0, 0);
            this.tb_SendSms.Multiline = true;
            this.tb_SendSms.Name = "tb_SendSms";
            this.tb_SendSms.ReadOnly = true;
            this.tb_SendSms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_SendSms.Size = new System.Drawing.Size(284, 262);
            this.tb_SendSms.TabIndex = 0;
            // 
            // frmSendSms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tb_SendSms);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSendSms";
            this.Text = "发送短信";
            this.Load += new System.EventHandler(this.frmSendSms_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_SendSms;
    }
}