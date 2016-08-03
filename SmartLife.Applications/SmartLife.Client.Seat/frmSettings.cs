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
using System.Threading;
using System.Collections;
using win.core.utils;
using win.core.update;


namespace SmartLife.Client.Seat
{
    public partial class frmSettings : Form
    {
        public event dPlatformReady PlatformReady;
        public event dSettingsSave SettingsSave;
        public event dPlayBackTone PlayBackTone;
#if V2X
        public frmMainV2X TheMotherWin { get; set; }
#else
        public frmMain TheMotherWin { get; set; }
#endif
        string callCenterIP;
        Thread testSpeedLoadingThread;
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            txtPlatformAddress.Text = INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLATFORM_ADDRESS, Common.INI_FILE_PATH);
            chkServiceOnlineFlag.Checked = INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_SERVICE_ONLINE, Common.INI_FILE_PATH) == "1";
            cbxPlayTone.Checked = INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE, Common.INI_FILE_PATH) == "1";
            if (INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE_STIME, Common.INI_FILE_PATH) != "")
            {
                dtpStart.Value = DateTime.Parse(INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE_STIME, Common.INI_FILE_PATH));
            }
            if (INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE_ETIME, Common.INI_FILE_PATH) != "")
            {
                dtpEnd.Value = DateTime.Parse(INIAdapter.ReadValue(Common.INI_SECTION_SYSTEM, Common.INI_KEY_PLAY_TONE_ETIME, Common.INI_FILE_PATH));
            }

            callCenterIP = TheMotherWin.GetCallCenterIP();
            btnTestSpeed.Enabled = callCenterIP != "";
        }

        #region 帮助方法
        private string formatPlatformAddress()
        {
            string testUrl;
            if (txtPlatformAddress.Text.Trim().StartsWith("http://"))
            {
                testUrl = txtPlatformAddress.Text.Trim();
            }
            else
            {
                testUrl = "http://" + txtPlatformAddress.Text.Trim();
            }
            if (testUrl.EndsWith("/"))
            {
                testUrl = testUrl.Substring(0, testUrl.Length - 1);
            }
            return testUrl;
        }

        private bool testPlatformReady()
        {
            bool isReady = false;
            try
            {

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(formatPlatformAddress() + "/forClient.htm");
                request.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode.ToString() == "OK")
                {
                    StreamReader myreader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    string responseText = myreader.ReadToEnd();
                    isReady = responseText == "ilovesmartlife";
                }
            }
            catch (Exception ex)
            {
                MessageBoxAdapter.ShowError(ex.Message, Properties.Settings.Default.MessageBoxTitle);
            }
            return isReady;
        }

        #endregion

        #region 按钮事件
        private void btnTest_Click(object sender, EventArgs e)
        {
            if (testPlatformReady())
            {
                MessageBoxAdapter.ShowInfo("连接成功", Properties.Settings.Default.MessageBoxTitle);
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (testPlatformReady())
            {
                if (PlatformReady != null)
                {
                    PlatformReady(this, new PlatformReadyEventArgs { PlatformAddress = formatPlatformAddress() });
                }
            }

            if (SettingsSave != null)
            {
                SettingsSave(this, new SettingsSaveEventArgs { ServiceOnline = chkServiceOnlineFlag.Checked });
            }

            if (dtpStart.Value < dtpEnd.Value)
            {
                if (PlayBackTone != null)
                {
                    PlayBackTone(this, new PlayBackToneEventArgs { PlayTone = cbxPlayTone.Checked, StartPlayTime = dtpStart.Value.ToString("HH:mm"), EndPlayTime = dtpEnd.Value.ToString("HH:mm") });
                }
            }
            else {
                MessageBoxAdapter.ShowError("报警起始时间不能大于结束时间！");
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCloud_Click(object sender, EventArgs e)
        {
            txtPlatformAddress.Text = "http://121.40.154.222:17101";
        }

        private void btnSpare_Click(object sender, EventArgs e)
        {
            txtPlatformAddress.Text = "http://115.236.175.110:17101";
        }
        #endregion

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            string updateFolder = AppDomain.CurrentDomain.BaseDirectory + Common.APPLICATION_ID;
            FileAdapter.EnsurePath(updateFolder);
            FileAdapter.Copy(Updater.AppName, updateFolder + @"\" + Updater.AppName, true);
            foreach (var file in Updater.DepandenceFiles)
            {
                FileAdapter.Copy(file, AppDomain.CurrentDomain.BaseDirectory + Common.APPLICATION_ID + @"\" + file, true);
            }

            string updateEndPoint = INIAdapter.ReadValue(Common.CFG_SECTION_WEB, Common.CFG_KEY_UPDATE_END_POINT, Common.CFG_FILE_PATH);
            string args = String.Format("-n {0} -v {1} -c {2}", AppAdapter.GetName(), VersionAdapter.GetPureVersion(VersionType.ASSEMBLY), updateEndPoint + "/" + Common.APPLICATION_ID);
            System.Diagnostics.Process.Start(updateFolder + @"\" + Updater.AppName, args);
        }

        //开启报警提示
        private void cbxPlayTone_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxPlayTone.Checked)
            {
                dtpStart.Enabled = true;
                dtpEnd.Enabled = true;
            }
            else {
                dtpStart.Enabled = false;
                dtpEnd.Enabled = false;
            }
        }
        //时间验证
        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value > dtpEnd.Value)
            {
                dtpEnd.Value = dtpStart.Value.AddHours(1);
            }
        }

        private void btnTestSpeed_Click(object sender, EventArgs e)
        {
            string oldText = btnTestSpeed.Text;
            btnTestSpeed.Enabled = false;
            testSpeedLoadingThread = ThreadAdapter.DoCircleTask(() =>
            {
                BeginInvoke(new Action(() =>
                {
                    if (btnTestSpeed.Text.IndexOf(".") == -1)
                    {
                        btnTestSpeed.Text += ".";
                    }
                    else if (btnTestSpeed.Text.IndexOf(".") + 3 == btnTestSpeed.Text.Length)
                    {
                        btnTestSpeed.Text = btnTestSpeed.Text.Substring(0, btnTestSpeed.Text.Length - 3);
                    }
                    else
                    {
                        btnTestSpeed.Text += ".";
                    }
                }));

            }, 700, () =>
            {
                return btnTestSpeed.Enabled;
            });

            string commandStr = string.Format("iperf.exe -c {0} -p 12347 -t 10 -y C", callCenterIP);
            CommandLineAdapter.Execute(commandStr, (o, dre) =>
            {
                string output = dre.Data;
                if (!string.IsNullOrEmpty(output))
                {
                    if (output.Contains("fail") || output.Contains("timed out"))
                    {
                        MessageBoxAdapter.ShowError("无法连接到呼叫中心，请检查网络！");
                    }
                    else
                    {
                        string[] results = output.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (results.Length == 9)
                        {
                            long bps = long.Parse(results[8]) / 8 / 1024;
                            MessageBoxAdapter.ShowInfo("连接到呼叫中心的即时速度为【" + bps.ToString() + "k】！");
                        }
                    }
                }

                if (!String.IsNullOrEmpty(dre.Data))
                {
                    Console.WriteLine(dre.Data);
                }

            }, "./tools", (o, ee) =>
            { 
                BeginInvoke(new Action(() =>
                {
                    btnTestSpeed.Enabled = true;
                    btnTestSpeed.Text = oldText;
                }));
                
            });
        }

        private void frmSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (testSpeedLoadingThread != null)
            {
                if (testSpeedLoadingThread.IsAlive)
                {
                    testSpeedLoadingThread.Abort();
                }
                testSpeedLoadingThread = null;
            }

        }

        
    }
}
