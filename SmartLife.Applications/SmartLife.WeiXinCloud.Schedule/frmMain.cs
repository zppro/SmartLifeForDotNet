using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using win.core.utils;
using System.Threading;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using SmartLife.Share.Models.Auth; 

namespace SmartLife.WeiXinCloud.Schedule
{
    public partial class frmMain : Form
    {
        bool isRunning = false; 
        List<AccessPointPart> accessPointParts = new List<AccessPointPart>();
        StringObjectDictionary threadsToAccessPoints = new StringObjectDictionary();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height - 5;
            this.MaximumSize = new Size(Width, Height);
            Text = Properties.Settings.Default.ProductName + " " + VersionAdapter.GetVersion(VersionType.ASSEMBLY);
            
        }


        #region 帮助方法

        #region 获取访问点
        private void FetchAccessPoints(Action success)
        {
            string authEndPoint = INIAdapter.ReadValue(Common.CFG_SECTION_WEB, Common.CFG_KEY_AUTH_END_POINT, Common.CFG_FILE_PATH);
            string strRunMode = INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_RUN_MODE, Common.INI_FILE_PATH);
            if (strRunMode == "")
            {
                strRunMode = "1";
            }
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
            //检测CTI_Call
            txtObjectId.Text = string.Join(",", accessPointParts.Select(item => item.ObjectId).ToArray());
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

                threadsToAccessPoints.Add(part.ObjectId, ThreadAdapter.DoCircleTask(
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

                    string url = part.Url + "/api/share/v1/SendWXMessageSchedule";
                    this.UIInvoke(() =>
                    {
                        lbRequestLog.Items.Add("[" + part.ObjectId + "] " + url + "     " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    });
                    
                    
                    HttpAdapter.postSyncAsJSON(url, (result, response) =>
                    {
                        if ((bool)result.Success)
                        {
                            lbResponseLog.Items.Insert(0, "[" + part.ObjectId + "] " + result.ret);
                        }
                        else
                        {
                            lbResponseLog.Items.Insert(0, "[" + part.ObjectId + "] ErrorMessage:     " + result.ErrorMessage);
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
                ));
            }
            
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

        #region

        #endregion

        #endregion

        private void frmMain_Shown(object sender, EventArgs e)
        {
            
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                btnStartStop.Text = "开始";
                isRunning = false;
                _ClearMessageLoop();
            }
            else
            {
                FetchAccessPoints(() =>
                {
                    btnStartStop.Text = "结束";
                    isRunning = true;
                    _MessageLoopForAccessPoints();
                });
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ClearMessageLoop();
        }
    }
}
