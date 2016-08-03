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
using SmartLife.Share.Models.PO;
using Newtonsoft.Json;
using win.core.utils;
namespace SmartLife.Client
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class frmWorkOrder : Form
    {
        public frmWorkOrder()
        {
            InitializeComponent();
        }

        public void LoadUrl(string url)
        {

            webBrowser.Url = new Uri(url);
        }

        public string GetUrl()
        {
            if (webBrowser.Url != null)
            {
                return webBrowser.Url.ToString();
            }
            return null;
        }

        public WorkOrder PageData { get; set; }
        public string Source { get; set; }
        public event dCloseWinWorkOrder CloseWinWorkOrder;

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser.Document.InvokeScript("setExecModeToCS");
            webBrowser.Document.InvokeScript("setEnvironmentVar", new object[] { EnvironmentVar.ToJSON() });
            webBrowser.Document.InvokeScript("setPageData", new object[] { JsonConvert.SerializeObject(PageData) });
        }

        private void frmWorkOrder_Load(object sender, EventArgs e)
        {
            webBrowser.ObjectForScripting = this;
        }

        #region BS交互 

        public void CloseWin()
        {
            if (CloseWinWorkOrder != null)
            {
                CloseWinWorkOrder();
            }
            this.Close();
        }
        #endregion
    }
}
