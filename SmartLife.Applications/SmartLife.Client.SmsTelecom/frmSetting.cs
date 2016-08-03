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


namespace SmartLife.Client.SmsTelecom
{
    public partial class frmSetting : Form
    {
        public event dSectionSettingsSave SectionSettingsSave;

        public frmSetting()
        {
            InitializeComponent();
        }


        #region 帮助方法
        private string formatEndPoint(string strurl)
        {
            string testUrl;
            if (strurl.StartsWith("http://"))
            {
                testUrl = strurl;
            }
            else
            {
                testUrl = "http://" + strurl;
            }
            if (testUrl.EndsWith("/"))
            {
                testUrl = testUrl.Substring(0, testUrl.Length - 1);
            }
            return testUrl;
        }
        #endregion


        #region 窗体事件
        private void frmSetting_Load(object sender, EventArgs e)
        {
            txtAuthEndPoint.Text = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, Common.INI_FILE_PATH);
            txtSmsEndPoint.Text = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_SMS_END_POINT, Common.INI_FILE_PATH);
            txtISMGEndPoint.Text = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_ISMG_END_POINT, Common.INI_FILE_PATH);
        }

        #endregion


        #region 按钮事件
        private void btnOk_Click(object sender, EventArgs e)
        {
            string authEndPoint = formatEndPoint(txtAuthEndPoint.Text.Trim());
            string smsEndPoint = formatEndPoint(txtSmsEndPoint.Text.Trim());
            string ismgEndPoint = formatEndPoint(txtISMGEndPoint.Text.Trim());

            string connectUrl = authEndPoint + "/";
            HttpAdapter.optionsAsyncTo(connectUrl, new { ApplicationId = "CS001" }.ToStringObjectDictionary(), (ret, res) =>
            {
                if (ret != "ok")
                {
                    MessageBoxAdapter.ShowError("无效的认证节点");
                    return;
                }

                if (SectionSettingsSave != null)
                {
                    SectionSettingsSave(this, new SectionSettingsSaveEventArgs
                    {
                        AuthEndPoint = authEndPoint,
                        SmsEndPoint = smsEndPoint,
                        IsmgEndPoint = ismgEndPoint
                    });
                }

                this.DialogResult = DialogResult.OK;

                this.UIInvoke(() =>
                {
                    this.Close();
                });

            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}
