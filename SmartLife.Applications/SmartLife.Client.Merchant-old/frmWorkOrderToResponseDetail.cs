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
using e0571.web.core.Utils;
using System.Xml;
using Newtonsoft.Json;
using System.Dynamic;
using System.Xml.Linq;

namespace SmartLife.Client.Merchant
{
    public partial class frmWorkOrderToResponseDetail : Form
    {
        public frmWorkOrderToResponseDetail()
        {
            InitializeComponent();
            xLoadingPanel.OnRotateStateChanged += new LoadingPanel.RotateStateChangedHandler(() => { });
        }

        string LastError { get; set; }

        public string AccessPoint { get; set; }
        public Guid? StationId { get; set; }
        public string WorkOrderId { get; set; }
        dynamic item;
        

        private void frmWorkOrderToResponseDetail_Load(object sender, EventArgs e)
        {
            InitForm();
            FetchData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SubmitData(); 
        }

        #region 帮助方法
        private void InitForm()
        {
            lblCurrentStation.Text = MerchantVar.CurrentMerchant.StationName; 
            
        } 
        #endregion

        #region 数据操作

        private void FetchData()
        {
            new Action(() =>
            {
                //string url = AccessPoint.Replace("http://115.236.175.110:17000/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Oca/WorkOrderService/GetWorkOrderInfo/" + WorkOrderId;
                string url = AccessPoint + "/Oca/WorkOrderService/GetWorkOrderInfo/" + WorkOrderId;
                HttpAdapter.getSyncTo(url, null, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode, StationId = StationId }.ToStringObjectDictionary(), (ret, res) =>
                {
                    if ((bool)ret.Success)
                    {
                        item = new ExpandoObject();
                        DynamicAdapter.Parse(item, XElement.Parse(ret.ret.ToString()));
                        LastError = null;
                    }
                    else
                    {
                        LastError = ret.ErrorMessage;
                    }
                });


            }).BeginInvoke(new AsyncCallback((ar) =>
            {
                //AsyncResult result = (AsyncResult)ar;
                this.UIInvoke(() =>
                {
                    if (item != null)
                    {
                        int doStatus = int.Parse(item.StringObjectDictionary.DoStatus);
                        IDictionary<string, object> dataItem = (item.StringObjectDictionary as IDictionary<string, object>);
                          
                        lblWorkOrderNo.Text = item.StringObjectDictionary.WorkOrderNo;
                        lblOperatedByName.Text = item.StringObjectDictionary.OperatedByName;
                        lblDoStatus.Text = WorkOrderInfo.getDoStatusName(doStatus);

                        foreach (var key in dataItem.Keys)
                        {
                            Control[] controlsOfInfo = tlpInfo.Controls.Find("lbl" + key, true);
                            if (controlsOfInfo.Length == 1)
                            {
                                (controlsOfInfo[0] as Label).Text = e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]);

                                if (key == "Gender")
                                {
                                    (controlsOfInfo[0] as Label).Text = WorkOrderInfo.getGenderName(e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]));
                                }
                                else if (key == "ServeFee" || key == "ServeFeeByGov" || key == "ServeFeeBySelf")
                                {
                                    if (e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]) != "")
                                    {
                                        (controlsOfInfo[0] as Label).Text = e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]) + "/小时";
                                    }
                                }
                            } 
                        }
                    }
                    xLoadingPanel.Stop();

                    if (!string.IsNullOrEmpty(LastError))
                    {
                        MessageBoxAdapter.ShowError(LastError);
                    }
                });
            }), null);
            xLoadingPanel.Start();
        }
        private void SubmitData()
        {
            if (item != null)
            {
                new Action(() =>
                {
                    int doStatus = int.Parse(item.StringObjectDictionary.DoStatus);

                    if (doStatus == 0)
                    {
                        //响应工单
                        if (MessageBoxAdapter.ShowConfirm("您确定要响应工单吗") == System.Windows.Forms.DialogResult.OK)
                        {
                            //string url = AccessPoint.Replace("http://115.236.175.110:17000/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Oca/WorkOrderService/ResponseWorkOrder";
                            string url = AccessPoint + "/Oca/WorkOrderService/ResponseWorkOrder";
                            HttpAdapter.postSyncAsJSON(url, new { WorkOrderId = Guid.Parse(WorkOrderId) }.ToStringObjectDictionary(), new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode, StationId = StationId }.ToStringObjectDictionary(), (ret, res) =>
                            {
                                if ((bool)ret.Success)
                                {
                                    LastError = null;
                                }
                                else
                                {
                                    LastError = ret.ErrorMessage;
                                }
                            });
                        }
                        else
                        {
                            LastError = Common.ERROR_USER_CANCEL;
                        }
                    } 

                }).BeginInvoke(new AsyncCallback((ar) =>
                {
                    //AsyncResult result = (AsyncResult)ar; 
                    this.UIInvoke(() =>
                    {
                        xLoadingPanel.Stop();

                        if (!string.IsNullOrEmpty(LastError))
                        {
                            if (LastError != Common.ERROR_USER_CANCEL)
                            {
                                MessageBoxAdapter.ShowError(LastError);
                            }
                        }
                        else
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    });
                }), null);

                xLoadingPanel.Start();


            }
        }
        #endregion
    }
}
