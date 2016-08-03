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

using e0571.web.core.TypeExtension;
using win.core.controls;
using win.core.utils;
using win.tools.ic;
using SmartLife.Client.PensionAgency.Models;

namespace SmartLife.Client.PensionAgency.SelfService
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private string _LoadItem;
        private int dotNum;
        private bool allLoaded;
        private bool isBreak;
        private void frmSplash_Load(object sender, EventArgs e)
        {

            InitForm();

            this.UIDoStepTasks(new List<Func<bool>>(){
                ()=>{
                    #region 检查硬件环境
                    lblLoadItems.Text = _LoadItem = "检查硬件环境";
                    Application.DoEvents();
                    API icAPI = new API();
                    icAPI.InitIC();
                    if(icAPI.IcDev<0){
                        if (MessageBoxAdapter.ShowConfirm("初始化IC读卡设备失败，请确认是否已连接IC读卡设备！是否要跳过该步骤？") == System.Windows.Forms.DialogResult.OK)
                        { 
                            isBreak = false;
                        }
                        else{
                            isBreak = true;
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            return false;
                        }
                    }
                    else{
                        icAPI.ExitIC();
                    }
                    isBreak = false;
                    return true;
                    #endregion
                },
                ()=>{
                    #region 获取业务服务器地址
                    lblLoadItems.Text = _LoadItem = "获取业务服务器地址";
                    isBreak = false;
                    Application.DoEvents();
                    if(string.IsNullOrEmpty(SettingsVar.DataExchangePoint)){
                        frmBindingPA frm = new frmBindingPA();
                        frm.ShowDialog();
                        if(frm.DialogResult != System.Windows.Forms.DialogResult.OK){
                            isBreak = true;
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            return false;
                        }
                        else{
                            this.Activate();
                        }
                    }
                    return true;
                    #endregion
                },
                ()=>{
                    #region 连接到业务服务器
                    lblLoadItems.Text = _LoadItem = "连接到业务服务器";
                    isBreak = false;
                    Application.DoEvents();
                    int errorCode = 0;
                    HttpAdapter.postSyncAsJSON(SettingsVar.DataExchangePoint + "/Pam/PamService/PADeviceBindingForSelfServiceTerminal", new { PACode = SettingsVar.BindingPACode, MachineKey = Common.MachineKey, Action = "check" }.ToStringObjectDictionary(), new { ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if(!(bool)ret.Success){
                            errorCode = (int)ret.ErrorCode;
                            lblError.Text = ret.ErrorCode;
                        }
                    });
                    if (errorCode == 53010 || errorCode==53011)
                    {
                        //设备未绑定或已解绑
                        frmBindingPA frm = new frmBindingPA();
                        frm.ShowDialog();
                        if (frm.DialogResult != System.Windows.Forms.DialogResult.OK)
                        {
                            isBreak = true;
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            return false;
                        }
                    }
                    else if(errorCode==53012){
                        //失效
                        MessageBoxAdapter.ShowError("您的设备使用期已到，请联系管理员！");
                        isBreak = true;
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        return false;
                    }  
                    return true;
                    #endregion
                },
                ()=>{
                    #region 更新机构信息
                    lblLoadItems.Text = _LoadItem = "更新机构信息";
                    isBreak = false;
                    Application.DoEvents();
                    HttpAdapter.getSyncTo(SettingsVar.DataExchangePoint + "/Pam/PamService/GetPAStationInfo", null, new { ApplicationId = Common.APPLICATION_ID, PACode = SettingsVar.BindingPACode }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if ((bool)ret.Success)
                        {
                            dynamic item = new ExpandoObject();
                            DynamicAdapter.Parse(item, XElement.Parse(ret.ret.ToString()));
                            PAStationInfo stationInfo = (item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<PAStationInfo>();
                            SettingsVar.BindingPAName = stationInfo.StationName;
                            INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INT_KEY_BINDING_PA_NAME, SettingsVar.BindingPAName, Common.INI_FILE_PATH);
                        }
                        else
                        {
                            isBreak = true;
                            lblError.Text = ret.ErrorCode;
                        }
                    });
                    if (isBreak)
                    {
                        return false;
                    } 
                    return true;
                    #endregion
                },
                ()=>{
                    #region 更新老人数据
                    lblLoadItems.Text = _LoadItem = "更新老人数据";
                    isBreak = false;
                    Application.DoEvents();
                    HttpAdapter.getSyncTo(SettingsVar.DataExchangePoint + "/Pam/PamService/GetOldManInfoForSelfServiceMachine", null, new { ApplicationId = Common.APPLICATION_ID, PACode = SettingsVar.BindingPACode }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if ((bool)ret.Success)
                        {
                            Data.OldMans = new List<OldManInfo>(); 
                            foreach (var row in ret.rows)
                            {
                                dynamic item = new ExpandoObject();
                                DynamicAdapter.Parse(item, XElement.Parse(row.ToString()));
                                Data.OldMans.Add((item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<OldManInfo>());
                            }
                        }
                        else
                        {
                            isBreak = true;
                            lblError.Text = ret.ErrorCode;
                        }
                    });
                    if (isBreak)
                    {
                        return false;
                    } 
                    return true;
                    #endregion
                },
                ()=>{
                    #region 更新配餐数据
                    lblLoadItems.Text = _LoadItem = "更新配餐数据";
                    isBreak = false;
                    Application.DoEvents();
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

                            //MessageBoxAdapter.ShowInfo("订餐：" + Data.BookMeals.Count.ToString());
                        }
                        else
                        {
                            isBreak = true;
                            lblError.Text = ret.ErrorCode;
                        }
                    });
                    if (isBreak)
                    {
                        return false;
                    } 
                    return true;
                    #endregion
                },
                ()=>{
                    #region 更新课程数据
                    lblLoadItems.Text = _LoadItem = "更新课程数据";
                    isBreak = false;
                    Application.DoEvents();
                    HttpAdapter.getSyncTo(SettingsVar.DataExchangePoint + "/Pam/PamService/LoadStationCourseList", null, new { ApplicationId = Common.APPLICATION_ID, PACode = SettingsVar.BindingPACode }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if ((bool)ret.Success)
                        {
                            Data.Courses = new List<CourseInfo>(); 
                            foreach (var row in ret.rows)
                            {
                                dynamic item = new ExpandoObject();
                                DynamicAdapter.Parse(item, XElement.Parse(row.ToString()));
                                Data.Courses.Add((item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<CourseInfo>());
                            }

                            //MessageBoxAdapter.ShowInfo("订餐：" + Data.BookMeals.Count.ToString());
                        }
                        else
                        {
                            isBreak = true;
                            lblError.Text = ret.ErrorCode;
                        }
                    });
                    if (isBreak)
                    {
                        return false;
                    } 
                    return true;
                    #endregion
                },
                ()=>{ 
                    allLoaded = true;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    return true;
                }
            }, Common.msDelay);
             
            lblLoadItems.UIDoCircleTask(() =>
            {
                if (dotNum == 3)
                {
                    lblLoadItems.Text = _LoadItem;
                    dotNum = 0;
                }
                else
                {
                    lblLoadItems.Text += ".";
                    dotNum++;
                }
            }, Common.msDot, () =>
            {
                return allLoaded || isBreak;
            });
        }

        private void InitForm()
        {
            lblTechSupport.Text = Properties.Settings.Default.Company;
        }
    }
}
