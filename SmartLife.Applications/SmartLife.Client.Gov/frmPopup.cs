using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Threading;

using Newtonsoft.Json;
using win.core.controls;
using win.core.utils;
using win.core.models;
using e0571.web.core.Utils;

namespace SmartLife.Client.Gov
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class frmPopup : Form
    {
        public frmPopup()
        {
            InitializeComponent();
        }

        public frmMain TheMotherWin { get; set; }
        public string PageUrl { get; set; }
        public string PageParamsOfJson { get; set; }

        private void frmPopup_Load(object sender, EventArgs e)
        {
            this.Location = TheMotherWin.getWorkAreaLocation();
            this.Size = TheMotherWin.getWorkAreaSize();
            wbWorkArea.ObjectForScripting = this;
            wbWorkArea.Url = new Uri(GovVar.AccessPoint + "/index.htm");
            btnClose.Left = (this.Size.Width - btnClose.Width) / 2;
        }

        #region 其他控件事件处理
        private void wbWorkArea_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            wbWorkArea.Document.InvokeScript("setExecModeForGov");
            wbWorkArea.Document.InvokeScript("setClientSize", new object[] { wbWorkArea.Width.ToString(), wbWorkArea.Height.ToString() });
            if (e.Url.ToString() == GovVar.AccessPoint + "/index.htm")
            {
                wbWorkArea.Document.InvokeScript("loadPage", new object[] { PageUrl, PageParamsOfJson });
            }
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
