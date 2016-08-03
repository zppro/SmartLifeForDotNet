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
using Newtonsoft.Json;

namespace SmartLife.Client.Seat
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class frmConference : Form
    {
        private string _pageUrl;
        public event dCTIWinClose CTIWinClose;
#if V2X
        public frmMainV2X TheMotherWin { get; set; }
#else
        public frmMain TheMotherWin { get; set; }
#endif

        public frmConference()
        {
            InitializeComponent();
        }

        private void frmConference_Load(object sender, EventArgs e)
        {
            //this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height;

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
                //设置分机,目前从url上带
                //webBrowser.Document.InvokeScript("setServiceTypeUrl", new object[] { strJsonStr });
            }
        }

        private void frmConference_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CTIWinClose != null)
            {
                CTIWinClose(this, new EventArgs());
            }
        }

        #region 调用BS
        //设置分机,目前从url上带
        #endregion
    }
}
