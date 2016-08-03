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

namespace SmartLife.Client.Seat
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class frmDialBack : Form
    {
        private string _pageUrl;
        public event dCTIWinClose CTIWinClose;

        public frmMainV2X TheMotherWin { get; set; }

        public string _CallServiceId;
        public string _pageData;

        public frmDialBack()
        {
            InitializeComponent();
        }

        private void frmDialBack_Load(object sender, EventArgs e)
        {
            webBrowser.ObjectForScripting = this;
        }

        public void LoadUrl(string url)
        {
            _pageUrl = url;
            webBrowser.Url = new Uri(_pageUrl);
        }


        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser.ReadyState == WebBrowserReadyState.Complete && e.Url.ToString() == _pageUrl)
            {
                Console.WriteLine(e.Url.ToString());
                webBrowser.Document.InvokeScript("setExecModeForSeat");
                webBrowser.Document.InvokeScript("setEnvironmentVar", new object[] { EnvironmentVar.ToJSON() });
            }
        }

        private void frmDialBack_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CTIWinClose != null)
            {
                CTIWinClose(this, new EventArgs());
            }
        }

        #region 调用BS
        public string getDialBackPageData() {
            return _pageData;
        }
        #endregion
    }
}
