namespace SmartLife.Client.Seat
{
    partial class frmTransfer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransfer));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTransferExtNo = new System.Windows.Forms.TextBox();
            this.cbTransferQueues = new System.Windows.Forms.ComboBox();
            this.rbExt = new System.Windows.Forms.RadioButton();
            this.rbQueue = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTransferExtNo);
            this.panel1.Controls.Add(this.cbTransferQueues);
            this.panel1.Controls.Add(this.rbExt);
            this.panel1.Controls.Add(this.rbQueue);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 112);
            this.panel1.TabIndex = 0;
            // 
            // txtTransferExtNo
            // 
            this.txtTransferExtNo.Location = new System.Drawing.Point(102, 68);
            this.txtTransferExtNo.Name = "txtTransferExtNo";
            this.txtTransferExtNo.Size = new System.Drawing.Size(183, 21);
            this.txtTransferExtNo.TabIndex = 3;
            // 
            // cbTransferQueues
            // 
            this.cbTransferQueues.FormattingEnabled = true;
            this.cbTransferQueues.Location = new System.Drawing.Point(102, 19);
            this.cbTransferQueues.Name = "cbTransferQueues";
            this.cbTransferQueues.Size = new System.Drawing.Size(183, 20);
            this.cbTransferQueues.TabIndex = 2;
            // 
            // rbExt
            // 
            this.rbExt.AutoSize = true;
            this.rbExt.Location = new System.Drawing.Point(21, 69);
            this.rbExt.Name = "rbExt";
            this.rbExt.Size = new System.Drawing.Size(59, 16);
            this.rbExt.TabIndex = 1;
            this.rbExt.TabStop = true;
            this.rbExt.Text = "按分机";
            this.rbExt.UseVisualStyleBackColor = true;
            this.rbExt.CheckedChanged += new System.EventHandler(this.rbExt_CheckedChanged);
            // 
            // rbQueue
            // 
            this.rbQueue.AutoSize = true;
            this.rbQueue.Location = new System.Drawing.Point(21, 20);
            this.rbQueue.Name = "rbQueue";
            this.rbQueue.Size = new System.Drawing.Size(59, 16);
            this.rbQueue.TabIndex = 0;
            this.rbQueue.TabStop = true;
            this.rbQueue.Text = "按队列";
            this.rbQueue.UseVisualStyleBackColor = true;
            this.rbQueue.CheckedChanged += new System.EventHandler(this.rbQueue_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(159, 133);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(240, 133);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 167);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "呼叫转移对话框";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmTransfer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbExt;
        private System.Windows.Forms.RadioButton rbQueue;
        private System.Windows.Forms.TextBox txtTransferExtNo;
        private System.Windows.Forms.ComboBox cbTransferQueues;
    }
}