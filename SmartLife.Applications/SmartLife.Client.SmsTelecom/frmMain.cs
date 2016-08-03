using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using win.core.utils;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using Newtonsoft.Json;

namespace SmartLife.Client.SmsTelecom
{
    public partial class frmMain : Form
    {
        #region 自定义变量
        string authEndPoint;
        string smsEndPoint;
        string ismgEndPoint;
        bool isRunning = false;
        System.Timers.Timer _tickTimer = null;


        StringObjectDictionary threadsToAccessPoints = new StringObjectDictionary();
        Dictionary<string, Thread> sendSmsThreadDic;
        Dictionary<string, Thread> getSmsThreadDic;
        #endregion

        public frmMain()
        {
            InitializeComponent();
        }


        #region 按钮事件
        private void tsbtn_frmSetting_Click(object sender, EventArgs e)
        {
            frmSetting frm = new frmSetting();

            frm.SectionSettingsSave += new dSectionSettingsSave((o, ce) =>
            {
                INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, ce.AuthEndPoint, Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_SMS_END_POINT, ce.SmsEndPoint, Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_ISMG_END_POINT, ce.IsmgEndPoint, Common.INI_FILE_PATH);
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

        #region 窗体事件
        private void frmMain_Load(object sender, EventArgs e)
        {
            authEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, Common.INI_FILE_PATH);
            smsEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_SMS_END_POINT, Common.INI_FILE_PATH);
            ismgEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_ISMG_END_POINT, Common.INI_FILE_PATH);

            lblMsg.Show();
            _tickTimer = new System.Timers.Timer(1 * 200);
            _tickTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs ee)
            {
                BeginInvoke(new Action(() =>
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
                }));

            });//到达时间的时候执行事件； 
            _tickTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            _tickTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 

            HttpAdapter.optionsAsyncTo(authEndPoint + "/", new { ApplicationId = "CS001" }.ToStringObjectDictionary(), (ret, res) =>
            {
                if (ret != "ok")
                {
                    MessageBoxAdapter.ShowError("无效的认证节点");
                    return;
                }

                this.UIInvoke(() =>
                {
                    lblMsg.Hide();
                });

                _tickTimer.Enabled = false;
                _tickTimer = null;

                InitSms();
            });

            

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBoxAdapter.ShowConfirm("您确定要退出吗", Properties.Settings.Default.MessageBoxTitle) == DialogResult.OK)
            {
                isRunning = false;
                //清空线程
                Thread thread;
                if (sendSmsThreadDic != null) {
                    foreach (var item in sendSmsThreadDic)
                    {
                        thread = item.Value;
                        if (thread != null && thread.IsAlive)
                        {
                            thread.Abort();
                            thread = null;
                        }
                        sendSmsThreadDic.Remove(item.Key);
                    }
                }
                if (getSmsThreadDic != null)
                {
                    foreach (var item in getSmsThreadDic)
                    {
                        thread = item.Value;
                        if (thread != null && thread.IsAlive)
                        {
                            thread.Abort();
                            thread = null;
                        }
                        getSmsThreadDic.Remove(item.Key);

                    }
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion


        #region 初始化处理短信业务库
        private void InitSms() {
            HttpAdapter.getAsyncTo(smsEndPoint + "/GetCityDeployNodes", null, new { ApplicationId = "CS001" }.ToStringObjectDictionary(), (ret, res) =>
            {
                if ((bool)ret.Success)
                {
                    if (ret.rows != null && ret.rows.lenght > 0)
                    {
                        _MessageLoopForSendSms_Call(ret.rows);
                        _MessageLoopForGetSms_Call(ret.rows);

                        //启动短信接收发送机制
                        isRunning = true;
                    }
                }
            });
        }

        //创建短信处理机制
        private void _MessageLoopForSendSms_Call(dynamic rows)
        {
            sendSmsThreadDic = new Dictionary<string, Thread>();
            var body = new { Status = "0" }.ToStringObjectDictionary();
            var header = new { ApplicationId = "CS001", Token = "", NodeId = "" }.ToStringObjectDictionary();
            foreach (var item in rows) {
                header["Token"] = item.Token;
                header["NodeId"] = item.NodeId;

                Thread thread = ThreadAdapter.DoCircleTask(
                () =>
                {
                    #region 执行任务=》检测发送短信消息
                    //同步请求
                    string url = (string)item.AccessPoint + "/Pub/SmsSendService/QueuedSendSms";
                    HttpAdapter.postSyncAsJSON(url, body, header , (result, response) =>
                    {
                        if ((bool)result.Success)
                        {
                            if (result.ret != null)
                            {
                                
                            }
                        }
                        else
                        {
                            Common.DebugLog(string.Format("---------------------ErrorMessage:{0} ", result.ErrorMessage));
                        }
                    });

                    #endregion
                },
                1000,
                () =>
                {
                    return !isRunning;
                },//未取到数据
                () =>
                {
                    return false;
                }//暂时永远不跳过
                );
                sendSmsThreadDic.Add((string)item.NodeId, thread);
                thread = null;
            }
        }

        private void _MessageLoopForGetSms_Call(dynamic rows)
        {
            getSmsThreadDic = new Dictionary<string, Thread>();
            foreach (var item in rows)
            {
                Thread thread = ThreadAdapter.DoCircleTask(
                () =>
                {
                    #region 执行任务=》检测接收短信消息
                    //同步请求
                    string url = ismgEndPoint;
                    HttpAdapter.postSyncAsJSON(url, (result, response) =>
                    {
                        if ((bool)result.Success)
                        {
                            if (result.ret != null)
                            {
                                
                            }
                        }
                        else
                        {
                            Common.DebugLog(string.Format("---------------------ErrorMessage:{0} ", result.ErrorMessage));
                        }
                    });

                    #endregion
                },
                1000,
                () =>
                {
                    return !isRunning;
                },//座席没登录则停止
                () =>
                {
                    return false;
                }//暂时永远不跳过
                );
                getSmsThreadDic.Add(item.NodeId, thread);
                thread = null;
            }
        }
        #endregion

        
    }
}
