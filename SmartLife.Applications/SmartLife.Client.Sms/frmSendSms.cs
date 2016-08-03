using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using win.core.utils;
using e0571.web.core.TypeExtension;
using System.Dynamic;

namespace SmartLife.Client.Sms
{
    public partial class frmSendSms : Form
    {
        private string authSmsEndPoint;
        private string smsEndPoint;
        private IDictionary<string, string> txtDictionary = new Dictionary<string, string>();
        private IDictionary<string, string> orderDictionary = new Dictionary<string, string>();
        private int txtLength = 0;

        //短信发送返回的状态码
        private Dictionary<string, string> smsRetCodeDic = new Dictionary<string, string>();
        private StringBuilder sb_retStatus = new StringBuilder();

        System.Timers.Timer _tickTimer = null;

        public frmSendSms()
        {
            InitializeComponent();
        }

        private void frmSendSms_Load(object sender, EventArgs e)
        {
            //初始化
            authSmsEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTHSMS_END_POINT, Common.INI_FILE_PATH);
            smsEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_SMS_END_POINT, Common.INI_FILE_PATH);
            txtDictionary = BaseUtility.StrToDictionary(INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_TXT_SMS_SETTING_LAST, Common.INI_FILE_PATH), '&');
            orderDictionary = BaseUtility.StrToDictionary(INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_ORDER_SMS_SETTING_LAST, Common.INI_FILE_PATH), '&');

            smsRetCodeDic = BaseUtility.GetEnumDesc(new SmsStatusEnum());

            //开始执行
            _tickTimer = new System.Timers.Timer(5 * 1000);
            _tickTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs ee)
            {
                _tickTimer.Enabled = false;
                HttpUtility.getAsyncTo(authSmsEndPoint + "/GetCityDeployNodes", null, null, new { ApplicationId = "CS001" }.ToStringObjectDictionary(), (ret, res) =>
                {
                    if ((bool)ret.Success)
                    {
                        string url;
                        foreach (var item in ret.rows)
                        {
                            url = (string)item.AccessPoint;//"http://localhost/SmartLife.CertManage.SmsServices";//
                            HttpUtility.postSyncAsJSON(url.Replace("115.236.175.110", "localhost") + "/Pub/SmsSendService/QueuedSendSms", null, new { Status = "0" }.ToStringObjectDictionary(), new { ApplicationId = "CS001", Token = item.Token, NodeId = item.NodeId }.ToStringObjectDictionary(), (smsret, smsres) =>
                            {
                                if ((bool)smsret.Success)
                                {
                                    QueuedSendSms(smsret.rows, url, item.Token, item.NodeId);
                                }
                            });
                        }
                        this.UIInvoke(() =>
                        {
                            if (sb_retStatus.Length > 0)
                            {
                                if (this.tb_SendSms.Lines.Length > 1000)
                                {
                                    int iline = tb_SendSms.GetFirstCharIndexFromLine(1);
                                    tb_SendSms.Text = tb_SendSms.Text.Remove(0, iline);
                                }
                                this.tb_SendSms.AppendText(sb_retStatus.ToString());
                                sb_retStatus.Remove(0, sb_retStatus.Length);
                            }
                        });
                    }
                });
                _tickTimer.Enabled = true;
            });
            //到达时间的时候执行事件； 
            _tickTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            _tickTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }

        //号码整合以批次实现发送
        private void QueuedSendSms(dynamic rows, string url, object token, object NodeId)
        {
            //按批次号实现号码整合
            List<SmsSendInfo> sendlist = new List<SmsSendInfo>();

            IDictionary<string, object> tmpdic;
            int count = 0;
            SmsSendInfo smsSendInfo;
            foreach (var row in rows)
            {
                dynamic list = new ExpandoObject();
                DynamicAdapter.Parse(list, System.Xml.Linq.XElement.Parse(row.ToString()));
                tmpdic = list.StringObjectDictionary as IDictionary<string, object>;
                smsSendInfo = new SmsSendInfo();
                smsSendInfo.SerialNumber = e0571.web.core.Utils.TypeConverter.ChangeString(tmpdic["BatchNum"]);
                smsSendInfo.ScheduleTime = e0571.web.core.Utils.TypeConverter.ChangeString(tmpdic["ScheduleTime"], "");
                smsSendInfo.SendCatalog = e0571.web.core.Utils.TypeConverter.ChangeString(tmpdic["SendCatalog"]);
                smsSendInfo.UserNumber = e0571.web.core.Utils.TypeConverter.ChangeString(tmpdic["Mobile"]);
                if (e0571.web.core.Utils.TypeConverter.ChangeString(tmpdic["SendCatalog"]) == "0")
                {
                    smsSendInfo.MessageContent = e0571.web.core.Utils.TypeConverter.ChangeString(tmpdic["SourceCatalog"]) + ":";
                }
                smsSendInfo.MessageContent += e0571.web.core.Utils.TypeConverter.ChangeString(tmpdic["SendContent"]);

                count = sendlist.Count(s => s.SerialNumber == smsSendInfo.SerialNumber);
                if (sendlist.Count == 0 || count == 0)
                {
                    sendlist.Add(smsSendInfo);
                }
                if (count > 0)
                {
                    sendlist.FirstOrDefault(s => s.SerialNumber == smsSendInfo.SerialNumber).UserNumber += "," + smsSendInfo.UserNumber;
                }
            }

            if (sendlist.Count > 0) SendSms(sendlist, url, token, NodeId);
        }

        private void SendSms(List<SmsSendInfo> smsList, string url, object token, object NodeId)
        {
            //文本提交的参数
            IDictionary<string, object> txtparms = new
            {
                SpCode = txtDictionary["SpCode"],
                LoginName = txtDictionary["LoginName"],
                Password = BaseUtility.DecryptDES(txtDictionary["Password"]),
                f = "1",
            }.ToStringObjectDictionary();
            //命令提交的参数
            IDictionary<string, object> orderparms = new
            {
                SpCode = orderDictionary["SpCode"],
                LoginName = orderDictionary["LoginName"],
                Password = BaseUtility.DecryptDES(orderDictionary["Password"]),
                f = "1",
            }.ToStringObjectDictionary();

            int status = 0;
            string error = "0";
            IDictionary<string, object> body;
            foreach (var key in smsList)
            {
                if(key.SendCatalog=="0")
                {
                    body = key.ToStringObjectDictionary(false).Union2(txtparms);
                }
                else{
                    body = key.ToStringObjectDictionary(false).Union2(orderparms);
                }

                HttpUtility.postSyncAsForm(smsEndPoint + "/Send.do", Encoding.Default, body, null, (txtret, txtres) =>
                {
                    Dictionary<string, string> retdict = BaseUtility.StrToDictionary(txtret, '&');

                    if (retdict["result"] == "0")
                    {
                        status = 1;
                    }
                    else { status = 2; }
                    error = retdict["result"];
                    //存在发送失败的号码，先更新好发送成功的号码
                    if (retdict.ContainsKey("faillist"))
                    {
                        status = 1;
                        error = "0";
                    }
                    UpdateSmsSendStatus(new { SendResult = error, Status = status, BatchNum = key.SerialNumber }.ToStringObjectDictionary(), url, token, NodeId);
                    //再更新发送失败的号码
                    if (retdict.ContainsKey("faillist"))
                    {
                        status = 2;
                        error = retdict["result"];
                        string[] faillArr = retdict["faillist"].Split(',');
                        for (int i = 0; i < faillArr.Length; i++)
                        {
                            if (string.IsNullOrEmpty(faillArr[i])) continue;
                            UpdateSmsSendStatus(new { SendResult = error, Status = status, Mobile = faillArr[i] ,BatchNum = key.SerialNumber }.ToStringObjectDictionary(), url, token, NodeId);
                        }
                    }
                });
            }
        }

        private void UpdateSmsSendStatus(IDictionary<string, object> parms, string url, object token, object NodeId)
        {
            sb_retStatus.Append((string)parms["BatchNum"] + ":");
            if (parms.ContainsKey("Mobile")) {
                sb_retStatus.Append((string)parms["Mobile"] + ",");
            }
            string sendRet = (string)parms["SendResult"];
            sb_retStatus.Append((smsRetCodeDic.ContainsKey(sendRet) ? smsRetCodeDic[sendRet] : "发送异常") + ",");
            sb_retStatus.Append(parms["Status"]);
            
            HttpUtility.postSyncAsJSON(url + "/Pub/SmsSendService/Update2", null, parms, new { ApplicationId = "CS001", Token = token, NodeId = NodeId }.ToStringObjectDictionary(), (ret, res) =>
            {
                if ((bool)ret.Success)
                {
                    sb_retStatus.Append(",数据库更新成功 " + "\r\n");
                    
                }
                else
                {
                    sb_retStatus.Append(",数据库更新失败 " + "\r\n");
                }
            });
        }

        public class SmsSendInfo {
            public string UserNumber { get; set; }
            public string ScheduleTime { get; set; }
            public string SerialNumber { get; set; }
            public string MessageContent { get; set; }
            public string SendCatalog { get; set; }
        }

    }
}
