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
                    SectionBizSettingsSave(this, new SectionBizSettingsSaveEventArgs { Type = rt });
                }

                this.DialogResult = DialogResult.OK;

                this.UIInvoke(() =>
                {
                    this.Close();
                });

            });
            
           
        }

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
            
        }


    }
}
