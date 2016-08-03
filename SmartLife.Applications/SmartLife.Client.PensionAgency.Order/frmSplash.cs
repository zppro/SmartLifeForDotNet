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
using e0571.web.core.Security;
using win.tools.ic;
using SmartLife.Client.PensionAgency.Models;

namespace SmartLife.Client.PensionAgency.Order
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
        }
        private void frmSplash_Activated(object sender, EventArgs e)
        {
            if (txtUserName.Text != "")
            {
                txtPassword.Focus();
            }
        }
        private void InitForm()
        {
            Util.SetBtnTransparency(btnOK);
            lblTechSupport.Text = Properties.Settings.Default.Company;

            txtUserName.Text = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_USER_NAME_SINCE_LAST, Common.INI_FILE_PATH);
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
            {
                MessageBoxAdapter.ShowError("请输入用户名");
                return;
            }
            if (txtPassword.Text.Trim() == "")
            {
                MessageBoxAdapter.ShowError("请输入密码");
                return;
            }

            DoLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtUserName.Text.Trim() == "")
                {
                    MessageBoxAdapter.ShowError("请输入用户名");
                    return;
                }
                if (txtPassword.Text.Trim() == "")
                {
                    MessageBoxAdapter.ShowError("请输入密码");
                    return;
                }
                DoLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            }
        }

        #region 登录
        private void DoLogin(string userCode, string password)
        {
            //
            btnOK.SafeButtonEnable(false);

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
                            btnOK.SafeButtonEnable(true);
                            isBreak = false; 
                        }
                        else{
                            isBreak = true;
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
                    #region 读取服务机构编码
                    lblLoadItems.Text = _LoadItem = "读取服务机构编码";
                    isBreak = false;
                    Application.DoEvents();
                    if(string.IsNullOrEmpty(SettingsVar.BindingPACode)){
                        frmBindingPA frm = new frmBindingPA();
                        frm.ShowDialog();
                        if (frm.DialogResult != System.Windows.Forms.DialogResult.OK)
                        {
                            isBreak = true;
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            btnOK.SafeButtonEnable(true);
                            return false;
                        }
                        else
                        {
                            this.Activate();
                        }
                    } 
                    return true;
                    #endregion
                },
                ()=>{
                    #region 用户认证
                    lblLoadItems.Text = _LoadItem = "用户认证";
                    isBreak = false;
                    Application.DoEvents();
                    string authEndPoint = INIAdapter.ReadValue(Common.CFG_SECTION_WEB, Common.CFG_KEY_AUTH_END_POINT, Common.CFG_FILE_PATH);
                    string objectId = SettingsVar.BindingPACode.Substring(2, 6);
                    HttpAdapter.postSyncAsJSON(authEndPoint, new { RunMode = SettingsVar.RunMode, ObjectId = objectId, UserCode = userCode, PasswordHash = MD5Provider.Generate(password) }.ToStringObjectDictionary(), new { ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if ((bool)ret.Success)
                        {
                            Data.UserId = Guid.Parse((string)ret.ret.UserId);
                            SettingsVar.DataExchangePoint = (string)ret.ret.AccessPoint;
                            INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_USER_NAME_SINCE_LAST, userCode, Common.INI_FILE_PATH);
                        }
                        else
                        {
                            MessageBoxAdapter.ShowError((string)ret.ErrorMessage);
                            isBreak = true;
                            btnOK.SafeButtonEnable(true);
                        } 

                    }); 
                    if(isBreak){
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

                            //MessageBoxAdapter.ShowInfo("老人：" + Data.OldMans.Count.ToString());
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

        #endregion

        

    }
}
