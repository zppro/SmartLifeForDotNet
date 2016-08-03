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

namespace SmartLife.Client.Sms
{
    public partial class frmSetting : Form
    {
        public event dSectionSettingsSave SectionSettingsSave;
        public frmSetting()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string authEndPoint = formatEndPoint(txtAuthEndPoint.Text.Trim());
            string authSmsEndPoint = formatEndPoint(txtAuthSmsEndPoint.Text.Trim());
            string smsEndPoint = formatEndPoint(tb_SmsEndPoint.Text.Trim());

            string shorError = "";
            shorError = tb_Txt_SpCode.Text.Trim();
            if (string.IsNullOrEmpty(shorError))
            {
                MessageBoxAdapter.ShowError("文本节点的企业编号不能为空！");
                return;
            }
            shorError = tb_Txt_LoginName.Text.Trim();
            if (string.IsNullOrEmpty(tb_Txt_SpCode.Text.Trim()))
            {
                MessageBoxAdapter.ShowError("文本节点的用户名不能为空！");
                return;
            }
            shorError = tb_Txt_Password.Text.Trim();
            if (string.IsNullOrEmpty(tb_Txt_SpCode.Text.Trim()))
            {
                MessageBoxAdapter.ShowError("文本节点的密码不能为空！");
                return;
            }
            shorError = tb_Digital_SpCode.Text.Trim();
            if (string.IsNullOrEmpty(tb_Txt_SpCode.Text.Trim()))
            {
                MessageBoxAdapter.ShowError("指令节点的企业编号不能为空！");
                return;
            }
            shorError = tb_Digital_LoginName.Text.Trim();
            if (string.IsNullOrEmpty(tb_Txt_SpCode.Text.Trim()))
            {
                MessageBoxAdapter.ShowError("指令节点的用户名不能为空！");
                return;
            }
            shorError = tb_Digital_Password.Text.Trim();
            if (string.IsNullOrEmpty(tb_Txt_SpCode.Text.Trim()))
            {
                MessageBoxAdapter.ShowError("指令节点的密码不能为空！");
                return;
            }


            string txtSmsSetting = "SpCode=" + tb_Txt_SpCode.Text.Trim() + "&LoginName=" + tb_Txt_LoginName.Text.Trim() + "&Password=" + BaseUtility.EncryptDES(tb_Txt_Password.Text.Trim());
            string orderSmsSetting = "SpCode=" + tb_Digital_SpCode.Text.Trim() + "&LoginName=" + tb_Digital_LoginName.Text.Trim() + "&Password=" + BaseUtility.EncryptDES(tb_Digital_Password.Text.Trim());

            string connectUrl = authEndPoint + "/";
            HttpUtility.optionsAsyncTo(connectUrl, null, new { ApplicationId = "CS001" }.ToStringObjectDictionary(), (ret, res) =>
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
                        OrderSmsSetting = orderSmsSetting
                    });
                }

                this.DialogResult = DialogResult.OK;

                this.UIInvoke(() =>
                {
                    this.Close();
                });

            });
        }


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

        private void frmSetting_Load(object sender, EventArgs e)
        {
            txtAuthEndPoint.Text = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, Common.INI_FILE_PATH);
            txtAuthSmsEndPoint.Text = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTHSMS_END_POINT, Common.INI_FILE_PATH);
            tb_SmsEndPoint.Text = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_SMS_END_POINT, Common.INI_FILE_PATH);
            string txtSmsSetting = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_TXT_SMS_SETTING_LAST, Common.INI_FILE_PATH);
            string orderSmsSetting = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_ORDER_SMS_SETTING_LAST, Common.INI_FILE_PATH);

            Dictionary<string, string> dict = BaseUtility.StrToDictionary(txtSmsSetting, '&');
            if (dict.Count > 2)
            {
                tb_Txt_SpCode.Text = dict["SpCode"];
                tb_Txt_LoginName.Text = dict["LoginName"];
                tb_Txt_Password.Text = BaseUtility.DecryptDES(dict["Password"]);
            }
            dict = BaseUtility.StrToDictionary(orderSmsSetting, '&');
            if (dict.Count > 2)
            {
                tb_Digital_SpCode.Text = dict["SpCode"];
                tb_Digital_LoginName.Text = dict["LoginName"];
                tb_Digital_Password.Text = BaseUtility.DecryptDES(dict["Password"]);
            }
        }
    }
}
