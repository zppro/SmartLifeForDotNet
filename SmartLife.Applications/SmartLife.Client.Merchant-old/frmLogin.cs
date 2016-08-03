using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using win.core.utils;
using System.Net;
using e0571.web.core.Security;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using System.IO;
using Newtonsoft.Json;
using SmartLife.Share.Models.Auth.MerchantServices;

namespace SmartLife.Client.Merchant
{
    public partial class frmLogin : Form
    {
        private string strUserNameSinceLast;
        private string authEndPoint;
        System.Timers.Timer _tickTimer = null;


        

        public frmLogin()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
            {
                MessageBoxAdapter.ShowError("请输入商家编号");
                return;
            }
            if (txtPassword.Text.Trim() == "")
            {
                MessageBoxAdapter.ShowError("请输入密码");
                return;
            }
            DoLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());
        }

        #region 登录
        private void DoLogin(string userName, string password)
        {
            //
            btnOk.Enabled = false;

            lblMsg.Text = "登录中";
            _tickTimer = new System.Timers.Timer(1 * 200);
            _tickTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs ee)
            {
                BeginInvoke(new Action(() =>
                {
                    if (lblMsg.Text.IndexOf(".") == -1)
                    {
                        lblMsg.Text += ".";
                    }
                    else if (lblMsg.Text.IndexOf(".") + 5 == lblMsg.Text.Length)
                    {
                        lblMsg.Text = lblMsg.Text.Substring(0, lblMsg.Text.Length - 5);
                    }
                    else
                    {
                        lblMsg.Text += ".";
                    }
                }));

            });//到达时间的时候执行事件； 
            _tickTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            _tickTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 

            
            HttpAdapter.postAsyncAsJSON(authEndPoint + "/AuthenticateMerchant", new AuthMerchantParam { StationCode = userName, PasswordHash = MD5Provider.Generate(password) }.ToStringObjectDictionary(), new { ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(), (ret, res) =>
            {
               
                //MessageBoxAdapter.ShowDebug(_MonitorId.ToString());
                //dynamic ret = new { Error = "", Success = false };
                //ret = JsonConvert.DeserializeObject(result);
                MerchantVar.Load(userName, ret.ret);
                
                if ((bool)ret.Success)
                {
                    INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_USER_NAME_SINCE_LAST, userName, Common.INI_FILE_PATH);
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
                else
                {
                    lblMsg.Text = ret.ErrorMessage;

                    this.UIInvoke(() =>
                    {
                        btnOk.Enabled = true;

                    });
                }
                _tickTimer.Enabled = false;
                _tickTimer = null;
                
            });
        }

        #endregion

        private void frmLogin_Load(object sender, EventArgs e)
        {
            authEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, Common.INI_FILE_PATH);
            strUserNameSinceLast = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_USER_NAME_SINCE_LAST, Common.INI_FILE_PATH);
            if (!string.IsNullOrEmpty(strUserNameSinceLast))
            {
                txtUserName.Text = strUserNameSinceLast;
                txtPassword.Focus();
            }
        }
    }
}
