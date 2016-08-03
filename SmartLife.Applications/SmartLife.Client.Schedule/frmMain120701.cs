using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json;
using win.core.utils;
using e0571.web.core.TypeExtension;

using SmartLife.Share.Models.Inc;


namespace SmartLife.Client.Schedule
{
    public partial class frmMain120701 : Form
    {
        #region 内部变量
        string _SourceAddress;
        string _TargetAddressToSaveOrder;
        Thread _FetchServiceWorkOrderThread;
        bool isEnableFetchServiceWorkOrder;
        bool isPauseFetchServiceWorkOrder;
        Thread _SaveOrderToRemoteThread;
        bool isSaving;
        Queue<dynamic> queues = new Queue<dynamic>();
        #endregion

        public frmMain120701()
        {
            InitializeComponent();
        }

        private void frmMain120701_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height - 5;
            this.MaximumSize = new Size(Width, Height);
            Text = Properties.Settings.Default.ProductName + " " + Assembly.GetEntryAssembly().GetName().Version.ToString();
             
            _SourceAddress = INIAdapter.ReadValue(Common.INI_SECTION_SYNC, Common.INI_KEY_SOURCE_BASE_ADDRESS, Common.INI_FILE_PATH);
            _TargetAddressToSaveOrder = INIAdapter.ReadValue(Common.INI_SECTION_SYNC, Common.INI_KEY_TARGET_ADDRESS_TO_SAVE_ORDER, Common.INI_FILE_PATH); 
        }

        #region 帮助方法
        private void _FetchServiceWorkOrderContinuously()
        {
            _FetchServiceWorkOrderThread = ThreadAdapter.DoCircleTask(
                () =>
                {
                    #region 执行任务=》获取需要同步到其他系统的工单

                    dOnAppendToOutput dc = new dOnAppendToOutput(_OnAppendToOutput);
                    this.Invoke(dc, new object[] { "获取需要同步到其他系统的工单..." });

                    //同步请求
                    string url = _SourceAddress + "/p12/c07/A01/GetSyncOutWorkOrders";

                    HttpAdapter.getSyncTo(url, (result, response) =>
                    {
                        if ((bool)result.Success)
                        {
                            this.Invoke(dc, new object[] { string.Format("###获取工单成功:{0}条 ", result.rows.Count) });
                            foreach (var row in result.rows)
                            {
                                queues.Enqueue(row);
                            }
                        }
                        else
                        {
                            this.Invoke(dc, new object[] { string.Format("#####获取工单出错:{0} ", result.ErrorMessage) });
                        }
                    });

                    #endregion
                },
                1000,//1秒执行一次
                () =>
                {
                    return !isEnableFetchServiceWorkOrder;
                },//没有允许取数据则停止
                () =>
                {
                    return queues.Count > 0 || isPauseFetchServiceWorkOrder;
                }//当数据队列不为空则跳过
                );
        }
        private void _SaveOrderToRemoteContinuously()
        {
            _SaveOrderToRemoteThread = ThreadAdapter.DoCircleTask(
                () =>
                {
                    #region 执行任务=》将工单提交到远程服务器

                    if (queues.Count == 0)
                    {
                        return;
                    }

                    dOnAppendToOutput dc = new dOnAppendToOutput(_OnAppendToOutput);
                    this.Invoke(dc, new object[] { "将工单提交到远程服务器..." });

                    isSaving = true;
                    var item = queues.Dequeue();
                    try
                    { 
                        HttpAdapter.postSyncAsForm(_TargetAddressToSaveOrder, new
                        {
                            unique = item.WorkOrderNo,
                            service = item.Service,
                            linkman = item.LinkMan,
                            linkphone = (item.LinkPhone == "" ? "没有linkphone" : item.LinkPhone),
                            note = item.Note,
                            accept_time = item.AcceptTime,
                            pie_time = item.PieTime,
                            finish_time = item.FinishTime,
                            service_time = item.ServieTime,
                            back_note = item.BackNote,
                            status = item.Status,
                            satisficing = item.Satisficing,
                            old_man_unique = item.OldManUnique,
                            longitude = item.LongitudeS,
                            latitude = item.LatitudeS
                        }.ToStringObjectDictionary(), (result, response) =>
                        {
                            if ((bool)result.flag)
                            {

                                this.Invoke(dc, new object[] { string.Format("#####提交工单成功:<{0}> ", item.WorkOrderNo) });

                                string url = _SourceAddress + "/p12/c07/A01/MarkSyncOutWorkOrder";

                                HttpAdapter.postSyncAsJSON(url, new { Id = item.Id }.ToStringObjectDictionary(), (result1, response1) =>
                                {
                                    if ((bool)result1.Success)
                                    {
                                        //将提交成功的工单设置同步标识
                                        this.Invoke(dc, new object[] { string.Format("###设置工单同步标识成功:<{0}> ", item.WorkOrderNo) });
                                    }
                                    else
                                    {
                                        this.Invoke(dc, new object[] { string.Format("#####设置工单同步标识出错:{0} ", result1.ErrorMessage) });
                                    }
                                });
                            }
                            else
                            {
                                this.Invoke(dc, new object[] { string.Format("#####提交工单出错:{0} ", result.msg) });
                            }
                        });

                        
                    }
                    catch(Exception ex) {
                        this.Invoke(dc, new object[] { ex.Message });
                    }
                    finally
                    {
                        isSaving = false;
                    }
                    #endregion
                },
                5000,//5秒执行一次
                () =>
                {
                    return false;
                },//一直运行
                () =>
                {
                    return queues.Count == 0 || isSaving;
                }//当数据队列为空时跳过
                );
        }
        private void _Clean()
        {
            if (_FetchServiceWorkOrderThread != null && _FetchServiceWorkOrderThread.IsAlive)
            {
                _FetchServiceWorkOrderThread.Abort();
                _FetchServiceWorkOrderThread = null;
            }

            if (_SaveOrderToRemoteThread != null && _SaveOrderToRemoteThread.IsAlive)
            {
                _SaveOrderToRemoteThread.Abort();
                _SaveOrderToRemoteThread = null;
            }
        }
        private void _OnAppendToOutput(string msg)
        {
            if (txtOutput.GetLineFromCharIndex(txtOutput.Text.Length) > 500)
            {
                txtOutput.Text = "";
            }

            txtOutput.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + " >> " + msg + "\r\n");
            txtOutput.SelectionStart = txtOutput.Text.Length;
            txtOutput.ScrollToCaret();
            Common.schedule120701.Info(msg);
        } 
        #endregion

        #region 按钮事件处理
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!isEnableFetchServiceWorkOrder)
            {
                isEnableFetchServiceWorkOrder = true;
                isPauseFetchServiceWorkOrder = false;
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnPause.Enabled = true;
                btnResume.Enabled = false;
                _FetchServiceWorkOrderContinuously();
                _SaveOrderToRemoteContinuously();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isEnableFetchServiceWorkOrder = false;
            isPauseFetchServiceWorkOrder = false;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnResume.Enabled = false;
            _Clean();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            isPauseFetchServiceWorkOrder = true;
            btnPause.Enabled = false;
            btnResume.Enabled = true;
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            isPauseFetchServiceWorkOrder = false;
            btnPause.Enabled = true;
            btnResume.Enabled = false;
        }
        #endregion


        private void frmMain120701_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (queues.Count > 0)
            {
                if (MessageBoxAdapter.ShowConfirm("还有数据没有处理完成，您确定要退出吗", Properties.Settings.Default.MessageBoxTitle) == DialogResult.OK)
                {
                    _Clean();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                _Clean();
            }
        }

        
        

    }
}
