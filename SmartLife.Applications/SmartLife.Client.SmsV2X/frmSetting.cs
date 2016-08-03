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

namespace SmartLife.Client.SmsV2X
{
    public partial class frmSetting : Form
    {
        public event dSectionSettingsSave SectionSettingsSave;

        public frmSetting()
        {
            InitializeComponent();
        }

        #region 窗口事件
        private void frmSetting_Load(object sender, EventArgs e)
        {
            txtAuthEndPoint.Text = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, Common.INI_FILE_PATH);
            txtAuthSmsEndPoint.Text = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTHSMS_END_POINT, Common.INI_FILE_PATH);
            txtSmsEndPoint.Text = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_SMS_END_POINT, Common.INI_FILE_PATH);

            Dictionary<string, string> txtDictionary = Common.StrToDictionary(INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_TXT_SMS_SETTING, Common.INI_FILE_PATH), '&');
            if (txtDictionary.Count > 2)
            {
                txt_SpCode.Text = txtDictionary["SpCode"];
                txt_LoginName.Text = txtDictionary["LoginName"];
                txt_Password.Text = txtDictionary["Password"];
            }
        }

        #endregion

        #region 按钮事件
        private void btn_ok_Click(object sender, EventArgs e)
        {
            string authEndPoint = formatEndPoint(txtAuthEndPoint.Text.Trim());
            string authSmsEndPoint = formatEndPoint(txtAuthSmsEndPoint.Text.Trim());
            string smsEndPoint = formatEndPoint(txtSmsEndPoint.Text.Trim());

            string showError = "";
            showError = txt_SpCode.Text.Trim();
            if (string.IsNullOrEmpty(showError))
            {
                MessageBoxAdapter.ShowError("企业编号不能为空！");
                return;
            }
            showError = txt_LoginName.Text.Trim();
            if (string.IsNullOrEmpty(showError))
            {
                MessageBoxAdapter.ShowError("用户名不能为空！");
                return;
            }
            showError = txt_Password.Text.Trim();
            if (string.IsNullOrEmpty(showError))
            {
                MessageBoxAdapter.ShowError("密码不能为空！");
                return;
            }

            string txtSmsSetting = "SpCode=" + txt_SpCode.Text.Trim() + "&LoginName=" + txt_LoginName.Text.Trim() + "&Password=" + txt_Password.Text.Trim();
            string connectUrl = authEndPoint + "/";
            HttpAdapter.optionsAsyncTo(connectUrl,new { ApplicationId = "CS001" }.ToStringObjectDictionary(), (ret, res) =>
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
                        AuthSmsEndPoint = authSmsEndPoint,
                        SmsEndPoint = smsEndPoint,
                        TxtSmsSetting = txtSmsSetting,
                    });
                }

                this.DialogResult = DialogResult.OK;

                this.UIInvoke(() =>
                {
                    this.Close();
                });

            });
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

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

        
    }
}
