namespace WindowsFormsDataBase
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxIsNull = new System.Windows.Forms.CheckBox();
            this.labTarget = new System.Windows.Forms.Label();
            this.btnTestTarget = new System.Windows.Forms.Button();
            this.cbxTargetDataBases = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labSource = new System.Windows.Forms.Label();
            this.btnTestSource = new System.Windows.Forms.Button();
            this.cbxSourceDataBases = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbx_DDL_All = new System.Windows.Forms.CheckBox();
            this.cbx_DDL_Job = new System.Windows.Forms.CheckBox();
            this.cbx_DDL_Type = new System.Windows.Forms.CheckBox();
            this.cbx_DDL_Function = new System.Windows.Forms.CheckBox();
            this.cbx_DDL_Procedure = new System.Windows.Forms.CheckBox();
            this.cbx_DDL_View = new System.Windows.Forms.CheckBox();
            this.cbx_DDL_Table = new System.Windows.Forms.CheckBox();
            this.btnCreateDDL = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txbResult = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgv_DataBaseName = new System.Windows.Forms.DataGridView();
            this.btnCreateDML = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DataBaseName)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxIsNull);
            this.groupBox1.Controls.Add(this.labTarget);
            this.groupBox1.Controls.Add(this.btnTestTarget);
            this.groupBox1.Controls.Add(this.cbxTargetDataBases);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labSource);
            this.groupBox1.Controls.Add(this.btnTestSource);
            this.groupBox1.Controls.Add(this.cbxSourceDataBases);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择数据库";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "目标库为空：";
            // 
            // cbxIsNull
            // 
            this.cbxIsNull.AutoSize = true;
            this.cbxIsNull.Location = new System.Drawing.Point(291, 69);
            this.cbxIsNull.Name = "cbxIsNull";
            this.cbxIsNull.Size = new System.Drawing.Size(15, 14);
            this.cbxIsNull.TabIndex = 5;
            this.cbxIsNull.UseVisualStyleBackColor = true;
            this.cbxIsNull.CheckedChanged += new System.EventHandler(this.cbxIsNull_CheckedChanged);
            // 
            // labTarget
            // 
            this.labTarget.AutoSize = true;
            this.labTarget.Location = new System.Drawing.Point(434, 66);
            this.labTarget.Name = "labTarget";
            this.labTarget.Size = new System.Drawing.Size(53, 12);
            this.labTarget.TabIndex = 7;
            this.labTarget.Text = "连接结果";
            // 
            // btnTestTarget
            // 
            this.btnTestTarget.Location = new System.Drawing.Point(337, 61);
            this.btnTestTarget.Name = "btnTestTarget";
            this.btnTestTarget.Size = new System.Drawing.Size(75, 23);
            this.btnTestTarget.TabIndex = 6;
            this.btnTestTarget.Text = "测试连接";
            this.btnTestTarget.UseVisualStyleBackColor = true;
            this.btnTestTarget.Click += new System.EventHandler(this.btnTestTarget_Click);
            // 
            // cbxTargetDataBases
            // 
            this.cbxTargetDataBases.FormattingEnabled = true;
            this.cbxTargetDataBases.Location = new System.Drawing.Point(339, 33);
            this.cbxTargetDataBases.Name = "cbxTargetDataBases";
            this.cbxTargetDataBases.Size = new System.Drawing.Size(178, 20);
            this.cbxTargetDataBases.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "目标数据库：";
            // 
            // labSource
            // 
            this.labSource.AutoSize = true;
            this.labSource.Location = new System.Drawing.Point(117, 69);
            this.labSource.Name = "labSource";
            this.labSource.Size = new System.Drawing.Size(53, 12);
            this.labSource.TabIndex = 3;
            this.labSource.Text = "连接结果";
            // 
            // btnTestSource
            // 
            this.btnTestSource.Location = new System.Drawing.Point(20, 64);
            this.btnTestSource.Name = "btnTestSource";
            this.btnTestSource.Size = new System.Drawing.Size(75, 23);
            this.btnTestSource.TabIndex = 2;
            this.btnTestSource.Text = "测试连接";
            this.btnTestSource.UseVisualStyleBackColor = true;
            this.btnTestSource.Click += new System.EventHandler(this.btnTestSource_Click);
            // 
            // cbxSourceDataBases
            // 
            this.cbxSourceDataBases.FormattingEnabled = true;
            this.cbxSourceDataBases.Location = new System.Drawing.Point(76, 31);
            this.cbxSourceDataBases.Name = "cbxSourceDataBases";
            this.cbxSourceDataBases.Size = new System.Drawing.Size(169, 20);
            this.cbxSourceDataBases.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源数据库：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbx_DDL_All);
            this.groupBox2.Controls.Add(this.cbx_DDL_Job);
            this.groupBox2.Controls.Add(this.cbx_DDL_Type);
            this.groupBox2.Controls.Add(this.cbx_DDL_Function);
            this.groupBox2.Controls.Add(this.cbx_DDL_Procedure);
            this.groupBox2.Controls.Add(this.cbx_DDL_View);
            this.groupBox2.Controls.Add(this.cbx_DDL_Table);
            this.groupBox2.Controls.Add(this.btnCreateDDL);
            this.groupBox2.Location = new System.Drawing.Point(22, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(527, 106);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DDL";
            // 
            // cbx_DDL_All
            // 
            this.cbx_DDL_All.AutoSize = true;
            this.cbx_DDL_All.Location = new System.Drawing.Point(38, 29);
            this.cbx_DDL_All.Name = "cbx_DDL_All";
            this.cbx_DDL_All.Size = new System.Drawing.Size(48, 16);
            this.cbx_DDL_All.TabIndex = 4;
            this.cbx_DDL_All.Text = "全选";
            this.cbx_DDL_All.UseVisualStyleBackColor = true;
            this.cbx_DDL_All.CheckedChanged += new System.EventHandler(this.cbx_DDL_All_CheckedChanged);
            // 
            // cbx_DDL_Job
            // 
            this.cbx_DDL_Job.AutoSize = true;
            this.cbx_DDL_Job.Location = new System.Drawing.Point(303, 62);
            this.cbx_DDL_Job.Name = "cbx_DDL_Job";
            this.cbx_DDL_Job.Size = new System.Drawing.Size(48, 16);
            this.cbx_DDL_Job.TabIndex = 4;
            this.cbx_DDL_Job.Text = "作业";
            this.cbx_DDL_Job.UseVisualStyleBackColor = true;
            // 
            // cbx_DDL_Type
            // 
            this.cbx_DDL_Type.AutoSize = true;
            this.cbx_DDL_Type.Location = new System.Drawing.Point(179, 62);
            this.cbx_DDL_Type.Name = "cbx_DDL_Type";
            this.cbx_DDL_Type.Size = new System.Drawing.Size(48, 16);
            this.cbx_DDL_Type.TabIndex = 4;
            this.cbx_DDL_Type.Text = "类型";
            this.cbx_DDL_Type.UseVisualStyleBackColor = true;
            // 
            // cbx_DDL_Function
            // 
            this.cbx_DDL_Function.AutoSize = true;
            this.cbx_DDL_Function.Location = new System.Drawing.Point(38, 62);
            this.cbx_DDL_Function.Name = "cbx_DDL_Function";
            this.cbx_DDL_Function.Size = new System.Drawing.Size(48, 16);
            this.cbx_DDL_Function.TabIndex = 4;
            this.cbx_DDL_Function.Text = "函数";
            this.cbx_DDL_Function.UseVisualStyleBackColor = true;
            // 
            // cbx_DDL_Procedure
            // 
            this.cbx_DDL_Procedure.AutoSize = true;
            this.cbx_DDL_Procedure.Location = new System.Drawing.Point(436, 29);
            this.cbx_DDL_Procedure.Name = "cbx_DDL_Procedure";
            this.cbx_DDL_Procedure.Size = new System.Drawing.Size(72, 16);
            this.cbx_DDL_Procedure.TabIndex = 4;
            this.cbx_DDL_Procedure.Text = "存储过程";
            this.cbx_DDL_Procedure.UseVisualStyleBackColor = true;
            // 
            // cbx_DDL_View
            // 
            this.cbx_DDL_View.AutoSize = true;
            this.cbx_DDL_View.Location = new System.Drawing.Point(303, 29);
            this.cbx_DDL_View.Name = "cbx_DDL_View";
            this.cbx_DDL_View.Size = new System.Drawing.Size(48, 16);
            this.cbx_DDL_View.TabIndex = 4;
            this.cbx_DDL_View.Text = "视图";
            this.cbx_DDL_View.UseVisualStyleBackColor = true;
            // 
            // cbx_DDL_Table
            // 
            this.cbx_DDL_Table.AutoSize = true;
            this.cbx_DDL_Table.Location = new System.Drawing.Point(179, 29);
            this.cbx_DDL_Table.Name = "cbx_DDL_Table";
            this.cbx_DDL_Table.Size = new System.Drawing.Size(36, 16);
            this.cbx_DDL_Table.TabIndex = 4;
            this.cbx_DDL_Table.Text = "表";
            this.cbx_DDL_Table.UseVisualStyleBackColor = true;
            // 
            // btnCreateDDL
            // 
            this.btnCreateDDL.Location = new System.Drawing.Point(415, 62);
            this.btnCreateDDL.Name = "btnCreateDDL";
            this.btnCreateDDL.Size = new System.Drawing.Size(93, 23);
            this.btnCreateDDL.TabIndex = 3;
            this.btnCreateDDL.Text = "生成脚本—DDL";
            this.btnCreateDDL.UseVisualStyleBackColor = true;
            this.btnCreateDDL.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(464, 579);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "退   出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txbResult
            // 
            this.txbResult.Location = new System.Drawing.Point(10, 17);
            this.txbResult.Multiline = true;
            this.txbResult.Name = "txbResult";
            this.txbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbResult.Size = new System.Drawing.Size(510, 150);
            this.txbResult.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txbResult);
            this.groupBox3.Location = new System.Drawing.Point(19, 398);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(530, 175);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "历史信息";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgv_DataBaseName);
            this.groupBox4.Controls.Add(this.btnCreateDML);
            this.groupBox4.Location = new System.Drawing.Point(22, 242);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(527, 150);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "DML";
            // 
            // dgv_DataBaseName
            // 
            this.dgv_DataBaseName.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_DataBaseName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DataBaseName.Location = new System.Drawing.Point(25, 20);
            this.dgv_DataBaseName.Name = "dgv_DataBaseName";
            this.dgv_DataBaseName.RowHeadersWidth = 5;
            this.dgv_DataBaseName.RowTemplate.Height = 23;
            this.dgv_DataBaseName.Size = new System.Drawing.Size(374, 113);
            this.dgv_DataBaseName.TabIndex = 2;
            // 
            // btnCreateDML
            // 
            this.btnCreateDML.Location = new System.Drawing.Point(415, 110);
            this.btnCreateDML.Name = "btnCreateDML";
            this.btnCreateDML.Size = new System.Drawing.Size(93, 23);
            this.btnCreateDML.TabIndex = 1;
            this.btnCreateDML.Text = "生成脚本—DML";
            this.btnCreateDML.UseVisualStyleBackColor = true;
            this.btnCreateDML.Click += new System.EventHandler(this.btnCreateDML_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(22, 580);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(137, 23);
            this.btnOpenFile.TabIndex = 4;
            this.btnOpenFile.Text = "打开脚本存放的文件夹";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 613);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "蓝谷科技数据库工具  0.3";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DataBaseName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTestSource;
        private System.Windows.Forms.ComboBox cbxSourceDataBases;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labTarget;
        private System.Windows.Forms.Button btnTestTarget;
        private System.Windows.Forms.ComboBox cbxTargetDataBases;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labSource;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCreateDDL;
        private System.Windows.Forms.TextBox txbResult;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox cbx_DDL_All;
        private System.Windows.Forms.CheckBox cbx_DDL_Type;
        private System.Windows.Forms.CheckBox cbx_DDL_Function;
        private System.Windows.Forms.CheckBox cbx_DDL_Procedure;
        private System.Windows.Forms.CheckBox cbx_DDL_View;
        private System.Windows.Forms.CheckBox cbx_DDL_Table;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnCreateDML;
        private System.Windows.Forms.DataGridView dgv_DataBaseName;
        private System.Windows.Forms.CheckBox cbxIsNull;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbx_DDL_Job;
        private System.Windows.Forms.Button btnOpenFile;
    }
}

