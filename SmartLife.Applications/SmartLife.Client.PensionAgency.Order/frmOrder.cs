using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Dynamic;

using win.tools.ic;
using win.core.controls;
using win.core.utils;
using e0571.web.core.TypeExtension;
using SmartLife.Client.PensionAgency.Models;


namespace SmartLife.Client.PensionAgency.Order
{
    public partial class frmOrder : Form
    {
        public frmOrder()
        {
            InitializeComponent();
        }

        private API _icAPI;
        private string _CurrentICNo = "0";
        private string _OldManId;
        bool isStop = false;

        private void frmOrder_Load(object sender, EventArgs e)
        { 
            MonitorICCardReader();
        }


        #region 加载icAPI
        public void LoadAPI(API icApi)
        {
            _icAPI = icApi;
        }
        #endregion

        #region 监测读卡器
        private void MonitorICCardReader()
        {
            this.UIDoCircleTask(() =>
            {
                _CurrentICNo = _icAPI.LoadIC();
                //Common.DebugLog("-----block _CurrentICNo:" + _CurrentICNo + " lblICNo:" + lblICNo.Text);
                if (("IC " + _CurrentICNo) != lblICNo.Text)
                {
                    if (_icAPI.AuthenticationKey(0))
                    {
                        _icAPI.Beep();

                        string read1 = _icAPI.ReadIC16Byte(1);
                        string read2 = _icAPI.ReadIC16Byte(2);

                        if ((read1 + read2) == "")
                        {
                            //空卡
                            lblTip.Text = "该卡未使用或已挂失！";
                            ClearOldManInfoUI();
                        }
                        else
                        {
                            //通过卡匹配老人
                            OldManInfo oldmanInfo = Data.OldMans.SingleOrDefault(item => item.IDNo == (read1 + read2) && item.ICNo == _CurrentICNo);
                            if (oldmanInfo != null)
                            {
                                lblTip.Text = "IC " + _CurrentICNo;
                                LoadOldManInfoUI(oldmanInfo);
                            }
                            else
                            {
                                lblTip.Text = "卡信息无效！";
                                ClearOldManInfoUI();
                            }
                        }
                    }
                }
                else
                {
                    if (_CurrentICNo == "0")
                    {
                        lblTip.Text = "请刷卡";
                    }
                }

            }, 700, () =>
            {
                return isStop;
            });


        }
        #endregion

        #region 清空老人信息界面
        private void ClearOldManInfoUI()
        {
            _OldManId = null;
            lblOldManName.Text = "-- -- --";
            lblGender.Text = "--";
            pbHeadPortrait.BackgroundImage = null;
            lblAge.Text = "(--)";
            lblIDNo.Text = "ID ------------------";
            lblICNo.Text = "IC ------------------";
            lblAddress.Text = "-- -- -- -- -- -- -- -- -- -- "; 

        }
        #endregion

        #region 加载老人信息界面
        private void LoadOldManInfoUI(OldManInfo oldmanInfo)
        {
            _OldManId = oldmanInfo.OldManId;
            lblOldManName.Text = oldmanInfo.OldManName;
            lblGender.Text = oldmanInfo.GenderName;
            pbHeadPortrait.BackgroundImage = lblGender.Text == "男" ? Properties.Resources.oldman_m : Properties.Resources.oldman_f;
            lblAge.Text = "(" + oldmanInfo.Age + "岁)";
            lblIDNo.Text = "ID " + oldmanInfo.IDNo.Substring(0, oldmanInfo.IDNo.Length - 6) + "******";
            lblICNo.Text = "IC " + _CurrentICNo;
            lblAddress.Text = oldmanInfo.Address;

            GetOrderInfo(_OldManId);
        }
        #endregion

        #region 获取老人配餐信息
        private void GetOrderInfo(string oldManId)
        {
            lblMT00001SetName.Text = lblMT00002SetName.Text = lblMT00003SetName.Text = "-- -- --";
            lblMT00001SetContent.Text = lblMT00002SetContent.Text = lblMT00003SetContent.Text = "-- -- -- -- -- -- -- -- --";
            btnConfirmMT00001.ForeColor = btnConfirmMT00002.ForeColor = btnConfirmMT00003.ForeColor = SystemColors.ControlLight;
            btnConfirmMT00001.Enabled = btnConfirmMT00002.Enabled = btnConfirmMT00003.Enabled = false;
            btnConfirmMT00001.Tag = btnConfirmMT00002.Tag = btnConfirmMT00003.Tag = null;
            btnConfirmMT00001.Text = btnConfirmMT00002.Text = btnConfirmMT00003.Text = "确认领餐";

            var meals = Data.BookMeals.Where(item => item.OldManId == oldManId);
            foreach (var meal in meals)
            {
                if (meal.MealType == "00001")
                {
                    lblMT00001SetName.Text = meal.SetMealName;
                    lblMT00001SetContent.Text = meal.SetMealContent;
                    if (meal.FetchFlag == "0")
                    {
                        btnConfirmMT00001.ForeColor = SystemColors.ControlText;
                        btnConfirmMT00001.Enabled = true;
                    }
                    else
                    {
                        if (Common.isInExhibition)
                        {
                            //展会模式允许循环使用
                            btnConfirmMT00001.ForeColor = SystemColors.ControlText;
                            btnConfirmMT00001.Enabled = true;
                            btnConfirmMT00001.Text = "循环使用";
                        }
                        else
                        {
                            btnConfirmMT00001.ForeColor = SystemColors.ControlLight;
                            btnConfirmMT00001.Enabled = false;
                            btnConfirmMT00001.Text = "确认领餐";
                        }

                       
                    }
                    btnConfirmMT00001.Tag = meal.SetMealId;
                }
                else if (meal.MealType == "00002")
                {
                    lblMT00002SetName.Text = meal.SetMealName;
                    lblMT00002SetContent.Text = meal.SetMealContent;
                    if (meal.FetchFlag == "0")
                    {
                        btnConfirmMT00002.ForeColor = SystemColors.ControlText;
                        btnConfirmMT00002.Enabled = true;
                    }
                    else
                    { 
                        if (Common.isInExhibition)
                        {
                            //展会模式允许循环使用
                            btnConfirmMT00002.ForeColor = SystemColors.ControlText;
                            btnConfirmMT00002.Enabled = true;
                            btnConfirmMT00002.Text = "循环使用";
                        }
                        else
                        {
                            btnConfirmMT00002.ForeColor = SystemColors.ControlLight;
                            btnConfirmMT00002.Enabled = false;
                            btnConfirmMT00002.Text = "确认领餐";
                        }
                    } 
                    btnConfirmMT00002.Tag = meal.SetMealId;
                }
                else if (meal.MealType == "00003")
                {
                    lblMT00003SetName.Text = meal.SetMealName;
                    lblMT00003SetContent.Text = meal.SetMealContent;
                    if (meal.FetchFlag == "0")
                    {
                        btnConfirmMT00003.ForeColor = SystemColors.ControlText;
                        btnConfirmMT00003.Enabled = true;
                    }
                    else
                    {
                        if (Common.isInExhibition)
                        {
                            //展会模式允许循环使用
                            btnConfirmMT00003.ForeColor = SystemColors.ControlText;
                            btnConfirmMT00003.Enabled = true;
                            btnConfirmMT00003.Text = "循环使用";
                        }
                        else
                        {
                            btnConfirmMT00003.ForeColor = SystemColors.ControlLight;
                            btnConfirmMT00003.Enabled = false;
                            btnConfirmMT00003.Text = "确认领餐";
                        }
                    }
                    btnConfirmMT00003.Tag = meal.SetMealId;
                }
            }
        }
        #endregion 

        #region 更新配餐信息
        private void ReloadBookMeals()
        {
            this.UIDoOnceTask(() =>
            {
                HttpAdapter.getSyncTo(SettingsVar.DataExchangePoint + "/Pam/PamService/GetOldManBookMealForToday", null, new { ApplicationId = Common.APPLICATION_ID, PACode = SettingsVar.BindingPACode }.ToStringObjectDictionary(), (ret, res) =>
                {
                    if ((bool)ret.Success)
                    {
                        Data.BookMeals = new List<BookMealInfo>();
                        foreach (var row in ret.rows)
                        {
                            dynamic item = new ExpandoObject();
                            DynamicAdapter.Parse(item, XElement.Parse(row.ToString()));
                            Data.BookMeals.Add((item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<BookMealInfo>());
                        }
                        MessageBoxAdapter.ShowInfo("更新配餐信息成功！");
                    }
                    else
                    { 
                        lblTip.Text = "更新配餐信息失败：" + (string)ret.ErrorCode;
                    }
                });
            });
        }
        #endregion

        #region 领取配餐
        private void FetchBookMeal(Button btn)
        {
            string mealType = btn.Name.Substring(btn.Name.Length - 5);
            string setMealId = btn.Tag as string;

            HttpAdapter.postSyncAsJSON(SettingsVar.DataExchangePoint + "/Pam/PamService/PAFetchBookMeal", new { OldManId = _OldManId.ToGuid(), MealType = mealType, SetMealId = setMealId, OperatedBy = Data.UserId }.ToStringObjectDictionary(), new { PACode = SettingsVar.BindingPACode, ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(), (ret, res) =>
            {
                if ((bool)ret.Success)
                {
                    MessageBoxAdapter.ShowInfo("领取配餐成功");
                    BookMealInfo bookMealInfo = Data.BookMeals.Single(item => item.OldManId == _OldManId && item.MealType == mealType && item.SetMealId == setMealId);
                    bookMealInfo.FetchFlag = "1";

                    if (Common.isInExhibition)
                    {
                        btn.ForeColor = SystemColors.ControlText;
                        btn.Enabled = true;
                        btn.Text = "循环使用";
                    }
                    else
                    {
                        btn.ForeColor = SystemColors.ControlLight;
                        btn.Enabled = false;
                        btn.Text = "确认领餐";
                    }
                   
                }
                else
                {
                    MessageBoxAdapter.ShowError((string)ret.ErrorMessage);
                }
            });
        }
        #endregion

        #region 循环使用
        private void ReuseBookMeal(Button btn)
        {
            string mealType = btn.Name.Substring(btn.Name.Length - 5);
            string setMealId = btn.Tag as string;

            HttpAdapter.postSyncAsJSON(SettingsVar.DataExchangePoint + "/Pam/PamService/PAReuseBookMeal", new { OldManId = _OldManId.ToGuid(), MealType = mealType, SetMealId = setMealId, OperatedBy = Data.UserId }.ToStringObjectDictionary(), new { PACode = SettingsVar.BindingPACode, ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(), (ret, res) =>
            {
                if ((bool)ret.Success)
                {
                    MessageBoxAdapter.ShowInfo("循环使用成功");
                    BookMealInfo bookMealInfo = Data.BookMeals.Single(item => item.OldManId == _OldManId && item.MealType == mealType && item.SetMealId == setMealId);
                    bookMealInfo.FetchFlag = "0";

                    btn.ForeColor = SystemColors.ControlText;
                    btn.Enabled = true;
                    btn.Text = "确认领餐"; 
                }
                else
                {
                    MessageBoxAdapter.ShowError((string)ret.ErrorMessage);
                }
            });
        }
        #endregion

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "确认领餐")
            {
                if (MessageBoxAdapter.ShowConfirm("确认要领餐么？") == System.Windows.Forms.DialogResult.OK)
                {
                    FetchBookMeal((sender as Button));
                }
            }
            else
            {
                if (MessageBoxAdapter.ShowConfirm("确认要循环使用么？") == System.Windows.Forms.DialogResult.OK)
                {
                    ReuseBookMeal((sender as Button));
                }
            }
            
        } 

        private void btnClose_Click(object sender, EventArgs e)
        {
            isStop = true;
            this.Close();
        }

        private void btnReloadData_Click(object sender, EventArgs e)
        {
            ReloadBookMeals();
        }
    }
}
