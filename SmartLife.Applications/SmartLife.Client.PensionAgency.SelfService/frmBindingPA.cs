using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using e0571.web.core.TypeExtension;
using win.core.controls;
using win.core.utils;

namespace SmartLife.Client.PensionAgency.SelfService
{
    public partial class frmBindingPA : Form
    {
        public frmBindingPA()
        {
            InitializeComponent();
        }
         
        public string ObjectId { get; set; }
        

        private void frmBindingPA_Load(object sender, EventArgs e)
        {
            InitForm();  
        }

        private void InitForm()
        {
            Util.SetBtnTransparency(btnBinding);
            Util.SetBtnTransparency(btnClose);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBinding_Click(object sender, EventArgs e)
        {
            BindingPA();
        }

        private string _LoadItem;
        private int dotNum;
        private bool allLoaded;
        private bool isBreak;

        private void BindingPA()
        {
            string bindingPACode = txtBindingCode.Text.Trim();
            ObjectId = bindingPACode.Substring(2, 6);
            if (ObjectId.Length < 6)
            {
                MessageBoxAdapter.ShowError("无效的PA码");
                return;
            }


            string authDataPoint = INIAdapter.ReadValue(Common.CFG_SECTION_WEB, Common.CFG_KEY_AUTH_DATA_POINT, Common.CFG_FILE_PATH);


            this.UIDoStepTasks(new List<Func<bool>>(){
                
                ()=>{
                    lblLoadItems.Text = _LoadItem = "获取业务服务器地址";
                    isBreak = false;
                    HttpAdapter.getSyncTo(authDataPoint + string.Format("/GetDeployNode/{0},{1},IC001", ObjectId, SettingsVar.RunMode), null, new { ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if ((bool)ret.Success)
                        {
                            SettingsVar.DataExchangePoint = (string)ret.ret.AccessPoint;
                            INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INT_KEY_DATA_EXCHANGE_POINT, SettingsVar.DataExchangePoint, Common.INI_FILE_PATH);
                        }
                        else
                        {
                            isBreak = true;
                            MessageBoxAdapter.ShowError((string)ret.ErrorMessage);
                        }
                    });

                    if(isBreak){
                        return false;
                    }
                    return true;
                },
                ()=>{
                    lblLoadItems.Text = _LoadItem = "绑定设备";
                    isBreak = false;
                    HttpAdapter.postSyncAsJSON(SettingsVar.DataExchangePoint + "/Pam/PamService/PADeviceBindingForSelfServiceTerminal", new { PACode = bindingPACode, MachineKey = Common.MachineKey, Action = "binding" }.ToStringObjectDictionary(), new { ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if ((bool)ret.Success)
                        {
                            SettingsVar.BindingPACode = bindingPACode;
                            INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INT_KEY_BINDING_PA_CODE, SettingsVar.BindingPACode, Common.INI_FILE_PATH);
                        }
                        else{
                            MessageBoxAdapter.ShowError((string)ret.ErrorCode);
                        }
                    });
                    if (isBreak)
                    {
                        return false;
                    }
                    return true;
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
             
    }
}
