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
using Newtonsoft.Json;
using win.core.utils;
using System.IO;
using System.Reflection;
using System.Dynamic;
using System.Xml.Linq;
using SmartLife.Share.Models.PO.eComm;
using e0571.web.core.TypeExtension;
using System.Threading;
using SmartLife.Share.Models.PO;
using e0571.web.core.MVC;
using win.core.models;

namespace SmartLife.Client.Seat
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class frmMainV2X : Form
    {
        #region 内部变量
        public string PlatformAddress { get; private set; }
        public bool ServiceOnlineFlag { get; private set; }
        public string IPCCOfCityInvokeAddress { get; set; }
        public string WeiXinOfServiceOnlineInovkeAddress { get; set; }
        public bool isSignInToIPCC = false;//IPCC是否登录
        public bool isSignInToWeiXinServiceOnline = false;//微信在线客服是否登录
        public bool isSignInToBIZ = false;//业务处理是否登录
        public bool isTalking = false;//通话标志
        public bool isProcessing = false;//处理标志
        public bool isHoldPhone = false;//呼叫保持标志
        public bool isInConference = false;//会议标志 
        public bool isOutBound = false;//外呼标志
        public bool isEmergentQueue = false;
        Guid? _CallServiceId = null;
        string _OldManPhoneNo = null;
        string _Uuid = null;
        string _Caller = null;
        string _Callee = null;
        Thread CTI_Call_CircleThread;
        Thread CTI_Conference_CircleThread;
        Thread CTI_DelayPopup_CircleThread;
        Thread CTI_WeiXinServiceOnline_CircleThread;//监听在没有打开客服窗口时，是否有新消息
        Thread heartbeat_Thread;
        Thread Emergent_Queue_CircleThread;
        frmCTI winCTI;
        frmInfo winInfo;
        frmConference winConference;
        List<frmServiceOnline> serviceOnlineFormsOpened = new List<frmServiceOnline>();
        Queue<frmServiceOnline> serviceOnlineFormsWaitOpen = new Queue<frmServiceOnline>();
        string runMode;
        Dictionary<string, string> telInfo = new Dictionary<string, string>();
        Dictionary<string, ConferenceMemberItem> conferenceMembers = new Dictionary<string, ConferenceMemberItem>();
        #region 语音提醒
        bool PlayToneFlag = false;
        string StartPlayToneTime;
        string EndPlayToneTime;
        #endregion
        #region 地址
        public const string QueueMemberListUrl = "/views/shared/tabContainer.htm?pageUrl=views/seat-client/call-of-queuemember.htm";
        public const string CallDialBackListUrl = "/views/shared/tabContainer.htm?pageUrl=views/seat-client/call-for-dialback-v2x.htm";
        public const string ExtOfConferenceUrl = "/views/shared/tabContainer.htm?pageUrl=views/seat-client/conference-of-ext.htm&extcode={0}";
        public const string DetailedDialBackListUrl = "/views/shared/tabContainer.htm?pageUrl=views/seat-client/call-of-detailed-dialback.htm";
        #endregion

        #endregion

        #region  重绘窗体引用函数以及闪烁
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hwnd, ref _Rect rect);
        [DllImport("User32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImportAttribute("user32.dll")]
        public static extern bool FlashWindow(IntPtr handle, bool bInvert);
        #endregion

        public frmMainV2X()
        {
            InitializeComponent();
        }

        private void frmMainV2X_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height - 5;
            this.MaximumSize = new Size(Width, Height);
            Text = Properties.Settings.Default.ProductName + " " + VersionAdapter.GetVersion(VersionType.ASSEMBLY);
            xClock.DateTime = DateTime.Now;

            cbbServiceList.Visible = false;
            wbWorkArea.ObjectForScripting = this;
            wbPhoneListQueueMember.ObjectForScripting = this;
            wbPhoneList4DialBack.ObjectForScripting = this;
            PlatformAddress = INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLATFORM_ADDRESS, Common.INI_FILE_PATH);
            ServiceOnlineFlag = INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_SERVICE_ONLINE, Common.INI_FILE_PATH) == "1";
            if (!ServiceOnlineFlag)
            {
                //没有开启在线客服功能的话则后面的按钮位置前移
                pnlConfView.Location = new Point(pnlConfView.Location.X - pnlServiceOnline.Size.Width, 0);
            }
            PlayToneFlag = INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE, Common.INI_FILE_PATH) == "1";
            StartPlayToneTime = INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE_STIME, Common.INI_FILE_PATH);
            EndPlayToneTime = INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE_ETIME, Common.INI_FILE_PATH);
            if (string.IsNullOrEmpty(PlatformAddress))
            {
                string path = Path.Combine(Environment.CurrentDirectory, "htm/main.htm");
                if (File.Exists(path))
                {
                    wbWorkArea.Url = new Uri("file://" + path);
                }
            }
        }

        #region 帮助方法

        #region 微信
        private void _CheckInToWeiXinServiceOnline()
        {
            if (string.IsNullOrEmpty(WeiXinOfServiceOnlineInovkeAddress))
            {
                return;
            }
            string url = WeiXinOfServiceOnlineInovkeAddress + "/api/v1/CheckIn/" + EnvironmentVar.ExtCode;

            HttpAdapter.postAsyncAsJSON(url, (result, response) =>
            {
                if ((bool)result.Success)
                {

                    //保证重复登录不会有影响
                    isSignInToWeiXinServiceOnline = true;

                }
                else if (!(bool)result.Success && (int)result.ErrorCode == 57001)
                {
                    isSignInToWeiXinServiceOnline = true;
                }
                if (isSignInToWeiXinServiceOnline)
                {
                    this.UIInvoke(() =>
                    {
                        btnServiceOnline.Enabled = true;
                        btnServiceOnline.BackColor = SystemColors.Control;
                    });

                    //加载事件监听
                    _RemoveListenerToWeiXinServiceOnline();
                    _AddListenerToWeiXinServiceOnline();
                }
            });
        }

        private void _CheckOutToWeiXinServiceOnline()
        {
            //移除事件监听
            _RemoveListenerToWeiXinServiceOnline();

            string url = WeiXinOfServiceOnlineInovkeAddress + "/api/v1/CheckOut/" + EnvironmentVar.ExtCode;
            HttpAdapter.postSyncAsJSON(url, (result, response) =>
            {

                //保证重复登录不会有影响
                isSignInToWeiXinServiceOnline = false;
                this.UIInvoke(() =>
                {
                    btnServiceOnline.Enabled = false;
                    btnServiceOnline.BackColor = SystemColors.ControlDarkDark;
                });
            });
        }

        private void _AddListenerToWeiXinServiceOnline()
        {
            CTI_WeiXinServiceOnline_CircleThread = ThreadAdapter.DoCircleTask(
                () =>
                {
                    #region 执行任务=》检测座席是否有来自微信的客户需要服务
                    //同步请求
                    string url = WeiXinOfServiceOnlineInovkeAddress + "/api/v1/GetWeiXinClient/" + EnvironmentVar.ExtCode;

                    HttpAdapter.getSyncTo(url, (result, response) =>
                    {
                        if ((bool)result.Success)
                        {
                            dOnGetWeiXinClientSuccess dc = new dOnGetWeiXinClientSuccess(_OnGetWeiXinClientSuccess);
                            this.Invoke(dc, new object[] { result.rows });

                        }
                        else
                        {
                            Common.DebugLog(string.Format("---------------------ErrorMessage:{0} ", result.ErrorMessage));
                        }
                    });

                    #endregion
                },
                4000,
                () =>
                {
                    return !isSignInToWeiXinServiceOnline;
                },//座席没登录则停止
                () =>
                {
                    return false;
                }//暂时永远不跳过
                );
        }

        private void _OnGetWeiXinClientSuccess(dynamic openIds)
        {
            string[] arrOpenIds = JsonConvert.DeserializeObject<string[]>(openIds.ToString());
            if (arrOpenIds.Count() > 0)
            {
                foreach (string openId in arrOpenIds)
                {
                    if (!serviceOnlineFormsOpened.Exists(item => item.Name.Equals(openId))
                        && serviceOnlineFormsWaitOpen.Count(item => item.Name.Equals(openId)) == 0
                        )
                    {
                        frmServiceOnline frm = new frmServiceOnline() { Name = openId };
                        frm.TheMotherWin = this;
                        serviceOnlineFormsWaitOpen.Enqueue(frm);
                    }
                }

                _CalculateOpenedForms();
            }
        }

        private void _CalculateOpenedForms()
        {
            int count = serviceOnlineFormsWaitOpen.Count;
            if (count > 0)
            {
                pbWeiXinClientCount.Visible = true;
                if (count > 9)
                {
                    pbWeiXinClientCount.Image = Properties.Resources.R9Plus;
                }
                else
                {
                    pbWeiXinClientCount.Image = (Image)Properties.Resources.ResourceManager.GetObject("R" + count.ToString());
                }
            }
            else
            {
                pbWeiXinClientCount.Visible = false;
                pbWeiXinClientCount.Image = null;
            }
        }

        private void _OpenNextServiceOnlineForm()
        {

            _CalculateOpenedForms();
        }

        private void _RemoveListenerToWeiXinServiceOnline()
        {
            if (CTI_WeiXinServiceOnline_CircleThread != null && CTI_WeiXinServiceOnline_CircleThread.IsAlive)
            {
                CTI_WeiXinServiceOnline_CircleThread.Abort();
                CTI_WeiXinServiceOnline_CircleThread = null;
            }
        }

        private void _PopupServiceOnlineWin()
        {
            if (serviceOnlineFormsWaitOpen.Count > 0)
            {
                frmServiceOnline winServiceOnline = serviceOnlineFormsWaitOpen.Dequeue();
                winServiceOnline.FormClosed += new FormClosedEventHandler((co, ce) =>
                {
                    for (int i = 0; i < serviceOnlineFormsOpened.Count; i++)
                    {
                        if (serviceOnlineFormsOpened[i].Name == (co as frmServiceOnline).Name)
                        {
                            serviceOnlineFormsOpened.RemoveAt(i);
                            break;
                        }
                    }
                });
                winServiceOnline.LoadUrl(PlatformAddress + "/views/shared/tabContainer.htm?pageUrl=views/seat-client/service-online.htm");
                winServiceOnline.Show();
                serviceOnlineFormsOpened.Add(winServiceOnline);
                _CalculateOpenedForms();
            }
        }
        #endregion

        #region IPCC
        internal string getIPCCInterface4EComm(string op, IDictionary<string, string> otherParams)
        {
            string baseUrl = "http://{0}/atstar/index.php/status-op?op={1}";
            if (runMode == Common.RUNMODE_INTERNET)
            {
                baseUrl = string.Format(baseUrl, EnvironmentVar.CallCenterIP + ":" + EnvironmentVar.CallCenterPort.ToString(), op);
            }
            else if (runMode == Common.RUNMODE_INTRANET)
            {
                baseUrl = string.Format(baseUrl, EnvironmentVar.CallCenterIPInner + ":" + EnvironmentVar.CallCenterPortInner.ToString(), op);
            }
            else
            {
                //Debug
                baseUrl = string.Format(baseUrl, EnvironmentVar.CallCenterIPProxy + ":" + EnvironmentVar.CallCenterPortProxy.ToString(), op);
            }
            List<string> otherParamsList = new List<string>();
            foreach (string key in otherParams.Keys)
            {
                otherParamsList.Add(key + "=" + otherParams[key]);
            }
            return baseUrl + "&" + string.Join("&", otherParamsList.ToArray());
        }
        internal void setTelInfo(int telType, string phoneNo, string message)
        {
            telInfo[phoneNo] = "[" + telType.ToString() + ":" + phoneNo + ":" + message + "]";
        }
        internal string getOldManPhoneNo()
        {
            return _OldManPhoneNo;
        }
        internal string getTelInfo(string phoneNo)
        {
            if (telInfo.ContainsKey(phoneNo))
            {
                return telInfo[phoneNo];
            }
            return phoneNo;
        }
        private void _DispatchCTI(string caller, string callee, string uuid)
        {
            //查找到CallService记录 
            object oRet = wbWorkArea.Document.InvokeScript("getCallServiceByUuid", new object[] { uuid });
            if (oRet != null && oRet.ToString() != "{}")
            {
                string logContent = "坐席工号<" + EnvironmentVar.UserCode + ">开始为您服务";

                CallService callService = JsonConvert.DeserializeObject<CallService>(oRet.ToString());
                _CallServiceId = callService.CallServiceId;
                _OldManPhoneNo = callService.Caller;
                _Uuid = callService.UuidOfIPCC;
                //更新CallService 并弹出
                //0-未受理  1-受理中 2-处理中  3-处理完成
                //string groupType = callService.ServiceQueueId.ToString().Substring(0, 5);
                //if (groupType == "10003")
                //{
                //    //生活服务 需要产生工单，所以有受理中的阶段
                //    callService.DoStatus = 1;
                //    callService.DoResults = "受理中";
                //}
                //else
                //{
                //    //其他直接处理中
                //    callService.DoStatus = 2;
                //    callService.DoResults = "处理中";
                //} 

                //更新已弹屏标志
                callService.PopFlag = 1;
                callService.ServiceExtId = EnvironmentVar.ExtId;
                callService.ServiceExtNo = EnvironmentVar.ExtCode;
                string strCallServiceId = callService.CallServiceId.ToString();
                string strJsonStr = JsonConvert.SerializeObject(
                    new
                    {
                        DoStatus = callService.DoStatus,
                        DoResults = callService.DoResults,
                        //CallSeconds = callService.CallSeconds,
                        ServiceExtId = callService.ServiceExtId,
                        ServiceExtNo = callService.ServiceExtNo,
                        PopFlag = callService.PopFlag
                    }
                    );
                string openUrl = "";
                switch (callService.ServiceQueueNo.Substring(5))
                {
                    case "0":
                        openUrl = Common.ADDRESS_DICT[Common.ADDRESS_KEY_PROCESS_EMERGENCY];
                        break;
                    case "1":
                        openUrl = Common.ADDRESS_DICT[Common.ADDRESS_KEY_PROCESS_FAMILYCALLS];
                        break;
                    case "2":
                        openUrl = Common.ADDRESS_DICT[Common.ADDRESS_KEY_PROCESS_COMMONSERVICE];
                        break;
                    case "3":
                        openUrl = Common.ADDRESS_DICT[Common.ADDRESS_KEY_PROCESS_LIFE];
                        break;
                    default:
                        break;
                }
                if (openUrl != "")
                {
                    object oRet2 = wbWorkArea.Document.InvokeScript("beginProcessCallService", new object[] { strCallServiceId, strJsonStr, logContent });
                    if (oRet2 != null && oRet2.ToString() == bool.TrueString)
                    {
                        _OpenNewWinAsProcessing(openUrl, callService);
                    }
                }
                else
                {
                    Common.DebugLog(string.Format("---------------------_DispatchCTI:无效的队列:{0}", callService.ServiceQueueNo));
                    MessageBoxAdapter.ShowDebug("无效的队列:" + callService.ServiceQueueNo);
                }
            }
            else
            {
                Common.DebugLog(string.Format("---------------------_DispatchCTI:无法找到服务:{0}", uuid));
                MessageBoxAdapter.ShowDebug("无法找到服务:" + uuid);
            }
        }
        private void _OpenNewWinAsProcessing(string urlFragment, CallService callService)
        {
            winCTI = new frmCTI();
            winCTI.TheMotherWin = this;
            winCTI.PageData = callService;

            winCTI.MakeCall += new dMakeCall((o, e) =>
            {
                //if (isSignInToIPCC)
                //{

                //}
                if (isTalking)
                {
                    if (!isInConference)
                    {
                        //创建多方通话
                        _ConvertToConference(e.PhoneNo, e.CallServiceId.ToString());
                    }
                    else
                    {
                        //邀请多方通话
                        _InviteConference(e.PhoneNo, e.CallServiceId.ToString());
                    }

                }
                else
                {
                    //判定
                    _DialBack();
                    //呼叫新号码 
                    _MakeCall(e.PhoneNo);

                }
                setTelInfo(e.PhoneType, e.PhoneNo, e.Message);
            });
            winCTI.RemoveCall += new dRemoveCall((o, e) =>
            {
                //移除新号码  都是移除会议室
                if (isInConference)
                {
                    e.Cancel = !_ControlConferenceMember("kick", e.PhoneNo);
                }
                else
                {
                    e.Cancel = true;
                }


            });
            winCTI.InvokeHoldCall += new dInvokeHoldCall((o, e) =>
            {
                //保持通话 
                _ChangePhoneHoldState();
            });
            winCTI.InvokeRetreiveCall += new dInvokeRetreiveCall((o, e) =>
            {
                //取消保持通话 
                _ChangePhoneHoldState();
            });
            winCTI.InvokeTransfer += new dInvokeTransfer((o, e) =>
            {
                //呼叫转接
                _TransferPhone();
            });
            winCTI.CTIWinClose += new dCTIWinClose((o, e) =>
            {
                //关闭窗体 
                wbWorkArea.Document.InvokeScript("refreshFromExternal");
                isProcessing = false;
                winCTI = null;


            });
            winCTI.LoadUrl(PlatformAddress + urlFragment);
            winCTI.Show();
            isProcessing = true;
        }
        private void _OpenNewWinAsInfo(string urlFragment, CallService callService)
        {
            winInfo = new frmInfo();
            winInfo.TheMotherWin = this;
            winInfo.PageData = callService;
            winInfo.LoadUrl(PlatformAddress + urlFragment);
            winInfo.MakeCall += new dMakeCall((o, e) =>
            {
                //呼叫新号码 
                string phoneNo = e.PhoneNo;
                if (isTalking)
                {
                    if (!isInConference)
                    {
                        //创建多方通话
                        _ConvertToConference(e.PhoneNo, e.CallServiceId.ToString());
                    }
                    else
                    {
                        //邀请多方通话
                        _InviteConference(e.PhoneNo, e.CallServiceId.ToString());
                    }
                }
                else
                {
                    _CallServiceId = e.CallServiceId;
                    _OldManPhoneNo = callService.Caller;
                    _Uuid = e.Uuid;
                    if (MessageBoxAdapter.ShowConfirm("确定要呼叫【" + phoneNo + "】？") == System.Windows.Forms.DialogResult.OK)
                    {
                        //回拨的时候签出
                        setTelInfo(e.PhoneType, e.PhoneNo, e.Message);
                        //判定
                        _DialBack();
                        _MakeCall(phoneNo);
                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            });
            winInfo.RemoveCall += new dRemoveCall((o, e) =>
            {
                //移除新号码 
                if (isInConference)
                {
                    e.Cancel = !_ControlConferenceMember("kick", e.PhoneNo);
                }
                else
                {
                    e.Cancel = true;
                }
            });
            winInfo.InvokeHoldCall += new dInvokeHoldCall((o, e) =>
            {
                //保持通话 
                _ChangePhoneHoldState();
            });
            winInfo.InvokeRetreiveCall += new dInvokeRetreiveCall((o, e) =>
            {
                //取消保持通话 
                _ChangePhoneHoldState();
            });
            winInfo.InvokeTransfer += new dInvokeTransfer((o, e) =>
            {
                //呼叫转接
                _TransferPhone();
            });
            winInfo.CTIWinClose += new dCTIWinClose((o, e) =>
            {
                //关闭窗体  
                wbWorkArea.Document.InvokeScript("refreshFromExternal");
                isProcessing = false;
                winInfo = null;
            });
            winInfo.Show();
            isProcessing = true;
        }
        private void _ResetPhoneHoldState()
        {
            if (isHoldPhone) isHoldPhone = false;
            btnCallHoldState.Text = "呼叫保持";
        }
        private void _ChangePhoneHoldState()
        {
            if (isSignInToIPCC && isTalking)
            {
                string op;
                string msg;
                if (isHoldPhone)
                {
                    //取消保持
                    msg = "呼叫保持";
                    op = "unhold";
                }
                else
                {
                    //呼叫保持
                    msg = "取消保持";
                    op = "hold";
                }

                string urlToDo = getIPCCInterface4EComm(op, new Dictionary<string, string> { { "ext_no", EnvironmentVar.ExtCode } });
                HttpAdapter.getAsyncTo(urlToDo, (result0, response0) =>
                {
                    Common.DebugLog(string.Format("################### chnageHoldState {0}:{1}", op, result0));
                    if (result0 == "+OK")
                    {
                        isHoldPhone = !isHoldPhone;
                        BeginInvoke(new Action(() =>
                        {
                            btnCallHoldState.Text = msg;
                        }));
                    }
                    else
                    {
                        MessageBoxAdapter.ShowDebug(result0);
                    }
                });
            }
        }
        private void _SetSeatBusyFree()
        {
            if (btnSetBusyFree.Text == "服务示忙")
            {
                if (CTI_Call_CircleThread != null)
                {
                    if (CTI_Call_CircleThread.IsAlive)
                    {
                        CTI_Call_CircleThread.Abort();
                    }
                    CTI_Call_CircleThread = null;
                }

                if (CTI_Conference_CircleThread != null)
                {
                    if (CTI_Conference_CircleThread.IsAlive)
                    {
                        CTI_Conference_CircleThread.Abort();
                    }
                    CTI_Conference_CircleThread = null;
                }

                if (CTI_DelayPopup_CircleThread != null)
                {
                    if (CTI_DelayPopup_CircleThread.IsAlive)
                    {
                        CTI_DelayPopup_CircleThread.Abort();
                    }
                    CTI_DelayPopup_CircleThread = null;
                }


                _CheckOut(true, (result1, response1) =>
                {
                    if (result1 == "+OK")
                    {
                        Common.DebugLog("################### setBusy:" + result1);
                        isSignInToIPCC = false;
                        this.UIInvoke(() =>
                        {
                            btnSetBusyFree.Text = "服务示闲";
                            pnlSetBusyFree.BackColor = Color.Yellow;
                            pnlSeat.BackColor = Color.DarkGray;
                        });
                    }
                    else
                    {
                        if (isOutBound)
                        {
                            isOutBound = false;
                        }
                    }
                });
            }
            else
            {
                _CheckIn(true, (result0, response0) =>
                {
                    Common.DebugLog("################### setFree:" + result0);
                    if (result0 == "+OK")
                    {
                        isSignInToIPCC = true;
                        this.UIInvoke(() =>
                        {
                            btnSetBusyFree.Text = "服务示忙";
                            pnlSetBusyFree.BackColor = Color.White;
                            pnlSeat.BackColor = Color.White;
                        });

                        if (CTI_Call_CircleThread == null && CTI_Conference_CircleThread == null && CTI_DelayPopup_CircleThread == null)
                        {
                            //重新加载事件监听
                            _MessageLoopForCTI_Call();
                        }
                    }
                    else
                    {
                        if (isOutBound)
                        {
                            MessageBoxAdapter.ShowError("网络异常，请手动示闲！");
                        }
                    }
                });
            }
        }
        private void _TransferPhone()
        {
            if (isSignInToIPCC)
            {
                frmTransfer transferDialog = new frmTransfer();
                transferDialog.TheMotherWin = this;
                transferDialog.ShowDialog();
            }
        }
        private void _AddServiceVoice(string uuidOfIPCC)
        {
            if (_CallServiceId != null)
            {
                wbWorkArea.Document.InvokeScript("addServiceVoice", new object[] { _CallServiceId.ToString(), "2", uuidOfIPCC, "座席录音开始" });
            }
        }
        public void _UpdateDialBackUuid(string uuidOfIPCC)
        {
            if (_CallServiceId != null)
            {
                string strJsonStr = JsonConvert.SerializeObject(new { UuidOfIPCC = uuidOfIPCC });
                wbWorkArea.Document.InvokeScript("saveNoLogCallService", new object[] { _CallServiceId.ToString(), strJsonStr });
            }
        }
        private void _FinishServiceVoice(string uuidOfIPCC)
        {
            if (_CallServiceId != null)
            {

                wbWorkArea.Document.InvokeScript("finishServiceVoiceInEComm", new object[] { _CallServiceId.ToString(), "2", uuidOfIPCC, "座席录音结束" });
            }
        }
        private void _CheckOut(bool async, HttpCallbackSuccess success)
        {
            string urlToCheckOut = getIPCCInterface4EComm("agentstatus", new Dictionary<string, string> { { "ext_no", EnvironmentVar.ExtCode }, { "status", "On Break" } });
            if (async)
            {
                HttpAdapter.getAsyncTo(urlToCheckOut, success);
            }
            else
            {
                HttpAdapter.getSyncTo(urlToCheckOut, success);
            }
        }
        private void _ClearCTI()
        {
            string url = IPCCOfCityInvokeAddress + "/api/eComm/ClearCTI/" + EnvironmentVar.ExtCode;
            HttpAdapter.postSyncAsJSON(url, (result, response) =>
            {
                if ((bool)result.Success) { }
                else
                {
                    Common.DebugLog(string.Format("---------------------ErrorMessage:{0} ", result.ErrorMessage));
                }
            });
        }
        private void _CheckIn(bool async, HttpCallbackSuccess success)
        {
            //清空非本机登录的消息
            _ClearCTI();

            string urlToQueryStatus = getIPCCInterface4EComm("query_agentstatus", new Dictionary<string, string> { { "ext_no", EnvironmentVar.ExtCode } });
            HttpAdapter.getAsyncTo(urlToQueryStatus, (result0, response0) =>
            {
                Common.DebugLog("################### query status:" + result0);
                if (result0 == "Available")
                {
                    //已经在线
                    _CheckOut(false, (result1, response1) =>
                    {
                        if (result1 == "+OK")
                        {
                            Common.DebugLog("################### checkout:" + result1);
                        }
                    });

                }

                //分机座席签入
                string urlToCheckIn = getIPCCInterface4EComm("agentstatus", new Dictionary<string, string> { { "ext_no", EnvironmentVar.ExtCode }, { "status", "Available" } });

                if (async)
                {
                    HttpAdapter.getAsyncTo(urlToCheckIn, success);
                }
                else
                {
                    HttpAdapter.getSyncTo(urlToCheckIn, success);
                }

            });




        }
        private void _MakeCall(string phoneNo)
        {
            string urlToMakeCall = getIPCCInterface4EComm("dial", new Dictionary<string, string> { { "ext_no", EnvironmentVar.ExtCode }, { "dia_num", phoneNo } });

            HttpAdapter.getAsyncTo(urlToMakeCall, (result0, response0) =>
            {
                Common.DebugLog("################### make call:" + result0);
                if (result0 == "+OK")
                {

                }
                else
                {
                    if (isOutBound)
                    {
                        if (btnSetBusyFree.Text == "服务示闲")
                        {
                            _SetSeatBusyFree();//签入
                        }
                    }

                    MessageBoxAdapter.ShowDebug(result0);
                }
            });
        }
        private void _DialBack()
        {
            isOutBound = true;
            //回拨的时候签出
            //判断是否处于签入状态，如果是则需要签出否则还是签入
            if (btnSetBusyFree.Text == "服务示忙")
            {
                _SetSeatBusyFree();//签出
            }
            if (!isOutBound)
            {
                //签出失败
                MessageBoxAdapter.ShowError("网络异常，请手动示忙！");
                return;
            }
            ////重新加载事件监听
            if (CTI_Call_CircleThread == null && CTI_Conference_CircleThread == null && CTI_DelayPopup_CircleThread == null)
            {
                //重新加载事件监听
                _MessageLoopForCTI_Call();
            }
        }
        private void _RemoveCall()
        {

        }
        private void _ConvertToConference(string phoneNo, string callServiceId)
        {
            ///新加一个参数，以每次呼叫的callserviceid作为一次会议上
            string urlConvertToConference = getIPCCInterface4EComm("conference", new Dictionary<string, string> { { "ext_no", EnvironmentVar.ExtCode }, { "confname", EnvironmentVar.ExtCode + "_" + callServiceId } });

            HttpAdapter.getAsyncTo(urlConvertToConference, (result0, response0) =>
            {
                Common.DebugLog("################### convert to conference:" + result0);
                if (result0 == "+OK")
                {
                    isInConference = true;
                    //多方通话消息处理
                    _MessageLoopForCTI_Conference();

                    _InviteConference(phoneNo, callServiceId);
                }
            });
        }
        private void _InviteConference(string phoneNo, string callServiceId)
        {
            if (conferenceMembers.ContainsKey(phoneNo))
            {
                MessageBoxAdapter.ShowInfo(string.Format("当前号码{0}已加入到多方通话中", phoneNo));
                return;
            }
            ///新加一个参数，以每次呼叫的callserviceid作为一次会议上
            string urlInviteConference = getIPCCInterface4EComm("inviteconference", new Dictionary<string, string> { { "ext_no", EnvironmentVar.ExtCode }, { "dial_num", phoneNo }, { "confname", EnvironmentVar.ExtCode + "_" + callServiceId } });

            HttpAdapter.getAsyncTo(urlInviteConference, (result0, response0) =>
            {
                Common.DebugLog("################### invite conference:" + result0);
                if (result0 == "+OK")
                {
                }
            });
        }
        private void _ControlConference()
        {

        }
        internal bool _ControlConferenceMember(string controlType, string phoneNo)
        {
            if (!conferenceMembers.ContainsKey(phoneNo))
            {
                MessageBoxAdapter.ShowInfo(string.Format("当前号码{0}不在多方通话中", phoneNo));
                return false;
            }

            string memberId = (conferenceMembers[phoneNo] as ConferenceMemberItem).MemberId;

            string urlCtrlMember = getIPCCInterface4EComm("ctrlmember", new Dictionary<string, string> { { "ext_no", EnvironmentVar.ExtCode }, { "confname", EnvironmentVar.ExtCode + "_" + _CallServiceId }, { "memberid", memberId }, { "subop", controlType } });
            HttpAdapter.getAsyncTo(urlCtrlMember, (result0, response0) =>
            {
                Common.DebugLog(string.Format("################### ctrlmember subop={0}:", controlType) + result0);
                if (result0 == "+OK")
                {
                    this.UIInvoke(() =>
                    {
                        ConferenceMemberItem memberItem = conferenceMembers[phoneNo];
                        if (controlType == "mute")
                        {
                            memberItem.IsMute = true;
                        }
                        else if (controlType == "unmute")
                        {
                            memberItem.IsMute = false;
                        }
                        else if (controlType == "deaf")
                        {
                            memberItem.IsDeaf = true;
                        }
                        else if (controlType == "undeaf")
                        {
                            memberItem.IsDeaf = false;
                        }

                        if (_CallServiceId != null)
                        {
                            wbWorkArea.Document.InvokeScript("controlConferenceMemberSuccess", new object[] { _CallServiceId.ToString(), telInfo[phoneNo], EnvironmentVar.ExtCode, controlType });
                        }
                    });


                }
            });
            return true;
        }

        #region 双方通话消息循环和处理
        private void _MessageLoopForCTI_Call()
        {
            //检测CTI_Call
            CTI_Call_CircleThread = ThreadAdapter.DoCircleTask(
                () =>
                {
                    #region 执行任务=》检测CTI_Call消息
                    //同步请求
                    string url = IPCCOfCityInvokeAddress + "/api/eComm/CTI_Call/" + EnvironmentVar.ExtCode;

                    HttpAdapter.postSyncAsJSON(url, (result, response) =>
                    {
                        if ((bool)result.Success)
                        {
                            if (result.ret != null)
                            {
                                CTI_Call cti_o = new CTI_Call { ExtNo = result.ret.ExtNo, CallState = result.ret.CallState, CallTime = result.ret.CallTime, CallType = result.ret.CallType, PeerNumber = result.ret.PeerNumber, Trunk = result.ret.Trunk, Uuid = result.ret.Uuid };
                                //判断消息：检查呼叫状态
                                if (cti_o.CallState == "ring")
                                {
                                    //振铃
                                    if (cti_o.CallType == "in")
                                    {
                                        dOnCallRing dc = new dOnCallRing(_OnCallInRing);
                                        this.Invoke(dc, new object[] { cti_o.ExtNo, cti_o.PeerNumber, cti_o.Trunk, cti_o.Uuid });
                                    }
                                    else if (cti_o.CallType == "out")
                                    {
                                        dOnCallRing dc = new dOnCallRing(_OnCallOutRing);
                                        this.Invoke(dc, new object[] { cti_o.ExtNo, cti_o.ExtNo, cti_o.PeerNumber, cti_o.Uuid });
                                    }
                                }
                                else if (cti_o.CallState == "busy")
                                {
                                    //接通或取消保持
                                    if (cti_o.CallType == "in")
                                    {
                                        dOnCallBusy dc = new dOnCallBusy(_OnCallInBusy);
                                        this.Invoke(dc, new object[] { cti_o.ExtNo, cti_o.PeerNumber, cti_o.Trunk, cti_o.Uuid });
                                    }
                                    else if (cti_o.CallType == "out")
                                    {
                                        dOnCallBusy dc = new dOnCallBusy(_OnCallOutBusy);
                                        this.Invoke(dc, new object[] { cti_o.ExtNo, cti_o.ExtNo, cti_o.PeerNumber, cti_o.Uuid });
                                    }
                                }
                                else if (cti_o.CallState == "hold")
                                {
                                    //呼叫保持
                                    if (cti_o.CallType == "in")
                                    {
                                        dOnCallHold dc = new dOnCallHold(_OnCallInHold);
                                        this.Invoke(dc, new object[] { cti_o.ExtNo, cti_o.PeerNumber, cti_o.Trunk, cti_o.Uuid });
                                    }
                                    else if (cti_o.CallType == "out")
                                    {
                                        dOnCallHold dc = new dOnCallHold(_OnCallOutHold);
                                        this.Invoke(dc, new object[] { cti_o.ExtNo, cti_o.ExtNo, cti_o.PeerNumber, cti_o.Uuid });
                                    }

                                }
                                else if (cti_o.CallState == "idle")
                                {
                                    //挂断
                                    if (cti_o.CallType == "in")
                                    {
                                        dOnCallIdle dc = new dOnCallIdle(_OnCallInIdle);
                                        this.Invoke(dc, new object[] { cti_o.ExtNo, cti_o.PeerNumber, cti_o.Trunk, cti_o.Uuid });
                                    }
                                    else if (cti_o.CallType == "out")
                                    {
                                        dOnCallIdle dc = new dOnCallIdle(_OnCallOutIdle);
                                        this.Invoke(dc, new object[] { cti_o.ExtNo, cti_o.ExtNo, cti_o.PeerNumber, cti_o.Uuid });
                                    }
                                }
                            }

                        }
                        else
                        {
                            Common.DebugLog(string.Format("---------------------ErrorMessage:{0} ", result.ErrorMessage));
                        }
                    });

                    #endregion
                },
                100,
                () =>
                {
                    return !isSignInToBIZ;
                },//座席没登录则停止
                () =>
                {
                    return false;
                }//暂时永远不跳过
                );
        }

        private void _OnCallInRing(string extNo, string caller, string callee, string uuid)
        {
            Common.DebugLog(string.Format("---------------------_OnCallInRing:{0},{1},{2},{3}", extNo, caller, callee, uuid));
            //呼入
            //弹屏 
            if (uuid != _Uuid)
            {
                //新呼叫振铃
                if (winCTI == null)
                {
                    _DispatchCTI(caller, callee, uuid);
                }
                else
                {
                    //倒计时当前窗口
                    winCTI.OnCountdownForClosing();
                }
            }
        }
        private void _OnCallOutRing(string extNo, string caller, string callee, string uuid)
        {
            Common.DebugLog(string.Format("---------------------_OnCallOutRing:{0},{1},{2},{3}", extNo, caller, callee, uuid));
        }
        private void _OnCallInBusy(string extNo, string caller, string callee, string uuid)
        {
            Common.DebugLog(string.Format("---------------------_OnCallInBusy:{0},{1},{2},{3}", extNo, caller, callee, uuid));

            //呼入
            if (isTalking)
            {
                //取消保持
                isHoldPhone = false;
            }
            else
            {
                /******保证ring事件不存在的时候能够继续弹屏*********/

                //接通
                isTalking = true;

                if (winCTI == null)
                {
                    _DispatchCTI(caller, callee, uuid);
                }
                else
                {
                    if (winCTI.CloseForCountdown)
                    {
                        CTI_DelayPopup_CircleThread = ThreadAdapter.DoCircleTask(
                            () =>
                            {
                                if (winCTI == null)
                                {
                                    this.Invoke(new Action<string, string, string>(_DispatchCTI), new object[] { caller, callee, uuid });

                                    _Caller = caller;
                                    _Callee = callee;
                                    this.Invoke(new Action(xClock4Timer.Reset));
                                    setTelInfo(98, _Caller, "老人");
                                    setTelInfo(99, _Callee, "座席<" + EnvironmentVar.ExtCode + ">");
                                    this.Invoke(new Action<string>(_AddServiceVoice), new object[] { uuid });
                                }
                            },
                            100,
                            () =>
                            {
                                return winCTI != null && winCTI.CloseForCountdown == false;
                            });
                    }
                }


                if (!winCTI.CloseForCountdown)
                {
                    _Caller = caller;
                    _Callee = callee;
                    //重置计时器
                    xClock4Timer.Reset();
                    setTelInfo(98, _Caller, "老人");
                    setTelInfo(99, _Callee, "座席<" + EnvironmentVar.ExtCode + ">");
                    _AddServiceVoice(uuid);
                }

            }
        }
        private void _OnCallOutBusy(string extNo, string caller, string callee, string uuid)
        {
            Common.DebugLog(string.Format("---------------------_OnCallOutBusy:{0},{1},{2},{3}", extNo, caller, callee, uuid));
            //呼出
            if (isTalking)
            {
                //取消保持
                isHoldPhone = false;
            }
            else
            {
                //接通
                isTalking = true;
                _Caller = caller;
                _Callee = callee;
                _CallServiceId = winInfo.PageData.CallServiceId;
                if (_Uuid != uuid)
                {
                    _Uuid = uuid;
                }

                CallService callService = winInfo.PageData;
                this.Invoke(new Action(() => { winInfo.CloseWin(); }));


                string openUrl = "";
                switch (callService.ServiceQueueNo.Substring(5))
                {
                    case "0":
                        openUrl = Common.ADDRESS_DICT[Common.ADDRESS_KEY_PROCESS_EMERGENCY];
                        break;
                    case "1":
                        openUrl = Common.ADDRESS_DICT[Common.ADDRESS_KEY_PROCESS_FAMILYCALLS];
                        break;
                    case "2":
                        openUrl = Common.ADDRESS_DICT[Common.ADDRESS_KEY_PROCESS_COMMONSERVICE];
                        break;
                    case "3":
                        openUrl = Common.ADDRESS_DICT[Common.ADDRESS_KEY_PROCESS_LIFE];
                        break;
                    default:
                        break;
                }
                if (openUrl != "")
                {
                    object oRet2 = wbWorkArea.Document.InvokeScript("reProcessCallService", new object[] { _CallServiceId.ToString(), extNo });
                    if (oRet2 != null && oRet2.ToString() == bool.TrueString)
                    {
                        _OpenNewWinAsProcessing(openUrl, callService);
                    }
                }
                else
                {
                    MessageBoxAdapter.ShowDebug("无效的队列:" + callService.ServiceQueueNo);
                }

                //重置计时器
                xClock4Timer.Reset();

                setTelInfo(98, _Callee, "老人");
                setTelInfo(99, _Caller, "座席<" + EnvironmentVar.ExtCode + ">");

                _AddServiceVoice(uuid);
                //_UpdateDialBackUuid(uuid);
            }
        }

        private void _OnCallInHold(string extNo, string caller, string callee, string uuid)
        {
            Common.DebugLog(string.Format("---------------------_OnCallInHold:{0},{1},{2},{3}", extNo, caller, callee, uuid));
            isHoldPhone = true;
        }
        private void _OnCallOutHold(string extNo, string caller, string callee, string uuid)
        {
            Common.DebugLog(string.Format("---------------------_OnCallOutHold:{0},{1},{2},{3}", extNo, caller, callee, uuid));
            isHoldPhone = true;
        }
        private void _OnCallInIdle(string extNo, string caller, string callee, string uuid)
        {
            Common.DebugLog(string.Format("---------------------_OnCallInIdle:{0},{1},{2},{3}", extNo, caller, callee, uuid));
            if (isTalking)
            {
                //已接通
                isTalking = false;
                xClock4Timer.Stop();
                if (_CallServiceId != null)
                {
                    _FinishServiceVoice(uuid);
                    //wbWorkArea.Document.InvokeScript("updateCallSeconds", new object[] { _CallServiceId.ToString(), extNo, xClock4Timer.TotalTicks.ToString() });
                    wbWorkArea.Document.InvokeScript("refreshFromExternal");
                }
            }
        }
        private void _OnCallOutIdle(string extNo, string caller, string callee, string uuid)
        {
            Common.DebugLog(string.Format("---------------------_OnCallOutIdle:{0},{1},{2},{3}", extNo, caller, callee, uuid));
            if (isTalking)
            {
                //已接通
                isTalking = false;
                xClock4Timer.Stop();
                if (_CallServiceId != null)
                {
                    _FinishServiceVoice(uuid);
                }
                if (isOutBound)
                {
                    if (btnSetBusyFree.Text == "服务示闲")
                    {
                        _SetSeatBusyFree();//签入
                    }
                }
            }
        }
        #endregion

        #region 多方通话消息循环和处理
        private void _MessageLoopForCTI_Conference()
        {
            CTI_Conference_CircleThread = ThreadAdapter.DoCircleTask(
                () =>
                {
                    #region 执行任务=》检测CTI_Conference消息
                    string url = IPCCOfCityInvokeAddress + "/api/eComm/CTI_Conference/" + EnvironmentVar.ExtCode + "," + _CallServiceId;

                    HttpAdapter.postSyncAsJSON(url, (result, response) =>
                    {
                        if ((bool)result.Success)
                        {
                            if (result.ret != null)
                            {
                                CTI_Conference cti_o = new CTI_Conference { ConferenceName = result.ret.ConferenceName, Action = result.ret.Action, UpdateTime = result.ret.UpdateTime, MemberPhoneNo = result.ret.MemberPhoneNo, MemberId = result.ret.MemberId };
                                //判断消息：检查呼叫状态
                                if (cti_o.Action == "conference-create")
                                {
                                    //开始多方通话
                                    dOnConferenceCreate dc = new dOnConferenceCreate(_OnConferenceCreate);
                                    this.Invoke(dc, new object[] { cti_o.ConferenceName, cti_o.UpdateTime });
                                    if (isProcessing)
                                    {
                                        if (winCTI != null)
                                        {
                                            this.Invoke(new dOnConferenceCreate(winCTI._OnConferenceCreate), new object[] { cti_o.ConferenceName, cti_o.UpdateTime });
                                        }

                                        if (winInfo != null)
                                        {
                                            this.Invoke(new dOnConferenceCreate(winInfo._OnConferenceCreate), new object[] { cti_o.ConferenceName, cti_o.UpdateTime });
                                        }
                                    }
                                }
                                else if (cti_o.Action == "conference-destroy")
                                {
                                    //结束多方通话
                                    dOnConferenceDestroy dc = new dOnConferenceDestroy(_OnConferenceDestroy);
                                    this.Invoke(dc, new object[] { cti_o.ConferenceName, cti_o.UpdateTime });
                                    if (isProcessing)
                                    {
                                        if (winCTI != null)
                                        {
                                            this.Invoke(new dOnConferenceDestroy(winCTI._OnConferenceDestroy), new object[] { cti_o.ConferenceName, cti_o.UpdateTime });
                                        }

                                        if (winInfo != null)
                                        {
                                            this.Invoke(new dOnConferenceDestroy(winInfo._OnConferenceDestroy), new object[] { cti_o.ConferenceName, cti_o.UpdateTime });
                                        }
                                    }
                                }
                                else if (cti_o.Action == "add-member")
                                {
                                    //成员加入
                                    dOnConferenceMemberAdd dc = new dOnConferenceMemberAdd(_OnConferenceMemberAdd);
                                    this.Invoke(dc, new object[] { cti_o.ConferenceName, cti_o.UpdateTime, cti_o.MemberPhoneNo, cti_o.MemberId });
                                    if (isProcessing)
                                    {
                                        if (winCTI != null)
                                        {
                                            this.Invoke(new dOnConferenceMemberAdd(winCTI._OnConferenceMemberAdd), new object[] { cti_o.ConferenceName, cti_o.UpdateTime, cti_o.MemberPhoneNo, cti_o.MemberId });
                                        }

                                        if (winInfo != null)
                                        {
                                            this.Invoke(new dOnConferenceMemberAdd(winInfo._OnConferenceMemberAdd), new object[] { cti_o.ConferenceName, cti_o.UpdateTime, cti_o.MemberPhoneNo, cti_o.MemberId });
                                        }
                                    }
                                }
                                else if (cti_o.Action == "del-member")
                                {
                                    //成员离开
                                    if (cti_o.MemberPhoneNo != EnvironmentVar.ExtCode)
                                    {
                                        //非座席挂断
                                        dOnConferenceMemberRemove dc = new dOnConferenceMemberRemove(_OnConferenceMemberRemove);
                                        this.Invoke(dc, new object[] { cti_o.ConferenceName, cti_o.UpdateTime, cti_o.MemberPhoneNo });
                                    }
                                    else
                                    {
                                        //座席挂断
                                        //结束业务上的多方通话
                                        dOnConferenceDestroy dc = new dOnConferenceDestroy(_OnConferenceDestroy);
                                        this.Invoke(dc, new object[] { cti_o.ConferenceName, cti_o.UpdateTime });
                                    }
                                    if (isProcessing)
                                    {
                                        if (cti_o.MemberPhoneNo != EnvironmentVar.ExtCode)
                                        {
                                            if (winCTI != null)
                                            {
                                                this.Invoke(new dOnConferenceMemberRemove(winCTI._OnConferenceMemberRemove), new object[] { cti_o.ConferenceName, cti_o.UpdateTime, cti_o.MemberPhoneNo });
                                            }

                                            if (winInfo != null)
                                            {
                                                this.Invoke(new dOnConferenceMemberRemove(winInfo._OnConferenceMemberRemove), new object[] { cti_o.ConferenceName, cti_o.UpdateTime, cti_o.MemberPhoneNo });
                                            }
                                        }
                                        else
                                        {
                                            if (winCTI != null)
                                            {
                                                this.Invoke(new dOnConferenceDestroy(winCTI._OnConferenceDestroy), new object[] { cti_o.ConferenceName, cti_o.UpdateTime });
                                            }

                                            if (winInfo != null)
                                            {
                                                this.Invoke(new dOnConferenceDestroy(winInfo._OnConferenceDestroy), new object[] { cti_o.ConferenceName, cti_o.UpdateTime });
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            Common.DebugLog(string.Format("---------------------ErrorMessage:{0} ", result.ErrorMessage));
                        }
                    });
                    #endregion
                },
                100,
                () =>
                {
                    //非会议退出
                    return !isInConference;
                }
            );
        }

        private void _OnConferenceCreate(string conferenceName, string updateTime)
        {
            Common.DebugLog(string.Format("---------------------_OnConferenceCreate:{0},{1}", conferenceName, updateTime));
            if (_CallServiceId != null)
            {
                wbWorkArea.Document.InvokeScript("conferenceCreate", new object[] { _CallServiceId.ToString() });
            }
        }
        private void _OnConferenceDestroy(string conferenceName, string updateTime)
        {
            Common.DebugLog(string.Format("---------------------_OnConferenceDestroy:{0},{1}", conferenceName, updateTime));
            isInConference = false;
            conferenceMembers.Clear();
            if (_CallServiceId != null)
            {
                wbWorkArea.Document.InvokeScript("conferenceRemove", new object[] { _CallServiceId.ToString() });
            }
        }
        private void _OnConferenceMemberAdd(string conferenceName, string updateTime, string phoneNo, string memberId)
        {
            Common.DebugLog(string.Format("---------------------_OnConferenceMemberAdd:{0},{1},{2},{3}", conferenceName, updateTime, phoneNo, memberId));
            if (_CallServiceId != null)
            {

                if (!conferenceMembers.ContainsKey(phoneNo))
                {

                    string memberName = phoneNo;
                    if (phoneNo == EnvironmentVar.ExtCode)
                    {
                        memberName = "座席<" + EnvironmentVar.ExtCode + ">";
                    }
                    else if (phoneNo == _OldManPhoneNo)
                    {
                        memberName = "老人";
                    }

                    conferenceMembers.Add(phoneNo, new ConferenceMemberItem { MemberId = memberId, IsMute = false, IsDeaf = false });


                    string telInfo = getTelInfo(phoneNo); //{telType}:{tel}:{message}
                    bool needRemove = (phoneNo != EnvironmentVar.ExtCode && phoneNo != _OldManPhoneNo);
                    wbWorkArea.Document.InvokeScript("conferenceMemberAdd", new object[] { _CallServiceId.ToString(), telInfo, needRemove });
                }
            }

        }
        private void _OnConferenceMemberRemove(string conferenceName, string updateTime, string phoneNo)
        {
            Common.DebugLog(string.Format("---------------------_OnConferenceMemberRemove:{0},{1},{2}", conferenceName, updateTime, phoneNo));
            if (_CallServiceId != null)
            {
                if (conferenceMembers.ContainsKey(phoneNo))
                {
                    conferenceMembers.Remove(phoneNo);

                    string telInfo = getTelInfo(phoneNo); //{telType}:{tel}:{message}
                    wbWorkArea.Document.InvokeScript("conferenceMemberRemove", new object[] { _CallServiceId.ToString(), telInfo });
                }
            }
        }

        internal bool isContainConferenceMember(string phoneNo)
        {
            return conferenceMembers.ContainsKey(phoneNo);
        }
        internal ConferenceMemberItem getMember(string phoneNo)
        {
            return conferenceMembers[phoneNo];
        }
        #endregion

        #region 心跳 监控呼叫中心平台分机是否在线
        private void _HeatbeatExtNoOnline()
        {
            heartbeat_Thread = pbHeartbeat.UIDoCircleTask(() =>
            {
                string urlToQueryStatus = getIPCCInterface4EComm("query_agentstatus", new Dictionary<string, string> { { "ext_no", EnvironmentVar.ExtCode } });
                try
                {
                    HttpAdapter.getSyncTo(urlToQueryStatus, (result0, response0) =>
                    {
                        Common.DebugLog("################### _HeatbeatExtNoOnline query status:" + result0);
                        if (result0 == "Available")
                        {
                            pbHeartbeat.BackgroundImage = Properties.Resources.Green;
                        }
                        else
                        {
                            pbHeartbeat.BackgroundImage = Properties.Resources.Red;
                            if (isSignInToIPCC)
                            {
                                if (!string.IsNullOrEmpty(Common.DEFAULT_SOUND_NAME_OF_EXT_OFFLINE))
                                {
                                    using (System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer())
                                    {
                                        soundPlayer.SoundLocation = Common.DEFAULT_SOUND_NAME_OF_EXT_OFFLINE;
                                        soundPlayer.Load();
                                        soundPlayer.Play();
                                    }
                                }
                            }
                        }
                    });
                }
                catch { }
            }, 7000);
        }
        #endregion

        #endregion

        #endregion

        #region 供BS调用
        //客户端回调模式设置
        public string SwitchClientRunMode()
        {
            if (PlatformAddress.StartsWith("http://localhost"))
            {
                runMode = Common.RUNMODE_DEBUG;
            }
            else if (PlatformAddress.StartsWith("http://192.168"))
            {
                runMode = Common.RUNMODE_INTRANET;
            }
            else
            {
                runMode = Common.RUNMODE_INTERNET;
            }
            return runMode;
        }

        public void Redirect(string newUrlFragment)
        {
            if (newUrlFragment.StartsWith("http://"))
            {
                wbWorkArea.Url = new Uri(newUrlFragment);
            }
            else
            {
                wbWorkArea.Url = new Uri(PlatformAddress);
            }
        }

        public void SetAddresses(string baseIPCCOfCityInvokeUrl, string baseWeiXinOfServiceOnlineInovkeAddress, string addressOfProcessEmergency, string addressOfProcessFamilyCalls, string addressOfProcessCommonService, string addressOfProcessLife)
        {
            IPCCOfCityInvokeAddress = baseIPCCOfCityInvokeUrl;
            if (baseWeiXinOfServiceOnlineInovkeAddress.StartsWith("/"))
            {
                WeiXinOfServiceOnlineInovkeAddress = PlatformAddress + baseWeiXinOfServiceOnlineInovkeAddress;
            }
            else
            {
                WeiXinOfServiceOnlineInovkeAddress = baseWeiXinOfServiceOnlineInovkeAddress;
            }

            if (!Common.ADDRESS_DICT.ContainsKey(Common.ADDRESS_KEY_PROCESS_EMERGENCY))
            {
                Common.ADDRESS_DICT.Add(Common.ADDRESS_KEY_PROCESS_EMERGENCY, addressOfProcessEmergency);
            }
            if (!Common.ADDRESS_DICT.ContainsKey(Common.ADDRESS_KEY_PROCESS_FAMILYCALLS))
            {
                Common.ADDRESS_DICT.Add(Common.ADDRESS_KEY_PROCESS_FAMILYCALLS, addressOfProcessFamilyCalls);
            }
            if (!Common.ADDRESS_DICT.ContainsKey(Common.ADDRESS_KEY_PROCESS_COMMONSERVICE))
            {
                Common.ADDRESS_DICT.Add(Common.ADDRESS_KEY_PROCESS_COMMONSERVICE, addressOfProcessCommonService);
            }
            if (!Common.ADDRESS_DICT.ContainsKey(Common.ADDRESS_KEY_PROCESS_LIFE))
            {
                Common.ADDRESS_DICT.Add(Common.ADDRESS_KEY_PROCESS_LIFE, addressOfProcessLife);
            }


        }

        public void SetTransferQueues(string queueJSONStr)
        {
            Common.GroupsToTransfer.Clear();
            Common.GroupsToTransfer.AddRange(JsonConvert.DeserializeObject<IList<ListItem>>(queueJSONStr));
        }

        public void SetEnvironment(string userId, string userCode, string userName, string extId, string extCode, string dialBackFlag, string callCenterId, string callCenterIP, string callCenterPort, string callCenterIPInner, string callCenterPortInner, string callCenterIPProxy, string callCenterPortProxy, string areaId)
        {
            EnvironmentVar.UserId = Guid.Parse(userId);
            EnvironmentVar.UserCode = userCode;
            EnvironmentVar.UserName = userName;
            EnvironmentVar.ExtId = Guid.Parse(extId);
            EnvironmentVar.ExtCode = extCode;
            EnvironmentVar.DialBackFlag = dialBackFlag;
            EnvironmentVar.CallCenterId = Guid.Parse(callCenterId);
            EnvironmentVar.CallCenterIP = callCenterIP;
            EnvironmentVar.CallCenterPort = int.Parse(callCenterPort);
            EnvironmentVar.CallCenterIPInner = callCenterIPInner;
            EnvironmentVar.CallCenterPortInner = int.Parse(callCenterPortInner);
            EnvironmentVar.CallCenterIPProxy = callCenterIPProxy;
            EnvironmentVar.CallCenterPortProxy = int.Parse(callCenterPortProxy);
            EnvironmentVar.AreaId = areaId;

            lblUserName.Text = EnvironmentVar.UserName;
            lblExtNo.Text = EnvironmentVar.ExtCode;

            isSignInToBIZ = true;
            //检测当前分机状态
            //如果还处于签入中的则自动签出
            _CheckIn(true, (result0, response0) =>
            {
                Common.DebugLog("################### checkin:" + result0);
                if (result0 == "+OK")
                {
                    isSignInToIPCC = true;
                    BeginInvoke(new Action(() =>
                    {
                        btnCallHoldState.Enabled = true;
                        btnCallHoldState.BackColor = SystemColors.Control;
                        btnTransfer.Enabled = true;
                        btnTransfer.BackColor = SystemColors.Control;
                        btnSetBusyFree.Enabled = true;
                        btnSetBusyFree.BackColor = SystemColors.Control;
                        btnLogout.Enabled = true;
                        btnLogout.BackColor = SystemColors.Control;
                        cbbServiceList.Visible = true;
                        btnViewConf.Enabled = true;
                        btnViewConf.BackColor = SystemColors.Control;
                    }));
                    
                    //加载回拨列表
                    wbPhoneList4DialBack.Url = new Uri(PlatformAddress + CallDialBackListUrl);
                    wbPhoneListQueueMember.Url = new Uri(PlatformAddress + QueueMemberListUrl);
                    _MessageLoopForCTI_Call();

                    _AlarmForEmergentQueue();
                }
            });
            if (ServiceOnlineFlag)
            {
                _CheckInToWeiXinServiceOnline();
            }

            _HeatbeatExtNoOnline();
        }

        public void SetSeatFuncs(string funcJSONStr)
        {

            //初始化主列表
            cbbServiceList.Items.Clear();
            cbbServiceList.Items.AddRange(JsonConvert.DeserializeObject<IList<BindingItem>>(funcJSONStr).ToArray());

            cbbServiceList.DisplayMember = "Text";
            cbbServiceList.ValueMember = "Value";
        }

        public void SetLogout()
        {

            if (CTI_Call_CircleThread != null)
            {
                if (CTI_Call_CircleThread.IsAlive)
                {
                    CTI_Call_CircleThread.Abort();
                }
                CTI_Call_CircleThread = null;
            }

            if (CTI_Conference_CircleThread != null)
            {
                if (CTI_Conference_CircleThread.IsAlive)
                {
                    CTI_Conference_CircleThread.Abort();
                }
                CTI_Conference_CircleThread = null;
            }

            if (CTI_DelayPopup_CircleThread != null)
            {
                if (CTI_DelayPopup_CircleThread.IsAlive)
                {
                    CTI_DelayPopup_CircleThread.Abort();
                }
                CTI_DelayPopup_CircleThread = null;
            }

            if (Emergent_Queue_CircleThread != null) {
                if (Emergent_Queue_CircleThread.IsAlive) {
                    Emergent_Queue_CircleThread.Abort();
                }
                Emergent_Queue_CircleThread = null;
            }

            _CheckOut(false, (result1, response1) =>
            {
                if (result1 == "+OK")
                {
                    isSignInToIPCC = false;
                    BeginInvoke(new Action(() =>
                    {
                        btnCallHoldState.Enabled = false;
                        btnCallHoldState.BackColor = SystemColors.ControlDarkDark;
                        btnTransfer.Enabled = false;
                        btnTransfer.BackColor = SystemColors.ControlDarkDark;
                        btnSetBusyFree.Enabled = false;
                        btnSetBusyFree.BackColor = SystemColors.ControlDarkDark;
                        btnSetBusyFree.Text = "服务示忙";
                        btnViewConf.Enabled = false;
                        btnViewConf.BackColor = SystemColors.ControlDarkDark;
                        btnLogout.Enabled = false;
                        btnLogout.BackColor = SystemColors.ControlDarkDark;
                        cbbServiceList.Visible = false;
                        wbPhoneListQueueMember.Url = new Uri("about:blank");
                        wbPhoneList4DialBack.Url = new Uri("about:blank");
                    }));
                }
            });

            if (isSignInToWeiXinServiceOnline)
            {
                _CheckOutToWeiXinServiceOnline();
            }
            if (heartbeat_Thread != null)
            {
                if (heartbeat_Thread.IsAlive)
                {
                    heartbeat_Thread.Abort();
                }
                heartbeat_Thread = null;
            }
            pbHeartbeat.BackgroundImage = Properties.Resources.Red;

            isEmergentQueue = false;
            lblUserName.Text = "--";
            lblExtNo.Text = "--";
            isTalking = false;
            isSignInToBIZ = false;
            EnvironmentVar.Clear();
        }

        public void OpenNewWinAsProcessing(string urlFragment, string callServiceJsonStr)
        {
            if (winCTI != null)
            {
                MessageBoxAdapter.ShowInfo("已打开处理窗口，请先处理完成或关闭");
                winCTI.Activate();
                return;
            }
            CallService callService = JsonConvert.DeserializeObject<CallService>(callServiceJsonStr);
            _OpenNewWinAsProcessing(urlFragment, callService);
        }

        public void OpenNewWinAsInfo(string urlFragment, string callServiceJsonStr)
        {
            if (winInfo != null)
            {
                MessageBoxAdapter.ShowInfo("已打开处理窗口，请先处理完成或关闭");
                winInfo.Activate();
                return;
            }
            CallService callService = JsonConvert.DeserializeObject<CallService>(callServiceJsonStr);
            _OpenNewWinAsInfo(urlFragment, callService);
        }

        public void DialBack(string callServiceId, string oldManId, string phoneNo, string oldManName, string serviceType, string batchCallServiceIds)
        {

            if (EnvironmentVar.DialBackFlag == "1")
            {
                MessageBoxAdapter.ShowInfo("请用手机或者其余的座机进行回拨");
                return;
            }

            //if (!isSignInToIPCC)
            //{
            //    MessageBoxAdapter.ShowInfo("请确保已登录到IPCC或座席处于空闲状态！");
            //    return;
            //}

            if (isTalking)
            {
                MessageBoxAdapter.ShowInfo("您已处在通话中！");
                return;
            }

            frmCallsDialog calldialog = new frmCallsDialog();
            WebBrowser wb = wbPhoneList4DialBack;
            object oRets = wb.Document.InvokeScript("getServiceObjectPhoneNos", new object[] { callServiceId });
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
                    return;
                }
            }
            if (MessageBoxAdapter.ShowConfirm("确定要呼叫【" + phoneNo + "】？") == System.Windows.Forms.DialogResult.OK)
            {
                object oRet = wb.Document.InvokeScript("dialBackForCallService", new object[] { callServiceId, EnvironmentVar.ExtCode, batchCallServiceIds });
                if (oRet != null && oRet.ToString() != "{}")
                {
                    //开始回拨

                    _CallServiceId = callServiceId.ToGuid();
                    //判定
                    _DialBack();

                    _MakeCall(phoneNo);
                    wb.Document.InvokeScript("OpenCallServiceInfoWinForSeat", new object[] { callServiceId });
                    wb.Document.InvokeScript("removeDialed", new object[] { callServiceId });
                }
                else
                {
                    MessageBoxAdapter.ShowDebug("无效的回拨");
                }
            }

        }

        public void PlaybackAlarmTone()
        {

            if (PlayToneFlag && !string.IsNullOrEmpty(Common.DEFAULT_SOUND_NAME_OF_CALL_IN))
            {
                DateTime startTime = DateTime.Parse(StartPlayToneTime);
                DateTime endTime = DateTime.Parse(EndPlayToneTime);
                if (startTime < endTime && DateTime.Now >= startTime && DateTime.Now <= endTime)
                {
                    System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer();
                    soundPlayer.SoundLocation = Common.DEFAULT_SOUND_NAME_OF_CALL_IN;
                    soundPlayer.Load();
                    soundPlayer.Play();
                }
            }
        }

        public void setEmergentQueueFlag(string iflag) {
            if (string.IsNullOrEmpty(iflag))
            {
                isEmergentQueue = false;
            }
            else {
                isEmergentQueue = true;
            }
        }

        //展示合并后的回拨信息
        public void OpenDailBackInfo(string pageData)
        {
            frmDialBack winDailBack = new frmDialBack();
            winDailBack.LoadUrl(PlatformAddress + DetailedDialBackListUrl);
            winDailBack.TheMotherWin = this;
            //winDailBack._CallServiceId = callServiceId;
            winDailBack._pageData = pageData;
            winDailBack.CTIWinClose += new dCTIWinClose((o, e1) =>
            {
                //关闭窗体  
                winDailBack = null;
            });
            winDailBack.Show();
        }
        #endregion

        #region 调用
        public string GetCallCenterIP()
        {
            object o = wbWorkArea.Document.InvokeScript("getCallCenterIP");
            return e0571.web.core.Utils.TypeConverter.ChangeString(o);
        }
        #endregion

        #region 窗体事件处理
        private void frmMainV2X_Shown(object sender, EventArgs e)
        {
            if (PlatformAddress == "")
            {
                frmSettings frm = new frmSettings();
                frm.TheMotherWin = this;
                frm.PlatformReady += new dPlatformReady((o, re) =>
                {
                    PlatformAddress = re.PlatformAddress;
                    INIAdapter.WriteValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLATFORM_ADDRESS, PlatformAddress, Common.INI_FILE_PATH);

                });
                frm.SettingsSave += new dSettingsSave((o, se) =>
                {
                    ServiceOnlineFlag = se.ServiceOnline;
                    INIAdapter.WriteValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_SERVICE_ONLINE, ServiceOnlineFlag ? "1" : "0", Common.INI_FILE_PATH);
                });
                frm.PlayBackTone += new dPlayBackTone((o, te) =>
                {
                    PlayToneFlag = te.PlayTone;
                    StartPlayToneTime = te.StartPlayTime;
                    EndPlayToneTime = te.EndPlayTime;
                    INIAdapter.WriteValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE, PlayToneFlag ? "1" : "0", Common.INI_FILE_PATH);
                    INIAdapter.WriteValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE_STIME, StartPlayToneTime, Common.INI_FILE_PATH);
                    INIAdapter.WriteValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE_ETIME, EndPlayToneTime, Common.INI_FILE_PATH);
                });
                frm.ShowDialog();
            }

            if (PlatformAddress != "")
            {
                wbWorkArea.Dock = DockStyle.Fill;
                wbWorkArea.Url = new Uri(PlatformAddress);
            }

            pnlServiceOnline.Visible = ServiceOnlineFlag;
        }

        private void frmMainV2X_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBoxAdapter.ShowConfirm("您确定要退出吗", Properties.Settings.Default.MessageBoxTitle) == DialogResult.OK)
            {
                if (isSignInToWeiXinServiceOnline)
                {
                    _CheckOutToWeiXinServiceOnline();

                }
                if (isSignInToIPCC)
                {

                    //执行BS上的清空动作
                    wbWorkArea.Document.InvokeScript("beforeLogout");
                    SetLogout();
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void frmMainV2X_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CTI_Call_CircleThread != null)
            {
                if (CTI_Call_CircleThread.IsAlive)
                {
                    CTI_Call_CircleThread.Abort();
                }
                CTI_Call_CircleThread = null;
            }

            if (CTI_Conference_CircleThread != null)
            {
                if (CTI_Conference_CircleThread.IsAlive)
                {
                    CTI_Conference_CircleThread.Abort();
                }
                CTI_Conference_CircleThread = null;
            }

            if (CTI_DelayPopup_CircleThread != null)
            {
                if (CTI_DelayPopup_CircleThread.IsAlive)
                {
                    CTI_DelayPopup_CircleThread.Abort();
                }
                CTI_DelayPopup_CircleThread = null;
            }

            if (Emergent_Queue_CircleThread != null) {
                if (Emergent_Queue_CircleThread.IsAlive) {
                    Emergent_Queue_CircleThread.Abort();
                }
                Emergent_Queue_CircleThread = null;
            }

            if (heartbeat_Thread != null)
            {
                if (heartbeat_Thread.IsAlive)
                {
                    heartbeat_Thread.Abort();
                }
                heartbeat_Thread = null;
            }
        }
        #endregion

        #region 按钮事件处理
        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.TheMotherWin = this;
            frm.PlatformReady += new dPlatformReady(delegate(object o, PlatformReadyEventArgs re)
            {
                PlatformAddress = re.PlatformAddress;
                INIAdapter.WriteValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLATFORM_ADDRESS, PlatformAddress, Common.INI_FILE_PATH);
            });
            frm.SettingsSave += new dSettingsSave((o, se) =>
            {
                ServiceOnlineFlag = se.ServiceOnline;
                INIAdapter.WriteValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_SERVICE_ONLINE, ServiceOnlineFlag ? "1" : "0", Common.INI_FILE_PATH);
            });
            frm.PlayBackTone += new dPlayBackTone((o, te) =>
            {
                PlayToneFlag = te.PlayTone;
                StartPlayToneTime = te.StartPlayTime;
                EndPlayToneTime = te.EndPlayTime;
                INIAdapter.WriteValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE, PlayToneFlag ? "1" : "0", Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE_STIME, StartPlayToneTime, Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE_ETIME, EndPlayToneTime, Common.INI_FILE_PATH);
            });
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (MessageBoxAdapter.ShowConfirm("您必须重新启动程序设置才能生效，是否重启程序？", Properties.Settings.Default.MessageBoxTitle) == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Restart();
                }
            }

        }

        private void btnCallHoldState_Click(object sender, EventArgs e)
        {
            if (isTalking)
            {
                if (xClock4Timer.IsTimerEnable && xClock4Timer.TotalTicks >= 5)
                {
                    _ChangePhoneHoldState();
                }
                else
                {
                    MessageBoxAdapter.ShowDebug("请确保已接起电话已经5秒");
                }
            }
            else
            {
                string strmessage = "请确保正在通话中...";
                if (winCTI != null)
                {
                    winCTI._OnMessageBoxShowInfo(strmessage);
                }
                else if (winInfo != null)
                {
                    winInfo._OnMessageBoxShowInfo(strmessage);
                }
                else
                {
                    MessageBox.Show(this, strmessage);
                }
                _ResetPhoneHoldState();
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (isTalking)
            {
                if (xClock4Timer.IsTimerEnable && xClock4Timer.TotalTicks >= 5)
                {
                    _TransferPhone();
                }
                else
                {
                    MessageBoxAdapter.ShowInfo("请确保已接起电话已经5秒");
                }
            }
        }

        private void btnViewConf_Click(object sender, EventArgs e)
        {
            winConference = new frmConference();
            winConference.LoadUrl(PlatformAddress + string.Format(ExtOfConferenceUrl, EnvironmentVar.ExtCode));
            winConference.TheMotherWin = this;
            winConference.CTIWinClose += new dCTIWinClose((o, e1) =>
            {
                //关闭窗体  
                winConference = null;
            });
            winConference.Show();
        }

        private void btnSetBusyFree_Click(object sender, EventArgs e)
        {
            if (isProcessing)
            {
                MessageBoxAdapter.ShowInfo("已打开处理窗口，请先处理完成或关闭");
                if (winInfo != null)
                {
                    winInfo.Activate();
                }
                if (winCTI != null)
                {
                    winCTI.Activate();
                }
                return;
            }

            _SetSeatBusyFree();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBoxAdapter.ShowConfirm("您确定要退出登录吗？", Properties.Settings.Default.MessageBoxTitle) == DialogResult.OK)
            {
                wbWorkArea.Document.InvokeScript("LogoutForSeat");
            }
        }

        private void btnServiceOnline_Click(object sender, EventArgs e)
        {
            _PopupServiceOnlineWin();
        }

        #endregion

        #region 其他事件处理
        private void wbWorkArea_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            wbWorkArea.Document.InvokeScript("setExecModeForSeat");
        }

        private void wbPhoneListQueueMember_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Common.DebugLog("wbPhoneListQueueMember url:" + e.Url.ToString());
            wbPhoneListQueueMember.Document.InvokeScript("setExecModeForSeat");
        }

        private void wbPhoneList4DialBack_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Common.DebugLog("wbPhoneList4DialBack url:" + e.Url.ToString());
            wbPhoneList4DialBack.Document.InvokeScript("setExecModeForSeat");
        }

        private void cbbServiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            object o = cbbServiceList.Items[cbbServiceList.SelectedIndex];
            if (o != null)
            {
                wbWorkArea.Document.InvokeScript("switchFunc", new object[] { (o as BindingItem).Value });
            }
        }

        private void pbWeiXinClientCount_Click(object sender, EventArgs e)
        {
            _PopupServiceOnlineWin();
        }
        #endregion


        #region 窗体边框重绘方法
        #region 报警 监控呼叫中心排队呼入是否有紧急呼入
        System.Timers.Timer _tickTimer;
        private void _AlarmForEmergentQueue()
        {
            //string frmName = this.Text;
            Emergent_Queue_CircleThread = ThreadAdapter.DoCircleTask(() =>
            {
                int ibeat = 0;
                _tickTimer = new System.Timers.Timer(1 * 1000);
                _tickTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs ee)
                {
                    BeginInvoke(new Action(() =>
                    {
                        FlashWindow(this.Handle, true);
                        ReDrawFormBorder(this.Handle, new SolidBrush(Color.Red));
                        System.Threading.Thread.Sleep(500);
                        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    }));
                    if (ibeat == 3)
                    {
                        _tickTimer.Enabled = false;
                        _tickTimer = null;
                        ibeat = 0;
                    }
                    ibeat++;
                });//到达时间的时候执行事件； 
                _tickTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
                _tickTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            },
            1000,
            () =>
            {
                //暂时设置一直执行
                return false;
            },
            () => {
                //正在执行的话跳过,并且允许执行
                return !(_tickTimer == null && isEmergentQueue);
            });
        }
        #endregion


        /// <summary>
        /// 重绘窗体边框
        /// </summary>
        /// <param name="brush">画笔</param>
        private void ReDrawFormBorder(IntPtr hWnd, Brush brush)
        {
            //CaptionRect 标题区，Rect窗体区，ClientRect工作区
            Rectangle CaptionRect, Rect, ClientRect;
            //窗体句柄
            //IntPtr hWnd = FindWindow(null, frmName);

            //FixedSingle模式下 窗口的边框粗细
            Size BorderSize = SystemInformation.FixedFrameBorderSize;
            //标准标题栏区域的高度
            int CaptionHeight = SystemInformation.CaptionHeight;
            //获取窗体的真实区域
            _Rect areatRect = new _Rect();
            GetWindowRect(hWnd, ref areatRect);
            //窗体的宽高
            int width = areatRect.Right - areatRect.Left;
            int height = areatRect.Bottom - areatRect.Top;
            //平移
            Point xy = new Point(areatRect.Left, areatRect.Top);
            xy.Offset(-areatRect.Left, -areatRect.Top);

            //获取区域
            CaptionRect = new Rectangle(xy.X, xy.Y + BorderSize.Height, width, CaptionHeight);
            Rect = new Rectangle(xy.X, xy.Y, width, height);
            ClientRect = new Rectangle(xy.X + BorderSize.Width, xy.Y + CaptionHeight + BorderSize.Height, width - BorderSize.Width * 2, height - CaptionHeight - BorderSize.Height * 2);

            //上边框
            Rectangle borderTop = new Rectangle(Rect.Left, Rect.Top, Rect.Left + Rect.Width, Rect.Top + BorderSize.Height);
            //左边框
            Rectangle borderLeft = new Rectangle(new Point(Rect.Location.X, Rect.Location.Y + BorderSize.Height),
                    new Size(BorderSize.Width, ClientRect.Height + CaptionHeight + BorderSize.Height));
            //右边框
            Rectangle borderRight = new Rectangle(Rect.Left + Rect.Width - BorderSize.Width, Rect.Top + BorderSize.Height,
                    BorderSize.Width, ClientRect.Height + CaptionHeight + BorderSize.Height);
            //底边框
            Rectangle borderBottom = new Rectangle(Rect.Left + BorderSize.Width, Rect.Top + Rect.Height - BorderSize.Height,
                      Rect.Width - BorderSize.Width * 2, Rect.Height);

            //获取整个窗口（包括边框、滚动条、标题栏、菜单等）的设备场景
            IntPtr dc = GetWindowDC(hWnd);
            //创建新的 Graphics
            Graphics g = Graphics.FromHdc(dc);
            //填充画笔颜色以及矩形 SolidBrush(Color.Red)
            g.FillRectangle(brush, borderTop);
            g.FillRectangle(brush, borderLeft);
            g.FillRectangle(brush, borderRight);
            g.FillRectangle(brush, borderBottom);
            //释放
            g.Dispose();
            //释放设备场景
            ReleaseDC(hWnd, dc);
        }
        #endregion
    }

    //结构区域
    public struct _Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
