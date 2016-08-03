using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using win.core.utils;
using e0571.web.core.TypeExtension;

namespace SmartLife.Client.Sms
{
    public partial class frmGetSms : Form
    {
        private string authEndPoint;
        private string smsEndPoint;
        private int txtLength = 0;
        private IDictionary<string, string> orderDictionary = new Dictionary<string, string>();

        System.Timers.Timer _tickTimer = null;
        private Dictionary<string, string> cache = new Dictionary<string, string>();

        public frmGetSms()
        {
            InitializeComponent();
        }

        private void frmGetSms_Load(object sender, EventArgs e)
        {
            authEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, Common.INI_FILE_PATH);
            smsEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_SMS_END_POINT, Common.INI_FILE_PATH);
            orderDictionary = BaseUtility.StrToDictionary(INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_ORDER_SMS_SETTING_LAST, Common.INI_FILE_PATH), '&');

            string lastReplyId = "0";
            StringBuilder sb_getSmsInfo = new StringBuilder();
            _tickTimer = new System.Timers.Timer(5 * 1000);
            _tickTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs ee)
            {
                _tickTimer.Enabled = false;
                HttpUtility.postSyncAsForm(smsEndPoint + "/reply.do", Encoding.Default, new { SpCode = orderDictionary["SpCode"], LoginName = orderDictionary["LoginName"], Password = BaseUtility.DecryptDES(orderDictionary["Password"]) }.ToStringObjectDictionary(), null, (ret, res) =>
                {
                    //ret = "replys=[{\"mdn\":\"13282147242\",\"content\":\"103/002/118.872887/28.970322/113.992516/22.528885/1000.000000/姓名/20\",\"reply_time\":\"2013-10-31 14:43:22\",}]";
                    Dictionary<string, string> dictionary = BaseUtility.StrToDictionary(ret, '&');
                    if (dictionary.ContainsKey("replys"))
                    {
                        string itemNo;
                        string content;
                        string replays = dictionary["replys"];
                        //比较最后一个回复ID号，没有新内容不执行发送,否则截取要发送的内容进行发送
                        if (!lastReplyId.Equals(dictionary["id"]))
                        {
                            lastReplyId = dictionary["id"];//保存最后一个ID号

                            dynamic dynJson = Newtonsoft.Json.JsonConvert.DeserializeObject(replays);
                            foreach (var item in dynJson)
                            {
                                itemNo = Convert.ToString(item.mdn);
                                content = (string)item.content;

                                if (string.IsNullOrEmpty(content) || content.Split('/').Length < 1) continue;
                                if (string.IsNullOrEmpty(itemNo)) continue;

                                HttpUtility.postSyncAsJSON(authEndPoint + "/AuthenticateUnicomMobileNo", null, new { MobileNo = itemNo }.ToStringObjectDictionary(), new { ApplicationId = "CS001" }.ToStringObjectDictionary(), (httpret, httpres) =>
                                {
                                    if ((bool)httpret.Success)
                                    {
                                        ArrayList arr = BaseUtility.StrSplitToArray(content);
                                        string strflag = (string)arr[0];

                                        string url = httpret.ret.AccessPoint;//"http://localhost/SmartLife.CertManage.SmsServices";//
                                        if (strflag == "102" || strflag == "103" || strflag == "104")
                                        {
                                            HttpUtility.postSyncAsJSON(url + "/Oca/OldManLocateInfoService/CreateLocateByCall", null, new { LocateTime = item.reply_time, LongitudeS = arr[2], LatitudeS = arr[3] }.ToStringObjectDictionary(), new { ApplicationId = "CS001", Token = httpret.ret.Token, MobileNo = itemNo }.ToStringObjectDictionary(), (locret, locres) =>
                                            {
                                                if ((bool)locret.Success)
                                                {
                                                    sb_getSmsInfo.Append("成功插入" + itemNo + " : " + strflag + "," + arr[2] + "|" + arr[3] + "\r\n");
                                                }
                                            });
                                        }
                                        if (strflag == "105" || strflag == "104")
                                        {
                                            string reminderContent = (strflag == "104" ? "超出警戒范围报警" : "电压低于20%报警");
                                            HttpUtility.postSyncAsJSON(url + "/Pub/ReminderService/CreateReminderByCall", null, new { LastTime = item.reply_time, SourceType = strflag, RemindContent = reminderContent }.ToStringObjectDictionary(), new { ApplicationId = "CS001", Token = httpret.ret.Token, MobileNo = itemNo }.ToStringObjectDictionary(), (remret, remres) =>
                                            {
                                                if ((bool)remret.Success)
                                                {
                                                    sb_getSmsInfo.Append("成功插入" + itemNo + " : " + strflag + "," + reminderContent + "\r\n");
                                                }
                                            });
                                        }
                                    }
                                });
                            }
                            this.UIInvoke(() =>
                            {
                                if (this.tb_GetSms.Lines.Length > 1000)
                                {
                                    int iline = tb_GetSms.GetFirstCharIndexFromLine(1);
                                    tb_GetSms.Text = tb_GetSms.Text.Remove(0, iline);
                                }
                                this.tb_GetSms.AppendText(sb_getSmsInfo.ToString());
                                sb_getSmsInfo.Remove(0, sb_getSmsInfo.Length);
                            });
                        }
                    }
                });
                HttpUtility.postAsyncAsForm(smsEndPoint + "/replyConfirm.do", Encoding.Default, new { SpCode = orderDictionary["SpCode"], LoginName = orderDictionary["LoginName"], Password = BaseUtility.DecryptDES(orderDictionary["Password"]), id = lastReplyId }.ToStringObjectDictionary(), null, (ret, res) =>
                {
                    string requestweb = (string)ret;
                    if (requestweb.Contains("result=0"))
                    {
                        sb_getSmsInfo.Append("上行回复内容确认成功! \r\n");
                    }
                    this.UIInvoke(() =>
                    {
                        if (this.tb_GetSms.Lines.Length > 1000)
                        {
                            int iline = tb_GetSms.GetFirstCharIndexFromLine(1);
                            tb_GetSms.Text = tb_GetSms.Text.Remove(0, iline);
                        }
                        this.tb_GetSms.AppendText(sb_getSmsInfo.ToString());
                        sb_getSmsInfo.Remove(0, sb_getSmsInfo.Length);
                    });
                });
                _tickTimer.Enabled = true;
            });
            ////到达时间的时候执行事件； 
            _tickTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            _tickTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }
    }
}
