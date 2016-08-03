namespace SmartLife.Client.Merchant
{
    partial class frmWorkOrderToResponseDetail
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
            this.xLoadingPanel = new win.core.controls.LoadingPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblDoStatus = new System.Windows.Forms.Label();
            this.lblWorkOrderNoTitle = new System.Windows.Forms.Label();
            this.lblWorkOrderNo = new System.Windows.Forms.Label();
            this.lblDoStatusTitle = new System.Windows.Forms.Label();
            this.pnlBaseInfo = new System.Windows.Forms.Panel();
            this.tlpInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblServeFeeBySelf = new System.Windows.Forms.Label();
            this.lblServeFeeByGov = new System.Windows.Forms.Label();
            this.lblServeFeeBySelfTitle = new System.Windows.Forms.Label();
            this.lblServeFeeByGovTitle = new System.Windows.Forms.Label();
            this.lblServeFee = new System.Windows.Forms.Label();
            this.lblServeFeeTitle = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblAddressTitle = new System.Windows.Forms.Label();
            this.lblMobile = new System.Windows.Forms.Label();
            this.lblMobileTitle = new System.Windows.Forms.Label();
            this.lblTel = new System.Windows.Forms.Label();
            this.lblTelTitle = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblGenderTitle = new System.Windows.Forms.Label();
            this.lblOldManName = new System.Windows.Forms.Label();
            this.lblOldManNameTitle = new System.Windows.Forms.Label();
            this.lblSupervisedByName = new System.Windows.Forms.Label();
            this.lblWorkOrderArea = new System.Windows.Forms.Label();
            this.lblWorkOrderAreaTitle = new System.Windows.Forms.Label();
            this.lblSupervisedByNameTitle = new System.Windows.Forms.Label();
            this.lblSuperviseTime = new System.Windows.Forms.Label();
            this.lblSuperviseTimeTitle = new System.Windows.Forms.Label();
            this.lblRemarkRequired = new System.Windows.Forms.Label();
            this.lblRemarkRequiredTitle = new System.Windows.Forms.Label();
            this.lblServiceTimeRequired = new System.Windows.Forms.Label();
            this.lblServiceTimeRequiredTitle = new System.Windows.Forms.Label();
            this.lblServeTypeName = new System.Windows.Forms.Label();
            this.lblServeTypeNameTitle = new System.Windows.Forms.Label();
            this.lblServeModeName = new System.Windows.Forms.Label();
            this.lblServeModeNameTitle = new System.Windows.Forms.Label();
            this.lblServeItemBName = new System.Windows.Forms.Label();
            this.lblServeItemBNameTitle = new System.Windows.Forms.Label();
            this.lblServeItemAName = new System.Windows.Forms.Label();
            this.lblServeItemANameTitle = new System.Windows.Forms.Label();
            this.lblWorkOrderContentTitle = new System.Windows.Forms.Label();
            this.lblWorkOrderContent = new System.Windows.Forms.Label();
            this.lblCurrentStation = new System.Windows.Forms.Label();
            this.lblOperatedByName = new System.Windows.Forms.Label();
            this.lblOperatedByNameTitle = new System.Windows.Forms.Label();
            this.pnlBaseInfo.SuspendLayout();
            this.tlpInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // xLoadingPanel
            // 
            this.xLoadingPanel.BackColor = System.Drawing.Color.White;
            this.xLoadingPanel.Interval = 100;
            this.xLoadingPanel.LoadingTextColor = System.Drawing.SystemColors.ControlText;
            this.xLoadingPanel.Location = new System.Drawing.Point(267, 178);
            this.xLoadingPanel.MinimumSize = new System.Drawing.Size(100, 30);
            this.xLoadingPanel.Name = "xLoadingPanel";
            this.xLoadingPanel.RingColor = System.Drawing.SystemColors.ControlText;
            this.xLoadingPanel.RingTailColor = System.Drawing.Color.White;
            this.xLoadingPanel.Size = new System.Drawing.Size(200, 30);
            this.xLoadingPanel.TabIndex = 18;
            this.xLoadingPanel.TailNumber = 6;
            this.xLoadingPanel.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(736, 334);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(655, 334);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "响应工单";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblDoStatus
            // 
            this.lblDoStatus.Location = new System.Drawing.Point(745, 41);
            this.lblDoStatus.Name = "lblDoStatus";
            this.lblDoStatus.Size = new System.Drawing.Size(61, 23);
            this.lblDoStatus.TabIndex = 15;
            this.lblDoStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWorkOrderNoTitle
            // 
            this.lblWorkOrderNoTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWorkOrderNoTitle.Location = new System.Drawing.Point(12, 41);
            this.lblWorkOrderNoTitle.Name = "lblWorkOrderNoTitle";
            this.lblWorkOrderNoTitle.Size = new System.Drawing.Size(77, 23);
            this.lblWorkOrderNoTitle.TabIndex = 12;
            this.lblWorkOrderNoTitle.Text = "工单编号：";
            this.lblWorkOrderNoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWorkOrderNo
            // 
            this.lblWorkOrderNo.Location = new System.Drawing.Point(95, 41);
            this.lblWorkOrderNo.Name = "lblWorkOrderNo";
            this.lblWorkOrderNo.Size = new System.Drawing.Size(225, 23);
            this.lblWorkOrderNo.TabIndex = 14;
            this.lblWorkOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDoStatusTitle
            // 
            this.lblDoStatusTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoStatusTitle.Location = new System.Drawing.Point(672, 41);
            this.lblDoStatusTitle.Name = "lblDoStatusTitle";
            this.lblDoStatusTitle.Size = new System.Drawing.Size(77, 23);
            this.lblDoStatusTitle.TabIndex = 13;
            this.lblDoStatusTitle.Text = "工单状态：";
            this.lblDoStatusTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlBaseInfo
            // 
            this.pnlBaseInfo.BackColor = System.Drawing.Color.White;
            this.pnlBaseInfo.Controls.Add(this.tlpInfo);
            this.pnlBaseInfo.Location = new System.Drawing.Point(14, 67);
            this.pnlBaseInfo.Name = "pnlBaseInfo";
            this.pnlBaseInfo.Size = new System.Drawing.Size(796, 255);
            this.pnlBaseInfo.TabIndex = 11;
            // 
            // tlpInfo
            // 
            this.tlpInfo.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpInfo.ColumnCount = 6;
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 163F));
            this.tlpInfo.Controls.Add(this.lblServeFeeBySelf, 5, 4);
            this.tlpInfo.Controls.Add(this.lblServeFeeByGov, 3, 4);
            this.tlpInfo.Controls.Add(this.lblServeFeeBySelfTitle, 4, 4);
            this.tlpInfo.Controls.Add(this.lblServeFeeByGovTitle, 2, 4);
            this.tlpInfo.Controls.Add(this.lblServeFee, 1, 4);
            this.tlpInfo.Controls.Add(this.lblServeFeeTitle, 0, 4);
            this.tlpInfo.Controls.Add(this.lblAddress, 3, 7);
            this.tlpInfo.Controls.Add(this.lblAddressTitle, 2, 7);
            this.tlpInfo.Controls.Add(this.lblMobile, 1, 7);
            this.tlpInfo.Controls.Add(this.lblMobileTitle, 0, 7);
            this.tlpInfo.Controls.Add(this.lblTel, 5, 6);
            this.tlpInfo.Controls.Add(this.lblTelTitle, 4, 6);
            this.tlpInfo.Controls.Add(this.lblGender, 3, 6);
            this.tlpInfo.Controls.Add(this.lblGenderTitle, 2, 6);
            this.tlpInfo.Controls.Add(this.lblOldManName, 1, 6);
            this.tlpInfo.Controls.Add(this.lblOldManNameTitle, 0, 6);
            this.tlpInfo.Controls.Add(this.lblSupervisedByName, 3, 5);
            this.tlpInfo.Controls.Add(this.lblWorkOrderArea, 1, 1);
            this.tlpInfo.Controls.Add(this.lblWorkOrderAreaTitle, 0, 1);
            this.tlpInfo.Controls.Add(this.lblSupervisedByNameTitle, 2, 5);
            this.tlpInfo.Controls.Add(this.lblSuperviseTime, 1, 5);
            this.tlpInfo.Controls.Add(this.lblSuperviseTimeTitle, 0, 5);
            this.tlpInfo.Controls.Add(this.lblRemarkRequired, 3, 3);
            this.tlpInfo.Controls.Add(this.lblRemarkRequiredTitle, 2, 3);
            this.tlpInfo.Controls.Add(this.lblServiceTimeRequired, 1, 3);
            this.tlpInfo.Controls.Add(this.lblServiceTimeRequiredTitle, 0, 3);
            this.tlpInfo.Controls.Add(this.lblServeTypeName, 5, 1);
            this.tlpInfo.Controls.Add(this.lblServeTypeNameTitle, 4, 1);
            this.tlpInfo.Controls.Add(this.lblServeModeName, 5, 2);
            this.tlpInfo.Controls.Add(this.lblServeModeNameTitle, 4, 2);
            this.tlpInfo.Controls.Add(this.lblServeItemBName, 3, 2);
            this.tlpInfo.Controls.Add(this.lblServeItemBNameTitle, 2, 2);
            this.tlpInfo.Controls.Add(this.lblServeItemAName, 1, 2);
            this.tlpInfo.Controls.Add(this.lblServeItemANameTitle, 0, 2);
            this.tlpInfo.Controls.Add(this.lblWorkOrderContentTitle, 0, 0);
            this.tlpInfo.Controls.Add(this.lblWorkOrderContent, 1, 0);
            this.tlpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInfo.Location = new System.Drawing.Point(0, 0);
            this.tlpInfo.Name = "tlpInfo";
            this.tlpInfo.RowCount = 8;
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInfo.Size = new System.Drawing.Size(796, 255);
            this.tlpInfo.TabIndex = 0;
            // 
            // lblServeFeeBySelf
            // 
            this.lblServeFeeBySelf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeFeeBySelf.Location = new System.Drawing.Point(639, 125);
            this.lblServeFeeBySelf.Name = "lblServeFeeBySelf";
            this.lblServeFeeBySelf.Size = new System.Drawing.Size(157, 30);
            this.lblServeFeeBySelf.TabIndex = 87;
            this.lblServeFeeBySelf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServeFeeByGov
            // 
            this.lblServeFeeByGov.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeFeeByGov.Location = new System.Drawing.Point(377, 125);
            this.lblServeFeeByGov.Name = "lblServeFeeByGov";
            this.lblServeFeeByGov.Size = new System.Drawing.Size(144, 30);
            this.lblServeFeeByGov.TabIndex = 86;
            this.lblServeFeeByGov.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServeFeeBySelfTitle
            // 
            this.lblServeFeeBySelfTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeFeeBySelfTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServeFeeBySelfTitle.Location = new System.Drawing.Point(528, 125);
            this.lblServeFeeBySelfTitle.Name = "lblServeFeeBySelfTitle";
            this.lblServeFeeBySelfTitle.Size = new System.Drawing.Size(104, 30);
            this.lblServeFeeBySelfTitle.TabIndex = 85;
            this.lblServeFeeBySelfTitle.Text = "自费(元)：";
            this.lblServeFeeBySelfTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServeFeeByGovTitle
            // 
            this.lblServeFeeByGovTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeFeeByGovTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServeFeeByGovTitle.Location = new System.Drawing.Point(266, 125);
            this.lblServeFeeByGovTitle.Name = "lblServeFeeByGovTitle";
            this.lblServeFeeByGovTitle.Size = new System.Drawing.Size(104, 30);
            this.lblServeFeeByGovTitle.TabIndex = 84;
            this.lblServeFeeByGovTitle.Text = "政府承担(元)：";
            this.lblServeFeeByGovTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServeFee
            // 
            this.lblServeFee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeFee.Location = new System.Drawing.Point(115, 125);
            this.lblServeFee.Name = "lblServeFee";
            this.lblServeFee.Size = new System.Drawing.Size(144, 30);
            this.lblServeFee.TabIndex = 81;
            this.lblServeFee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServeFeeTitle
            // 
            this.lblServeFeeTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeFeeTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServeFeeTitle.Location = new System.Drawing.Point(4, 125);
            this.lblServeFeeTitle.Name = "lblServeFeeTitle";
            this.lblServeFeeTitle.Size = new System.Drawing.Size(104, 30);
            this.lblServeFeeTitle.TabIndex = 80;
            this.lblServeFeeTitle.Text = "服务费用(元)：";
            this.lblServeFeeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAddress
            // 
            this.tlpInfo.SetColumnSpan(this.lblAddress, 3);
            this.lblAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAddress.Location = new System.Drawing.Point(377, 218);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(419, 36);
            this.lblAddress.TabIndex = 65;
            this.lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAddressTitle
            // 
            this.lblAddressTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAddressTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAddressTitle.Location = new System.Drawing.Point(266, 218);
            this.lblAddressTitle.Name = "lblAddressTitle";
            this.lblAddressTitle.Size = new System.Drawing.Size(104, 36);
            this.lblAddressTitle.TabIndex = 64;
            this.lblAddressTitle.Text = "家庭地址：";
            this.lblAddressTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMobile.Location = new System.Drawing.Point(115, 218);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(144, 36);
            this.lblMobile.TabIndex = 63;
            this.lblMobile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMobileTitle
            // 
            this.lblMobileTitle.AutoSize = true;
            this.lblMobileTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMobileTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMobileTitle.Location = new System.Drawing.Point(4, 218);
            this.lblMobileTitle.Name = "lblMobileTitle";
            this.lblMobileTitle.Size = new System.Drawing.Size(104, 36);
            this.lblMobileTitle.TabIndex = 62;
            this.lblMobileTitle.Text = "老人手机：";
            this.lblMobileTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTel
            // 
            this.lblTel.AutoSize = true;
            this.lblTel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTel.Location = new System.Drawing.Point(639, 187);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(157, 30);
            this.lblTel.TabIndex = 61;
            this.lblTel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTelTitle
            // 
            this.lblTelTitle.AutoSize = true;
            this.lblTelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTelTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTelTitle.Location = new System.Drawing.Point(528, 187);
            this.lblTelTitle.Name = "lblTelTitle";
            this.lblTelTitle.Size = new System.Drawing.Size(104, 30);
            this.lblTelTitle.TabIndex = 60;
            this.lblTelTitle.Text = "老人座机：";
            this.lblTelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGender.Location = new System.Drawing.Point(377, 187);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(144, 30);
            this.lblGender.TabIndex = 59;
            this.lblGender.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGenderTitle
            // 
            this.lblGenderTitle.AutoSize = true;
            this.lblGenderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGenderTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGenderTitle.Location = new System.Drawing.Point(266, 187);
            this.lblGenderTitle.Name = "lblGenderTitle";
            this.lblGenderTitle.Size = new System.Drawing.Size(104, 30);
            this.lblGenderTitle.TabIndex = 58;
            this.lblGenderTitle.Text = "老人性别：";
            this.lblGenderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOldManName
            // 
            this.lblOldManName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOldManName.Location = new System.Drawing.Point(112, 187);
            this.lblOldManName.Margin = new System.Windows.Forms.Padding(0);
            this.lblOldManName.Name = "lblOldManName";
            this.lblOldManName.Size = new System.Drawing.Size(150, 30);
            this.lblOldManName.TabIndex = 57;
            this.lblOldManName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOldManNameTitle
            // 
            this.lblOldManNameTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOldManNameTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOldManNameTitle.Location = new System.Drawing.Point(1, 187);
            this.lblOldManNameTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblOldManNameTitle.Name = "lblOldManNameTitle";
            this.lblOldManNameTitle.Size = new System.Drawing.Size(110, 30);
            this.lblOldManNameTitle.TabIndex = 56;
            this.lblOldManNameTitle.Text = "老人姓名：";
            this.lblOldManNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSupervisedByName
            // 
            this.lblSupervisedByName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSupervisedByName.Location = new System.Drawing.Point(377, 156);
            this.lblSupervisedByName.Name = "lblSupervisedByName";
            this.lblSupervisedByName.Size = new System.Drawing.Size(144, 30);
            this.lblSupervisedByName.TabIndex = 55;
            this.lblSupervisedByName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWorkOrderArea
            // 
            this.tlpInfo.SetColumnSpan(this.lblWorkOrderArea, 3);
            this.lblWorkOrderArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWorkOrderArea.Location = new System.Drawing.Point(115, 32);
            this.lblWorkOrderArea.Name = "lblWorkOrderArea";
            this.lblWorkOrderArea.Size = new System.Drawing.Size(406, 30);
            this.lblWorkOrderArea.TabIndex = 54;
            this.lblWorkOrderArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWorkOrderAreaTitle
            // 
            this.lblWorkOrderAreaTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWorkOrderAreaTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWorkOrderAreaTitle.Location = new System.Drawing.Point(4, 32);
            this.lblWorkOrderAreaTitle.Name = "lblWorkOrderAreaTitle";
            this.lblWorkOrderAreaTitle.Size = new System.Drawing.Size(104, 30);
            this.lblWorkOrderAreaTitle.TabIndex = 52;
            this.lblWorkOrderAreaTitle.Text = "工单归属：";
            this.lblWorkOrderAreaTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSupervisedByNameTitle
            // 
            this.lblSupervisedByNameTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSupervisedByNameTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSupervisedByNameTitle.Location = new System.Drawing.Point(266, 156);
            this.lblSupervisedByNameTitle.Name = "lblSupervisedByNameTitle";
            this.lblSupervisedByNameTitle.Size = new System.Drawing.Size(104, 30);
            this.lblSupervisedByNameTitle.TabIndex = 45;
            this.lblSupervisedByNameTitle.Text = "督办人员：";
            this.lblSupervisedByNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSuperviseTime
            // 
            this.lblSuperviseTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSuperviseTime.Location = new System.Drawing.Point(115, 156);
            this.lblSuperviseTime.Name = "lblSuperviseTime";
            this.lblSuperviseTime.Size = new System.Drawing.Size(144, 30);
            this.lblSuperviseTime.TabIndex = 44;
            this.lblSuperviseTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSuperviseTimeTitle
            // 
            this.lblSuperviseTimeTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSuperviseTimeTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSuperviseTimeTitle.Location = new System.Drawing.Point(4, 156);
            this.lblSuperviseTimeTitle.Name = "lblSuperviseTimeTitle";
            this.lblSuperviseTimeTitle.Size = new System.Drawing.Size(104, 30);
            this.lblSuperviseTimeTitle.TabIndex = 43;
            this.lblSuperviseTimeTitle.Text = "督办时间：";
            this.lblSuperviseTimeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRemarkRequired
            // 
            this.tlpInfo.SetColumnSpan(this.lblRemarkRequired, 3);
            this.lblRemarkRequired.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRemarkRequired.Location = new System.Drawing.Point(377, 97);
            this.lblRemarkRequired.Margin = new System.Windows.Forms.Padding(3);
            this.lblRemarkRequired.Name = "lblRemarkRequired";
            this.lblRemarkRequired.Size = new System.Drawing.Size(419, 24);
            this.lblRemarkRequired.TabIndex = 32;
            // 
            // lblRemarkRequiredTitle
            // 
            this.lblRemarkRequiredTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRemarkRequiredTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRemarkRequiredTitle.Location = new System.Drawing.Point(263, 94);
            this.lblRemarkRequiredTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblRemarkRequiredTitle.Name = "lblRemarkRequiredTitle";
            this.lblRemarkRequiredTitle.Size = new System.Drawing.Size(110, 30);
            this.lblRemarkRequiredTitle.TabIndex = 31;
            this.lblRemarkRequiredTitle.Text = "要求备注：";
            this.lblRemarkRequiredTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServiceTimeRequired
            // 
            this.lblServiceTimeRequired.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceTimeRequired.Location = new System.Drawing.Point(115, 94);
            this.lblServiceTimeRequired.Name = "lblServiceTimeRequired";
            this.lblServiceTimeRequired.Size = new System.Drawing.Size(144, 30);
            this.lblServiceTimeRequired.TabIndex = 30;
            this.lblServiceTimeRequired.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServiceTimeRequiredTitle
            // 
            this.lblServiceTimeRequiredTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServiceTimeRequiredTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServiceTimeRequiredTitle.Location = new System.Drawing.Point(4, 94);
            this.lblServiceTimeRequiredTitle.Name = "lblServiceTimeRequiredTitle";
            this.lblServiceTimeRequiredTitle.Size = new System.Drawing.Size(104, 30);
            this.lblServiceTimeRequiredTitle.TabIndex = 29;
            this.lblServiceTimeRequiredTitle.Text = "要求上门时间：";
            this.lblServiceTimeRequiredTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServeTypeName
            // 
            this.lblServeTypeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeTypeName.Location = new System.Drawing.Point(639, 32);
            this.lblServeTypeName.Name = "lblServeTypeName";
            this.lblServeTypeName.Size = new System.Drawing.Size(157, 30);
            this.lblServeTypeName.TabIndex = 28;
            this.lblServeTypeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServeTypeNameTitle
            // 
            this.lblServeTypeNameTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeTypeNameTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServeTypeNameTitle.Location = new System.Drawing.Point(528, 32);
            this.lblServeTypeNameTitle.Name = "lblServeTypeNameTitle";
            this.lblServeTypeNameTitle.Size = new System.Drawing.Size(104, 30);
            this.lblServeTypeNameTitle.TabIndex = 27;
            this.lblServeTypeNameTitle.Text = "服务类别：";
            this.lblServeTypeNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServeModeName
            // 
            this.lblServeModeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeModeName.Location = new System.Drawing.Point(639, 63);
            this.lblServeModeName.Name = "lblServeModeName";
            this.lblServeModeName.Size = new System.Drawing.Size(157, 30);
            this.lblServeModeName.TabIndex = 24;
            this.lblServeModeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServeModeNameTitle
            // 
            this.lblServeModeNameTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeModeNameTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServeModeNameTitle.Location = new System.Drawing.Point(528, 63);
            this.lblServeModeNameTitle.Name = "lblServeModeNameTitle";
            this.lblServeModeNameTitle.Size = new System.Drawing.Size(104, 30);
            this.lblServeModeNameTitle.TabIndex = 23;
            this.lblServeModeNameTitle.Text = "服务方式：";
            this.lblServeModeNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServeItemBName
            // 
            this.lblServeItemBName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeItemBName.Location = new System.Drawing.Point(377, 63);
            this.lblServeItemBName.Name = "lblServeItemBName";
            this.lblServeItemBName.Size = new System.Drawing.Size(144, 30);
            this.lblServeItemBName.TabIndex = 22;
            this.lblServeItemBName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServeItemBNameTitle
            // 
            this.lblServeItemBNameTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeItemBNameTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServeItemBNameTitle.Location = new System.Drawing.Point(266, 63);
            this.lblServeItemBNameTitle.Name = "lblServeItemBNameTitle";
            this.lblServeItemBNameTitle.Size = new System.Drawing.Size(104, 30);
            this.lblServeItemBNameTitle.TabIndex = 21;
            this.lblServeItemBNameTitle.Text = "服务小类：";
            this.lblServeItemBNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServeItemAName
            // 
            this.lblServeItemAName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeItemAName.Location = new System.Drawing.Point(115, 63);
            this.lblServeItemAName.Name = "lblServeItemAName";
            this.lblServeItemAName.Size = new System.Drawing.Size(144, 30);
            this.lblServeItemAName.TabIndex = 20;
            this.lblServeItemAName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServeItemANameTitle
            // 
            this.lblServeItemANameTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblServeItemANameTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServeItemANameTitle.Location = new System.Drawing.Point(4, 63);
            this.lblServeItemANameTitle.Name = "lblServeItemANameTitle";
            this.lblServeItemANameTitle.Size = new System.Drawing.Size(104, 30);
            this.lblServeItemANameTitle.TabIndex = 19;
            this.lblServeItemANameTitle.Text = "服务大类：";
            this.lblServeItemANameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWorkOrderContentTitle
            // 
            this.lblWorkOrderContentTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWorkOrderContentTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWorkOrderContentTitle.Location = new System.Drawing.Point(1, 1);
            this.lblWorkOrderContentTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblWorkOrderContentTitle.Name = "lblWorkOrderContentTitle";
            this.lblWorkOrderContentTitle.Size = new System.Drawing.Size(110, 30);
            this.lblWorkOrderContentTitle.TabIndex = 0;
            this.lblWorkOrderContentTitle.Text = "工单内容：";
            this.lblWorkOrderContentTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWorkOrderContent
            // 
            this.tlpInfo.SetColumnSpan(this.lblWorkOrderContent, 5);
            this.lblWorkOrderContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWorkOrderContent.Location = new System.Drawing.Point(115, 4);
            this.lblWorkOrderContent.Margin = new System.Windows.Forms.Padding(3);
            this.lblWorkOrderContent.Name = "lblWorkOrderContent";
            this.lblWorkOrderContent.Size = new System.Drawing.Size(681, 24);
            this.lblWorkOrderContent.TabIndex = 53;
            // 
            // lblCurrentStation
            // 
            this.lblCurrentStation.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentStation.Location = new System.Drawing.Point(10, 8);
            this.lblCurrentStation.Name = "lblCurrentStation";
            this.lblCurrentStation.Size = new System.Drawing.Size(800, 23);
            this.lblCurrentStation.TabIndex = 20;
            this.lblCurrentStation.Text = "商家名称";
            this.lblCurrentStation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOperatedByName
            // 
            this.lblOperatedByName.Location = new System.Drawing.Point(394, 41);
            this.lblOperatedByName.Name = "lblOperatedByName";
            this.lblOperatedByName.Size = new System.Drawing.Size(120, 23);
            this.lblOperatedByName.TabIndex = 28;
            this.lblOperatedByName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOperatedByNameTitle
            // 
            this.lblOperatedByNameTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOperatedByNameTitle.Location = new System.Drawing.Point(340, 41);
            this.lblOperatedByNameTitle.Name = "lblOperatedByNameTitle";
            this.lblOperatedByNameTitle.Size = new System.Drawing.Size(47, 23);
            this.lblOperatedByNameTitle.TabIndex = 27;
            this.lblOperatedByNameTitle.Text = "座席：";
            this.lblOperatedByNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmWorkOrderToResponseDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(818, 367);
            this.Controls.Add(this.lblOperatedByName);
            this.Controls.Add(this.lblOperatedByNameTitle);
            this.Controls.Add(this.xLoadingPanel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblDoStatus);
            this.Controls.Add(this.lblWorkOrderNoTitle);
            this.Controls.Add(this.lblWorkOrderNo);
            this.Controls.Add(this.lblDoStatusTitle);
            this.Controls.Add(this.pnlBaseInfo);
            this.Controls.Add(this.lblCurrentStation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWorkOrderToResponseDetail";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "响应工单对话框";
            this.Load += new System.EventHandler(this.frmWorkOrderToResponseDetail_Load);
            this.pnlBaseInfo.ResumeLayout(false);
            this.tlpInfo.ResumeLayout(false);
            this.tlpInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private win.core.controls.LoadingPanel xLoadingPanel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblDoStatus;
        private System.Windows.Forms.Label lblWorkOrderNoTitle;
        private System.Windows.Forms.Label lblWorkOrderNo;
        private System.Windows.Forms.Label lblDoStatusTitle;
        private System.Windows.Forms.Panel pnlBaseInfo;
        private System.Windows.Forms.TableLayoutPanel tlpInfo;
        private System.Windows.Forms.Label lblServeFeeBySelf;
        private System.Windows.Forms.Label lblServeFeeByGov;
        private System.Windows.Forms.Label lblServeFeeBySelfTitle;
        private System.Windows.Forms.Label lblServeFeeByGovTitle;
        private System.Windows.Forms.Label lblServeFee;
        private System.Windows.Forms.Label lblServeFeeTitle;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblAddressTitle;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.Label lblMobileTitle;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.Label lblTelTitle;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblGenderTitle;
        private System.Windows.Forms.Label lblOldManName;
        private System.Windows.Forms.Label lblOldManNameTitle;
        private System.Windows.Forms.Label lblSupervisedByName;
        private System.Windows.Forms.Label lblWorkOrderArea;
        private System.Windows.Forms.Label lblWorkOrderAreaTitle;
        private System.Windows.Forms.Label lblSupervisedByNameTitle;
        private System.Windows.Forms.Label lblSuperviseTime;
        private System.Windows.Forms.Label lblSuperviseTimeTitle;
        private System.Windows.Forms.Label lblRemarkRequired;
        private System.Windows.Forms.Label lblRemarkRequiredTitle;
        private System.Windows.Forms.Label lblServiceTimeRequired;
        private System.Windows.Forms.Label lblServiceTimeRequiredTitle;
        private System.Windows.Forms.Label lblServeTypeName;
        private System.Windows.Forms.Label lblServeTypeNameTitle;
        private System.Windows.Forms.Label lblServeModeName;
        private System.Windows.Forms.Label lblServeModeNameTitle;
        private System.Windows.Forms.Label lblServeItemBName;
        private System.Windows.Forms.Label lblServeItemBNameTitle;
        private System.Windows.Forms.Label lblServeItemAName;
        private System.Windows.Forms.Label lblServeItemANameTitle;
        private System.Windows.Forms.Label lblWorkOrderContentTitle;
        private System.Windows.Forms.Label lblWorkOrderContent;
        private System.Windows.Forms.Label lblCurrentStation;
        private System.Windows.Forms.Label lblOperatedByName;
        private System.Windows.Forms.Label lblOperatedByNameTitle;
    }
}