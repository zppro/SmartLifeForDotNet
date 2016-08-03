namespace SmartLife.Client.Merchant
{
    partial class frmWorkOrderFinished
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
            this.spA = new System.Windows.Forms.SplitContainer();
            this.pnlSearchBox = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dateTimePickerLeaveTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerArrivalTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txbOldManName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlFeedbackToOldManName = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.rb00004of11017 = new System.Windows.Forms.RadioButton();
            this.rb00005of11017 = new System.Windows.Forms.RadioButton();
            this.rb00000of11017 = new System.Windows.Forms.RadioButton();
            this.rb00003of11017 = new System.Windows.Forms.RadioButton();
            this.rb00002of11017 = new System.Windows.Forms.RadioButton();
            this.rb00001of11017 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numUDServiceTimeTo = new System.Windows.Forms.NumericUpDown();
            this.numUDServiceTimeFrom = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.xLoadingPanel = new win.core.controls.LoadingPanel();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.xSource = new System.Windows.Forms.BindingSource(this.components);
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spA)).BeginInit();
            this.spA.Panel1.SuspendLayout();
            this.spA.Panel2.SuspendLayout();
            this.spA.SuspendLayout();
            this.pnlSearchBox.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlFeedbackToOldManName.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDServiceTimeTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDServiceTimeFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xSource)).BeginInit();
            this.SuspendLayout();
            // 
            // spA
            // 
            this.spA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spA.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spA.IsSplitterFixed = true;
            this.spA.Location = new System.Drawing.Point(0, 0);
            this.spA.Name = "spA";
            this.spA.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spA.Panel1
            // 
            this.spA.Panel1.Controls.Add(this.pnlSearchBox);
            this.spA.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // spA.Panel2
            // 
            this.spA.Panel2.Controls.Add(this.xLoadingPanel);
            this.spA.Panel2.Controls.Add(this.dgvList);
            this.spA.Size = new System.Drawing.Size(818, 636);
            this.spA.TabIndex = 0;
            // 
            // pnlSearchBox
            // 
            this.pnlSearchBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSearchBox.Controls.Add(this.panel3);
            this.pnlSearchBox.Controls.Add(this.panel4);
            this.pnlSearchBox.Controls.Add(this.pnlFeedbackToOldManName);
            this.pnlSearchBox.Controls.Add(this.panel2);
            this.pnlSearchBox.Controls.Add(this.btnQuery);
            this.pnlSearchBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchBox.Location = new System.Drawing.Point(5, 5);
            this.pnlSearchBox.Name = "pnlSearchBox";
            this.pnlSearchBox.Size = new System.Drawing.Size(808, 40);
            this.pnlSearchBox.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.dateTimePickerLeaveTime);
            this.panel3.Controls.Add(this.dateTimePickerArrivalTime);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(604, -3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(324, 43);
            this.panel3.TabIndex = 17;
            // 
            // dateTimePickerLeaveTime
            // 
            this.dateTimePickerLeaveTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerLeaveTime.Location = new System.Drawing.Point(121, 17);
            this.dateTimePickerLeaveTime.Name = "dateTimePickerLeaveTime";
            this.dateTimePickerLeaveTime.Size = new System.Drawing.Size(153, 21);
            this.dateTimePickerLeaveTime.TabIndex = 12;
            this.dateTimePickerLeaveTime.Visible = false;
            // 
            // dateTimePickerArrivalTime
            // 
            this.dateTimePickerArrivalTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerArrivalTime.Location = new System.Drawing.Point(121, -2);
            this.dateTimePickerArrivalTime.Name = "dateTimePickerArrivalTime";
            this.dateTimePickerArrivalTime.Size = new System.Drawing.Size(153, 21);
            this.dateTimePickerArrivalTime.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "服务人员离开时间：";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "服务人员到达时间从";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txbOldManName);
            this.panel4.Controls.Add(this.lblName);
            this.panel4.Location = new System.Drawing.Point(-2, -2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(142, 38);
            this.panel4.TabIndex = 16;
            // 
            // txbOldManName
            // 
            this.txbOldManName.Location = new System.Drawing.Point(39, 9);
            this.txbOldManName.Name = "txbOldManName";
            this.txbOldManName.Size = new System.Drawing.Size(90, 21);
            this.txbOldManName.TabIndex = 4;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(4, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(41, 12);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "姓名：";
            // 
            // pnlFeedbackToOldManName
            // 
            this.pnlFeedbackToOldManName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlFeedbackToOldManName.Controls.Add(this.label4);
            this.pnlFeedbackToOldManName.Controls.Add(this.rb00004of11017);
            this.pnlFeedbackToOldManName.Controls.Add(this.rb00005of11017);
            this.pnlFeedbackToOldManName.Controls.Add(this.rb00000of11017);
            this.pnlFeedbackToOldManName.Controls.Add(this.rb00003of11017);
            this.pnlFeedbackToOldManName.Controls.Add(this.rb00002of11017);
            this.pnlFeedbackToOldManName.Controls.Add(this.rb00001of11017);
            this.pnlFeedbackToOldManName.Location = new System.Drawing.Point(334, -2);
            this.pnlFeedbackToOldManName.Name = "pnlFeedbackToOldManName";
            this.pnlFeedbackToOldManName.Size = new System.Drawing.Size(272, 38);
            this.pnlFeedbackToOldManName.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "满意度：";
            // 
            // rb00004of11017
            // 
            this.rb00004of11017.AutoSize = true;
            this.rb00004of11017.Location = new System.Drawing.Point(61, 20);
            this.rb00004of11017.Name = "rb00004of11017";
            this.rb00004of11017.Size = new System.Drawing.Size(71, 16);
            this.rb00004of11017.TabIndex = 5;
            this.rb00004of11017.TabStop = true;
            this.rb00004of11017.Text = "较不满意";
            this.rb00004of11017.UseVisualStyleBackColor = true;
            // 
            // rb00005of11017
            // 
            this.rb00005of11017.AutoSize = true;
            this.rb00005of11017.Location = new System.Drawing.Point(138, 19);
            this.rb00005of11017.Name = "rb00005of11017";
            this.rb00005of11017.Size = new System.Drawing.Size(59, 16);
            this.rb00005of11017.TabIndex = 4;
            this.rb00005of11017.TabStop = true;
            this.rb00005of11017.Text = "不满意";
            this.rb00005of11017.UseVisualStyleBackColor = true;
            // 
            // rb00000of11017
            // 
            this.rb00000of11017.AutoSize = true;
            this.rb00000of11017.Checked = true;
            this.rb00000of11017.Location = new System.Drawing.Point(217, 19);
            this.rb00000of11017.Name = "rb00000of11017";
            this.rb00000of11017.Size = new System.Drawing.Size(47, 16);
            this.rb00000of11017.TabIndex = 4;
            this.rb00000of11017.TabStop = true;
            this.rb00000of11017.Text = "全选";
            this.rb00000of11017.UseVisualStyleBackColor = true;
            // 
            // rb00003of11017
            // 
            this.rb00003of11017.AutoSize = true;
            this.rb00003of11017.Location = new System.Drawing.Point(217, 0);
            this.rb00003of11017.Name = "rb00003of11017";
            this.rb00003of11017.Size = new System.Drawing.Size(47, 16);
            this.rb00003of11017.TabIndex = 4;
            this.rb00003of11017.TabStop = true;
            this.rb00003of11017.Text = "一般";
            this.rb00003of11017.UseVisualStyleBackColor = true;
            // 
            // rb00002of11017
            // 
            this.rb00002of11017.AutoSize = true;
            this.rb00002of11017.Location = new System.Drawing.Point(138, 1);
            this.rb00002of11017.Name = "rb00002of11017";
            this.rb00002of11017.Size = new System.Drawing.Size(59, 16);
            this.rb00002of11017.TabIndex = 4;
            this.rb00002of11017.TabStop = true;
            this.rb00002of11017.Text = "较满意";
            this.rb00002of11017.UseVisualStyleBackColor = true;
            // 
            // rb00001of11017
            // 
            this.rb00001of11017.AutoSize = true;
            this.rb00001of11017.Location = new System.Drawing.Point(60, 0);
            this.rb00001of11017.Name = "rb00001of11017";
            this.rb00001of11017.Size = new System.Drawing.Size(47, 16);
            this.rb00001of11017.TabIndex = 4;
            this.rb00001of11017.TabStop = true;
            this.rb00001of11017.Text = "满意";
            this.rb00001of11017.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.numUDServiceTimeTo);
            this.panel2.Controls.Add(this.numUDServiceTimeFrom);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(140, -3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 43);
            this.panel2.TabIndex = 13;
            // 
            // numUDServiceTimeTo
            // 
            this.numUDServiceTimeTo.Location = new System.Drawing.Point(115, 8);
            this.numUDServiceTimeTo.Name = "numUDServiceTimeTo";
            this.numUDServiceTimeTo.Size = new System.Drawing.Size(30, 21);
            this.numUDServiceTimeTo.TabIndex = 10;
            this.numUDServiceTimeTo.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numUDServiceTimeFrom
            // 
            this.numUDServiceTimeFrom.Location = new System.Drawing.Point(61, 9);
            this.numUDServiceTimeFrom.Name = "numUDServiceTimeFrom";
            this.numUDServiceTimeFrom.Size = new System.Drawing.Size(30, 21);
            this.numUDServiceTimeFrom.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "小时";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "—";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "服务时长：";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(724, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnQuery.Size = new System.Drawing.Size(75, 22);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // xLoadingPanel
            // 
            this.xLoadingPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.xLoadingPanel.Interval = 100;
            this.xLoadingPanel.LoadingTextColor = System.Drawing.SystemColors.ControlText;
            this.xLoadingPanel.Location = new System.Drawing.Point(313, 231);
            this.xLoadingPanel.MinimumSize = new System.Drawing.Size(100, 30);
            this.xLoadingPanel.Name = "xLoadingPanel";
            this.xLoadingPanel.RingColor = System.Drawing.SystemColors.ControlText;
            this.xLoadingPanel.RingTailColor = System.Drawing.Color.White;
            this.xLoadingPanel.Size = new System.Drawing.Size(200, 30);
            this.xLoadingPanel.TabIndex = 1;
            this.xLoadingPanel.TailNumber = 6;
            this.xLoadingPanel.Visible = false;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(818, 582);
            this.dgvList.TabIndex = 0;
            this.dgvList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_RowEnter);
            this.dgvList.DoubleClick += new System.EventHandler(this.dgvList_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(279, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "至今";
            // 
            // frmWorkOrderFinished
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 636);
            this.ControlBox = false;
            this.Controls.Add(this.spA);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmWorkOrderFinished";
            this.Text = "frmWorkOrderToResponse";
            this.Load += new System.EventHandler(this.frmWorkOrderToResponse_Load);
            this.Shown += new System.EventHandler(this.frmWorkOrderToResponse_Shown);
            this.spA.Panel1.ResumeLayout(false);
            this.spA.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spA)).EndInit();
            this.spA.ResumeLayout(false);
            this.pnlSearchBox.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlFeedbackToOldManName.ResumeLayout(false);
            this.pnlFeedbackToOldManName.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDServiceTimeTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDServiceTimeFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spA;
        private System.Windows.Forms.BindingSource xSource;
        private System.Windows.Forms.Panel pnlSearchBox;
        private win.core.controls.LoadingPanel xLoadingPanel;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txbOldManName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel pnlFeedbackToOldManName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rb00004of11017;
        private System.Windows.Forms.RadioButton rb00001of11017;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb00005of11017;
        private System.Windows.Forms.RadioButton rb00003of11017;
        private System.Windows.Forms.RadioButton rb00002of11017;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker dateTimePickerLeaveTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerArrivalTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rb00000of11017;
        private System.Windows.Forms.NumericUpDown numUDServiceTimeTo;
        private System.Windows.Forms.NumericUpDown numUDServiceTimeFrom;
        private System.Windows.Forms.Label label7;

    }
}