using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using SmartLife.Share.Models.PO;
using Newtonsoft.Json;
using win.core.utils;

namespace SmartLife.Client
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class frmCallService : Form
    {
        private bool _CurrentCallServiceFinished = false; 
        public frmCallService()
        {
            InitializeComponent();
        }

        public void LoadUrl(string url)
        {
           
            webBrowser.Url = new Uri(url);
        }
        public CallService PageData { get; set; } 

        public string GetUrl()
        {
            if (webBrowser.Url != null)
            {
                return webBrowser.Url.ToString();
            }
            return null;
        }

        public frmMain TheParentWin { get; set; }

        private void frmCallService_Load(object sender, EventArgs e)
        {
            webBrowser.ObjectForScripting = this;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser.Document.InvokeScript("setExecModeToCS");
            webBrowser.Document.InvokeScript("setEnvironmentVar", new object[] { EnvironmentVar.ToJSON() });
            string strJsonStr = JsonConvert.SerializeObject(
                new
                {
                    Emergency = frmMain.ProcessEmergencyUrl,
                    FamilyCalls = frmMain.ProcessFamilyCallsUrl,
                    //Infotainment = frmMain.ProcessInfotainmentUrl,
                    Life = frmMain.ProcessLifeUrl
                }
            );
            webBrowser.Document.InvokeScript("setServiceTypeUrl", new object[] { strJsonStr });
            webBrowser.Document.InvokeScript("setPageData", new object[] { JsonConvert.SerializeObject(PageData) });
        }

        public event dTransferPhone TransferPhone;
        public event dRemovePhone RemovePhone;//结束通话
        public event dCallServiceWinClose CallServiceWinClose; 
        public event dInvokeHoldCall InvokeHoldCall;
        public event dInvokeRetreiveCall InvokeRetreiveCall;
        public event dInvokeFastTransfer InvokeFastTransfer;
        public event dCloseWinCallServiceToProcessing CloseWinCallServiceToProcessing;

        public void OnHoldCall()
        {
            webBrowser.Document.InvokeScript("changeHoldRetreiveCall", new object[] { 0 });
        }
        public void OnRetreiveCall()
        {
            webBrowser.Document.InvokeScript("changeHoldRetreiveCall", new object[] { 1 });
        }     

        #region BS交互
        public bool InvokeTransferPhone(int phoneType, string phoneNo,string message)
        {
            bool result = false;
            if (TransferPhone != null)
            {
                TransferPhoneEventArgs tpe = new TransferPhoneEventArgs { CallServiceId = PageData.CallServiceId, PhoneType = phoneType, PhoneNo = phoneNo, Message = message };
                TransferPhone(this, tpe);
                if (!tpe.Cancel)
                {
                    //没有取消，则记录日志
                    result = true;
                }
            }
            return result;
        }

        public bool InvokeRemovePhone(int phoneType, string phoneNo, string message)
        {
            bool result = false;
            if (RemovePhone != null)
            {
                RemovePhoneEventArgs rpe = new RemovePhoneEventArgs { CallServiceId = PageData.CallServiceId, PhoneType = phoneType, PhoneNo = phoneNo, Message = message };
                RemovePhone(this, rpe);
                if (!rpe.Cancel)
                {
                    //没有取消，则记录日志
                    result = true;
                }
            }
            return result;
        }
         
        public void CloseWin(bool isFinished)
        {
            if (isFinished && CloseWinCallServiceToProcessing != null)
            {
                CloseWinCallServiceToProcessing();
            }
            _CurrentCallServiceFinished = isFinished;
            this.Close();
            
        }
         
        public void SetCallHold()
        {
            if (InvokeHoldCall != null)
            {
                InvokeHoldCall(this, new EventArgs());
            }
        }
        public void SetCallRetreive()
        {
            if (InvokeRetreiveCall != null)
            {
                InvokeRetreiveCall(this, new EventArgs());
            }

        }

        public void SetFastTransfer(string queueNo)
        {
            FastTransferEventArgs eventArgs = new FastTransferEventArgs { QueueNo = queueNo };
            if (InvokeFastTransfer != null)
            {
                InvokeFastTransfer(this, eventArgs);
            }
        }

        public void OpenNewWinAsCallService(string urlFragment, string callServiceJsonStr)
        {
            CallService callService = JsonConvert.DeserializeObject<CallService>(callServiceJsonStr);
            this.PageData = callService;
            this.LoadUrl(TheParentWin.cmsHostName + TheParentWin.virPathName + urlFragment);
        }

        //客户端回调模式设置
        public string SwitchClientRunMode()
        {
            string cmsRunMode = INIAdapter.ReadValue(Common.INI_SECTION_SMARTCARE, Common.INI_KEY_RUNMODE, Common.INI_FILE_PATH);
            if (!string.IsNullOrEmpty(cmsRunMode))
            {
                return cmsRunMode;
            }
            return "";
        }
        #endregion

        private void frmCallService_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_CurrentCallServiceFinished)
            {
                if (MessageBoxAdapter.ShowConfirm("本次服务还未处理完成,退出时将自动保存,确定要退出吗", Properties.Settings.Default.MessageBoxTitle) == DialogResult.OK)
                {
                    //执行BS上的自动保存动作
                    string strCallServiceId = PageData.CallServiceId.ToString();
                    webBrowser.Document.InvokeScript("saveCallService", new object[] { strCallServiceId, "{}", "坐席工号<" + EnvironmentVar.UserCode + ">保存并退出服务" });
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void frmCallService_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CallServiceWinClose != null)
            {
                CallServiceWinClose(this, new EventArgs());
            }
        }

    }
}
