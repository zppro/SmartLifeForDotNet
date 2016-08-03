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
    public partial class frmWorkOrderProcessedDetail : Form
    {
        public frmWorkOrderProcessedDetail()
        {
            InitializeComponent();
            xLoadingPanel.OnRotateStateChanged += new LoadingPanel.RotateStateChangedHandler(() => { });
        }

        string LastError { get; set; }

        public string AccessPoint { get; set; }
        public Guid? StationId { get; set; }
        public string WorkOrderId { get; set; }

        dynamic item;

        private void frmWorkOrderProcessedDetail_Load(object sender, EventArgs e)
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
             
            dtpServeBeginTime.Visible = false;
            dtpServeEndTime.Visible = false;
            IEnumerable<RadioButton> rbs1 = pnlServeResultName.Controls.OfType<RadioButton>();
            foreach (var rb in rbs1)
            {
                rb.Visible = false;
            }
            txtServeResultRemark.Visible = false;

            dtpServeBeginTime.ValueChanged += new EventHandler(dtp_ValueChanged);
            dtpServeEndTime.ValueChanged += new EventHandler(dtp_ValueChanged);

            //回访页签中的控件隐藏
            IEnumerable<RadioButton> rbs2 = pnlFeedbackToOldManName.Controls.OfType<RadioButton>();
            foreach (var rb in rbs2)
            {
                rb.Visible = false;
            }
            txtFeedbackRemarkToOldMan.Visible = false;
        }
        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            if (dtp.Checked)
            {
                dtp.CustomFormat = "yyyy-MM-dd HH:mm";
            }
            else
            {
                dtp.CustomFormat = " ";
            }
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
                        if (doStatus > 0 && doStatus < 4)
                        {
                            if (e0571.web.core.Utils.TypeConverter.ChangeString(dataItem["ServeResult"]) == ""
                            ||
                            e0571.web.core.Utils.TypeConverter.ChangeString(dataItem["FeedbackToOldMan"]) == ""
                            )
                            {
                                //处置督办 
                                dtpServeBeginTime.Visible = true;
                                dtpServeEndTime.Visible = true;

                                IEnumerable<RadioButton> rbsOfServeResultName = pnlServeResultName.Controls.OfType<RadioButton>();
                                foreach (var rb in rbsOfServeResultName)
                                {
                                    rb.Visible = true;
                                }
                                txtServeResultRemark.Visible = true;

                                IEnumerable<RadioButton> rbsOfFeedbackToOldManName = pnlFeedbackToOldManName.Controls.OfType<RadioButton>();
                                foreach (var rb in rbsOfFeedbackToOldManName)
                                {
                                    rb.Visible = true;
                                }

                                txtFeedbackRemarkToOldMan.Visible = true;
                            }
                        }

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

                            if (dtpServeBeginTime.Visible && dtpServeBeginTime.Name == ("dtp" + key))
                            {
                                dtpServeBeginTime.Text = e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]);
                            }
                            if (dtpServeEndTime.Visible && dtpServeEndTime.Name == ("dtp" + key))
                            {
                                dtpServeEndTime.Text = e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]);
                            }
                            if (pnlServeResultName.Name == ("pnl" + key + "Name"))
                            {
                                IEnumerable<RadioButton> rbsOfServeResult = pnlServeResultName.Controls.OfType<RadioButton>().Where(x => x.Visible == true);
                                if (rbsOfServeResult.Count() > 0)
                                {
                                    pnlServeResultName.BackColor = Color.LightGreen;
                                }
                                foreach (var rb in rbsOfServeResult)
                                {
                                    if (rb.Name == "rb" + e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]) + "Of11016")
                                    {
                                        rb.Checked = true;
                                        break;
                                    }
                                }
                            }
                            if (txtServeResultRemark.Visible && txtServeResultRemark.Name == ("txt" + key))
                            {
                                txtServeResultRemark.Text = e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]);
                            }
                            if (pnlFeedbackToOldManName.Name == ("pnl" + key + "Name"))
                            {
                                IEnumerable<RadioButton> rbsOfReturnVisit = pnlFeedbackToOldManName.Controls.OfType<RadioButton>().Where(x => x.Visible == true);
                                if (rbsOfReturnVisit.Count() > 0)
                                {
                                    pnlFeedbackToOldManName.BackColor = Color.LightGreen;
                                }
                                foreach (var rb in rbsOfReturnVisit)
                                {
                                    if (rb.Name == "rb" + e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]) + "Of11017")
                                    {
                                        rb.Checked = true;
                                        break;
                                    }
                                }
                            }
                            if (txtFeedbackRemarkToOldMan.Visible && txtFeedbackRemarkToOldMan.Name == ("txt" + key))
                            {
                                txtFeedbackRemarkToOldMan.Text = e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]);
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
                         
                    }
                    else
                    {
                        //商家填写工单服务过程信息 
                        if (MessageBoxAdapter.ShowConfirm("您确定要提交工单服务处理与反馈吗") == System.Windows.Forms.DialogResult.OK)
                        {
                            bool canSubmit = true;
                            StringObjectDictionary postParam = new { WorkOrderId = Guid.Parse(WorkOrderId),  ServeResultRemark = txtServeResultRemark.Text.Trim(), FeedbackRemarkToOldMan = txtFeedbackRemarkToOldMan.Text.Trim() }.ToStringObjectDictionary();

                            if (txtOtherCharges.Text.Trim() != "")
                            {
                                postParam["OtherCharges"] = txtOtherCharges.Text.Trim();
                            }

                            decimal otherChargesFee;
                            if (canSubmit)
                            {
                                if (txtOtherChargesFee.Text.Trim() != "")
                                {
                                    if (decimal.TryParse(txtOtherChargesFee.Text.Trim(), out otherChargesFee))
                                    {
                                        postParam["OtherChargesFee"] = otherChargesFee;
                                    }
                                    else
                                    {
                                        LastError = "其他收费必须是数字";
                                        canSubmit = false;
                                    }
                                }
                            }

                            DateTime serveBeginTime, serveEndTime;
                            if (canSubmit)
                            {

                                if (DateTime.TryParse(dtpServeBeginTime.Text, out serveBeginTime))
                                {
                                    postParam["ServeBeginTime"] = serveBeginTime;
                                }
                                else
                                {
                                    LastError = "必须填写服务开始时间";
                                    canSubmit = false;
                                }


                            }
                            if (canSubmit)
                            {
                                if (DateTime.TryParse(dtpServeEndTime.Text, out serveEndTime))
                                {
                                    postParam["ServeEndTime"] = serveEndTime;
                                }
                                else
                                {
                                    LastError = "必须填写服务结束时间";
                                    canSubmit = false;
                                }
                            }
                            if (canSubmit)
                            {
                                RadioButton rbOfServeResultName = pnlServeResultName.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                                if (rbOfServeResultName != null)
                                {
                                    string serveResult = rbOfServeResultName.Name.Substring(2, 5);
                                    postParam["ServeResult"] = serveResult;
                                }
                                else
                                {
                                    LastError = "必须填写服务结果";
                                    canSubmit = false;
                                }
                            }

                            if (canSubmit)
                            {
                                RadioButton rbOfFeedbackToOldManName = pnlFeedbackToOldManName.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                                if (rbOfFeedbackToOldManName != null)
                                {
                                    string feedbackToOldMan = rbOfFeedbackToOldManName.Name.Substring(2, 5);
                                    postParam["FeedbackToOldMan"] = feedbackToOldMan;
                                }
                                else
                                {
                                    LastError = "必须评价老人";
                                    canSubmit = false;
                                }
                            }


                            if (canSubmit)
                            {
                                //string url = AccessPoint.Replace("http://115.236.175.110:17000/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Oca/WorkOrderService/InputWorkOrder";
                                string url = AccessPoint + "/Oca/WorkOrderService/InputWorkOrder";
                                HttpAdapter.postSyncAsJSON(url, postParam, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode, StationId = StationId }.ToStringObjectDictionary(), (ret, res) =>
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
