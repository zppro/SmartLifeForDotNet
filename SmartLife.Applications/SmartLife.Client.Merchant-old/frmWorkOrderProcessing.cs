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
    

    public partial class frmWorkOrderProcessing : Form
    {
        List<WorkOrderInfo> datas;
        bool isFetchingData = false;

        public frmWorkOrderProcessing()
        {
            InitializeComponent();
            dateTimePickerArrivalTime.CustomFormat = @"yyyy\MM\dd (ddd) HH:mm:ss";
            dateTimePickerLeaveTime.CustomFormat = @"yyyy\MM\dd (ddd) HH:mm:ss";
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
                            //string url = nodeInfo.AccessPoint.Replace("http://115.236.175.110:17000/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Oca/WorkOrderService/GetWorkOrderProcessing";
                            string url = nodeInfo.AccessPoint + "/Oca/WorkOrderService/GetWorkOrderProcessing";
                            HttpAdapter.getSyncTo(url, null, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode, StationId = nodeInfo.StationId }.ToStringObjectDictionary(), (ret, res) =>
                            {
                                if ((bool)ret.Success)
                                {
                                    foreach (var row in ret.rows)
                                    { 
                                        dynamic item = new ExpandoObject();
                                        DynamicAdapter.Parse(item, XElement.Parse(row.ToString()));
                                        WorkOrderInfo dataItem = (item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<WorkOrderInfo>();
                                        dataItem.DoStatusName = WorkOrderInfo.getDoStatusName(int.Parse(item.StringObjectDictionary.DoStatus));
                                        dataItem.AccessPoint = nodeInfo.AccessPoint;
                                        dataItem.StationId = nodeInfo.StationId;

                                        datas.Add(dataItem);
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
            c2.DataPropertyName = "ServiceTimeRequired";
            c2.Name = c2.DataPropertyName;
            c2.HeaderText = "要求服务时间";
            c2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c2.Width = 150;
            dgvList.Columns.Add(c2);

            DataGridViewTextBoxColumn c3 = new DataGridViewTextBoxColumn();
            c3.DataPropertyName = "WorkOrderContent";
            c3.Name = c3.DataPropertyName;
            c3.HeaderText = "工单内容";
            c3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c3.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c3.Width = 300;
            dgvList.Columns.Add(c3);


            DataGridViewTextBoxColumn c5 = new DataGridViewTextBoxColumn();
            c5.DataPropertyName = "OldManName";
            c5.Name = c5.DataPropertyName;
            c5.HeaderText = "老人姓名";
            c5.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c5.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c5.Width = 120;
            dgvList.Columns.Add(c5);

            DataGridViewTextBoxColumn c6 = new DataGridViewTextBoxColumn();
            c6.DataPropertyName = "DoStatusName";
            c6.Name = c6.DataPropertyName;
            c6.HeaderText = "工单状态";
            c6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c6.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c6.Width = 120;
            dgvList.Columns.Add(c6);
        }

        #endregion 

        private void frmWorkOrderProcessing_Load(object sender, EventArgs e)
        {
            InitSearchBox();
            InitGridView();
            
        }

        private void frmWorkOrderProcessing_Shown(object sender, EventArgs e)
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
            frmWorkOrderProcessingDetail frm = new frmWorkOrderProcessingDetail();
            WorkOrderInfo  currentWorkOrderInfo =  (xSource[dgvList.CurrentRow.Index] as WorkOrderInfo);
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
