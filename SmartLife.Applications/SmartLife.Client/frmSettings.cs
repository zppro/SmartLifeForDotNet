using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using win.core.utils;
using System.Collections;

namespace SmartLife.Client
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        
        public event dRemoteHostConnect RemoteHostConnect;
        public event dCallCenterSettingsSave CallCenterSettingsSave;
        private void btnTest_Click(object sender, EventArgs e)
        {
            if (ConnectRemoteHost())
            {
                MessageBoxAdapter.ShowInfo("连接成功", Properties.Settings.Default.MessageBoxTitle);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CallCenterSettingsSave != null)
            {
                CallCenterSettingsSave(this, new CallCenterSettingsSaveEventArgs { CallCenterNodeAddress = formatCallCenterNodeAddress() });
            }

            string strRunMode = (string)cbbRunMode.SelectedValue;
            if (ConnectRemoteHost())
            {
                if (RemoteHostConnect != null)
                {
                    RemoteHostConnect(this, new RemoteHostConnectEventArgs { RunMode = strRunMode, RemoteHost = formatRemoteHost(), VirPath = formatVirPath() });
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string formatCallCenterNodeAddress()
        {
            string testUrl;
            if (txtRemoteHost.Text.Trim().StartsWith("http://"))
            {
                testUrl = txtCallCenterNodeAddress.Text.Trim();
            }
            else
            {
                testUrl = "http://" + txtCallCenterNodeAddress.Text.Trim();
            }
            if (testUrl.EndsWith("/"))
            {
                testUrl = testUrl.Substring(0, testUrl.Length - 1);
            }
            return testUrl.Replace("http://", "");
        }

        private string formatRemoteHost()
        {
            string testUrl;
            if (txtRemoteHost.Text.Trim().StartsWith("http://"))
            {
                testUrl = txtRemoteHost.Text.Trim();
            }
            else
            {
                testUrl = "http://" + txtRemoteHost.Text.Trim();
            }
            if (testUrl.EndsWith("/"))
            {
                testUrl = testUrl.Substring(0, testUrl.Length - 1);
            }
            return testUrl ;
        }
        private string formatVirPath()
        {
            if (txtVirPath.Text.Trim() != "")
            {
                return "/" + txtVirPath.Text.Trim();
            }
            else
            {
                return "";
            }
        }
        private bool ConnectRemoteHost()
        {
            bool Connected = false;
            try
            {

                HttpWebRequest request =
                (HttpWebRequest)HttpWebRequest.Create(formatRemoteHost() +"/"+ txtVirPath.Text.Trim() + "/forClient.htm");
                request.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode.ToString() == "OK")
                {
                    StreamReader myreader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    string responseText = myreader.ReadToEnd();
                    Connected = responseText == "ilovesmartlife";
                }
            }
            catch (Exception ex)
            {
                MessageBoxAdapter.ShowError(ex.Message, Properties.Settings.Default.MessageBoxTitle);
            }
            return Connected;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            //初始化运行模式
            List<KeyValuePair<object, string>> listKeyValue = new List<KeyValuePair<object, string>>();
            listKeyValue.Add(new KeyValuePair<object,string>("0", "外网模式"));
            listKeyValue.Add(new KeyValuePair<object, string>("2", "内网模式"));
            listKeyValue.Add(new KeyValuePair<object, string>("1", "测试模式"));
            cbbRunMode.DataSource = listKeyValue;
            cbbRunMode.DisplayMember = "value";
            cbbRunMode.ValueMember = "key";

            string defaultSelectMode = INIAdapter.ReadValue(Common.INI_SECTION_SMARTCARE, Common.INI_KEY_RUNMODE, Common.INI_FILE_PATH);
            if (string.IsNullOrEmpty(defaultSelectMode))
            {
                cbbRunMode.SelectedValue = "0";
            }
            else
            {
                cbbRunMode.SelectedValue = defaultSelectMode;
            }

            //初始化连接数据
            txtCallCenterNodeAddress.Text = INIAdapter.ReadValue(Common.INI_SECTION_CALLCENTER, Common.INI_KEY_CALLCENTER_NODE_ADDRESS, Common.INI_FILE_PATH);
        }

        private void cbbRunMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSelectValue = (string)((KeyValuePair<object,string>)cbbRunMode.SelectedItem).Key;

            txtRemoteHost.Text = INIAdapter.ReadValue(Common.INI_SECTION_SMARTCARE, Common.GetValueByFiledsName("INI_KEY_CMSHOST_" + strSelectValue), Common.INI_FILE_PATH);
            txtVirPath.Text = INIAdapter.ReadValue(Common.INI_SECTION_SMARTCARE, Common.GetValueByFiledsName("INI_KEY_VIRPATH_" + strSelectValue), Common.INI_FILE_PATH);
            if (txtVirPath.Text.StartsWith("/"))
            {
                txtVirPath.Text = txtVirPath.Text.Substring(1);
            }
        }

    }
}
