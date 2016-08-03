using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using win.core.utils;
using e0571.web.core.TypeExtension;
using e0571.web.core.Utils;
using win.core.update;
using Newtonsoft.Json;

namespace SmartLife.Client.Merchant
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        public event dSectionWebSettingsSave SectionWebSettingsSave;
        public event dSectionBizSettingsSave SectionBizSettingsSave;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

#if V2X
        private void btnOk_Click(object sender, EventArgs e)
        {
            //
            RemindType rt = EnumAdapter.GetEnum<RemindType>(e0571.web.core.Utils.TypeConverter.ChangeString(cbRemindType.SelectedValue));

            if (SectionBizSettingsSave != null)
            {
                SectionBizSettingsSave(this, new SectionBizSettingsSaveEventArgs { Type = rt, YYRemindFlag = (chkPlayYY.Checked ? 1 : 0) });
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
#else 
        private void btnOk_Click(object sender, EventArgs e)
        {
            //
            string authEndPoint = formatAuthEndPoint();
            RemindType rt = EnumAdapter.GetEnum<RemindType>(e0571.web.core.Utils.TypeConverter.ChangeString(cbRemindType.SelectedValue));
            string connectUrl = authEndPoint + "/";

            HttpAdapter.optionsAsyncTo(connectUrl, new { ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(), (ret, res) =>
            {
                if (ret != "ok")
                {
                    MessageBoxAdapter.ShowError("无效的认证节点");
                    return;
                }

                if (SectionWebSettingsSave != null)
                {
                    SectionWebSettingsSave(this, new SectionWebSettingsSaveEventArgs { AuthEndPoint = authEndPoint });
                }

                if (SectionBizSettingsSave != null)
                {
                    SectionBizSettingsSave(this, new SectionBizSettingsSaveEventArgs { Type = rt, YYRemindFlag = (chkPlayYY.Checked ? 1 : 0) });
                }

                this.DialogResult = DialogResult.OK;

                this.UIInvoke(() =>
                {
                    this.Close();
                });

            });
            
           
        }
#endif

        private string formatAuthEndPoint()
        {
            string testUrl;
            if (txtAuthEndPoint.Text.Trim().StartsWith("http://"))
            {
                testUrl = txtAuthEndPoint.Text.Trim();
            }
            else
            {
                testUrl = "http://" + txtAuthEndPoint.Text.Trim();
            }
            if (testUrl.EndsWith("/"))
            {
                testUrl = testUrl.Substring(0, testUrl.Length - 1);
            }
            return testUrl;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            txtAuthEndPoint.Text = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, Common.INI_FILE_PATH);
            cbRemindType.DataSource = EnumAdapter.ToListItem(typeof(RemindType)).ToList();
            cbRemindType.ValueMember = "Value";
            cbRemindType.DisplayMember = "Text";
            string s = INIAdapter.ReadValue(Common.INI_SECTION_BIZ, Common.INI_KEY_REMIND_TYPE, Common.INI_FILE_PATH);
            cbRemindType.SelectedValue = Convert.ToInt32(INIAdapter.ReadValue(Common.INI_SECTION_BIZ, Common.INI_KEY_REMIND_TYPE, Common.INI_FILE_PATH));
            chkPlayYY.Checked = INIAdapter.ReadValue(Common.INI_SECTION_BIZ, Common.INI_KEY_YYREMIND_FLAG, Common.INI_FILE_PATH) == "1" ? true : false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string delegateApplicationId = "PM001";//Common.APPLICATION_ID
            string updateFolder = AppDomain.CurrentDomain.BaseDirectory + delegateApplicationId;
            win.core.utils.FileAdapter.EnsurePath(updateFolder);
            win.core.utils.FileAdapter.Copy(Updater.AppName, updateFolder + @"\" + Updater.AppName, true);
            foreach (var file in Updater.DepandenceFiles)
            {
                win.core.utils.FileAdapter.Copy(file, AppDomain.CurrentDomain.BaseDirectory + delegateApplicationId + @"\" + file, true);
            }

            string updateEndPoint = INIAdapter.ReadValue(Common.CFG_SECTION_WEB, Common.CFG_KEY_UPDATE_END_POINT, Common.CFG_FILE_PATH);
            string args = String.Format("-n {0} -v {1} -c {2}", AppAdapter.GetName(), VersionAdapter.GetPureVersion(VersionType.ASSEMBLY), updateEndPoint + "/" + delegateApplicationId);
            System.Diagnostics.Process.Start(updateFolder + @"\" + Updater.AppName, args);
        }

        private void btnAreaSync_Click(object sender, EventArgs e)
        {
            //远程获取deployNodeObjects
            string authDataPoint = INIAdapter.ReadValue(Common.CFG_SECTION_WEB, Common.CFG_KEY_AUTH_DATA_POINT, Common.CFG_FILE_PATH);
            ThreadAdapter.DoOnceTask(() =>
            {
                HttpAdapter.getSyncTo(authDataPoint + "/GetDeployNodeObjects", (ret, res) =>
                {
                    if ((bool)ret.Success)
                    {
                        string strDeployNodeObjects = JsonConvert.SerializeObject(ret.rows);
                        INIAdapter.WriteValue(Common.INI_SECTION_DATA, Common.INI_KEY_DEPLOY_NODE_OBJECTS, strDeployNodeObjects, Common.INI_FILE_PATH);
                        MessageBoxAdapter.ShowInfo("区域同步成功");
                    }
                    else
                    {
                        MessageBoxAdapter.ShowError(ret.ErrorMessage.ToString());
                    }

                });
            });
        }


    }
}
