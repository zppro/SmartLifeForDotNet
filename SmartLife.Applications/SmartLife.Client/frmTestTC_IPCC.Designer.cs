namespace SmartLife.Client
{
    partial class frmTestTC_IPCC
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
            this.gbInit = new System.Windows.Forms.GroupBox();
            this.btnManager_Disconnect = new System.Windows.Forms.Button();
            this.btnManager_Connect = new System.Windows.Forms.Button();
            this.btnManager_Init = new System.Windows.Forms.Button();
            this.gbMonitor = new System.Windows.Forms.GroupBox();
            this.btnManager_CloseMonitor = new System.Windows.Forms.Button();
            this.btnManager_OpenMonitor = new System.Windows.Forms.Button();
            this.gbCore = new System.Windows.Forms.GroupBox();
            this.btnManager_CurRetreiveCall = new System.Windows.Forms.Button();
            this.btnManager_HoldCall = new System.Windows.Forms.Button();
            this.btnManager_RetreiveCall = new System.Windows.Forms.Button();
            this.btnManager_ClearCurCall = new System.Windows.Forms.Button();
            this.btnManager_CurFastTransfer = new System.Windows.Forms.Button();
            this.btnManager_CurHoldCall = new System.Windows.Forms.Button();
            this.btnManager_ClearCall = new System.Windows.Forms.Button();
            this.btnManager_FastTransfer = new System.Windows.Forms.Button();
            this.btnManager_ChanSpy = new System.Windows.Forms.Button();
            this.btnManager_Pickup = new System.Windows.Forms.Button();
            this.btnManager_MakeOutCall = new System.Windows.Forms.Button();
            this.btnManager_MakeCall = new System.Windows.Forms.Button();
            this.btnManager_QueueAdd = new System.Windows.Forms.Button();
            this.btnManager_QueueRemove = new System.Windows.Forms.Button();
            this.btnManager_QueuePause = new System.Windows.Forms.Button();
            this.gbConf = new System.Windows.Forms.GroupBox();
            this.btnManager_ConfList = new System.Windows.Forms.Button();
            this.btnManager_ConfInvite = new System.Windows.Forms.Button();
            this.btnManager_CreatConf = new System.Windows.Forms.Button();
            this.btnManager_ConfKickUser = new System.Windows.Forms.Button();
            this.btnManager_ConfKickAll = new System.Windows.Forms.Button();
            this.gbSMS = new System.Windows.Forms.GroupBox();
            this.btnManager_SendSMS = new System.Windows.Forms.Button();
            this.btnSetEventCallback = new System.Windows.Forms.Button();
            this.lbMonitor = new System.Windows.Forms.ListBox();
            this.gbInit.SuspendLayout();
            this.gbMonitor.SuspendLayout();
            this.gbCore.SuspendLayout();
            this.gbConf.SuspendLayout();
            this.gbSMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbInit
            // 
            this.gbInit.Controls.Add(this.btnManager_Disconnect);
            this.gbInit.Controls.Add(this.btnManager_Connect);
            this.gbInit.Controls.Add(this.btnManager_Init);
            this.gbInit.Location = new System.Drawing.Point(12, 12);
            this.gbInit.Name = "gbInit";
            this.gbInit.Size = new System.Drawing.Size(632, 86);
            this.gbInit.TabIndex = 0;
            this.gbInit.TabStop = false;
            this.gbInit.Text = "初始化";
            // 
            // btnManager_Disconnect
            // 
            this.btnManager_Disconnect.Location = new System.Drawing.Point(411, 34);
            this.btnManager_Disconnect.Name = "btnManager_Disconnect";
            this.btnManager_Disconnect.Size = new System.Drawing.Size(158, 23);
            this.btnManager_Disconnect.TabIndex = 2;
            this.btnManager_Disconnect.Text = "Manager_Disconnect";
            this.btnManager_Disconnect.UseVisualStyleBackColor = true;
            this.btnManager_Disconnect.Click += new System.EventHandler(this.btnManager_Disconnect_Click);
            // 
            // btnManager_Connect
            // 
            this.btnManager_Connect.Location = new System.Drawing.Point(215, 34);
            this.btnManager_Connect.Name = "btnManager_Connect";
            this.btnManager_Connect.Size = new System.Drawing.Size(158, 23);
            this.btnManager_Connect.TabIndex = 1;
            this.btnManager_Connect.Text = "Manager_Connect";
            this.btnManager_Connect.UseVisualStyleBackColor = true;
            this.btnManager_Connect.Click += new System.EventHandler(this.btnManager_Connect_Click);
            // 
            // btnManager_Init
            // 
            this.btnManager_Init.Location = new System.Drawing.Point(22, 34);
            this.btnManager_Init.Name = "btnManager_Init";
            this.btnManager_Init.Size = new System.Drawing.Size(158, 23);
            this.btnManager_Init.TabIndex = 0;
            this.btnManager_Init.Text = "Manager_Init";
            this.btnManager_Init.UseVisualStyleBackColor = true;
            this.btnManager_Init.Click += new System.EventHandler(this.btnManager_Init_Click);
            // 
            // gbMonitor
            // 
            this.gbMonitor.Controls.Add(this.lbMonitor);
            this.gbMonitor.Controls.Add(this.btnManager_CloseMonitor);
            this.gbMonitor.Controls.Add(this.btnManager_OpenMonitor);
            this.gbMonitor.Controls.Add(this.btnSetEventCallback);
            this.gbMonitor.Location = new System.Drawing.Point(12, 114);
            this.gbMonitor.Name = "gbMonitor";
            this.gbMonitor.Size = new System.Drawing.Size(632, 213);
            this.gbMonitor.TabIndex = 1;
            this.gbMonitor.TabStop = false;
            this.gbMonitor.Text = "监视器";
            // 
            // btnManager_CloseMonitor
            // 
            this.btnManager_CloseMonitor.Location = new System.Drawing.Point(411, 24);
            this.btnManager_CloseMonitor.Name = "btnManager_CloseMonitor";
            this.btnManager_CloseMonitor.Size = new System.Drawing.Size(158, 23);
            this.btnManager_CloseMonitor.TabIndex = 2;
            this.btnManager_CloseMonitor.Text = "Manager_CloseMonitor";
            this.btnManager_CloseMonitor.UseVisualStyleBackColor = true;
            this.btnManager_CloseMonitor.Click += new System.EventHandler(this.btnManager_CloseMonitor_Click);
            // 
            // btnManager_OpenMonitor
            // 
            this.btnManager_OpenMonitor.Location = new System.Drawing.Point(215, 24);
            this.btnManager_OpenMonitor.Name = "btnManager_OpenMonitor";
            this.btnManager_OpenMonitor.Size = new System.Drawing.Size(158, 23);
            this.btnManager_OpenMonitor.TabIndex = 1;
            this.btnManager_OpenMonitor.Text = "Manager_OpenMonitor";
            this.btnManager_OpenMonitor.UseVisualStyleBackColor = true;
            this.btnManager_OpenMonitor.Click += new System.EventHandler(this.btnManager_OpenMonitor_Click);
            // 
            // gbCore
            // 
            this.gbCore.Controls.Add(this.btnManager_CurRetreiveCall);
            this.gbCore.Controls.Add(this.btnManager_HoldCall);
            this.gbCore.Controls.Add(this.btnManager_RetreiveCall);
            this.gbCore.Controls.Add(this.btnManager_ClearCurCall);
            this.gbCore.Controls.Add(this.btnManager_CurFastTransfer);
            this.gbCore.Controls.Add(this.btnManager_CurHoldCall);
            this.gbCore.Controls.Add(this.btnManager_ClearCall);
            this.gbCore.Controls.Add(this.btnManager_FastTransfer);
            this.gbCore.Controls.Add(this.btnManager_ChanSpy);
            this.gbCore.Controls.Add(this.btnManager_Pickup);
            this.gbCore.Controls.Add(this.btnManager_MakeOutCall);
            this.gbCore.Controls.Add(this.btnManager_MakeCall);
            this.gbCore.Location = new System.Drawing.Point(12, 345);
            this.gbCore.Name = "gbCore";
            this.gbCore.Size = new System.Drawing.Size(632, 181);
            this.gbCore.TabIndex = 2;
            this.gbCore.TabStop = false;
            this.gbCore.Text = "电话";
            // 
            // btnManager_CurRetreiveCall
            // 
            this.btnManager_CurRetreiveCall.Location = new System.Drawing.Point(411, 138);
            this.btnManager_CurRetreiveCall.Name = "btnManager_CurRetreiveCall";
            this.btnManager_CurRetreiveCall.Size = new System.Drawing.Size(158, 23);
            this.btnManager_CurRetreiveCall.TabIndex = 4;
            this.btnManager_CurRetreiveCall.Text = "Manager_CurRetreiveCall";
            this.btnManager_CurRetreiveCall.UseVisualStyleBackColor = true;
            this.btnManager_CurRetreiveCall.Click += new System.EventHandler(this.btnManager_CurRetreiveCall_Click);
            // 
            // btnManager_HoldCall
            // 
            this.btnManager_HoldCall.Location = new System.Drawing.Point(411, 105);
            this.btnManager_HoldCall.Name = "btnManager_HoldCall";
            this.btnManager_HoldCall.Size = new System.Drawing.Size(158, 23);
            this.btnManager_HoldCall.TabIndex = 4;
            this.btnManager_HoldCall.Text = "Manager_HoldCall";
            this.btnManager_HoldCall.UseVisualStyleBackColor = true;
            this.btnManager_HoldCall.Click += new System.EventHandler(this.btnManager_HoldCall_Click);
            // 
            // btnManager_RetreiveCall
            // 
            this.btnManager_RetreiveCall.Location = new System.Drawing.Point(215, 138);
            this.btnManager_RetreiveCall.Name = "btnManager_RetreiveCall";
            this.btnManager_RetreiveCall.Size = new System.Drawing.Size(158, 23);
            this.btnManager_RetreiveCall.TabIndex = 3;
            this.btnManager_RetreiveCall.Text = "Manager_RetreiveCall";
            this.btnManager_RetreiveCall.UseVisualStyleBackColor = true;
            this.btnManager_RetreiveCall.Click += new System.EventHandler(this.btnManager_RetreiveCall_Click);
            // 
            // btnManager_ClearCurCall
            // 
            this.btnManager_ClearCurCall.Location = new System.Drawing.Point(411, 67);
            this.btnManager_ClearCurCall.Name = "btnManager_ClearCurCall";
            this.btnManager_ClearCurCall.Size = new System.Drawing.Size(158, 23);
            this.btnManager_ClearCurCall.TabIndex = 4;
            this.btnManager_ClearCurCall.Text = "Manager_ClearCurCall";
            this.btnManager_ClearCurCall.UseVisualStyleBackColor = true;
            this.btnManager_ClearCurCall.Click += new System.EventHandler(this.btnManager_ClearCurCall_Click);
            // 
            // btnManager_CurFastTransfer
            // 
            this.btnManager_CurFastTransfer.Location = new System.Drawing.Point(215, 105);
            this.btnManager_CurFastTransfer.Name = "btnManager_CurFastTransfer";
            this.btnManager_CurFastTransfer.Size = new System.Drawing.Size(158, 23);
            this.btnManager_CurFastTransfer.TabIndex = 3;
            this.btnManager_CurFastTransfer.Text = "Manager_CurFastTransfer";
            this.btnManager_CurFastTransfer.UseVisualStyleBackColor = true;
            this.btnManager_CurFastTransfer.Click += new System.EventHandler(this.btnManager_CurFastTransfer_Click);
            // 
            // btnManager_CurHoldCall
            // 
            this.btnManager_CurHoldCall.Location = new System.Drawing.Point(22, 138);
            this.btnManager_CurHoldCall.Name = "btnManager_CurHoldCall";
            this.btnManager_CurHoldCall.Size = new System.Drawing.Size(158, 23);
            this.btnManager_CurHoldCall.TabIndex = 2;
            this.btnManager_CurHoldCall.Text = "Manager_CurHoldCall";
            this.btnManager_CurHoldCall.UseVisualStyleBackColor = true;
            this.btnManager_CurHoldCall.Click += new System.EventHandler(this.btnManager_CurHoldCall_Click);
            // 
            // btnManager_ClearCall
            // 
            this.btnManager_ClearCall.Location = new System.Drawing.Point(215, 67);
            this.btnManager_ClearCall.Name = "btnManager_ClearCall";
            this.btnManager_ClearCall.Size = new System.Drawing.Size(158, 23);
            this.btnManager_ClearCall.TabIndex = 3;
            this.btnManager_ClearCall.Text = "Manager_ClearCall";
            this.btnManager_ClearCall.UseVisualStyleBackColor = true;
            this.btnManager_ClearCall.Click += new System.EventHandler(this.btnManager_ClearCall_Click);
            // 
            // btnManager_FastTransfer
            // 
            this.btnManager_FastTransfer.Location = new System.Drawing.Point(22, 105);
            this.btnManager_FastTransfer.Name = "btnManager_FastTransfer";
            this.btnManager_FastTransfer.Size = new System.Drawing.Size(158, 23);
            this.btnManager_FastTransfer.TabIndex = 2;
            this.btnManager_FastTransfer.Text = "Manager_FastTransfer";
            this.btnManager_FastTransfer.UseVisualStyleBackColor = true;
            this.btnManager_FastTransfer.Click += new System.EventHandler(this.btnManager_FastTransfer_Click);
            // 
            // btnManager_ChanSpy
            // 
            this.btnManager_ChanSpy.Location = new System.Drawing.Point(411, 29);
            this.btnManager_ChanSpy.Name = "btnManager_ChanSpy";
            this.btnManager_ChanSpy.Size = new System.Drawing.Size(158, 23);
            this.btnManager_ChanSpy.TabIndex = 4;
            this.btnManager_ChanSpy.Text = "Manager_ChanSpy";
            this.btnManager_ChanSpy.UseVisualStyleBackColor = true;
            this.btnManager_ChanSpy.Click += new System.EventHandler(this.btnManager_ChanSpy_Click);
            // 
            // btnManager_Pickup
            // 
            this.btnManager_Pickup.Location = new System.Drawing.Point(22, 67);
            this.btnManager_Pickup.Name = "btnManager_Pickup";
            this.btnManager_Pickup.Size = new System.Drawing.Size(158, 23);
            this.btnManager_Pickup.TabIndex = 2;
            this.btnManager_Pickup.Text = "Manager_Pickup";
            this.btnManager_Pickup.UseVisualStyleBackColor = true;
            this.btnManager_Pickup.Click += new System.EventHandler(this.btnManager_Pickup_Click);
            // 
            // btnManager_MakeOutCall
            // 
            this.btnManager_MakeOutCall.Location = new System.Drawing.Point(215, 29);
            this.btnManager_MakeOutCall.Name = "btnManager_MakeOutCall";
            this.btnManager_MakeOutCall.Size = new System.Drawing.Size(158, 23);
            this.btnManager_MakeOutCall.TabIndex = 3;
            this.btnManager_MakeOutCall.Text = "Manager_MakeOutCall";
            this.btnManager_MakeOutCall.UseVisualStyleBackColor = true;
            this.btnManager_MakeOutCall.Click += new System.EventHandler(this.btnManager_MakeOutCall_Click);
            // 
            // btnManager_MakeCall
            // 
            this.btnManager_MakeCall.Location = new System.Drawing.Point(22, 29);
            this.btnManager_MakeCall.Name = "btnManager_MakeCall";
            this.btnManager_MakeCall.Size = new System.Drawing.Size(158, 23);
            this.btnManager_MakeCall.TabIndex = 2;
            this.btnManager_MakeCall.Text = "Manager_MakeCall";
            this.btnManager_MakeCall.UseVisualStyleBackColor = true;
            this.btnManager_MakeCall.Click += new System.EventHandler(this.btnManager_MakeCall_Click);
            // 
            // btnManager_QueueAdd
            // 
            this.btnManager_QueueAdd.Location = new System.Drawing.Point(22, 31);
            this.btnManager_QueueAdd.Name = "btnManager_QueueAdd";
            this.btnManager_QueueAdd.Size = new System.Drawing.Size(158, 23);
            this.btnManager_QueueAdd.TabIndex = 2;
            this.btnManager_QueueAdd.Text = "Manager_QueueAdd";
            this.btnManager_QueueAdd.UseVisualStyleBackColor = true;
            this.btnManager_QueueAdd.Click += new System.EventHandler(this.btnManager_QueueAdd_Click);
            // 
            // btnManager_QueueRemove
            // 
            this.btnManager_QueueRemove.Location = new System.Drawing.Point(215, 31);
            this.btnManager_QueueRemove.Name = "btnManager_QueueRemove";
            this.btnManager_QueueRemove.Size = new System.Drawing.Size(158, 23);
            this.btnManager_QueueRemove.TabIndex = 3;
            this.btnManager_QueueRemove.Text = "Manager_QueueRemove";
            this.btnManager_QueueRemove.UseVisualStyleBackColor = true;
            this.btnManager_QueueRemove.Click += new System.EventHandler(this.btnManager_QueueRemove_Click);
            // 
            // btnManager_QueuePause
            // 
            this.btnManager_QueuePause.Location = new System.Drawing.Point(411, 31);
            this.btnManager_QueuePause.Name = "btnManager_QueuePause";
            this.btnManager_QueuePause.Size = new System.Drawing.Size(158, 23);
            this.btnManager_QueuePause.TabIndex = 4;
            this.btnManager_QueuePause.Text = "Manager_QueuePause";
            this.btnManager_QueuePause.UseVisualStyleBackColor = true;
            this.btnManager_QueuePause.Click += new System.EventHandler(this.btnManager_QueuePause_Click);
            // 
            // gbConf
            // 
            this.gbConf.Controls.Add(this.btnManager_ConfList);
            this.gbConf.Controls.Add(this.btnManager_ConfInvite);
            this.gbConf.Controls.Add(this.btnManager_CreatConf);
            this.gbConf.Controls.Add(this.btnManager_ConfKickUser);
            this.gbConf.Controls.Add(this.btnManager_QueuePause);
            this.gbConf.Controls.Add(this.btnManager_ConfKickAll);
            this.gbConf.Controls.Add(this.btnManager_QueueAdd);
            this.gbConf.Controls.Add(this.btnManager_QueueRemove);
            this.gbConf.Location = new System.Drawing.Point(12, 532);
            this.gbConf.Name = "gbConf";
            this.gbConf.Size = new System.Drawing.Size(632, 132);
            this.gbConf.TabIndex = 5;
            this.gbConf.TabStop = false;
            this.gbConf.Text = "多方通话";
            // 
            // btnManager_ConfList
            // 
            this.btnManager_ConfList.Location = new System.Drawing.Point(22, 95);
            this.btnManager_ConfList.Name = "btnManager_ConfList";
            this.btnManager_ConfList.Size = new System.Drawing.Size(158, 23);
            this.btnManager_ConfList.TabIndex = 2;
            this.btnManager_ConfList.Text = "Manager_ConfList";
            this.btnManager_ConfList.UseVisualStyleBackColor = true;
            this.btnManager_ConfList.Click += new System.EventHandler(this.btnManager_ConfList_Click);
            // 
            // btnManager_ConfInvite
            // 
            this.btnManager_ConfInvite.Location = new System.Drawing.Point(411, 64);
            this.btnManager_ConfInvite.Name = "btnManager_ConfInvite";
            this.btnManager_ConfInvite.Size = new System.Drawing.Size(158, 23);
            this.btnManager_ConfInvite.TabIndex = 4;
            this.btnManager_ConfInvite.Text = "Manager_ConfInvite";
            this.btnManager_ConfInvite.UseVisualStyleBackColor = true;
            this.btnManager_ConfInvite.Click += new System.EventHandler(this.btnManager_ConfInvite_Click);
            // 
            // btnManager_CreatConf
            // 
            this.btnManager_CreatConf.Location = new System.Drawing.Point(22, 64);
            this.btnManager_CreatConf.Name = "btnManager_CreatConf";
            this.btnManager_CreatConf.Size = new System.Drawing.Size(158, 23);
            this.btnManager_CreatConf.TabIndex = 2;
            this.btnManager_CreatConf.Text = "Manager_CreatConf";
            this.btnManager_CreatConf.UseVisualStyleBackColor = true;
            this.btnManager_CreatConf.Click += new System.EventHandler(this.btnManager_CreatConf_Click);
            // 
            // btnManager_ConfKickUser
            // 
            this.btnManager_ConfKickUser.Location = new System.Drawing.Point(215, 95);
            this.btnManager_ConfKickUser.Name = "btnManager_ConfKickUser";
            this.btnManager_ConfKickUser.Size = new System.Drawing.Size(158, 23);
            this.btnManager_ConfKickUser.TabIndex = 3;
            this.btnManager_ConfKickUser.Text = "Manager_ConfKickUser";
            this.btnManager_ConfKickUser.UseVisualStyleBackColor = true;
            this.btnManager_ConfKickUser.Click += new System.EventHandler(this.btnManager_ConfKickUser_Click);
            // 
            // btnManager_ConfKickAll
            // 
            this.btnManager_ConfKickAll.Location = new System.Drawing.Point(215, 64);
            this.btnManager_ConfKickAll.Name = "btnManager_ConfKickAll";
            this.btnManager_ConfKickAll.Size = new System.Drawing.Size(158, 23);
            this.btnManager_ConfKickAll.TabIndex = 3;
            this.btnManager_ConfKickAll.Text = "Manager_ConfKickAll";
            this.btnManager_ConfKickAll.UseVisualStyleBackColor = true;
            this.btnManager_ConfKickAll.Click += new System.EventHandler(this.btnManager_ConfKickAll_Click);
            // 
            // gbSMS
            // 
            this.gbSMS.Controls.Add(this.btnManager_SendSMS);
            this.gbSMS.Location = new System.Drawing.Point(12, 670);
            this.gbSMS.Name = "gbSMS";
            this.gbSMS.Size = new System.Drawing.Size(632, 60);
            this.gbSMS.TabIndex = 6;
            this.gbSMS.TabStop = false;
            this.gbSMS.Text = "短信";
            // 
            // btnManager_SendSMS
            // 
            this.btnManager_SendSMS.Location = new System.Drawing.Point(22, 20);
            this.btnManager_SendSMS.Name = "btnManager_SendSMS";
            this.btnManager_SendSMS.Size = new System.Drawing.Size(158, 23);
            this.btnManager_SendSMS.TabIndex = 4;
            this.btnManager_SendSMS.Text = "Manager_SendSMS";
            this.btnManager_SendSMS.UseVisualStyleBackColor = true;
            this.btnManager_SendSMS.Click += new System.EventHandler(this.btnManager_SendSMS_Click);
            // 
            // btnSetEventCallback
            // 
            this.btnSetEventCallback.Location = new System.Drawing.Point(22, 24);
            this.btnSetEventCallback.Name = "btnSetEventCallback";
            this.btnSetEventCallback.Size = new System.Drawing.Size(158, 23);
            this.btnSetEventCallback.TabIndex = 0;
            this.btnSetEventCallback.Text = "SetEventCallback";
            this.btnSetEventCallback.UseVisualStyleBackColor = true;
            this.btnSetEventCallback.Click += new System.EventHandler(this.btnSetEventCallback_Click);
            // 
            // lbMonitor
            // 
            this.lbMonitor.FormattingEnabled = true;
            this.lbMonitor.ItemHeight = 12;
            this.lbMonitor.Location = new System.Drawing.Point(22, 67);
            this.lbMonitor.Name = "lbMonitor";
            this.lbMonitor.Size = new System.Drawing.Size(351, 136);
            this.lbMonitor.TabIndex = 5;
            // 
            // frmTestTC_IPCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 732);
            this.Controls.Add(this.gbSMS);
            this.Controls.Add(this.gbConf);
            this.Controls.Add(this.gbCore);
            this.Controls.Add(this.gbMonitor);
            this.Controls.Add(this.gbInit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTestTC_IPCC";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "测试TC-IPCC平台";
            this.Load += new System.EventHandler(this.frmTestTC_IPCC_Load);
            this.gbInit.ResumeLayout(false);
            this.gbMonitor.ResumeLayout(false);
            this.gbCore.ResumeLayout(false);
            this.gbConf.ResumeLayout(false);
            this.gbSMS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbInit;
        private System.Windows.Forms.Button btnManager_Connect;
        private System.Windows.Forms.Button btnManager_Init;
        private System.Windows.Forms.Button btnManager_Disconnect;
        private System.Windows.Forms.GroupBox gbMonitor;
        private System.Windows.Forms.Button btnManager_OpenMonitor;
        private System.Windows.Forms.Button btnManager_CloseMonitor;
        private System.Windows.Forms.GroupBox gbCore;
        private System.Windows.Forms.Button btnManager_MakeCall;
        private System.Windows.Forms.Button btnManager_MakeOutCall;
        private System.Windows.Forms.Button btnManager_ChanSpy;
        private System.Windows.Forms.Button btnManager_ClearCurCall;
        private System.Windows.Forms.Button btnManager_ClearCall;
        private System.Windows.Forms.Button btnManager_Pickup;
        private System.Windows.Forms.Button btnManager_HoldCall;
        private System.Windows.Forms.Button btnManager_CurFastTransfer;
        private System.Windows.Forms.Button btnManager_FastTransfer;
        private System.Windows.Forms.Button btnManager_CurRetreiveCall;
        private System.Windows.Forms.Button btnManager_RetreiveCall;
        private System.Windows.Forms.Button btnManager_CurHoldCall;
        private System.Windows.Forms.Button btnManager_QueuePause;
        private System.Windows.Forms.Button btnManager_QueueRemove;
        private System.Windows.Forms.Button btnManager_QueueAdd;
        private System.Windows.Forms.GroupBox gbConf;
        private System.Windows.Forms.Button btnManager_ConfInvite;
        private System.Windows.Forms.Button btnManager_CreatConf;
        private System.Windows.Forms.Button btnManager_ConfKickAll;
        private System.Windows.Forms.Button btnManager_ConfList;
        private System.Windows.Forms.Button btnManager_ConfKickUser;
        private System.Windows.Forms.GroupBox gbSMS;
        private System.Windows.Forms.Button btnManager_SendSMS;
        private System.Windows.Forms.Button btnSetEventCallback;
        private System.Windows.Forms.ListBox lbMonitor;
    }
}