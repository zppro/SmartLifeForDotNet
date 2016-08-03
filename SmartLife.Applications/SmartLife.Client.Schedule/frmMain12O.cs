using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Xml.Linq;
using System.Dynamic;
using Newtonsoft.Json;

using win.core.utils;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;

using SmartLife.Share.Models.Auth;


namespace SmartLife.Client.Schedule
{
    public partial class frmMain12O : Form
    {
        List<AccessPointPart> accessPointParts = new List<AccessPointPart>();
        StringObjectDictionary threadsToAccessPoints = new StringObjectDictionary();
        string sendReminderUrl = "";
        string strRunMode = "0";

        public frmMain12O()
        {
            InitializeComponent();
        }

        private void frmMain12O_Load(object sender, EventArgs e)
        {
            Text = Properties.Settings.Default.ProductName + " " + VersionAdapter.GetVersion(VersionType.ASSEMBLY);
        }

        #region 窗口事件

        private void frmMain12O_Shown(object sender, EventArgs e)
        {
            FetchAccessPoints(() =>
            {
                _MessageLoopForAccessPoints();
            });
        }

        private void frmMain12O_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ClearMessageLoop();
        }

        #endregion

        #region 帮助方法

        #region 获取访问点
        private void FetchAccessPoints(Action success)
        {
            sendReminderUrl = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_SEND_REMINDER_POINT, Common.INI_FILE_PATH);
            strRunMode = INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_RUN_MODE, Common.INI_FILE_PATH);
            string authEndPoint = INIAdapter.ReadValue(Common.CFG_SECTION_WEB, Common.CFG_KEY_AUTH_END_POINT, Common.CFG_FILE_PATH);

            //异步认证
            byte runMode = byte.Parse(strRunMode);//测试1 正式0 
            HttpAdapter.postAsyncAsJSON(authEndPoint, new { RunMode = runMode }.ToStringObjectDictionary(), new { ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(), (ret, res) =>
            {
                if ((bool)ret.Success)
                {
                    foreach (var accessPoint in ret.ret.AccessPoints)
                    {
                        accessPointParts.Add(new AccessPointPart { ObjectId = accessPoint.ObjectId.ToString(), Url = accessPoint.Url.ToString() });
                    }

                    if (success != null)
                    {
                        success();
                    }
                }
                else
                {
                    MessageBoxAdapter.ShowError(ret.ErrorMessage);
                }
            });
        }
        #endregion

        #region 消息处理
        private void _MessageLoopForAccessPoints()
        {
            foreach (var part in accessPointParts)
            {
                if (threadsToAccessPoints.ContainsKey(part.ObjectId))
                {
                    Thread accessPointThead = threadsToAccessPoints[part.ObjectId] as Thread;
                    if (accessPointThead != null)
                    {
                        if (accessPointThead.IsAlive)
                        {
                            accessPointThead.Abort();
                            threadsToAccessPoints.Remove(part.ObjectId);
                        }
                    }
                }

                threadsToAccessPoints.Add(part.ObjectId, InitAccessPointThread(part));
            }

        }

        private Thread InitAccessPointThread(AccessPointPart app)
        {
            return ThreadAdapter.DoCircleTask(
            () =>
            {
                if (lbResponseLog.Items.Count > 1000)
                {
                    lbResponseLog.Items.Clear();
                }
                if (lbResponseLog.Items.Count > 1000)
                {
                    lbResponseLog.Items.Clear();
                }
                #region 执行任务=》请求objectId对应的AccessPoint

                string url = app.Url + "/Pam/PamService/LoadRemindersWorkExecute";
                this.UIInvoke(() =>
                {
                    lbRequestLog.Items.Add("[" + app.ObjectId + "] " + url + "     " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                });


                HttpAdapter.postSyncAsJSON(url, (result, response) =>
                {
                    if ((bool)result.Success)
                    {
                        dynamic item;
                        //执行发送信息
                        IList<YYReminder> yyReminders = new List<YYReminder>();
                        YYReminder yyReminder;
                        //更新信息
                        Dictionary<string, string> IdCallNoDic = new Dictionary<string, string>();
                        foreach (var row in result.rows)
                        {
                            item = new ExpandoObject();
                            DynamicAdapter.Parse(item, XElement.Parse(row.ToString()));

                            yyReminder = new YYReminder();
                            yyReminder.dest = item.StringObjectDictionary.CallNo;
                            yyReminder.roomNum = item.StringObjectDictionary.RoomNo;
                            yyReminder.sickbedNum = string.IsNullOrEmpty(item.StringObjectDictionary.SickBedNo) ? "0" : item.StringObjectDictionary.SickBedNo;
                            yyReminder.repeatTimes = item.StringObjectDictionary.PlayRepeats;
                            yyReminder.warnType = "13001" + item.StringObjectDictionary.WorkItem;

                            IdCallNoDic.Add(item.StringObjectDictionary.Id, item.StringObjectDictionary.CallNo);
                            yyReminders.Add(yyReminder);
                        }

                        if (yyReminders.Count == 0)
                        {
                            return;
                        }

                        HttpAdapter.postAsyncAsJSON(sendReminderUrl, new { info = yyReminders }.ToStringObjectDictionary(), (ret, res) =>
                        {
                            if (ret != null)
                            {
                                //暂时不做处理 默认发送成功
                                /*item = Newtonsoft.Json.JsonConvert.DeserializeObject(ret);
                                Dictionary<string, string> successSendDic = new Dictionary<string, string>();
                                foreach (var key in item)
                                {
                                    if((string)key.result!="+OK")
                                    {
                                        successSendDic = IdCallNoDic.Where(s => s.Value == (string)key.dest) as Dictionary<string, string>;
                                    }
                                }
                                */
                                if (IdCallNoDic.Count > 0)
                                {
                                    HttpAdapter.postAsyncAsJSON(app.Url + "/Pam/PamService/UpdateWorkExecute", new { Ids = IdCallNoDic.Keys.ToArray() }.ToStringObjectDictionary(), (ret1, res1) =>
                                    {
                                        this.UIInvoke(() =>
                                        {
                                            lbResponseLog.Items.Insert(0, "[" + app.ObjectId + "] :     " + app.Url + "/Pam/PamService/UpdateWorkExecute");
                                        });
                                    });
                                }
                            }
                        });
                    }
                    else
                    {
                        this.UIInvoke(() =>
                        {
                            lbResponseLog.Items.Insert(0, "[" + app.ObjectId + "] ErrorMessage:     " + result.ErrorMessage);
                        });
                    }
                });

                #endregion
            },
            60000,
            () =>
            {
                return false;//永远执行
            },
            () =>
            {
                return string.IsNullOrEmpty(sendReminderUrl);//空URL 跳过，否则执行
            });
        }

        private void _ClearMessageLoop()
        {
            accessPointParts.Clear();
            foreach (var key in threadsToAccessPoints.Keys)
            {
                Thread accessPointThead = threadsToAccessPoints[key] as Thread;
                if (accessPointThead != null)
                {
                    if (accessPointThead.IsAlive)
                    {
                        accessPointThead.Abort();

                    }
                }
            }

            threadsToAccessPoints.Clear();
        }
        #endregion

        #endregion
    }


    public class YYReminder
    {
        public string dest { get; set; }
        public string roomNum { get; set; }
        public string sickbedNum { get; set; }
        public string repeatTimes { get; set; }
        public string warnType { get; set; }
    }
}
