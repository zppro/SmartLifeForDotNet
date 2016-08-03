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
    public partial class frmCallServiceInfo : Form
    {
        public frmCallServiceInfo()
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

        private void frmCallServiceInfo_Load(object sender, EventArgs e)
        {
            webBrowser.ObjectForScripting = this;
        }

        public event dTransferPhone TransferPhone;
        public event dRemovePhone RemovePhone;//结束通话
        public event dCallServiceInfoWinClose CallServiceInfoWinClose;
        public event dDialBackNoWin DialBackNoWinFromInfo;
        #region BS交互
        public bool InvokeTransferPhone(int phoneType, string phoneNo, string message)
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
        //移除会话
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

        //直接呼出不弹屏
        public void DialBackNoWin(string callServiceId, string oldManId ,string callNo, string oldManName)
        {
            DialBackNoWinEventArgs eventArgs = new DialBackNoWinEventArgs { CallServiceId = callServiceId, OldManId = oldManId ,CallNo = callNo, OldManName = oldManName };
            if (DialBackNoWinFromInfo != null)
            {
                DialBackNoWinFromInfo(this, eventArgs);
            }
        }

        public void CloseWin()
        { 
            this.Close();
        }
        public void OpenNewWinAsCallServiceInfo(string urlFragment, string callServiceJsonStr)
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

        private void frmCallServiceInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CallServiceInfoWinClose != null)
            {
                CallServiceInfoWinClose(this, new EventArgs());
            }
        }
    }
}
