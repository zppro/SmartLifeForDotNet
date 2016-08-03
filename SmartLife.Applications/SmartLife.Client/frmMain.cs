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
using CallCenterAPI.TC_IPCC;
using Newtonsoft.Json;
using SmartLife.Share.Models.PO;
using win.core.utils;
using System.IO;
using System.Reflection;

namespace SmartLife.Client
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        /*
         public string cmsHostName = "http://localhost";
         public string virPathName = "";
         public const string loginUrl = "/SmartLife.CertManage.ManageCMS/login.htm";
         public const string indexUrl = "/SmartLife.CertManage.ManageCMS/index.htm";
         public const string AlertUrl = "/SmartLife.CertManage.ManageCMS/views/shared/tabContainer.htm?pageUrl=views/old-care/emergency-aid.htm&menuCode=B180101&oldManId=5B06502F-956F-469E-9AD0-01B1EAB03293";//OnEstablished代替OnAlert
         public const string ProcessEmergencyUrl = "/SmartLife.CertManage.ManageCMS/views/shared/tabContainer.htm?pageUrl=views/old-care/emergency-aid.htm&menuCode=B180101";
         */

        System.Timers.Timer _tickTimer = null;
        System.Timers.Timer _pingTimer = null;//心跳发起
        System.Timers.Timer _pingCheckTimer = null;//心跳检测
        byte pingpongValue = 0;

        public string cmsRunMode = "";
        public string cmsHostName = "";
        public string virPathName = "";
        public string callCenterNodeAddress = "";
        //public const string loginUrl = "/login.htm";
        public const string indexUrl = "/index.htm";
        public const string AlertUrl = "/views/shared/tabContainer.htm?pageUrl=views/old-care/emergency-aid.htm&menuCode=B180101&oldManId=5B06502F-956F-469E-9AD0-01B1EAB03293";//OnEstablished代替OnAlert
        public const string ProcessEmergencyUrl = "/views/shared/tabContainer.htm?pageUrl=views/old-care/emergency-aid.htm&menuCode=B180101";
        public const string ProcessFamilyCallsUrl = "/views/shared/tabContainer.htm?pageUrl=views/old-care/family-calls.htm&menuCode=B180201";
        public const string ProcessInfotainmentUrl = "/views/shared/tabContainer.htm?pageUrl=views/old-care/infotainment-aid.htm&menuCode=B180301";
        public const string ProcessLifeUrl = "/views/shared/tabContainer.htm?pageUrl=views/old-care/life-aid.htm&menuCode=B180498";
        public const string ReminderEmergencyUrl = "/views/shared/tabContainer.htm?pageUrl=views/widgets/old-care-scroll-reminder.htm&serviceType=0";
        public const string ReminderLifeUrl = "/views/shared/tabContainer.htm?pageUrl=views/widgets/old-care-scroll-reminder.htm&serviceType=3";
        public const string ReminderFamilyUrl = "/views/shared/tabContainer.htm?pageUrl=views/widgets/old-care-scroll-reminder.htm&serviceType=1";

        bool isSignIn = false;
        frmCallService theWinOfCallServic;
        frmDialDialog theWinOfDialDialog;
        

        private void frmMain_Load(object sender, EventArgs e)
        {
             
            this.Top = 0;
            this.Left = 0;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 5;
            this.MaximumSize = new Size(Width, Height);
            Text = Properties.Settings.Default.ProductName + " " + Assembly.GetEntryAssembly().GetName().Version.ToString();
            wbEmergency.ObjectForScripting = this;
            wbLife.ObjectForScripting = this;
            wbFamily.ObjectForScripting = this;
            webBrowser.ObjectForScripting = this;
            tlblSpace.Width = Width - tlblProcess.Width - tlblUserCodeName.Width - 20;
            tssbtnSetBusyFree.Visible = false;

            pnlReminder.Top = pnlMain.Height - pnlReminder.Height - statusStrip1.Height;
            pnlReminder.Left = 0;

            /*
            webBrowser.Url = new Uri(cmsHostName + loginUrl);
            */

            cmsRunMode = INIAdapter.ReadValue(Common.INI_SECTION_SMARTCARE, Common.INI_KEY_RUNMODE, Common.INI_FILE_PATH);
            cmsHostName = INIAdapter.ReadValue(Common.INI_SECTION_SMARTCARE, Common.GetValueByFiledsName("INI_KEY_CMSHOST_" + cmsRunMode), Common.INI_FILE_PATH);
            virPathName = INIAdapter.ReadValue(Common.INI_SECTION_SMARTCARE, Common.GetValueByFiledsName("INI_KEY_VIRPATH_" + cmsRunMode), Common.INI_FILE_PATH);
            callCenterNodeAddress = INIAdapter.ReadValue(Common.INI_SECTION_CALLCENTER, Common.INI_KEY_CALLCENTER_NODE_ADDRESS, Common.INI_FILE_PATH);
            if (cmsHostName != "" && cmsRunMode != "")
            {
                //webBrowser.Url = new Uri(cmsHostName + virPathName);
            }
            else
            {
                string path = Path.Combine(Environment.CurrentDirectory, "htm/main.htm");
                if (File.Exists(path))
                {
                    webBrowser.Url = new Uri("file://" + path);
                }
            } 
            //toolStrip1.Visible = false; 
            //
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSignIn)
            {
                if (MessageBoxAdapter.ShowConfirm("您确定要退出吗", Properties.Settings.Default.MessageBoxTitle) == DialogResult.OK)
                {
                    //执行BS上的清空动作
                    webBrowser.Document.InvokeScript("beforeLogout");
                    SetLogout();
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (cmsRunMode == "" || cmsHostName == "" || callCenterNodeAddress == "")
            {
                frmSettings frm = new frmSettings();
                frm.RemoteHostConnect += new dRemoteHostConnect(delegate(object o, RemoteHostConnectEventArgs re)
                {
                    cmsRunMode = re.RunMode;
                    cmsHostName = re.RemoteHost;
                    virPathName = re.VirPath;
                    INIAdapter.WriteValue(Common.INI_SECTION_SMARTCARE, Common.INI_KEY_RUNMODE, re.RunMode, Common.INI_FILE_PATH);
                    INIAdapter.WriteValue(Common.INI_SECTION_SMARTCARE, Common.GetValueByFiledsName("INI_KEY_CMSHOST_" + re.RunMode), re.RemoteHost, Common.INI_FILE_PATH);
                    INIAdapter.WriteValue(Common.INI_SECTION_SMARTCARE, Common.GetValueByFiledsName("INI_KEY_VIRPATH_" + re.RunMode), re.VirPath, Common.INI_FILE_PATH);

                });
                frm.CallCenterSettingsSave += new dCallCenterSettingsSave(delegate(object o, CallCenterSettingsSaveEventArgs ce)
                {
                    callCenterNodeAddress = ce.CallCenterNodeAddress;
                    INIAdapter.WriteValue(Common.INI_SECTION_CALLCENTER, Common.INI_KEY_CALLCENTER_NODE_ADDRESS, ce.CallCenterNodeAddress, Common.INI_FILE_PATH);
                });
                frm.ShowDialog();
            }

            if (cmsHostName != "")
            {
                webBrowser.Url = new Uri(cmsHostName + virPathName);
            }
        }

        private void tbtnSettings_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.RemoteHostConnect += new dRemoteHostConnect(delegate(object o, RemoteHostConnectEventArgs re)
            {
                INIAdapter.WriteValue(Common.INI_SECTION_SMARTCARE, Common.INI_KEY_RUNMODE, re.RunMode, Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_SMARTCARE, Common.GetValueByFiledsName("INI_KEY_CMSHOST_" + re.RunMode), re.RemoteHost, Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_SMARTCARE, Common.GetValueByFiledsName("INI_KEY_VIRPATH_" + re.RunMode), re.VirPath, Common.INI_FILE_PATH);
            });
            frm.CallCenterSettingsSave += new dCallCenterSettingsSave(delegate(object o, CallCenterSettingsSaveEventArgs ce)
            {
                callCenterNodeAddress = ce.CallCenterNodeAddress;
                INIAdapter.WriteValue(Common.INI_SECTION_CALLCENTER, Common.INI_KEY_CALLCENTER_NODE_ADDRESS, ce.CallCenterNodeAddress, Common.INI_FILE_PATH);
            });
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (MessageBoxAdapter.ShowConfirm("您必须重新启动程序设置才能生效，是否重启程序？", Properties.Settings.Default.MessageBoxTitle) == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Restart();
                }
            }
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser.Document.InvokeScript("setExecModeToCS");
            if (e.Url.ToString().StartsWith(cmsHostName + virPathName + indexUrl))
            {
            }
            else
            {
                //tssbtnSetBusyFree.Visible = false;
                
            }
        }


        #region 供BS调用
        public void Redirect(string newUrlFragment)
        {
            if (newUrlFragment.StartsWith("http://"))
            {
                webBrowser.Url = new Uri(newUrlFragment);
            }
            else
            {
                webBrowser.Url = new Uri(cmsHostName + newUrlFragment);
            }
        }
        public void SetExt(string extId, string extCode, string iPCCDial, string iPCCRelayType, string IPCCRelayPrefix)
        {
            EnvironmentVar.ExtId = Guid.Parse(extId);
            EnvironmentVar.ExtCode = extCode;
            EnvironmentVar.IPCCDial = iPCCDial;
            EnvironmentVar.IPCCRelayType = iPCCRelayType;
            EnvironmentVar.IPCCRelayPrefix = IPCCRelayPrefix;
            if (EnvironmentVar.IPCCRelayType == null)
            {
                EnvironmentVar.IPCCRelayType = EnvironmentVar.IPCCRelayType_DEFAULT;
            }

            tlblUserCodeName.Text = string.Format("当前登录：({0}){1},分机：{2}", EnvironmentVar.UserCode, EnvironmentVar.UserName, EnvironmentVar.ExtCode);
            
           
            if (callCenterNodeAddress != "")
            {
                tlblProcess.BackColor = SystemColors.ActiveCaption;

                //TC_IPCC_Init();//是坐席则打开初始化IPCC

                AsyncAdapter.BeginInvoke(() =>
                {
                    tlblProcess.Text = "正在连接呼叫中心";
                    _tickTimer = new System.Timers.Timer(1 * 1000);
                    _tickTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs ee)
                    {
                        
                        BeginInvoke(new Action(() =>
                        {
                            if (tlblProcess.Text.IndexOf(".") == -1)
                            {
                                tlblProcess.Text += ".";
                            }
                            else if (tlblProcess.Text.IndexOf(".") + 5 == tlblProcess.Text.Length)
                            {
                                tlblProcess.Text = tlblProcess.Text.Substring(0, tlblProcess.Text.Length - 5);
                            }
                            else
                            {
                                tlblProcess.Text += ".";
                            }
                        }));

                    });//到达时间的时候执行事件； 
                    _tickTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
                    _tickTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；  

                    TC_IPCC_Init();//是坐席则打开初始化IPCC

                    

                }, () =>
                {

                    _tickTimer.Enabled = false;
                    _tickTimer = null;
                    //MessageBoxAdapter.ShowDebug(_MonitorId.ToString());

                    IPCCVar.conferenceMembers = new List<string>();
                    IPCCVar.telInfo = new Dictionary<string, string>();
                });
                
            }
        }
        public void SetSeat(string userId, string userCode, string userName)
        {
            isSignIn = true;

            EnvironmentVar.UserId = Guid.Parse(userId);
            EnvironmentVar.UserCode = userCode;
            EnvironmentVar.UserName = userName;
            
            tlblUserCodeName.Text = string.Format("当前登录：({0}){1}", EnvironmentVar.UserCode, EnvironmentVar.UserName);

            
        }
        public void ArrageStatusBar()
        {
            if (EnvironmentVar.ExtCode != null)
            {
                tssbtnSetBusyFree.Visible = true;
                tlblSpace.Width = Width - tlblProcess.Width - tlblUserCodeName.Width - -tssbtnSetBusyFree.Width - 180;
            }
            else
            {
                tssbtnSetBusyFree.Visible = false;
                tlblSpace.Width = Width - tlblProcess.Width - tlblUserCodeName.Width - 20;
            } 
        }
        public void SetCallCenterId(string callCenterId)
        {
            EnvironmentVar.CallCenterId = Guid.Parse(callCenterId);
        }
        public void SetDialBackFlag(string dialBackFlag)
        {
            EnvironmentVar.DialBackFlag = dialBackFlag;
        }
        public void SetAreaId(string areaId)
        {
            EnvironmentVar.AreaId = areaId;
        }  
        public void SetLogout()
        {
            if (EnvironmentVar.ExtId != null)
            {
                TC_IPCC_UnInit();
                
            }

            if (_tickTimer != null)
            {
                _tickTimer.Stop();
                _tickTimer.Enabled = false;
                _tickTimer = null;
            }
            if (_pingTimer != null)
            {
                _pingTimer.Stop();
                _pingTimer.Enabled = false;
                _pingTimer = null;
            }
            if (_pingCheckTimer != null)
            {
                _pingCheckTimer.Stop();
                _pingCheckTimer.Enabled = false;
                _pingCheckTimer = null;
            }
            tlblProcess.Text = "";
            tlblProcess.BackColor = SystemColors.Control;
            tlblUserCodeName.Text = "";

            IPCCVar.Clear();
            EnvironmentVar.Clear();
            isSignIn = false; 
        }

        //客户端回调模式设置
        public string SwitchClientRunMode()
        {
            if (!string.IsNullOrEmpty(cmsRunMode))
            {
                return cmsRunMode;
            }
            return "";
        }

        //old-care-scroll-reminder.htm
        public void DialBack(string callServiceId, string oldManId, string callNo, string oldManName,string dialType)
        {
            //获取呼叫号码
            frmCallsDialog calldialog = new frmCallsDialog();
            object oRets = webBrowser.Document.InvokeScript("getDialBackCallNos", new object[] { oldManId });
            if (oRets != null && oRets.ToString() != "{}")
            {
                Dictionary<string, string> callNoDic = new Dictionary<string, string>();
                dynamic result = JsonConvert.DeserializeObject(oRets.ToString());
                foreach (var row in result)
                {
                    if (((string)row.Name).IndexOf("CallNo") > -1 && !string.IsNullOrEmpty((string)row.Value))
                    {
                        callNoDic.Add((string)row.Name, (string)row.Value);
                    }
                }
                calldialog.CallNoDic = callNoDic;
                calldialog.ChooseCallNo = callNo;
            }
            if (calldialog.CallNoDic.Count > 1)
            {
                calldialog.ShowDialog();
                if (calldialog.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    callNo = calldialog.ChooseCallNo;
                }
                else
                {
                    return;
                }
            }

            if (EnvironmentVar.DialBackFlag == "1") {
                MessageBoxAdapter.ShowInfo("请用手机或者其余的座机进行回拨");
                return;
            }

            if (MessageBoxAdapter.ShowConfirm("确定要呼叫【" + oldManName + "】？", Properties.Settings.Default.Company) == System.Windows.Forms.DialogResult.OK)
            {
                object oRet = webBrowser.Document.InvokeScript("takeOverCallServiceOfCallBack", new object[] { callServiceId, EnvironmentVar.ExtCode });
                if (oRet != null && oRet.ToString() != "{}")
                {
                    //开始回拨
                    string backCallNo = "";
                    if (EnvironmentVar.IPCCRelayPrefix != null && EnvironmentVar.IPCCRelayPrefix.Trim() != "")
                    {
                        backCallNo = EnvironmentVar.IPCCRelayPrefix.Trim() + callNo;
                    }
                    else {
                        backCallNo = callNo;
                    }

                    MgrResult ret = MgrAPIWrapper.Manager_MakeCall(EnvironmentVar.ExtCode, backCallNo, EnvironmentVar.IPCCDial, callNo);
                    if (ret == MgrResult.ok)
                    {
                        frmDialDialog dialog = new frmDialDialog();
                        dialog.DialText = "呼叫【" + oldManName + "】";
                        dialog.CallServiceId = Guid.Parse(callServiceId);
                        dialog.DialType = dialType;
                        theWinOfDialDialog = dialog;
                        CallService callService=JsonConvert.DeserializeObject<CallService>(oRet.ToString());
                        if (callService.CallServiceId == dialog.CallServiceId)
                        {
                            theWinOfDialDialog.CurrentCallService = callService;
                        }
                    }
                    else
                    {
                        MessageBoxAdapter.ShowDebug(ret.ToString());
                    }
                }
                else
                {
                    MessageBoxAdapter.ShowDebug("无效的回拨");
                }
                
            }
        }

        public void OpenNewWinAsCallServiceFromIndex(string urlFragment, string callServiceId)
        {
            //通过callServiceId打开弹屏处理
            object oRet = webBrowser.Document.InvokeScript("getCallService2", new object[] { callServiceId });
            if (oRet != null && oRet.ToString() != "{}")
            {
                _DispatchCallService("index", oRet.ToString(), "坐席工号<" + EnvironmentVar.UserCode + ">重新开始处理服务");
            } 
        }
        public void OpenNewWinAsWorkOrderFromIndex(string urlFragment, string workOrderId)
        {
            frmWorkOrder frm = new frmWorkOrder();
            frm.PageData = new WorkOrder { WorkOrderId = Guid.Parse(workOrderId) };
            frm.Source = "index";
            frm.CloseWinWorkOrder += new dCloseWinWorkOrder(() => {
                webBrowser.Document.InvokeScript("refreshWorkOrderFromIndex");
            });
            frm.LoadUrl(cmsHostName + virPathName + urlFragment);
            frm.Show();
        }
        private void OpenNewWinAsCallServiceFromClient(string caller, string callee, string queue)
        {
            Common.ipccLogger.Info(string.Format("OpenNewWinAsCallServiceFromClient-----caller:{0}|callee:{1}|queue:{2}", caller, callee, queue));
            //通过caller,callee,queue打开弹屏处理
            //查找到CallService记录 
            string jsonStr = JsonConvert.SerializeObject(new { Caller = caller, Callee = callee, Queue = queue });
            object oRet = webBrowser.Document.InvokeScript("getCallService", new object[] { jsonStr });
            if (oRet != null && oRet.ToString() != "{}")
            {
                _DispatchCallService("client",oRet.ToString(), "坐席工号<" + EnvironmentVar.UserCode + ">开始为您服务");
                 
            }
        }
        #endregion 

        #region 调用BS接口 
        private void onAddServiceVoice(string strCallServiceId, int voiceType, string address, string logContent)
        {
            object oRet1 = webBrowser.Document.InvokeScript("addServiceVoice", new object[] { strCallServiceId, voiceType, address, logContent });
            if (oRet1 != null && oRet1.ToString() == bool.TrueString)
            {
                //成功创建二人语音
            }
            else
            {
                MessageBoxAdapter.ShowDebug("错误");
            }
        }
        private void onFinishServiceVoice(string strCallServiceId, int voiceType, string ipAddress, string logContent)
        {
            //语音会话结束
            object oRet1 = webBrowser.Document.InvokeScript("finishServiceVoice", new object[] { strCallServiceId, voiceType, ipAddress, logContent });
            if (oRet1 != null && oRet1.ToString() == bool.TrueString)
            {
                //成功创建二人语音
            }
            else
            {
                MessageBoxAdapter.ShowDebug("错误");
            }
        }
        private void OnConfJoin(string telInfo)
        {

            webBrowser.Document.InvokeScript("telConnected", new object[] { IPCCVar.currentCallServiceId.ToString(), telInfo, 1 });
            //MessageBoxAdapter.ShowDebug("会议成员加入" + telInfo);
        }
        private void OnConfLeave(string telInfo)
        {
            webBrowser.Document.InvokeScript("telDisConnected", new object[] { IPCCVar.currentCallServiceId.ToString(), telInfo });
            //MessageBoxAdapter.ShowDebug("会议成员离开" + telInfo);
        }
        private void OnChangeBusyFree(int status)
        {
            if (status == 0)
            {
                tssbtnSetBusyFree.Text = "空闲";
            }
            else if (status == 1)
            {
                tssbtnSetBusyFree.Text = "忙碌";
            } 
        }
        private void OnLostConnect()
        {
            if (MessageBoxAdapter.ShowInfo("坐席离线，请检查网络环境，并重新登录尝试", Properties.Settings.Default.Company) == System.Windows.Forms.DialogResult.OK)
            {
                webBrowser.Document.InvokeScript("BS_Logout");
                SetLogout();
                
            }
        }
        private void OnAbandon(string queueNo, string callNo)
        {
            string serviceType = queueNo.Substring(queueNo.Length - 1, 1);
            if (serviceType == "0")
            {
                wbEmergency.Document.InvokeScript("AbandonByCaller", new object[] {serviceType, callNo });
            }
            else if (serviceType == "1")
            {
                wbFamily.Document.InvokeScript("AbandonByCaller", new object[] { serviceType, callNo });
            }
            else if (serviceType == "3")
            {
                wbLife.Document.InvokeScript("AbandonByCaller", new object[] { serviceType, callNo });
            }
        }


        public void OpenNewWinAsCallServiceInfo(string urlFragment, string callServiceJsonStr)
        {
            frmCallServiceInfo frm = new frmCallServiceInfo();
            frm.TheParentWin = this;
            CallService callService = JsonConvert.DeserializeObject<CallService>(callServiceJsonStr);
            frm.PageData = callService;
            frm.LoadUrl(cmsHostName + virPathName + urlFragment);
            frm.TransferPhone += new dTransferPhone(delegate(object o, TransferPhoneEventArgs e)
            {
                CreateConferenceCall("", e);
            });
            frm.RemovePhone += new dRemovePhone((o, e) =>
            {
                RemoveConferenceCall(e);
            });
            frm.CallServiceInfoWinClose += new dCallServiceInfoWinClose(delegate(object o, EventArgs e)
            {
                webBrowser.Document.InvokeScript("refreshFromExternal");
            });
            frm.DialBackNoWinFromInfo += new dDialBackNoWin(delegate(object o, DialBackNoWinEventArgs e)
            {
                DialBack(e.CallServiceId, e.OldManId, e.CallNo, e.OldManName, "1");
            });
            frm.Show();
        }
        private string GetQueueNoAndCallee()
        {
            string queueStr = "";
            object oRet1 = webBrowser.Document.InvokeScript("CS_GetQueueNoAndCallee");
            if (oRet1 != null)
            {
                IList<QueueNoAndCallee> queueNoAndCallees = JsonConvert.DeserializeObject<IList<QueueNoAndCallee>>(oRet1.ToString());

                queueStr = string.Join(",", queueNoAndCallees.Select(item => item.QueueNo).ToArray());

                EnvironmentVar.dictsOfQueueNoAndCallee = queueNoAndCallees.ToDictionary(item => item.QueueNo, item => item.Callee);


                //MessageBoxAdapter.ShowDebug(queueStr);
            }
            else
            {
                MessageBoxAdapter.ShowDebug("读取队列失败"); 
            }
            return queueStr;
        }
        #endregion

        #region 内部方法
        private void _DispatchCallService(string fromWhere, string jsonStr, string logContent)
        {
            CallService callService = JsonConvert.DeserializeObject<CallService>(jsonStr);

            //更新CallService 并弹出
            //0-未受理  1-受理中 2-处理中  3-处理完成
            string groupType = callService.ServiceQueueId.ToString().Substring(0, 5);
            if (groupType == "10003")
            {
                //生活服务 需要产生工单，所以有受理中的阶段
                callService.DoStatus = 1;
                callService.DoResults = "受理中";
            }
            else
            {
                //其他直接处理中
                callService.DoStatus = 2;
                callService.DoResults = "处理中";
            }
            //callService.OperatedBy = EnvironmentVar.UserId;
            //callService.OperatedOn = DateTime.Now;
            double d = (DateTime.Now - callService.CheckInTime.Value).TotalSeconds;
            if (d > int.MaxValue)
            {
                callService.CallSeconds = -1;
            }
            else
            {
                callService.CallSeconds = Convert.ToInt32(d);
            }

            callService.ServiceExtId = EnvironmentVar.ExtId;
            callService.ServiceExtNo = EnvironmentVar.ExtCode;
            string strCallServiceId = callService.CallServiceId.ToString();
            string strJsonStr = JsonConvert.SerializeObject(
                new
                {
                    DoStatus = callService.DoStatus,
                    DoResults = callService.DoResults,
                    CallSeconds = callService.CallSeconds,
                    ServiceExtId = callService.ServiceExtId,
                    ServiceExtNo = callService.ServiceExtNo
                }
                ); 
            string openUrl = "";
            switch (callService.ServiceQueueNo.Substring(5))
            {
                case "0":
                    openUrl = ProcessEmergencyUrl;
                    break;
                case "1":
                    openUrl = ProcessFamilyCallsUrl;
                    break;
                case "2":
                    openUrl = ProcessInfotainmentUrl;
                    break;
                case "3":
                    openUrl = ProcessLifeUrl;
                    break;
                default:
                    break;
            }
            if (openUrl != "")
            {
                object oRet2 = webBrowser.Document.InvokeScript("beginProcessCallService", new object[] { strCallServiceId, strJsonStr, logContent });
                if (oRet2 != null && oRet2.ToString() == bool.TrueString)
                {
                    _OpenNewWinAsCallService(fromWhere,openUrl, callService); 
                }
            }
            else
            {
                MessageBoxAdapter.ShowDebug("无效的队列" + callService.ServiceQueueNo);
            }
        }
        public void _OpenNewWinAsCallService(string fromWhere, string urlFragment, CallService callService)
        {
            frmCallService frm = new frmCallService();
            theWinOfCallServic = frm;
            frm.TheParentWin = this;
            frm.PageData = callService;
            frm.LoadUrl(cmsHostName + virPathName + urlFragment);
            frm.CloseWinCallServiceToProcessing += new dCloseWinCallServiceToProcessing(() =>
            {
                webBrowser.Document.InvokeScript("refreshWorkOrderFromIndex");
            });
            frm.InvokeHoldCall += new dInvokeHoldCall(delegate(object o, EventArgs e)
            {
                MgrAPIWrapper.Manager_CurHoldCall(EnvironmentVar.ExtCode, "from-internal");
            });
            frm.InvokeRetreiveCall += new dInvokeRetreiveCall(delegate(object o, EventArgs e)
            {
                MgrAPIWrapper.Manager_CurRetreiveCall(EnvironmentVar.ExtCode, "from-internal");
            });
            frm.InvokeFastTransfer += new dInvokeFastTransfer(delegate(object o, FastTransferEventArgs e)
            {
                MgrAPIWrapper.Manager_CurFastTransfer(EnvironmentVar.ExtCode, e.QueueNo, "from-internal");
            });
            frm.TransferPhone += new dTransferPhone(delegate(object o, TransferPhoneEventArgs e)
            {
                CreateConferenceCall(fromWhere, e);
                //加入队列
                /**/
            });
            frm.RemovePhone += new dRemovePhone((o, e) =>
            {
                RemoveConferenceCall(e);
            });

            frm.CallServiceWinClose += new dCallServiceWinClose(delegate(object o, EventArgs e)
            {
                if (IPCCVar.conferenceMembers != null)
                {
                    IPCCVar.conferenceMembers.Clear();
                }
                IPCCVar.currentConferenceNum = null;
                webBrowser.Document.InvokeScript("refreshFromExternal");
            });
            frm.Show();

        }
        
        #endregion

        #region IPCC事件公用方法
        //常见多方通话
        private void CreateConferenceCall(string fromWhere, TransferPhoneEventArgs e)
        {
            if (IPCCVar.isEstablished)
            {
                if (fromWhere == "client")
                {
                    //目前什么都不做
                }
                //创建队列
                if (IPCCVar.currentConferenceNum == null)
                {
                    string conferenceNum = "9" + IPCCVar.currentCaller;
                    MgrResult ret1 = MgrAPIWrapper.Manager_CreatConf(0, "", IPCCVar.currentCaller, EnvironmentVar.ExtCode, conferenceNum, EnvironmentVar.IPCCRelayType + e.PhoneNo);
                    if (ret1 != MgrResult.ok)
                    {
                        MessageBoxAdapter.ShowDebug("创建会议失败：" + ret1.ToString());
                    }
                    IPCCVar.currentConferenceNum = conferenceNum;
                    IPCCVar.AddConfMember(IPCCVar.currentCaller);
                    IPCCVar.setTelInfo(98, IPCCVar.currentCaller, "老人");
                    IPCCVar.AddConfMember(EnvironmentVar.ExtCode);
                    IPCCVar.setTelInfo(99, EnvironmentVar.ExtCode, "坐席工号<" + EnvironmentVar.ExtCode + ">");
                    IPCCVar.currentCallServiceId = e.CallServiceId;
                }

                if (!IPCCVar.conferenceMembers.Contains(e.PhoneNo))
                {
                    MgrResult ret2 = MgrAPIWrapper.Manager_ConfInvite(e.PhoneNo, IPCCVar.currentConferenceNum, EnvironmentVar.IPCCRelayType + e.PhoneNo);
                    if (ret2 != MgrResult.ok)
                    {
                        MessageBoxAdapter.ShowDebug("邀请加入会议失败：" + ret2.ToString());
                    }
                    //IPCCVar.AddConfMember(e.PhoneNo);
                    IPCCVar.setTelInfo(e.PhoneType, e.PhoneNo, e.Message);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        //移除多方通话成员
        private void RemoveConferenceCall(RemovePhoneEventArgs e)
        {
            if (IPCCVar.isEstablished)
            {
                if (IPCCVar.conferenceMembers.Contains(e.PhoneNo))
                {
                    //从会议中移除该号码

                    MgrResult ret = MgrAPIWrapper.Manager_ConfKickUser(IPCCVar.currentConferenceNum, e.PhoneNo);
                    if (ret != MgrResult.ok)
                    {
                        MessageBoxAdapter.ShowDebug("将成员移出会议失败：" + ret.ToString());
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region IPCC相关
        public void TC_IPCC_UnInit()
        {
            pnlReminder.Visible = false;
            wbEmergency.Url = new Uri("about:blank");
            wbLife.Url = new Uri("about:blank");
            wbFamily.Url = new Uri("about:blank");

            string queueStr = "";
            foreach (var queue in EnvironmentVar.dictsOfQueueNoAndCallee)
            {
                MgrResult ret00 = MgrAPIWrapper.Manager_QueueRemove(EnvironmentVar.ExtCode, queue.Key, EnvironmentVar.UserCode, EnvironmentVar.UserCode);
                if (ret00 == MgrResult.ok)
                {
                }
                else {
                    queueStr += queue.Key + "|";
                }
            }
            if (queueStr.IndexOf('|') > -1)
            {
                MessageBoxAdapter.ShowDebug("QueueRemove error");
            }

            if (_MonitorId != -1)
            {
                bool ret = MgrAPIWrapper.Manager_CloseMonitor(_MonitorId);
                if (ret)
                {
                    //MessageBoxAdapter.ShowDebug("CloseMonitor ok");
                }
                else
                {
                    MessageBoxAdapter.ShowDebug("CloseMonitor error");
                }
            }
            bool ret2 = MgrAPIWrapper.Manager_Disconnect();
            if (ret2)
            {
                //MessageBoxAdapter.ShowDebug("disconnect ok");
            }
            else
            {
                MessageBoxAdapter.ShowDebug("disconnect error");
            }

            //GC.Collect();
        }
        private void TC_IPCC_Init()
        {
            try
            {
                bool ret = MgrAPIWrapper.Manager_Init();
                if (ret)
                {
                    //MessageBoxAdapter.ShowDebug("init ok");
                }
                else
                {
                    tlblProcess.Text = "初始化失败：" + ret.ToString();
                    return;
                }

                string ip = callCenterNodeAddress; 
                string pwd = "";
                //string ext = "714002";
                //string queueStr = "71410,71411,71412,71413";
                //string queueStr = "751050,751051,751052,751053";
                string queueStr = "";

                if (this.InvokeRequired)
                {
                    dGetQueueNoAndCallee dc = new dGetQueueNoAndCallee(GetQueueNoAndCallee);
                    queueStr = this.Invoke(dc).ToString();
                }

                
                MgrResult ret2 = MgrAPIWrapper.Manager_Connect(ip, EnvironmentVar.UserCode, pwd, EnvironmentVar.ExtCode, queueStr);
                if (ret2 == MgrResult.ok)
                { 
                    tlblProcess.Text = "已连接到呼叫中心" + callCenterNodeAddress;
                    this.UIInvoke(() =>
                    {
                        tlblProcess.BackColor = SystemColors.Control;
                    });
                }
                else
                {
                    tlblProcess.Text = "连接失败：" + ret2.ToString(); 
                    return;
                }

                string[] queues = queueStr.Split(",".ToCharArray());
                foreach (var queue in queues)
                {
                    MgrResult ret00 = MgrAPIWrapper.Manager_QueueAdd(EnvironmentVar.ExtCode, queue, EnvironmentVar.UserCode, EnvironmentVar.UserCode, 0);
                    if (ret00 == MgrResult.ok)
                    {
                        MgrResult ret01 = MgrAPIWrapper.Manager_QueuePause(EnvironmentVar.ExtCode, queue, EnvironmentVar.UserCode, EnvironmentVar.UserCode, 0);

                        if (ret01 == MgrResult.ok)
                        {

                        }
                    } 
                }

                myCallBack = new EventCallbackDelegate(Callback);
                MgrAPIWrapper.SetEventCallback(myCallBack);//自动释放
                /*
               if (InvokeRequired)
               {

                   dSetCallback ds = new dSetCallback(() => {
                       myCallBack = new EventCallbackDelegate(Callback);
                       MgrAPIWrapper.SetEventCallback(myCallBack);//自动释放 
                   });
                   this.Invoke(ds, null);
               }
               else
               {
                   myCallBack = new EventCallbackDelegate(Callback);
                   MgrAPIWrapper.SetEventCallback(myCallBack);//自动释放
               }
               
                   
               myCallBack = new EventCallbackDelegate(Callback); //修改后
               EnumWindows(myCallBack, 0);
               MgrAPIWrapper.SetEventCallback(myCallBack);
                 */

                //MessageBoxAdapter.ShowDebug("SetEventCallback ok");
               

                string dn = EnvironmentVar.IPCCDial; //"SIP/751025";//监听端口
                _MonitorId = MgrAPIWrapper.Manager_OpenMonitor(dn, EnvironmentVar.ExtCode);
                if (_MonitorId == 1)
                {
                    //MessageBoxAdapter.ShowDebug("OpenMonitor ok");
                }
                else
                {
                    MessageBoxAdapter.ShowDebug("OpenMonitor " + _MonitorId.ToString());
                    return;
                }

                //发送心跳
                _pingTimer = new System.Timers.Timer(15 * 1000);
                _pingTimer.Elapsed += new System.Timers.ElapsedEventHandler((object source, System.Timers.ElapsedEventArgs ee) =>
                {
                    MgrResult ret_ping = MgrAPIWrapper.Manager_Ping(EnvironmentVar.ExtCode);
                    if (ret_ping == MgrResult.ok)
                    {

                    }
                    else
                    {
                        MessageBoxAdapter.ShowDebug("Manager_Ping " + ret_ping.ToString());
                        return;
                    }

                }); 
                _pingTimer.AutoReset = true;  
                _pingTimer.Enabled = true; 

                //检测心跳
                _pingCheckTimer = new System.Timers.Timer(47 * 1000);
                _pingCheckTimer.Elapsed += new System.Timers.ElapsedEventHandler((object source, System.Timers.ElapsedEventArgs ee) =>
                {
                    if (pingpongValue == 0)
                    {
                        this.Invoke(new dLostConnect(OnLostConnect));
                    }
                    else
                    {
                        pingpongValue = 0;
                    }
                });
                _pingCheckTimer.AutoReset = true;
                _pingCheckTimer.Enabled = true;


                BeginInvoke(new Action(() =>
                {
                    pnlReminder.Visible = true;

                    wbEmergency.Url = new Uri(cmsHostName + virPathName + ReminderEmergencyUrl);
                    wbLife.Url = new Uri(cmsHostName + virPathName + ReminderLifeUrl);
                    wbFamily.Url = new Uri(cmsHostName + virPathName + ReminderFamilyUrl);
                }));
                    
            }
            catch (Exception ex)
            {
                MessageBoxAdapter.ShowError(ex.Message, Properties.Settings.Default.MessageBoxTitle);
            }

        }

        private int _MonitorId = -1; 

        [DllImport("user32")]
        public static extern int EnumWindows(EventCallbackDelegate x, int y);
        private static EventCallbackDelegate myCallBack; //声明回调函数
         
        public void Callback(int eventType, int watchId, int param3, string param4, string param5)
        {

            Common.ipccLogger.Info(string.Format("param1:{0}|param2:{1}|param3:{2}|param4:{3}|param5:{4}", Enum.GetName(typeof(MgrEvent), eventType), watchId.ToString(), param3.ToString(), param4, param5));
            //Console.WriteLine(string.Format("param1:{0}|param2:{1}|param3:{2}|param4:{3}|param5:{4}", eventType.ToString(), watchId.ToString(), param3.ToString(), param4, param5));
            if ((MgrEvent)eventType == MgrEvent.Event_OnAlerting)
            {
                //string callee = "28080210";

                #region 判断来电和去电
                //判断来电和去电
                if (EnvironmentVar.ExtCode == param4)
                {
                    //Console.WriteLine(string.Format("param1:{0}|param2:{1}|param3:{2}|param4:{3}|param5:{4}", eventType.ToString(), watchId.ToString(), param3.ToString(), param4, param5));
                    if (theWinOfDialDialog != null)
                    {
                        //去电
                        IPCCVar.currentCaller = param4;
                        IPCCVar.currentDispatchQueue = null;
                        string[] arrParam5 = param5.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        IPCCVar.currentCallee = arrParam5[0];
                        IPCCVar.currentUniqueId = arrParam5[1];
                        IPCCVar.currentCallServiceId = theWinOfDialDialog.CallServiceId;
                        //dOpenNewWinAsCallServiceFromClient dc = new dOpenNewWinAsCallServiceFromClient(OpenNewWinAsCallServiceFromClient);
                        //this.Invoke(dc, new object[] { IPCCVar.currentCaller, callee, IPCCVar.currentDispatchQueue });

                        BeginInvoke(new Action(() =>
                        {
                            theWinOfDialDialog.OnCancelCallBySeat += new dCancelCallBySeat(() =>
                            {
                                //挂断
                                if (MgrAPIWrapper.Manager_ClearCurCall())
                                {

                                }
                                theWinOfDialDialog = null;
                            });
                            theWinOfDialDialog.Show();
                        }));
                    }
                    
                }
                else
                {
                    
                    //来电
                    if (param4.IndexOf(",") != -1)
                    {
                        //老人呼叫通过队列
                        string[] arrParam4 = param4.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        string[] arrParam5 = param5.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (IPCCVar.currentUniqueId != arrParam5[1])
                        {
                            //因为同一个号码的震铃如果不接电话会一直循环发送，所以需要过滤
                            IPCCVar.currentCaller = arrParam4[0];
                            IPCCVar.currentDispatchQueue = arrParam4[1];
                            IPCCVar.currentCallee = arrParam5[0];
                            IPCCVar.currentUniqueId = arrParam5[1];

                            string callee = "";
                            if (EnvironmentVar.dictsOfQueueNoAndCallee.ContainsKey(IPCCVar.currentDispatchQueue))
                            {
                                callee = EnvironmentVar.dictsOfQueueNoAndCallee[IPCCVar.currentDispatchQueue];
                            }
                            else
                            {
                                Common.ipccLogger.Info(string.Format("EnvironmentVar.dictsOfQueueNoAndCallee.ContainsKey-----currentCaller:{0}|currentDispatchQueue:{1}|currentCallee:{2}|currentUniqueId:{3}", IPCCVar.currentCaller, IPCCVar.currentDispatchQueue, IPCCVar.currentCallee, IPCCVar.currentUniqueId));
                            }

                            if (this.InvokeRequired)
                            {
                                dOpenNewWinAsCallServiceFromClient dc = new dOpenNewWinAsCallServiceFromClient(OpenNewWinAsCallServiceFromClient);
                                this.Invoke(dc, new object[] { IPCCVar.currentCaller, callee, IPCCVar.currentDispatchQueue });
                            }
                        }
                       
                    }
                    else
                    {
                        //非从队列来
                    }
                }
                //MessageBoxAdapter.ShowDebug("callback - " + Enum.GetName(typeof(MgrEvent), eventType) + string.Format("|{0}|{1}|{2}|{3}", watchId.ToString(), callId.ToString(), IPCCVar.currentCaller, param5));
                //DispatchCallService(IPCCVar.currentCaller, callee, queue);

                #endregion

            }
            else if ((MgrEvent)eventType == MgrEvent.Event_OnEstablished)
            {
                string[] arrParam5 = param5.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (EnvironmentVar.ExtCode == param4 && arrParam5[1] == IPCCVar.currentUniqueId)
                {
                    //去电
                    //Console.WriteLine(string.Format("param1:{0}|param2:{1}|param3:{2}|param4:{3}|param5:{4}", eventType.ToString(), watchId.ToString(), param3.ToString(), param4, param5));
                    IPCCVar.isEstablished = true;
                    
                    if (theWinOfDialDialog != null)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            theWinOfDialDialog.Established();

                            string groupTypeFlag = "";
                            if (string.IsNullOrEmpty(theWinOfDialDialog.DialType))
                            {
                                //弹屏
                                string strJsonStr = JsonConvert.SerializeObject(
                                new
                                {
                                    AbandonFlag = 0,
                                }
                                );
                                string openUrl = "";
                                groupTypeFlag = theWinOfDialDialog.CurrentCallService.ServiceQueueNo.Substring(5);
                                switch (groupTypeFlag)
                                {
                                    case "0":
                                        openUrl = ProcessEmergencyUrl;
                                        break;
                                    case "1":
                                        openUrl = ProcessFamilyCallsUrl;
                                        break;
                                    case "2":
                                        openUrl = ProcessInfotainmentUrl;
                                        break;
                                    case "3":
                                        openUrl = ProcessLifeUrl;
                                        break;
                                    default:
                                        break;
                                }
                                if (openUrl != "")
                                {
                                    object oRet2 = webBrowser.Document.InvokeScript("beginProcessCallService", new object[] { theWinOfDialDialog.CurrentCallService.CallServiceId.ToString(), strJsonStr, "回拨接通" });
                                    if (oRet2 != null && oRet2.ToString() == bool.TrueString)
                                    {
                                        _OpenNewWinAsCallService("client", openUrl, theWinOfDialDialog.CurrentCallService);
                                    }
                                }
                            }
                            dAddServiceVoice dc = new dAddServiceVoice(onAddServiceVoice);
                            this.Invoke(dc, new object[] { theWinOfDialDialog.CallServiceId.ToString(), 0, IPCCVar.currentUniqueId, "开始录音" });
                            if (!string.IsNullOrEmpty(groupTypeFlag))
                            {
                                if (groupTypeFlag == "0")
                                {
                                    wbEmergency.Refresh();
                                }
                                else if (groupTypeFlag == "1")
                                {
                                    wbFamily.Refresh();
                                }
                                else if (groupTypeFlag == "3")
                                {
                                    wbLife.Refresh();
                                }

                                theWinOfDialDialog = null;
                            }
                        }));
                    }
                }
                else
                {
                    //来电
                    //Console.WriteLine(string.Format("param1:{0}|param2:{1}|param3:{2}|param4:{3}|param5:{4}", eventType.ToString(), watchId.ToString(), param3.ToString(), param4, param5));

                    if (param4 == IPCCVar.currentCaller && arrParam5[1] == IPCCVar.currentUniqueId)
                    {
                        if (theWinOfCallServic != null && theWinOfCallServic.PageData != null && theWinOfCallServic.PageData.CallServiceId != null)
                        {
                            IPCCVar.isEstablished = true;
                            dAddServiceVoice dc = new dAddServiceVoice(onAddServiceVoice);
                            this.Invoke(dc, new object[] { theWinOfCallServic.PageData.CallServiceId.ToString(), 0, IPCCVar.currentUniqueId, "开始录音" });
                        }
                    }
                }
            }
            else if ((MgrEvent)eventType == MgrEvent.Event_OnConfRecord)
            {
                if (IPCCVar.currentConferenceNum == param5)
                {
                    dFinishServiceVoice df = new dFinishServiceVoice(onFinishServiceVoice);
                    this.Invoke(df, new object[] { theWinOfCallServic.PageData.CallServiceId.ToString(), 0, callCenterNodeAddress, "服务录音结束" });
                    //会议语音 
                    dAddServiceVoice dc = new dAddServiceVoice(onAddServiceVoice);
                    this.Invoke(dc, new object[] { theWinOfCallServic.PageData.CallServiceId.ToString(), 1, param4, "开始三方通话录音" });
                }
            }
            else if ((MgrEvent)eventType == MgrEvent.Event_OnConfJoin)
            {
                if (IPCCVar.currentConferenceNum == param5)
                {
                    //会议成员加入
                    IPCCVar.AddConfMember(param4);
                    //接通 
                    if (param4 == IPCCVar.currentCaller || param4 == EnvironmentVar.ExtCode)
                    {
                        return;
                    }
                    if (this.InvokeRequired)
                    {
                        dConfJoin dc = new dConfJoin(OnConfJoin);
                        this.Invoke(dc, new object[] { IPCCVar.getTelInfo(param4) });
                    }
                }
            }
            else if ((MgrEvent)eventType == MgrEvent.Event_OnConfLeave)
            {
                //会议成员离开
                if (IPCCVar.currentConferenceNum == param5)
                {
                    IPCCVar.RemoveConfMember(param4);
                    if (this.InvokeRequired)
                    {
                        dConfLeave dc = new dConfLeave(OnConfLeave);
                        this.Invoke(dc, new object[] { IPCCVar.getTelInfo(param4) });
                    }
                    if (IPCCVar.conferenceMembers.Count == 0)
                    {
                        IPCCVar.isEstablished = false;
                        dFinishServiceVoice df = new dFinishServiceVoice(onFinishServiceVoice);
                        this.Invoke(df, new object[] { theWinOfCallServic.PageData.CallServiceId.ToString(), 1, callCenterNodeAddress, "三方通话结束" });
                    }
                }
            }
            else if ((MgrEvent)eventType == MgrEvent.Event_OnConnectionCleared)
            { 
                //双方通话结束
                if (watchId == 1 && IPCCVar.isEstablished )
                {
                    if (IPCCVar.conferenceMembers.Count == 0)
                    {
                        dFinishServiceVoice df = new dFinishServiceVoice(onFinishServiceVoice);
                        if (theWinOfDialDialog != null)
                        {
                            this.Invoke(df, new object[] { theWinOfDialDialog.CallServiceId.ToString(), 0, callCenterNodeAddress, "服务录音结束" });
                            theWinOfDialDialog = null;
                        }
                        else
                        {
                            this.Invoke(df, new object[] { theWinOfCallServic.PageData.CallServiceId.ToString(), 0, callCenterNodeAddress, "服务录音结束" });
                        }
                        IPCCVar.isEstablished = false;
                    }
                }
            }
            else if ((MgrEvent)eventType == MgrEvent.Event_OnCallCleared)
            {
                //挂机
                IPCCVar.isEstablished = false;

                //控制按钮隐掉

            }
            else if ((MgrEvent)eventType == MgrEvent.Event_OnPaused)
            {
                //示忙示闲 
                dChangeBusyFree dc = new dChangeBusyFree(OnChangeBusyFree);
                this.Invoke(dc, new object[] { param3 });
            }
            else if ((MgrEvent)eventType == MgrEvent.Event_OnHoldCall)
            {
                //保持通话成功
                if (theWinOfCallServic != null)
                {
                    dHoldCall dc = new dHoldCall(theWinOfCallServic.OnHoldCall);
                    this.Invoke(dc);
                }

            }
            else if ((MgrEvent)eventType == MgrEvent.Event_OnRetreiveCall)
            {
                //取消保持通话成功
                if (theWinOfCallServic != null)
                {
                    dRetreiveCall dc = new dRetreiveCall(theWinOfCallServic.OnRetreiveCall);
                    this.Invoke(dc);
                }
            }
            else if ((MgrEvent)eventType == MgrEvent.Event_OnLeave)
            {
                if (param4 == "18958133608")
                {
                    this.Invoke(new dLostConnect(OnLostConnect));
                }
            }
            else if ((MgrEvent)eventType == MgrEvent.Resp_OnPong)
            {
                if (pingpongValue == 0)
                {
                    pingpongValue = 1;
                }
                //Console.WriteLine(string.Format("param1:{0}|param2:{1}|param3:{2}|param4:{3}|param5:{4}", eventType.ToString(), watchId.ToString(), param3.ToString(), param4, param5));
            }
            else if ((MgrEvent)eventType == MgrEvent.Event_OnQueued)
            {
                //进入队列
            }
            else if ((MgrEvent)eventType == MgrEvent.Event_OnAbandon)
            {
                //放弃来电
                dAbandon dc = new dAbandon(OnAbandon);
                this.Invoke(dc, new object[] { param4, param5 });
            }
        }

        private void _SetBusyFree(byte status)
        {
            foreach (var key in EnvironmentVar.dictsOfQueueNoAndCallee.Keys)
            {
                MgrResult ret01 = MgrAPIWrapper.Manager_QueuePause(EnvironmentVar.ExtCode, key, EnvironmentVar.UserCode, EnvironmentVar.UserCode, status);
                if (ret01 == MgrResult.ok)
                {

                }
            }
        }
        #endregion

        private void tbtnTestTC_IPCC_Click(object sender, EventArgs e)
        {
            frmTestTC_IPCC frm = new frmTestTC_IPCC();
            frm.ShowDialog();
        }
         
        
        private void tbtnSimulateEmergencyService_Click(object sender, EventArgs e)
        {
            if (EnvironmentVar.ExtCode != "")
            {
                //分机号存在
                //模拟数据
                string caller = "13757140245";
                string callee = "28080210";
                string queue = "710020";

                OpenNewWinAsCallServiceFromClient(caller, callee, queue);
            }
        }

        private void tbtnSimulateFamilyCallService_Click(object sender, EventArgs e)
        {
            if (EnvironmentVar.ExtCode != "")
            {
                //分机号存在
                //模拟数据
                string caller = "13282147146";
                string callee = "28080211";
                string queue = "710011";

                OpenNewWinAsCallServiceFromClient(caller, callee, queue);
            }
        }

        private void tbtnSimulateLifeService_Click(object sender, EventArgs e)
        {
            if (EnvironmentVar.ExtCode != "")
            {
                //测试库120300
                //分机号存在
                //模拟数据
                string caller = "13282147140";
                string callee = "28080213";
                string queue = "710013";
               
                /* 
                //120305西湖区
                string caller = "13282147146";
                string callee = "28080213";
                string queue = "715013";
                */
                OpenNewWinAsCallServiceFromClient(caller, callee, queue);
            }
        }

        private void tbtnReload_Click(object sender, EventArgs e)
        {
            webBrowser.Refresh();
        }

        private void 示忙ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tssbtnSetBusyFree.Text = "切换中...";
            _SetBusyFree(1);
        }

        private void 示闲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tssbtnSetBusyFree.Text = "切换中...";
            _SetBusyFree(0);
        }

        private void pbIcon_Click(object sender, EventArgs e)
        {
            if (pnlReminder.Left == -220)
            {
                pnlReminder.Left = 0;
                pbIcon.Image = Properties.Resources.left2arrow;
            }
            else
            {
                pnlReminder.Left = -220;
                pbIcon.Image = Properties.Resources.right2arrow;
            }
        }

       

       
        
    }
}
