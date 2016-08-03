using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartLife.Client.Seat
{
    public partial class frmVoice : Form
    {
        private string _pageUrl;
        public event dCTIWinClose CTIWinClose;

        public frmVoice()
        {
            InitializeComponent();
        }

        private void frmVoice_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
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
            }
        }

        private void frmVoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CTIWinClose != null)
            {
                CTIWinClose(this, new EventArgs());
            }
        }
    }
}
