namespace SmartLife.Client.PensionAgency.Order
{
    partial class frmBindingPA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBindingPA));
            this.txtBindingCode = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnBinding = new System.Windows.Forms.Button();
            this.lblLoadItems = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBindingCode
            // 
            this.txtBindingCode.Font = new System.Drawing.Font("宋体", 48F);
            this.txtBindingCode.Location = new System.Drawing.Point(12, 25);
            this.txtBindingCode.Name = "txtBindingCode";
            this.txtBindingCode.Size = new System.Drawing.Size(526, 80);
            this.txtBindingCode.TabIndex = 0;
            this.txtBindingCode.Text = "4612030003001001";
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("华文行楷", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(345, 124);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(193, 85);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnBinding
            // 
            this.btnBinding.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBinding.BackgroundImage")));
            this.btnBinding.Font = new System.Drawing.Font("华文行楷", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBinding.ForeColor = System.Drawing.Color.White;
            this.btnBinding.Location = new System.Drawing.Point(12, 124);
            this.btnBinding.Name = "btnBinding";
            this.btnBinding.Size = new System.Drawing.Size(193, 85);
            this.btnBinding.TabIndex = 15;
            this.btnBinding.Text = "绑定";
            this.btnBinding.UseVisualStyleBackColor = true;
            this.btnBinding.Click += new System.EventHandler(this.btnBinding_Click);
            // 
            // lblLoadItems
            // 
            this.lblLoadItems.BackColor = System.Drawing.Color.Transparent;
            this.lblLoadItems.ForeColor = System.Drawing.Color.White;
            this.lblLoadItems.Location = new System.Drawing.Point(10, 231);
            this.lblLoadItems.Name = "lblLoadItems";
            this.lblLoadItems.Size = new System.Drawing.Size(528, 21);
            this.lblLoadItems.TabIndex = 16;
            this.lblLoadItems.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // frmBindingPA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(108)))), ((int)(((byte)(228)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(555, 261);
            this.Controls.Add(this.lblLoadItems);
            this.Controls.Add(this.btnBinding);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtBindingCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBindingPA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBindingPA";
            this.Load += new System.EventHandler(this.frmBindingPA_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBindingCode;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnBinding;
        private System.Windows.Forms.Label lblLoadItems;
    }
}