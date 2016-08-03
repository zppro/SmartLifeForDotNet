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

namespace SmartLife.Client.Seat
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class frmInfo : Form
    {
        private string _pageUrl;
        public event dMakeCall MakeCall;
        public event dRemoveCall RemoveCall;
        public event dCTIWinClose CTIWinClose;
        public event dInvokeHoldCall InvokeHoldCall;
        public event dInvokeRetreiveCall InvokeRetreiveCall;
        public event dInvokeTransfer InvokeTransfer;

        public CallService PageData { get; set; }
#if V2X
        public frmMainV2X TheMotherWin { get; set; }
#else
        public frmMain TheMotherWin { get; set; }
#endif
        frmVoice winVoice;

        public frmInfo()
        {
            InitializeComponent();
        }

        private void frmInfo_Load(object sender, EventArgs e)
        {
            this.Width = Screen.PrimaryScreen.WorkingArea.Width - 200;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height - 77 - this.Location.Y;
            webBrowser.ObjectForScripting = this;
            this.Text = PageData.Content;

            lbConferenceMember.DisplayMember = "Text";
            lbConferenceMember.ValueMember = "Value";
            lbConferenceMember.Height = pnlConference.Height - lblConferenceName.Height - 20;
            lbConferenceMember.Top = lblConferenceName.Height + 20 / 2;
            lbConferenceMember.ItemHeight = 40;
        }

        public void LoadUrl(string url)
        {
            _pageUrl = url;
            webBrowser.Url = new Uri(_pageUrl);

        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser.ReadyState == WebBrowserReadyState.Complete && e.Url.ToString() == _pageUrl)
            {
                Console.WriteLine(e.Url.ToString());
                webBrowser.Document.InvokeScript("setExecModeForSeat");
                webBrowser.Document.InvokeScript("setEnvironmentVar", new object[] { EnvironmentVar.ToJSON() });
                string strJsonStr = JsonConvert.SerializeObject(
                    new
                    {
                        Emergency = Common.ADDRESS_DICT[Common.ADDRESS_KEY_PROCESS_EMERGENCY],
                        FamilyCalls = Common.ADDRESS_DICT[Common.ADDRESS_KEY_PROCESS_FAMILYCALLS],
                        Life = Common.ADDRESS_DICT[Common.ADDRESS_KEY_PROCESS_LIFE],
                    }
                );
                webBrowser.Document.InvokeScript("setServiceTypeUrl", new object[] { strJsonStr });
                webBrowser.Document.InvokeScript("setPageData", new object[] { JsonConvert.SerializeObject(PageData) });
            } 
        }

        #region 供BS调用
        public bool InvokeTransferPhone(int phoneType, string phoneNo, string message)
        {
            bool result = false;
            if (MakeCall != null)
            {

                if (EnvironmentVar.DialBackFlag == "1")
                {
                    MessageBoxAdapter.ShowInfo("请用手机或者其余的座机进行回拨");
                    return false;
                }

                //if (!TheMotherWin.isSignInToIPCC)
                //{
                //    MessageBoxAdapter.ShowInfo("请确保已登录到IPCC或座席处于空闲状态！");
                //    return false;
                //} 

                if (phoneType == 98)
                {
                    //回拨老人电话
                    frmCallsDialog calldialog = new frmCallsDialog();
                    object oRets = webBrowser.Document.InvokeScript("getServiceObjectPhoneNos", new object[] { PageData.CallServiceId.ToString() });
                    if (oRets != null && oRets.ToString() != "[]")
                    {
                        List<string> phoneNos = JsonConvert.DeserializeObject<List<string>>(oRets.ToString());
                        calldialog.PhoneNos = phoneNos;
                        calldialog.ChooseCallNo = phoneNo;
                    }
                    if (calldialog.PhoneNos != null && calldialog.PhoneNos.Count > 1)
                    {
                        calldialog.ShowDialog();
                        if (calldialog.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            phoneNo = calldialog.ChooseCallNo;
                        }
                        else
                        {
                            //主动撤销
                            return false;
                        }
                    }
                }
                MakeCallEventArgs tpe = new MakeCallEventArgs { CallServiceId = PageData.CallServiceId, Uuid = PageData.UuidOfIPCC, PhoneType = phoneType, PhoneNo = phoneNo, Message = message };
                MakeCall(this, tpe);
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
            if (RemoveCall != null)
            {
                RemoveCallEventArgs rpe = new RemoveCallEventArgs { CallServiceId = PageData.CallServiceId, PhoneType = phoneType, PhoneNo = phoneNo, Message = message };
                RemoveCall(this, rpe);
                if (!rpe.Cancel)
                {
                    //没有取消，则记录日志
                    result = true;
                }
            }
            return result;
        }

        public void CloseWin()
        {
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

        public void SetCallTransfer()
        {
            if (InvokeTransfer != null)
            {
                InvokeTransfer(this, new EventArgs());
            }
        }

        public void OpenNewWinAsCallServiceInfo(string urlFragment, string callServiceJsonStr)
        {
            CallService callService = JsonConvert.DeserializeObject<CallService>(callServiceJsonStr);
            this.PageData = callService;
            this.LoadUrl(TheMotherWin.PlatformAddress + urlFragment);
        }

        public void OpenWinAsServiceVoice(string urlFragment)
        {
            winVoice = new frmVoice();
            winVoice.LoadUrl(urlFragment);
            winVoice.CTIWinClose += new dCTIWinClose((o, e) =>
            {
                //关闭窗体  
                winVoice = null;
            });
            winVoice.Show();
        }
        #endregion

        private void frmInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CTIWinClose != null)
            {
                CTIWinClose(this, new EventArgs());
            }
        }

        private void lbConferenceMember_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = lbConferenceMember.IndexFromPoint(e.Location);
                if (index >= 0)
                {
                    lbConferenceMember.SelectedIndex = index;
                    ListBoxItem item = lbConferenceMember.SelectedItem as ListBoxItem;
                    cmenuConference.Items.Find("cmenuitemKick", true)[0].Visible = (item.Value != EnvironmentVar.ExtCode && item.Value != TheMotherWin.getOldManPhoneNo());
                    ConferenceMemberItem memberItem = TheMotherWin.getMember(item.Value);
                    cmenuConference.Items.Find("cmenuitemMute", true)[0].Visible = !memberItem.IsMute;
                    cmenuConference.Items.Find("cmenuitemUnMute", true)[0].Visible = memberItem.IsMute;
                    cmenuConference.Items.Find("cmenuitemDeaf", true)[0].Visible = !memberItem.IsDeaf;
                    cmenuConference.Items.Find("cmenuitemUnDeaf", true)[0].Visible = memberItem.IsDeaf;
                    cmenuConference.Show(lbConferenceMember, e.Location);
                }
            }
        }

        #region 供父窗口会议事件调用
        internal void _OnConferenceCreate(string conferenceName, string updateTime)
        {
            Common.DebugLog(string.Format("---------------------frmCTI_OnConferenceCreate:{0},{1}", conferenceName, updateTime));
            //lblConferenceName.Text = "多方通话:" + conferenceName;
        }
        internal void _OnConferenceDestroy(string conferenceName, string updateTime)
        {
            Common.DebugLog(string.Format("---------------------frmCTI_OnConferenceDestroy:{0},{1}", conferenceName, updateTime));
            lblConferenceName.Text = "--";
            if (lbConferenceMember != null && lbConferenceMember.Items.Count > 0)
            {
                lbConferenceMember.Items.Clear();
            }
        }
        internal void _OnConferenceMemberAdd(string conferenceName, string updateTime, string phoneNo, string memberId)
        {
            Common.DebugLog(string.Format("---------------------frmCTI_OnConferenceMemberAdd:{0},{1},{2},{3}", conferenceName, updateTime, phoneNo, memberId));

            string memberName = phoneNo;
            if (phoneNo == EnvironmentVar.ExtCode)
            {
                memberName = "座席<" + EnvironmentVar.ExtCode + ">";
            }
            else if (phoneNo == TheMotherWin.getOldManPhoneNo())
            {
                memberName = "老人";
            }
            lbConferenceMember.Items.Add(new ListBoxItem { Text = memberName, Value = phoneNo });

        }
        internal void _OnConferenceMemberRemove(string conferenceName, string updateTime, string phoneNo)
        {
            Common.DebugLog(string.Format("---------------------frmCTI_OnConferenceMemberRemove:{0},{1},{2}", conferenceName, updateTime, phoneNo));
            int index = 0;
            foreach (ListBoxItem item in lbConferenceMember.Items)
            {
                if (item.Value == phoneNo)
                {
                    lbConferenceMember.Items.RemoveAt(index);
                    break;
                }
                index++;
            }
        }
        internal void _OnMessageBoxShowInfo(string message) {
            Common.DebugLog(string.Format("---------------------frmInfo_OnMessageBoxShowInfo:{0}", message));
            if (!string.IsNullOrEmpty(message)) {
                MessageBox.Show(this, message);
            }
        }
        #endregion

        #region 多方通话菜单事件处理
        private void cmenuitemKick_Click(object sender, EventArgs e)
        {
            ListBoxItem item = lbConferenceMember.SelectedItem as ListBoxItem;
            if (item == null)
            {
                MessageBoxAdapter.ShowDebug("请确保选中要踢出的号码！");
                return;
            }
            TheMotherWin._ControlConferenceMember("kick", item.Value);

        }

        private void cmenuitemMute_Click(object sender, EventArgs e)
        {
            ListBoxItem item = lbConferenceMember.SelectedItem as ListBoxItem;
            if (item == null)
            {
                MessageBoxAdapter.ShowDebug("请确保选中要禁麦的号码！");
                return;
            }
            TheMotherWin._ControlConferenceMember("mute", item.Value);
        }

        private void cmenuitemUnMute_Click(object sender, EventArgs e)
        {
            ListBoxItem item = lbConferenceMember.SelectedItem as ListBoxItem;
            if (item == null)
            {
                MessageBoxAdapter.ShowDebug("请确保选中要取消禁麦的号码！");
                return;
            }
            TheMotherWin._ControlConferenceMember("unmute", item.Value);
        }

        private void cmenuitemDeaf_Click(object sender, EventArgs e)
        {
            ListBoxItem item = lbConferenceMember.SelectedItem as ListBoxItem;
            if (item == null)
            {
                MessageBoxAdapter.ShowDebug("请确保选中要静音的号码！");
                return;
            }
            TheMotherWin._ControlConferenceMember("deaf", item.Value);
        }

        private void cmenuitemUnDeaf_Click(object sender, EventArgs e)
        {
            ListBoxItem item = lbConferenceMember.SelectedItem as ListBoxItem;
            if (item == null)
            {
                MessageBoxAdapter.ShowDebug("请确保选中要取消静音的号码！");
                return;
            }
            TheMotherWin._ControlConferenceMember("undeaf", item.Value);
        }
        #endregion
    }
}
