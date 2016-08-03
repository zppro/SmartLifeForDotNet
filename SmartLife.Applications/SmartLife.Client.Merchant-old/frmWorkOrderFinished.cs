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


namespace SmartLife.Client.Merchant
{
    public partial class frmWorkOrderFinished : Form
    {
        List<WorkOrderInfo> datas;
        bool isFetchingData = false;

        public frmWorkOrderFinished()
        {
            InitializeComponent();
            dateTimePickerArrivalTime.CustomFormat = @"yyyy-MM-dd HH:mm:ss";
            dateTimePickerLeaveTime.CustomFormat = @"yyyy-MM-dd HH:mm:ss";
            dateTimePickerArrivalTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            dateTimePickerLeaveTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            xLoadingPanel.OnRotateStateChanged += new LoadingPanel.RotateStateChangedHandler(() => { });
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        #region 数据相关
        public void RefreshDataGridView()
        {
            dgvList.DataSource = null;
            FetchData();
            dgvList.DataSource = xSource;
        }
        private void FetchData()
        {
            if (!isFetchingData)
            {
                if (datas == null)
                {
                    datas = new List<WorkOrderInfo>();
                }
                else
                {
                    datas.Clear();
                }

                new Action(() =>
                {
                    isFetchingData = true;
                    if (MerchantVar.CurrentMerchant.AuthNodeInfos.Count > 0)
                    {
                        foreach (var nodeInfo in MerchantVar.CurrentMerchant.AuthNodeInfos)
                        {
                            //string url = nodeInfo.AccessPoint.Replace("http://115.236.175.110:17000/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Oca/WorkOrderService/GetWorkOrderFinished";
                            //string url = nodeInfo.AccessPoint + "/Oca/WorkOrderService/GetWorkOrderFinished";
                            string url = "http://localhost/SmartLife.CertManage.MerchantServices/Oca/WorkOrderService/GetWorkOrderFinished";
                            //string OldManName = txbOldManName.Text.Trim();
                            //string ServeManArriveTime = dateTimePickerArrivalTime.Text;
                            //string ServeManLeaveTime = dateTimePickerLeaveTime.Text;

                            //设置传递的参数
                            StringObjectDictionary postPram = new StringObjectDictionary();
                            if (txbOldManName.Text.Trim() != "")
                            {
                                postPram["OldManName"] = txbOldManName.Text.Trim();
                            }
                            else
                            {
                                postPram["OldManName"] = null;
                            }
                            //满意度
                            IEnumerable<RadioButton> rbsOfReturnVisit = pnlFeedbackToOldManName.Controls.OfType<RadioButton>().Where(x => x.Checked == true);
                            if (rbsOfReturnVisit != null)
                            {
                                foreach (var rb in rbsOfReturnVisit)
                                {
                                    if (rb.Name.Remove(0, 2).Remove(5) != "00000")
                                    {
                                        postPram["FeedbackToServiceStation"] = rb.Name.Remove(0, 2).Remove(5);
                                    }
                                    else
                                    {
                                        postPram["FeedbackToServiceStation"] = null;
                                    }
                                }
                            }
                            else
                            {
                                postPram["FeedbackToServiceStation"] = null;
                            }
                            //服务人员到达时间
                            postPram["ServeManArriveTime_Start"] = dateTimePickerArrivalTime.Text;

                            HttpAdapter.postSyncAsJSON(url, postPram, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode, StationId = nodeInfo.StationId }.ToStringObjectDictionary(), (ret, res) =>
                            {
                                if ((bool)ret.Success)
                                {
                                    System.TimeSpan ts = new TimeSpan();
                                    foreach (var row in ret.rows)
                                    {
                                        dynamic item = new ExpandoObject();
                                        DynamicAdapter.Parse(item, XElement.Parse(row.ToString()));
                                        WorkOrderInfo dataItem = (item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<WorkOrderInfo>();
                                        dataItem.DoStatusName = WorkOrderInfo.getDoStatusName(int.Parse(item.StringObjectDictionary.DoStatus));
                                        dataItem.AccessPoint = nodeInfo.AccessPoint;
                                        dataItem.StationId = nodeInfo.StationId;
                                        ts = DateTime.Parse(dataItem.ServeManLeaveTime).Subtract(DateTime.Parse(dataItem.ServeManArriveTime));
                                        dataItem.ServiceTime = ts.TotalHours.ToString("0.00") + "小时";
                                        if (numUDServiceTimeFrom.Text.Trim() != "" && numUDServiceTimeTo.Text.Trim() != "")
                                        {
                                            if (int.Parse(numUDServiceTimeFrom.Text.Trim()) <= float.Parse(ts.TotalHours.ToString("0.00")) && float.Parse(ts.TotalHours.ToString("0.00")) <= int.Parse(numUDServiceTimeTo.Text.Trim()))
                                            {
                                                datas.Add(dataItem);
                                            }
                                        }
                                        else if (numUDServiceTimeFrom.Text.Trim() == "" && numUDServiceTimeTo.Text.Trim() != "")
                                        {
                                            if (float.Parse(ts.TotalHours.ToString("0.00")) <= int.Parse(numUDServiceTimeTo.Text.Trim()))
                                            {
                                                datas.Add(dataItem);
                                            }
                                        }
                                        else if (numUDServiceTimeFrom.Text.Trim() != "" && numUDServiceTimeTo.Text.Trim() == "")
                                        {
                                            if (int.Parse(numUDServiceTimeFrom.Text.Trim()) <= float.Parse(ts.TotalHours.ToString("0.00")))
                                            {
                                                datas.Add(dataItem);
                                            }
                                        }
                                        else if (numUDServiceTimeFrom.Text.Trim() == "" && numUDServiceTimeTo.Text.Trim() == "")
                                        {
                                            datas.Add(dataItem);
                                        }
                                    }
                                }
                            });
                        }
                    }


                }).BeginInvoke(new AsyncCallback((ar) =>
                {
                    //AsyncResult result = (AsyncResult)ar;
                    this.UIInvoke(() =>
                    {
                        xLoadingPanel.Stop();
                        xSource.DataSource = datas;
                        xSource.ResetBindings(false);
                    });

                    isFetchingData = false;
                }), null);
                xLoadingPanel.Start();
            }
        }

        #endregion

        #region 帮助方法
        private void InitSearchBox()
        {

        }
        private void InitGridView()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.Columns.Clear();

            DataGridViewTextBoxColumn c0 = new DataGridViewTextBoxColumn();
            c0.DataPropertyName = "WorkOrderId";
            c0.Name = c0.DataPropertyName;
            c0.HeaderText = "工单ID";
            c0.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c0.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c0.Width = 0;
            c0.Visible = false;
            dgvList.Columns.Add(c0);

            DataGridViewTextBoxColumn c1 = new DataGridViewTextBoxColumn();
            c1.DataPropertyName = "WorkOrderNo";
            c1.Name = c1.DataPropertyName;
            c1.HeaderText = "工单编号";
            c1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c1.Width = 180;
            dgvList.Columns.Add(c1);


            DataGridViewTextBoxColumn c2 = new DataGridViewTextBoxColumn();
            c2.DataPropertyName = "OldManName";
            c2.Name = c2.DataPropertyName;
            c2.HeaderText = "老人姓名";
            c2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c2.Width = 80;
            dgvList.Columns.Add(c2);

            DataGridViewTextBoxColumn c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "Address";
            c3.Name = c3.DataPropertyName;
            c3.HeaderText = "地址";
            c3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c3.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c3.Width = 150;
            dgvList.Columns.Add(c3);

            DataGridViewTextBoxColumn c4 = new DataGridViewTextBoxColumn();
            c4.DataPropertyName = "WorkOrderContent";
            c4.Name = c4.DataPropertyName;
            c4.HeaderText = "工单内容";
            c4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c4.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c4.Width = 150;
            dgvList.Columns.Add(c4);


            DataGridViewTextBoxColumn c5 = new DataGridViewTextBoxColumn();
            c5.DataPropertyName = "ServiceTimeRequired";
            c5.Name = c5.DataPropertyName;
            c5.HeaderText = "要求服务时间";
            c5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c5.Width = 130;
            dgvList.Columns.Add(c5);

            DataGridViewTextBoxColumn c6 = new DataGridViewTextBoxColumn();
            c6.DataPropertyName = "ServeManArriveTime";
            c6.Name = c6.DataPropertyName;
            c6.HeaderText = "服务人员到达时间";
            c6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c6.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c6.Width = 130;
            dgvList.Columns.Add(c6);

            DataGridViewTextBoxColumn c7 = new DataGridViewTextBoxColumn();
            c7.DataPropertyName = "ServeManLeaveTime";
            c7.Name = c7.DataPropertyName;
            c7.HeaderText = "服务人员离开时间";
            c7.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c7.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c7.Width = 130;
            dgvList.Columns.Add(c7);


            DataGridViewTextBoxColumn c8 = new DataGridViewTextBoxColumn();
            c8.DataPropertyName = "FeedbackToServiceStationName";
            c8.Name = c8.DataPropertyName;
            c8.HeaderText = "满意度";
            c8.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c8.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c8.Width = 70;
            dgvList.Columns.Add(c8);


            DataGridViewTextBoxColumn c9 = new DataGridViewTextBoxColumn();
            c9.DataPropertyName = "DoStatusName";
            c9.Name = c9.DataPropertyName;
            c9.HeaderText = "工单状态";
            c9.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c9.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c9.Width = 80;
            dgvList.Columns.Add(c9);

            //
            //   
            DataGridViewTextBoxColumn c10 = new DataGridViewTextBoxColumn();
            c10.DataPropertyName = "ServiceTime";
            c10.Name = c10.DataPropertyName;
            c10.HeaderText = "服务时长";
            c10.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c10.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c10.Width = 80;
            dgvList.Columns.Add(c10);
        }

        #endregion

        private void frmWorkOrderToResponse_Load(object sender, EventArgs e)
        {
            InitSearchBox();
            InitGridView();

        }

        private void frmWorkOrderToResponse_Shown(object sender, EventArgs e)
        {
            btnQuery.Left = spA.Width - btnQuery.Width - 20;
            xLoadingPanel.Left = (dgvList.Width - xLoadingPanel.Width) / 2;
            xLoadingPanel.Top = (dgvList.Height - xLoadingPanel.Height) / 2;
            RefreshDataGridView();
        }

        private void dgvList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBoxAdapter.ShowDebug("RowEnter:" + e.RowIndex);
        }

        private void dgvList_DoubleClick(object sender, EventArgs e)
        {
            frmWorkOrderFinishedDetail frm = new frmWorkOrderFinishedDetail();
            WorkOrderInfo currentWorkOrderInfo = (xSource[dgvList.CurrentRow.Index] as WorkOrderInfo);
            frm.AccessPoint = currentWorkOrderInfo.AccessPoint;
            frm.StationId = currentWorkOrderInfo.StationId;
            frm.WorkOrderId = currentWorkOrderInfo.WorkOrderId;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //刷新
                RefreshDataGridView();
            }
        }
    }
}
