using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection; 
using win.core.utils;
using System.Runtime.InteropServices;
using CallCenterAPI.TC_IPCC;

namespace SmartLife.Client
{
    public partial class frmTestTC_IPCC : Form
    {
        public frmTestTC_IPCC()
        {
            InitializeComponent();
        }


        #region 按钮事件
        private void btnManager_Init_Click(object sender, EventArgs e)
        {
            bool ret = MgrAPIWrapper.Manager_Init();
            if (ret)
            {
                MessageBoxAdapter.ShowDebug("init ok");
            }
            else
            {
                MessageBoxAdapter.ShowDebug("init error");
            }
        }

        private void btnManager_Connect_Click(object sender, EventArgs e)
        {
            string ip = "192.168.1.18";
            string login = "714101";
            string pwd = "";
            string ext = "714001";
            string queueStr = "71410,71411,71412,71413";
            MgrResult ret = MgrAPIWrapper.Manager_Connect(ip, login, pwd, ext, queueStr);
            if (ret == MgrResult.ok)
            {
                MessageBoxAdapter.ShowDebug("connect ok");
            }
            else
            {
                MessageBoxAdapter.ShowDebug("connect " + ret.ToString());
            }

            string[] queues = queueStr.Split(",".ToCharArray());
            foreach (var queue in queues)
            {
                MgrResult ret00 = MgrAPIWrapper.Manager_QueueAdd(ext, queue, "1003", "1003", 0);
                if (ret00 == MgrResult.ok)
                {
                    MgrResult ret01 = MgrAPIWrapper.Manager_QueuePause(ext, queue, "1003", "1003", 0);

                    if (ret01 == MgrResult.ok)
                    {

                    }
                }

            }
        }

        private void btnManager_Disconnect_Click(object sender, EventArgs e)
        {
            bool ret = MgrAPIWrapper.Manager_Disconnect();
            if (ret)
            {
                MessageBoxAdapter.ShowDebug("disconnect ok");
            }
            else
            {
                MessageBoxAdapter.ShowDebug("disconnect error");
            }
        }

        private void btnSetEventCallback_Click(object sender, EventArgs e)
        {
            //MgrAPIWrapper.SetEventCallback(Callback);//自动释放

            myCallBack = new EventCallbackDelegate(Callback); //修改后
            EnumWindows(myCallBack, 0);
            MgrAPIWrapper.SetEventCallback(myCallBack);

            MessageBoxAdapter.ShowDebug("SetEventCallback ok");
        }
        private int MonitorId = -1;
        private void btnManager_OpenMonitor_Click(object sender, EventArgs e)
        {
            string dn = "DAHDI/1";
            string ext = "714001";
            int ret = MgrAPIWrapper.Manager_OpenMonitor(dn,ext);
            if (ret == 1)
            {
                MessageBoxAdapter.ShowDebug("OpenMonitor ok");
            }
            else
            {
                MessageBoxAdapter.ShowDebug("OpenMonitor " + ret.ToString());
            }
        }

        private void btnManager_CloseMonitor_Click(object sender, EventArgs e)
        {

            bool ret = MgrAPIWrapper.Manager_CloseMonitor(MonitorId);
            if (ret)
            {
                MessageBoxAdapter.ShowDebug("CloseMonitor ok");
            }
            else
            {
                MessageBoxAdapter.ShowDebug("CloseMonitor error");
            }
        }

        private void btnManager_MakeCall_Click(object sender, EventArgs e)
        {
            
            string from = "5501";
            string to="18668001381";
            string context = "DAHDI/1";
            string calledIdName = "18668001381";
            MgrResult ret = MgrAPIWrapper.Manager_MakeCall(from, to, context, calledIdName);
            if (ret == MgrResult.ok)
            {
                MessageBoxAdapter.ShowDebug("MakeCall ok");
            }
            else
            {
                MessageBoxAdapter.ShowDebug("MakeCall " + ret.ToString());
            }
        }

        private void btnManager_MakeOutCall_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_ChanSpy_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_Pickup_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_ClearCall_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_ClearCurCall_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_FastTransfer_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_CurFastTransfer_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_HoldCall_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_CurHoldCall_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_RetreiveCall_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_CurRetreiveCall_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_QueueAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_QueueRemove_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_QueuePause_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_CreatConf_Click(object sender, EventArgs e)
        {
            //创建会议
            int callId = 0;
            string inviter = "";//邀请亲人的号码
            string caller = "28937860";//老人号码
            string callee = "714001";//坐席号码
            string confnum = "8714001";//坐席号码前面+8
            string dial_inviter = "";
            //string dial_inviter = "DAHDI/1";//内线
            MgrResult ret = MgrAPIWrapper.Manager_CreatConf(callId, inviter, caller, callee, confnum, dial_inviter);
            if (ret == MgrResult.ok)
            {
                MessageBoxAdapter.ShowDebug("connect ok");
            }
            else
            {
                MessageBoxAdapter.ShowDebug("connect " + ret.ToString());
            }
        }

        private void btnManager_ConfKickAll_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_ConfInvite_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_ConfList_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_ConfKickUser_Click(object sender, EventArgs e)
        {

        }

        private void btnManager_SendSMS_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region CC监控回调事件
        public void Callback(int eventType, int watchId, int callId, string caller, string callee)
        {
            //MessageBoxAdapter.ShowDebug(Enum.GetName(typeof(MgrEvent), eventType));
            if (InvokeRequired)
            {
                UpdateUIDelegate ui = new UpdateUIDelegate(delegate(object o)
                {
                    lbMonitor.Items.Add("callback - " + eventType.ToString() + "   :" + Enum.GetName(typeof(MgrEvent), eventType) + string.Format("|{0}|{1}|{2}|{3}", watchId.ToString(), callId.ToString(), caller, callee));
                });
                BeginInvoke(ui, new object[] { null });
            }
            else
            {
                lbMonitor.Items.Add("callback - " + eventType.ToString() + "   :" + Enum.GetName(typeof(MgrEvent), eventType) + string.Format("|{0}|{1}|{2}|{3}", watchId.ToString(), callId.ToString(), caller, callee));
            }
        }
        #endregion

        private void frmTestTC_IPCC_Load(object sender, EventArgs e)
        {
            #region 复制厂商SDK
            bool isSDKOK = false;
            string msg = "";
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string sourceOfSDK = Path.Combine(path, @"TC-IPCC"); ;
            try
            {

                isSDKOK = FileAdapter.CopyDir(sourceOfSDK, path, true);
                if (!isSDKOK)
                {
                    msg = "无法加载厂商SDK";
                }
            }
            catch (Exception ex)
            {
                isSDKOK = false;
                msg = ex.Message;
            }
            finally
            { 
                if (!isSDKOK)
                {
                    gbInit.Enabled = gbMonitor.Enabled = gbCore.Enabled = gbConf.Enabled = gbSMS.Enabled = isSDKOK;
                    MessageBoxAdapter.ShowDebug(msg);
                }
            }

            #endregion

           
        }



        [DllImport("user32")]
        public static extern int EnumWindows(EventCallbackDelegate x, int y);

        private static EventCallbackDelegate myCallBack; //声明回调函数

    }
}
