using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using win.core.controls;
using win.core.utils;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using System.Xml;
using Newtonsoft.Json;
using System.Dynamic;
using System.Xml.Linq;
using System.Reflection;


namespace SmartLife.Client.Merchant
{
    public partial class frmMain : Form
    {
        #region Timers
        private System.Timers.Timer fetchDataTimer;
        private System.Timers.Timer raiseRemindTimer;

        frmWorkOrderNotify notifyWindow = new frmWorkOrderNotify();  //实例化类MainForm的一个对象
        Queue<WorkOrderReminderStatInfo> queuesOfWorkOrderReminderStatInfo = new Queue<WorkOrderReminderStatInfo>();
        Queue<WorkOrderReminderInfo> queuesOfWorkOrderReminderInfo = new Queue<WorkOrderReminderInfo>();
        #endregion

        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {

            this.Top = 0;
            this.Left = 0;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 5;
            this.MaximumSize = new Size(Width, Height);

            Text = Properties.Settings.Default.ProductName + " " + Assembly.GetEntryAssembly().GetName().Version.ToString();

            tlblUserCodeName.Text = "当前商家：" + MerchantVar.CurrentMerchant.StationName;

            tvNav.Nodes[0].Nodes.Clear();
            tvNav.Nodes[0].Nodes.AddRange(Common.sourceTypes.Select(item => new TreeNode { Name = item.Value.ToString(), Text = item.Text }).ToArray());
            tvNav.ExpandAll();

            notifyWindow.ClickReminderInfoWin += new dNotifyWorkOrderReminderInfo(OnClickReminderInfoWin);
            notifyWindow.ClickReminderStatInfoWin += new dNotifyWorkOrderReminderStatInfo(OnClickReminderStatInfoWin);
            notifyWindow.IgnoreReminderInfo += new dIgnoreWorkOrderReminderInfo(OnIgnoreReminderInfo);
            notifyWindow.IgnoreReminderStatInfo += new dIgnoreWorkOrderReminderStatInfo(OnIgnoreReminderStatInfo);
            notifyWindow.CloseWin += new Action(OnCloseWin);

            if (SettingsVar.CurrentRemindType != RemindType.不提醒)
            {
                fetchDataTimer = new System.Timers.Timer(1000 * 5);
                fetchDataTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs ee)
                {
                    fetchDataTimer.Enabled = false;
                    //frmWorkOrderNotify.IconFlickerFlag = true;
                    //notifyWindow.ReceiveReminderInfo(new WorkOrderReminderInfo { RemindContent = "您有新工单需要响应" + DateTime.Now.ToString("HH:mm:ss"), SourceType = "A0101" });
                    FetchReminderData();
                    fetchDataTimer.Enabled = true;
                });
                fetchDataTimer.Start();

                raiseRemindTimer = new System.Timers.Timer(1000 * 20);
                raiseRemindTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs ee)
                {
                    raiseRemindTimer.Enabled = false;
                    frmWorkOrderNotify.IconFlickerFlag = true;
                    if (SettingsVar.CurrentRemindType == RemindType.详细信息)
                    {
                        if (queuesOfWorkOrderReminderInfo.Count > 0)
                        {
                            var xItem = queuesOfWorkOrderReminderInfo.Dequeue();
                            Console.WriteLine(xItem.RemindContent);
                            notifyWindow.ReceiveReminderInfo(xItem);
                        }
                        else
                        {
                            raiseRemindTimer.Enabled = true;
                        }
                    }
                    else if (SettingsVar.CurrentRemindType == RemindType.统计信息)
                    {
                        if (queuesOfWorkOrderReminderStatInfo.Count > 0)
                        {
                            notifyWindow.ReceiveReminderStatInfo(queuesOfWorkOrderReminderStatInfo.Dequeue());
                        }
                        else
                        {
                            raiseRemindTimer.Enabled = true;
                        }
                    }
                });
                raiseRemindTimer.Start();
            }


        }

        private void FetchReminderData()
        {
            if (MerchantVar.CurrentMerchant.AuthNodeInfos.Count > 0)
            {
                foreach (var nodeInfo in MerchantVar.CurrentMerchant.AuthNodeInfos)
                {
                    string url = nodeInfo.AccessPoint + "/Pub/ReminderService/GetReminderStatGroupBySourceType";
                    if (SettingsVar.CurrentRemindType == RemindType.详细信息)
                    {
                        url = nodeInfo.AccessPoint + "/Pub/ReminderService/GetReminderItems";
                    }
                    url += "/Oca_ServiceWorkOrder,WorkOrderId";
                    HttpAdapter.getSyncTo(url, null, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationId =nodeInfo.StationId }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if ((bool)ret.Success)
                        {
                            foreach (var row in ret.rows)
                            {
                                dynamic item = new ExpandoObject();
                                DynamicAdapter.Parse(item, XElement.Parse(row.ToString()));
                                if (SettingsVar.CurrentRemindType == RemindType.详细信息)
                                {
                                    WorkOrderReminderInfo dataItem = (item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<WorkOrderReminderInfo>();

                                    if (queuesOfWorkOrderReminderInfo.Count(it => it.SourceType == dataItem.SourceType && it.SourceKey == dataItem.SourceKey) == 0)
                                    {
                                        //不存在
                                        dataItem.AccessPoint = nodeInfo.AccessPoint;
                                        dataItem.StationId = nodeInfo.StationId;
                                        queuesOfWorkOrderReminderInfo.Enqueue(dataItem);
                                    }
                                }
                                else if (SettingsVar.CurrentRemindType == RemindType.统计信息)
                                {
                                    WorkOrderReminderStatInfo dataItem = (item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<WorkOrderReminderStatInfo>();
                                    if (queuesOfWorkOrderReminderStatInfo.Count(it => it.SourceType == dataItem.SourceType) == 0)
                                    {
                                        dataItem.AccessPoint = nodeInfo.AccessPoint;
                                        dataItem.StationId = nodeInfo.StationId;
                                        queuesOfWorkOrderReminderStatInfo.Enqueue(dataItem);
                                    }
                                    else
                                    {
                                        WorkOrderReminderStatInfo xItem = queuesOfWorkOrderReminderStatInfo.SingleOrDefault(it => it.SourceType == dataItem.SourceType);
                                        if (xItem != null)
                                        {
                                            if (xItem.StationId == nodeInfo.StationId)
                                            {
                                                xItem.ReminderNum = dataItem.ReminderNum;
                                            }
                                            else
                                            {
                                                //不同区域
                                                dataItem.AccessPoint = nodeInfo.AccessPoint;
                                                dataItem.StationId = nodeInfo.StationId;
                                                queuesOfWorkOrderReminderStatInfo.Enqueue(dataItem);
                                            }
                                        } 
                                    }
                                }  
                            }
                        } 
                    });
                }
            }
           
        }

        private void OnClickReminderInfoWin(WorkOrderReminderInfo item)
        {
            if (item.SourceType == "A0101")
            {
                //待响应的工单 
                frmWorkOrderToResponseDetail frm = new frmWorkOrderToResponseDetail();  
                frm.AccessPoint = item.AccessPoint;
                frm.StationId = item.StationId;
                frm.WorkOrderId = item.SourceKey;
                frm.ShowDialog();
            }
            else if (item.SourceType == "A0102")
            {
                //处理中的工单
                frmWorkOrderProcessingDetail frm = new frmWorkOrderProcessingDetail(); 
                frm.AccessPoint = item.AccessPoint;
                frm.StationId = item.StationId;
                frm.WorkOrderId = item.SourceKey;
                frm.ShowDialog();

            }
            raiseRemindTimer.Enabled = true;
        }
        private void OnClickReminderStatInfoWin(WorkOrderReminderStatInfo item)
        {
            TreeNode[] nodes = tvNav.Nodes.Find(item.SourceType, true);
            if (nodes.Length == 1)
            {
                tvNav.SelectedNode = nodes[0];
                NodeSelect(nodes[0]);
            }
            raiseRemindTimer.Enabled = true;
        }

        private void OnIgnoreReminderInfo(WorkOrderReminderInfo item)
        {
            //string url = item.AccessPoint.Replace("http://115.236.175.110:17001/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Pub/ReminderService/IgnoreReminder";
            HttpAdapter.postAsyncAsJSON(item.AccessPoint + "/Pub/ReminderService/IgnoreReminder", new { ResponseAppType = "00001", ObjectType = "Merchant", ObjectKey = item.StationId, SourceTable = "Oca_ServiceWorkOrder", SourceColumn = "WorkOrderId", SourceType = item.SourceType, SourceKey = item.SourceKey }.ToStringObjectDictionary(), new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode, StationId = item.StationId }.ToStringObjectDictionary(), (ret, res) =>
            {

            });
        }
        private void OnIgnoreReminderStatInfo(WorkOrderReminderStatInfo item)
        {
            //string url = item.AccessPoint.Replace("http://115.236.175.110:17001/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Pub/ReminderService/IgnoreReminder";
            HttpAdapter.postAsyncAsJSON(item.AccessPoint + "/Pub/ReminderService/IgnoreReminder", new { ResponseAppType = "00001", ObjectType = "Merchant", ObjectKey = item.StationId, SourceTable = "Oca_ServiceWorkOrder", SourceColumn = "WorkOrderId", SourceType = item.SourceType }.ToStringObjectDictionary(), new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode, StationId = item.StationId }.ToStringObjectDictionary(), (ret, res) =>
            {
                //MessageBoxAdapter.ShowDebug(ret.ErrorMessage);
            });
        }
        private void OnCloseWin()
        {
            raiseRemindTimer.Enabled = true;
        }

        #region NodeSelect
        private void NodeSelect(TreeNode node)
        {
            Control[] findForms = spcMain.Panel2.Controls.Find("frm_" + node.Name, false);
            if (findForms.Length == 1)
            {
                //找到  
                findForms[0].BringToFront();
                if (findForms[0] is frmWorkOrderToResponse)
                {
                    (findForms[0] as frmWorkOrderToResponse).RefreshDataGridView();
                }
                else if (findForms[0] is frmWorkOrderProcessing)
                {
                    (findForms[0] as frmWorkOrderProcessing).RefreshDataGridView();
                }
                else if (findForms[0] is frmWorkOrderFinished)
                {
                    (findForms[0] as frmWorkOrderFinished).RefreshDataGridView();
                }
            }
            else
            {
                //没找到
                Form frm = null;
                switch (node.Name)
                {
                    case "A0101":
                        //待响应的工单
                        frm = new frmWorkOrderToResponse(); 
                        break;
                    case "A0102":
                        //处理中的工单
                        frm = new frmWorkOrderProcessing(); 
                        break;
                    case "A0103":
                        frm = new frmWorkOrderProcessed();
                        break;
                    case "A0104": 
                         //已完成的工单查询
                        frm = new frmWorkOrderFinished();
                        break;
                    default:
                        break;
                }
                if (frm != null)
                {
                    frm.Name = "frm_" + node.Name;
                    frm.TopLevel = false;
                    frm.Visible = true;
                    frm.Dock = DockStyle.Fill;
                    spcMain.Panel2.Controls.Add(frm);
                    frm.BringToFront();
                    frm.Show(); 
                }
            } 
        }
        #endregion

        private void tvNav_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                NodeSelect(e.Node);
            }
        }

        private void tsmiNodeSetting_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();

            frm.SectionWebSettingsSave += new dSectionWebSettingsSave((o, ce) =>
            {

                INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, ce.AuthEndPoint, Common.INI_FILE_PATH);
            });
            frm.SectionBizSettingsSave += new dSectionBizSettingsSave((o, ce) =>
            {
                SettingsVar.CurrentRemindType = ce.Type;
                INIAdapter.WriteValue(Common.INI_SECTION_BIZ, Common.INI_KEY_REMIND_TYPE, ((int)ce.Type).ToString(), Common.INI_FILE_PATH);
            });
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (MessageBoxAdapter.ShowConfirm("您必须重新启动程序设置才能生效，是否重启程序？", Properties.Settings.Default.MessageBoxTitle) == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Restart();
                }
            }
        }
        //配置服务
        private void tsmiServiceSetting_Click(object sender, EventArgs e)
        {
            frmServiceSettings frm = new frmServiceSettings();
            frm.ShowDialog();
        }
    }
}
