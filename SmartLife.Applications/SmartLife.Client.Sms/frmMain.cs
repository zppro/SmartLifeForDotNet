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
    public partial class frmMain : Form
    {
        private string authEndPoint;
        private ContextMenu notifyiconMnu;
        System.Timers.Timer _tickTimer = null;
        
        public frmMain()
        {
            InitializeComponent();
        }

        private void tstbn_frmSetting_Click(object sender, EventArgs e)
        {
            frmSetting frm = new frmSetting();

            frm.SectionSettingsSave += new dSectionSettingsSave((o, ce) =>
            {
                INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, ce.AuthEndPoint, Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTHSMS_END_POINT, ce.AuthSmsEndPoint, Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_SMS_END_POINT, ce.SmsEndPoint, Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_TXT_SMS_SETTING_LAST, ce.TxtSmsSetting, Common.INI_FILE_PATH);
                INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_ORDER_SMS_SETTING_LAST, ce.OrderSmsSetting, Common.INI_FILE_PATH);
            });
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (MessageBoxAdapter.ShowConfirm("您必须重新启动程序设置才能生效，是否重启程序？", Properties.Settings.Default.MessageBoxTitle) == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Restart();
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            authEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, Common.INI_FILE_PATH);

            lblMsg.Show();
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


            HttpUtility.optionsAsyncTo(authEndPoint + "/", null, new { ApplicationId = "CS001" }.ToStringObjectDictionary(), (ret, res) =>
            {
                if (ret != "ok")
                {
                    MessageBoxAdapter.ShowError("无效的认证节点");
                    return;
                }

                this.UIInvoke(() =>
                {
                    AnchorStyles anS = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                | System.Windows.Forms.AnchorStyles.Left)
                                | System.Windows.Forms.AnchorStyles.Right)));

                    frmSendSms fmSendSms = new frmSendSms();
                    fmSendSms.FormBorderStyle = FormBorderStyle.None;
                    fmSendSms.TopLevel = false;
                    fmSendSms.Parent = this;
                    fmSendSms.Anchor = anS;
                    fmSendSms.Size = this.panel1.Size;
                    this.panel1.Controls.Add(fmSendSms);//将子窗体载入panel
                    fmSendSms.Show();

                    frmGetSms fmGetSms = new frmGetSms();
                    fmGetSms.FormBorderStyle = FormBorderStyle.None;
                    fmGetSms.TopLevel = false;
                    fmGetSms.Parent = this;
                    fmGetSms.Anchor = anS;
                    fmGetSms.Size = this.panel2.Size;
                    this.panel2.Controls.Add(fmGetSms);//将子窗体载入panel
                    fmGetSms.Show();
                    lblMsg.Hide();
                });
                
                _tickTimer.Enabled = false;
                _tickTimer = null;

            });



        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) //判断是否最小化
            {
                notifyIcon1.Visible = true;
                this.Hide();
                this.ShowInTaskbar = false;

                Initializenotifyicon();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                notifyIcon1.Visible = false;
            }
        }

        /// <summary>
        /// 最小化到任务栏
        /// </summary>
        private void Initializenotifyicon()
        {
            //定义一个MenuItem数组，并把此数组同时赋值给ContextMenu对象 
            MenuItem[] mnuItms = new MenuItem[3];
            mnuItms[0] = new MenuItem();
            mnuItms[0].Text = "显示窗口";
            mnuItms[0].Click += new System.EventHandler(this.notifyIcon1_ShowForm);

            mnuItms[1] = new MenuItem("-");

            mnuItms[2] = new MenuItem();
            mnuItms[2].Text = "退出系统";
            mnuItms[2].Click += new System.EventHandler(this.ExitSelect);
            mnuItms[2].DefaultItem = true;

            notifyiconMnu = new ContextMenu(mnuItms);
            notifyIcon1.ContextMenu = notifyiconMnu;
            //为托盘程序加入设定好的ContextMenu对象 
        }
        public void notifyIcon1_ShowForm(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                notifyIcon1.Visible = false;
            }
        }

        public void ExitSelect(object sender, System.EventArgs e)
        {
            //隐藏托盘程序中的图标 
            notifyIcon1.Visible = false;
            //关闭系统 
            this.Close();
            this.Dispose(true);
        }
    }
}
