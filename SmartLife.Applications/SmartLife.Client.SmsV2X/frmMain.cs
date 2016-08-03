using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

using win.core.utils;
using e0571.web.core.TypeInherited;
using e0571.web.core.TypeExtension;
using SmartLife.Client.Sms;
using System.Dynamic;
using System.Web;


namespace SmartLife.Client.SmsV2X
{
    public partial class frmMain : Form
    {
        #region 参数
        private string authEndPoint;
        private string authSmsEndPoint;
        private string smsEndPoint;
        
        private IDictionary<string, object> txtDictionary = new Dictionary<string, object>();
        //短信发送返回的状态码
        private Dictionary<string, string> smsRetCodeDic = new Dictionary<string, string>();

        bool bStopAuthThread = false;   //关闭验证
        bool bPassValidated = false;    //验证未通过

        private static object lockme = new object();  // 让lock用，不用管是什么东西
        private static string lastReplayId = "0";    //上行回复内容最后一个接口号，以便每次取最新内容

        //线程安全
        ConcurrentDictionary<string, SendSmsEndPoint> smsPointDic = new ConcurrentDictionary<string, SendSmsEndPoint>();   //获取短信节点
        ConcurrentDictionary<string, StringObjectDictionary> sendSmsDic = new ConcurrentDictionary<string, StringObjectDictionary>();   //短信发送状态(发送，回执)
        

        Thread Auth_CircleThread;   //验证
        Thread GetSmsPoint_CircleThread;    //获取短信节点
        Thread SendSms_CircleThread;    //发送短信
        Thread ReportSms_CircleThread;  //回执
        Thread ReplySms_CircleThread;   //上行回复
        Thread ReplyConfirmSms_CircleThread;    //上行回复内容确认

        Thread UiSendSms_CircleThread;
        Thread UiGetSms_CircleThread;
        #endregion

        public frmMain()
        {
            InitializeComponent();
        }

        #region 窗体事件
        private void frmMain_Load(object sender, EventArgs e)
        {
            authEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, Common.INI_FILE_PATH);
            authSmsEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTHSMS_END_POINT, Common.INI_FILE_PATH);
            smsEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_SMS_END_POINT, Common.INI_FILE_PATH);

            smsRetCodeDic = Common.GetEnumDesc(new SmsStatusEnum());

            Dictionary<string,string> txtSmsSettingDic =Common.StrToDictionary(INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_TXT_SMS_SETTING, Common.INI_FILE_PATH), '&');
            if (txtSmsSettingDic.Count > 2)
            {
                //默认发送短信参数
                txtDictionary.Add("SpCode", txtSmsSettingDic["SpCode"]);
                txtDictionary.Add("LoginName", txtSmsSettingDic["LoginName"]);
                txtDictionary.Add("Password", txtSmsSettingDic["Password"]);
                txtDictionary.Add("f", "1");

                _LoopForAuth();
                _LoopForGetSmsPoint();

                _LoopForSendSms();
                _LoopForReportSms();

                _LoopForReplySms();
                _LoopForReplyConfirmSms();

                _LoopForUiSendSms();
                _LoopForUiGetms();
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            bStopAuthThread = false;
            bPassValidated = false;

            if (Auth_CircleThread != null)
            {
                if (Auth_CircleThread.IsAlive)
                {
                    Auth_CircleThread.Abort();
                }
                Auth_CircleThread = null;
            }
            if (GetSmsPoint_CircleThread != null)
            {
                if (GetSmsPoint_CircleThread.IsAlive)
                {
                    GetSmsPoint_CircleThread.Abort();
                }
                GetSmsPoint_CircleThread = null;
            }
            if (SendSms_CircleThread != null)
            {
                if (SendSms_CircleThread.IsAlive)
                {
                    SendSms_CircleThread.Abort();
                }
                SendSms_CircleThread = null;
            }
            if (ReportSms_CircleThread != null)
            {
                if (ReportSms_CircleThread.IsAlive)
                {
                    ReportSms_CircleThread.Abort();
                }
                ReportSms_CircleThread = null;
            }
            if (ReplySms_CircleThread != null)
            {
                if (ReplySms_CircleThread.IsAlive)
                {
                    ReplySms_CircleThread.Abort();
                }
                ReplySms_CircleThread = null;
            }
            if (ReplyConfirmSms_CircleThread != null)
            {
                if (ReplyConfirmSms_CircleThread.IsAlive)
                {
                    ReplyConfirmSms_CircleThread.Abort();
                }
                ReplyConfirmSms_CircleThread = null;
            }
            if (UiSendSms_CircleThread != null)
            {
                if (UiSendSms_CircleThread.IsAlive)
                {
                    UiSendSms_CircleThread.Abort();
                }
                UiSendSms_CircleThread = null;
            }
            if (UiGetSms_CircleThread != null)
            {
                if (UiGetSms_CircleThread.IsAlive)
                {
                    UiGetSms_CircleThread.Abort();
                }
                UiGetSms_CircleThread = null;
            }
        }
        #endregion

        #region 按钮事件
        //配置
        private void tsmi_config_Click(object sender, EventArgs e)
        {
            frmSetting frm = new frmSetting();

            frm.SectionSettingsSave += new dSectionSettingsSave((o, ce) =>
            {
                INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, ce.AuthEndPoint, Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTHSMS_END_POINT, ce.AuthSmsEndPoint, Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_SMS_END_POINT, ce.SmsEndPoint, Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_TXT_SMS_SETTING, ce.TxtSmsSetting, Common.INI_FILE_PATH);
            });
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (MessageBoxAdapter.ShowConfirm("您必须重新启动程序设置才能生效，是否重启程序？", Properties.Settings.Default.MessageBoxTitle) == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Restart();
                }
            }
        }

        #endregion

        #region 线程事件
        //验证
        private void _LoopForAuth() {
            Auth_CircleThread = ThreadAdapter.UIDoCircleTask(lblMsg, () =>
            {
                if (lblMsg.Text.IndexOf(".") == -1)
                {
                    lblMsg.Text += ".";
                }
                else if (lblMsg.Text.IndexOf(".") + 5 == lblMsg.Text.Length)
                {
                    lblMsg.Text = lblMsg.Text.Substring(0, lblMsg.Text.Length - 5);
                }
                else
                {
                    lblMsg.Text += ".";
                }
            },
            200,
            () =>
            {
                return bStopAuthThread;
            },//验证未通过永远不停止
            () =>
            {
                return false;
            }//暂时永远不跳过
            );

            //验证请求
            HttpAdapter.optionsAsyncTo(authEndPoint + "/", new { ApplicationId = "CS001" }.ToStringObjectDictionary(), (ret, res) =>
            {
                this.Invoke(new Action(() => {
                    lblMsg.Hide();  //
                }));
                bStopAuthThread = true; //停止验证
                if (ret != "ok")
                {
                    MessageBoxAdapter.ShowError("无效的认证节点");
                }
                else
                {
                    bPassValidated = true;
                }
            });
        }

        //获取短信节点个数
        private void _LoopForGetSmsPoint()
        {
            GetSmsPoint_CircleThread = ThreadAdapter.DoCircleTask(() =>
            {
                //验证请求
                HttpAdapter.getAsyncTo(authSmsEndPoint + "/GetCityDeployNodes",null, new { ApplicationId = "CS001" }.ToStringObjectDictionary(), (ret, res) =>
                {
                    if ((bool)ret.Success)
                    {
                        SendSmsEndPoint sendSmsEndPoint;
                        foreach (var item in ret.rows)
                        {
                            sendSmsEndPoint = new SendSmsEndPoint() { ApplicationId = "CS001" };
                            sendSmsEndPoint.Url = (string)item.AccessPoint;//"http://localhost/SmartLife.CertManage.SmsServices";//
                            sendSmsEndPoint.NodeId = (string)item.NodeId;
                            sendSmsEndPoint.Token = (string)item.Token;
                            if (smsPointDic.ContainsKey(sendSmsEndPoint.NodeId))
                            {
                                smsPointDic.TryUpdate(sendSmsEndPoint.NodeId, sendSmsEndPoint, smsPointDic[sendSmsEndPoint.NodeId]);
                            }
                            else {
                                smsPointDic.TryAdd(sendSmsEndPoint.NodeId, sendSmsEndPoint);
                            }
                        }
                    }
                });
            },
            30000,
            () =>
            {
                return false;
            },//永远不停止
            () =>
            {
                return !bPassValidated;
            }//验证通过开始执行
            );
        }

        //发送短信
        private void _LoopForSendSms() {
            SendSms_CircleThread = ThreadAdapter.DoCircleTask(() => {
                string strUrl,strToken,strNodeId;
                foreach (var item in smsPointDic)
                {
                    strUrl = item.Value.Url;
                    strToken = item.Value.Token;
                    strNodeId = item.Value.NodeId;

                    HttpAdapter.getSyncTo(strUrl + "/Pub/SmsSendService/QueuedUnSendSms", null, new { ApplicationId = "CS001", Token = strToken, NodeId = strNodeId }.ToStringObjectDictionary(), (smsret, smsres) =>
                    {
                        if ((bool)smsret.Success)
                        {
                            StringObjectDictionary sod;
                            IDictionary<string, object> dic;
                            string strItem = "";
                            DateTime scheduleTime = DateTime.Now;
                            foreach (var row in smsret.rows)
                            {
                                sod = new StringObjectDictionary();
                                dynamic list = new ExpandoObject();
                                DynamicAdapter.Parse(list, System.Xml.Linq.XElement.Parse(row.ToString()));
                                dic = list.StringObjectDictionary;

                                strItem = e0571.web.core.Utils.TypeConverter.ChangeString(dic["SendCatalog"]);
                                if (strItem == "0")
                                {
                                    sod = sod.Union2(txtDictionary.ToStringObjectDictionary());
                                }

                                strItem = e0571.web.core.Utils.TypeConverter.ChangeString(dic["Mobiles"]);
                                if (string.IsNullOrEmpty(strItem)) { continue; }  //跳过
                                sod.Add("UserNumber", strItem);

                                //是否有预约时间   是-转化yyyyMMddHHmmss;否-空
                                strItem = e0571.web.core.Utils.TypeConverter.ChangeString(dic["ScheduleTime"], "");
                                sod.Add("ScheduleTime", strItem);
                                if (!string.IsNullOrEmpty(strItem))
                                {
                                    if (DateTime.TryParse(strItem, out scheduleTime))
                                    {
                                        sod["ScheduleTime"] = scheduleTime.ToString("yyyyMMddHHmmss");
                                    }
                                }

                                strItem = e0571.web.core.Utils.TypeConverter.ChangeString(dic["SendContent"]);
                                sod.Add("MessageContent", HttpUtility.UrlEncode(strItem, Encoding.GetEncoding("GBK")));
                                //批次号
                                strItem = e0571.web.core.Utils.TypeConverter.ChangeString(dic["BatchNum"]);
                                sod.Add("SerialNumber", strItem);

                                //发送HttpAdapter.postSyncAsForm
                                Common.postSyncAsForm(smsEndPoint + "/Send.do", sod, null, (ret, res) =>
                                {
                                    Dictionary<string, string> retdict = Common.StrToDictionary(ret, '&');
                                    if (retdict["result"] == "0")
                                    {
                                        this.Invoke(new Action<string, string>((p1, p2) =>
                                        {
                                            lb_Send.Items.Add("[" + p1 + "] 成功发送 [" + p2 + "]");
                                        }), new object[] { sod["UserNumber"], HttpUtility.UrlDecode((string)sod["MessageContent"], Encoding.GetEncoding("GBK")) });

                                        //需要回执的短信
                                        StringObjectDictionary sod2;
                                        if (!sendSmsDic.TryGetValue(strItem, out sod2))
                                        {
                                            sod2 = new { SmsNodeId = strNodeId, SmsToken = strToken, SmsUrl = strUrl }.ToStringObjectDictionary();
                                            sendSmsDic.TryAdd(strItem, sod2);
                                        }

                                        //更新成功发送数据
                                        UpdateSmsSendStatus(new { Status = 1, Mobiles = ((string)sod["UserNumber"]), BatchNum = sod["SerialNumber"] }.ToStringObjectDictionary(), strUrl, strToken, strNodeId, 0);
                                        //发送成功 检查是否有发送失败数据
                                        if (!string.IsNullOrEmpty(retdict["faillist"]))
                                        {
                                            string[] strMobiles = retdict["faillist"].Split(',');
                                            string mobiles = "'" + string.Join("','", strMobiles.Where(s => !string.IsNullOrEmpty(s))) + "'";

                                            this.Invoke(new Action<string>(p1 =>
                                            {
                                                lb_Send.Items.Add("[" + p1 + "] 无效号码");
                                            }), new object[] { retdict["faillist"] });

                                            UpdateSmsSendStatus(new { Status = 2, Mobiles = mobiles, BatchNum = sod["SerialNumber"] }.ToStringObjectDictionary(), strUrl, strToken, strNodeId, 0);
                                        }
                                    }
                                    else {
                                        this.Invoke(new Action<string, string>((p1, p2) =>
                                        {
                                            lb_Send.Items.Add("[" + p1 + "] 发送失败[" + (smsRetCodeDic.ContainsKey(p2) ? smsRetCodeDic[p2] : "发送异常") + "]");
                                        }), new object[] { (string)sod["UserNumber"], retdict["result"] });

                                        //发送失败
                                        UpdateSmsSendStatus(new { Status = 2, Mobiles = ((string)sod["UserNumber"]), BatchNum = sod["SerialNumber"] }.ToStringObjectDictionary(), strUrl, strToken, strNodeId, 0);
                                    }
                                    
                                });

                                //回执
                            }
                        }
                    });
                }
            },
            5000,
            () =>
            {
                return false;
            },//永远不停止
            () =>
            {
                return !(smsPointDic.Count > 0);
            }//未获取到节点时跳过
            );
        }

        //回执
        private void _LoopForReportSms() {
            ReportSms_CircleThread = ThreadAdapter.DoCircleTask(() => {
                Common.postSyncAsForm(smsEndPoint + "/report.do", txtDictionary, null, (rret, rres) =>
                {
                    Dictionary<string, string> dic = Common.StrToDictionary(rret, '&');
                    if (!string.IsNullOrEmpty(dic["out"]))
                    {
                        string[] strPArray = dic["out"].Split(';');
                        string[] strCArray;
                        Dictionary<string, string> reportSmsDic = new Dictionary<string, string>();
                        for (int i = 0; i < strPArray.Length; i++)
                        {
                            //sendresult 默认应该是短信接口发送成功，暂时只更新短信接口发送失败的数据
                            strCArray = strPArray[i].Split(',');
                            if (strCArray.Length < 2) break;

                            if (reportSmsDic.ContainsKey(strCArray[0]))
                            {
                                if (strCArray[2] != "0")
                                {
                                    reportSmsDic[strCArray[0]] += "," + strCArray[1];
                                }
                            }
                            else
                            {
                                reportSmsDic.Add(strCArray[0], (strCArray[2] != "0" ? strCArray[1] : ""));
                            }
                        }
                        //循环更新
                        StringObjectDictionary sod;
                        string[] strErrorNum;
                        string strToken, strUrl, strNodeId, strMobiles;
                        foreach (var item in reportSmsDic)
                        {
                            if (sendSmsDic.TryRemove(item.Key, out sod))
                            {
                                strUrl = (string)sod["SmsUrl"];
                                strToken = (string)sod["SmsToken"];
                                strNodeId = (string)sod["SmsNodeId"];
                                //更新
                                UpdateSmsSendStatus(new { SendResult = 0, Mobiles = "", BatchNum = item.Key }.ToStringObjectDictionary(), strUrl, strToken, strNodeId, 1);

                                strErrorNum = item.Value.Split(',');
                                if (strErrorNum.Length > 1)
                                {
                                    strMobiles = "'" + string.Join("','", strErrorNum.Where(s => !string.IsNullOrEmpty(s))) + "'";
                                    UpdateSmsSendStatus(new { SendResult = 1, Mobiles = strMobiles, BatchNum = item.Key }.ToStringObjectDictionary(), strUrl, strToken, strNodeId, 1);
                                }
                            }
                        }
                    }
                });
            },
            6000,
            () => { 
                return false; //永不停止
            },
            ()=>{
                return !(smsPointDic.Count() > 0);  //未找到节点 跳过
            });
        }

        //上行回复
        private void _LoopForReplySms() {
            ReplySms_CircleThread = ThreadAdapter.DoCircleTask(() => {
                Common.postSyncAsForm(smsEndPoint + "/reply.do", txtDictionary, (ret, res) =>
                {
                    Dictionary<string, string> dic = Common.StrToDictionary(ret, '&');
                    if (dic.ContainsKey("replys"))
                    {
                        string itemNo;
                        string content;
                        string replays = (string)dic["replys"];
                        //锁定 lastReplayId 保持线程同步
                        //比较最后一个回复ID号，没有新内容不执行发送,否则截取要发送的内容进行发送
                        lock (lockme)
                        {
                            if (lastReplayId != ((string)dic["id"]))
                            {
                                lastReplayId = (string)dic["id"];//保存最后一个ID号

                                dynamic dynJson = Newtonsoft.Json.JsonConvert.DeserializeObject(replays);
                                foreach (var item in dynJson)
                                {
                                    itemNo = Convert.ToString(item.mdn);
                                    content = HttpUtility.UrlDecode((string)item.content, Encoding.GetEncoding("GBK"));

                                    if (string.IsNullOrEmpty(content) || content.Split('/').Length < 1) continue;
                                    if (string.IsNullOrEmpty(itemNo)) continue;


                                    HttpAdapter.postSyncAsJSON(authEndPoint + "/AuthenticateUnicomMobileNo", new { MobileNo = itemNo }.ToStringObjectDictionary(), new { ApplicationId = "CS001" }.ToStringObjectDictionary(), (httpret, httpres) =>
                                    {
                                        if ((bool)httpret.Success)
                                        {
                                            ArrayList arr = Common.StrSplitToArray(content);
                                            string strflag = (string)arr[0];

                                            string url = httpret.ret.AccessPoint;//"http://localhost/SmartLife.CertManage.SmsServices";//
                                            if (strflag == "102" || strflag == "103" || strflag == "104")
                                            {
                                                HttpAdapter.postSyncAsJSON(url + "/Oca/OldManLocateInfoService/CreateLocateByCall", new { LocateTime = item.reply_time, LongitudeS = arr[2], LatitudeS = arr[3] }.ToStringObjectDictionary(), new { ApplicationId = "CS001", Token = httpret.ret.Token, MobileNo = itemNo }.ToStringObjectDictionary(), (locret, locres) =>
                                                {
                                                    this.Invoke(new Action<string, string, bool>((p1, p2, p3) =>
                                                    {
                                                        lb_Get.Items.Add(p1 + " 执行命令 [" + p2 + "] " + (p3 == true ? "成功" : "失败"));
                                                    }), new object[] { itemNo, strflag, (bool)locret.Success });
                                                });
                                            }
                                            if (strflag == "105" || strflag == "104")
                                            {
                                                string reminderContent = (strflag == "104" ? "超出警戒范围报警" : "电压低于20%报警");
                                                HttpAdapter.postSyncAsJSON(url + "/Pub/ReminderService/CreateReminderByCall", new { LastTime = item.reply_time, SourceType = strflag, RemindContent = reminderContent }.ToStringObjectDictionary(), new { ApplicationId = "CS001", Token = httpret.ret.Token, MobileNo = itemNo }.ToStringObjectDictionary(), (remret, remres) =>
                                                {
                                                    this.Invoke(new Action<string, string, bool>((p1, p2, p3) =>
                                                    {
                                                        lb_Get.Items.Add(p1 + " 执行命令 [" + p2 + "] " + (p3 == true ? "成功" : "失败"));
                                                    }), new object[] { itemNo, strflag, (bool)remret.Success });
                                                });
                                            }
                                        }
                                    });
                                }
                            }
                        }
                    }
                });
            },
            5000,
            () =>
            {
                return false;   //永不停止
            },
            () =>
            {
                return !bPassValidated;  //验证通过开始执行
            });
        }

        //上行回复确认
        private void _LoopForReplyConfirmSms() {
            ReplyConfirmSms_CircleThread = ThreadAdapter.DoCircleTask(() =>
            {
                lock (lockme)
                {
                    Common.postSyncAsForm(smsEndPoint + "/replyConfirm.do", new { SpCode = txtDictionary["SpCode"], LoginName = txtDictionary["LoginName"], Password = txtDictionary["Password"], id = lastReplayId }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        bool bstr = ((string)ret).Contains("result=0");
                        this.Invoke(new Action<bool>(p1 =>
                        {
                            lb_Get.Items.Add("上行回复内容确认" + (p1 == true ? "成功!" : "失败!"));
                        }), new object[] { bstr });
                    });

                    lastReplayId = "0";     //重新清0
                }
            },
            15000,
            () =>
            {
                return false;   //永不停止
            },
            () =>
            {
                return !(lastReplayId != "0");  //未取到最后一个查询Id  不执行确认接口
            });
        }


        //ui清理
        private void _LoopForUiSendSms()
        {
            UiSendSms_CircleThread = ThreadAdapter.UIDoCircleTask(lb_Send, () =>
            {
                if (lb_Send.Items.Count > 1000) {
                    lb_Send.Items.Clear();
                }
            }, 
            180000, 
            () => {
                return false;   //暂时不停止
            }, 
            () => {
                return !bPassValidated;    //验证通过不跳过
            });
        }

        private void _LoopForUiGetms() {
            UiGetSms_CircleThread = ThreadAdapter.UIDoCircleTask(lb_Get, () =>
            {
                if (lb_Get.Items.Count > 1000)
                {
                    lb_Get.Items.Clear();
                }
            },
            180000,
            () =>
            {
                return false;   //暂时不停止
            },
            () =>
            {
                return !bPassValidated;    //验证通过不跳过
            });
        }
        #endregion

        #region 处理方法
        //更新短信状体
        private void UpdateSmsSendStatus(StringObjectDictionary sod, string url, string token, string nodeId,int itype)
        {
            HttpAdapter.postSyncAsJSON(url + "/Pub/SmsSendService/Update2", sod, new { ApplicationId = "CS001", Token = token, NodeId = nodeId }.ToStringObjectDictionary(), (ret, res) =>
            {
                this.Invoke(new Action<int,string, bool>((p1, p2,p3) =>
                {
                    lb_Send.Items.Add((itype == 1 ? "[回执]" : "") + (string.IsNullOrEmpty(p2) ? "" : "[" + p2 + "] ") + "更新数据库短信状态:" + (p3 == true ? "成功" : "失败"));
                }), new object[] { itype, (string)sod["Mobiles"], (bool)ret.Success });
            });
        }

        #endregion

    }



}
