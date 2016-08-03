namespace SmartLife.Client.PensionAgency.SelfService
{
    partial class frmSplash
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
            this.lblLoadItems = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.lblTechSupport = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLoadItems
            // 
            this.lblLoadItems.BackColor = System.Drawing.Color.Transparent;
            this.lblLoadItems.ForeColor = System.Drawing.Color.White;
            this.lblLoadItems.Location = new System.Drawing.Point(12, 232);
            this.lblLoadItems.Name = "lblLoadItems";
            this.lblLoadItems.Size = new System.Drawing.Size(270, 16);
            this.lblLoadItems.TabIndex = 1;
            this.lblLoadItems.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblError
            // 
            this.lblError.BackColor = System.Drawing.Color.Transparent;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(14, 86);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(354, 23);
            this.lblError.TabIndex = 2;
            // 
            // lblTechSupport
            // 
            this.lblTechSupport.BackColor = System.Drawing.Color.Transparent;
            this.lblTechSupport.ForeColor = System.Drawing.Color.SlateBlue;
            this.lblTechSupport.Location = new System.Drawing.Point(293, 232);
            this.lblTechSupport.Name = "lblTechSupport";
            this.lblTechSupport.Size = new System.Drawing.Size(80, 16);
            this.lblTechSupport.TabIndex = 21;
            this.lblTechSupport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SmartLife.Client.PensionAgency.SelfService.Properties.Resources.splash_bg;
            this.ClientSize = new System.Drawing.Size(380, 255);
            this.Controls.Add(this.lblTechSupport);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblLoadItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSplash";
            this.Load += new System.EventHandler(this.frmSplash_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLoadItems;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblTechSupport;
    }
}