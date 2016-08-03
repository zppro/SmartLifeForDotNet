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
using win.core.update;
using System.Collections;
using Newtonsoft.Json;

namespace SmartLife.Client.Gov
{
    public partial class frmSettings : Form
    {

        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {

        }

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

        private void frmSettings_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
