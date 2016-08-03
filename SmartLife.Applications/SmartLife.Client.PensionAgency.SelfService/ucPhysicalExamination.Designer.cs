namespace SmartLife.Client.PensionAgency.SelfService
{
    partial class ucPhysicalExamination
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
            this.lblSBP = new System.Windows.Forms.Label();
            this.lblDBP = new System.Windows.Forms.Label();
            this.lblSBPTitle = new System.Windows.Forms.Label();
            this.lblDBPTitle = new System.Windows.Forms.Label();
            this.lblHRTitle = new System.Windows.Forms.Label();
            this.lblHR = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSBP
            // 
            this.lblSBP.Font = new System.Drawing.Font("华文楷体", 18F);
            this.lblSBP.Location = new System.Drawing.Point(170, 119);
            this.lblSBP.Name = "lblSBP";
            this.lblSBP.Size = new System.Drawing.Size(274, 30);
            this.lblSBP.TabIndex = 11;
            this.lblSBP.Text = "--";
            this.lblSBP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDBP
            // 
            this.lblDBP.Font = new System.Drawing.Font("华文楷体", 18F);
            this.lblDBP.Location = new System.Drawing.Point(170, 26);
            this.lblDBP.Name = "lblDBP";
            this.lblDBP.Size = new System.Drawing.Size(274, 30);
            this.lblDBP.TabIndex = 10;
            this.lblDBP.Text = "--";
            this.lblDBP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSBPTitle
            // 
            this.lblSBPTitle.Font = new System.Drawing.Font("华文楷体", 24F);
            this.lblSBPTitle.Location = new System.Drawing.Point(39, 112);
            this.lblSBPTitle.Name = "lblSBPTitle";
            this.lblSBPTitle.Size = new System.Drawing.Size(137, 40);
            this.lblSBPTitle.TabIndex = 9;
            this.lblSBPTitle.Text = "收缩压:";
            this.lblSBPTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDBPTitle
            // 
            this.lblDBPTitle.Font = new System.Drawing.Font("华文楷体", 24F);
            this.lblDBPTitle.Location = new System.Drawing.Point(39, 19);
            this.lblDBPTitle.Name = "lblDBPTitle";
            this.lblDBPTitle.Size = new System.Drawing.Size(137, 40);
            this.lblDBPTitle.TabIndex = 8;
            this.lblDBPTitle.Text = "舒张压:";
            this.lblDBPTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHRTitle
            // 
            this.lblHRTitle.Font = new System.Drawing.Font("华文楷体", 24F);
            this.lblHRTitle.Location = new System.Drawing.Point(39, 205);
            this.lblHRTitle.Name = "lblHRTitle";
            this.lblHRTitle.Size = new System.Drawing.Size(137, 40);
            this.lblHRTitle.TabIndex = 12;
            this.lblHRTitle.Text = "心    率:";
            this.lblHRTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHR
            // 
            this.lblHR.Font = new System.Drawing.Font("华文楷体", 18F);
            this.lblHR.Location = new System.Drawing.Point(170, 212);
            this.lblHR.Name = "lblHR";
            this.lblHR.Size = new System.Drawing.Size(274, 30);
            this.lblHR.TabIndex = 13;
            this.lblHR.Text = "--";
            this.lblHR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucPhysicalExamination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblHR);
            this.Controls.Add(this.lblHRTitle);
            this.Controls.Add(this.lblSBP);
            this.Controls.Add(this.lblDBP);
            this.Controls.Add(this.lblSBPTitle);
            this.Controls.Add(this.lblDBPTitle);
            this.Name = "ucPhysicalExamination";
            this.Size = new System.Drawing.Size(777, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSBP;
        private System.Windows.Forms.Label lblDBP;
        private System.Windows.Forms.Label lblSBPTitle;
        private System.Windows.Forms.Label lblDBPTitle;
        private System.Windows.Forms.Label lblHRTitle;
        private System.Windows.Forms.Label lblHR;
    }
}
