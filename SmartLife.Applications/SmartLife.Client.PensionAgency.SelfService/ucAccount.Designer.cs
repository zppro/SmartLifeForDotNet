namespace SmartLife.Client.PensionAgency.SelfService
{
    partial class ucAccount
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
            this.lblGOV = new System.Windows.Forms.Label();
            this.lblSelf = new System.Windows.Forms.Label();
            this.lblGOVAccount = new System.Windows.Forms.Label();
            this.lblSelfAccount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblGOV
            // 
            this.lblGOV.Font = new System.Drawing.Font("华文楷体", 24F);
            this.lblGOV.Location = new System.Drawing.Point(70, 41);
            this.lblGOV.Name = "lblGOV";
            this.lblGOV.Size = new System.Drawing.Size(231, 40);
            this.lblGOV.TabIndex = 3;
            this.lblGOV.Text = "政府补助账户:";
            this.lblGOV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSelf
            // 
            this.lblSelf.Font = new System.Drawing.Font("华文楷体", 24F);
            this.lblSelf.Location = new System.Drawing.Point(70, 134);
            this.lblSelf.Name = "lblSelf";
            this.lblSelf.Size = new System.Drawing.Size(216, 40);
            this.lblSelf.TabIndex = 4;
            this.lblSelf.Text = "个人充值账户:";
            this.lblSelf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGOVAccount
            // 
            this.lblGOVAccount.Font = new System.Drawing.Font("华文楷体", 18F);
            this.lblGOVAccount.Location = new System.Drawing.Point(307, 48);
            this.lblGOVAccount.Name = "lblGOVAccount";
            this.lblGOVAccount.Size = new System.Drawing.Size(274, 30);
            this.lblGOVAccount.TabIndex = 6;
            this.lblGOVAccount.Text = "--";
            this.lblGOVAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSelfAccount
            // 
            this.lblSelfAccount.Font = new System.Drawing.Font("华文楷体", 18F);
            this.lblSelfAccount.Location = new System.Drawing.Point(307, 141);
            this.lblSelfAccount.Name = "lblSelfAccount";
            this.lblSelfAccount.Size = new System.Drawing.Size(274, 30);
            this.lblSelfAccount.TabIndex = 7;
            this.lblSelfAccount.Text = "--";
            this.lblSelfAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblSelfAccount);
            this.Controls.Add(this.lblGOVAccount);
            this.Controls.Add(this.lblSelf);
            this.Controls.Add(this.lblGOV);
            this.Name = "ucAccount";
            this.Size = new System.Drawing.Size(777, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblGOV;
        private System.Windows.Forms.Label lblSelf;
        private System.Windows.Forms.Label lblGOVAccount;
        private System.Windows.Forms.Label lblSelfAccount;
    }
}
