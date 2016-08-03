namespace SmartLife.Client.Sms
{
    partial class frmGetSms
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
            this.tb_GetSms = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tb_GetSms
            // 
            this.tb_GetSms.BackColor = System.Drawing.Color.Black;
            this.tb_GetSms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_GetSms.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_GetSms.ForeColor = System.Drawing.Color.White;
            this.tb_GetSms.Location = new System.Drawing.Point(0, 0);
            this.tb_GetSms.Multiline = true;
            this.tb_GetSms.Name = "tb_GetSms";
            this.tb_GetSms.ReadOnly = true;
            this.tb_GetSms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_GetSms.Size = new System.Drawing.Size(284, 262);
            this.tb_GetSms.TabIndex = 0;
            // 
            // frmGetSms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tb_GetSms);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmGetSms";
            this.Text = "获取短信";
            this.Load += new System.EventHandler(this.frmGetSms_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_GetSms;
    }
}