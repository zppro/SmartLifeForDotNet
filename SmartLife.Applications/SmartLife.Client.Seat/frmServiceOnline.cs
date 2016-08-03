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
using win.core.utils;
using win.core.models;
using e0571.web.core.TypeExtension;

using web.core.share.models.WeiXin.Pub;

namespace SmartLife.Client.Seat
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class frmServiceOnline : Form
    {
        string _pageUrl;
        Point downPosition;   //记录鼠标按下时的坐标
        bool down = false;                   //判断鼠标是状态 True:按下 False:抬起
#if V2X
        public frmMainV2X TheMotherWin { get; set; }
#else
        public frmMain TheMotherWin { get; set; }
#endif

        public frmServiceOnline()
        {
            InitializeComponent();
        }

        private void frmServiceOnline_Load(object sender, EventArgs e)
        {
            InitToolbar();
            lblNickName.Text = "";
            rtxtInputBox.Focus();
            webBrowser.ObjectForScripting = this; 
        }
        private void InitToolbar()
        {
            foreach (PictureBox item in pnlToolbar.Controls)
            {
                if (item != null)
                {
                    item.MouseEnter += new EventHandler(pbToolItem_MouseEnter);
                    item.MouseLeave += new EventHandler(pbToolItem_MouseLeave);
                }
            }
        }
        public string GetUrl()
        {
            return _pageUrl;
        }
        public void LoadUrl(string url)
        {
            _pageUrl = url;
            webBrowser.Url = new Uri(_pageUrl);

        }


        #region 供BS调用
        public void SetWXUserInfo(string nickName)
        {
            lblNickName.Text = nickName;
        }
        #endregion

        #region 帮助方法
        private void SendMsg()
        {
            string content = rtxtInputBox.Text.Trim();
            if (content == "")
            {
                MessageBoxAdapter.ShowInfo("发送内容不能为空，请重新输入");
                rtxtInputBox.Text = "";
                return;
            }
            string openId = this.Name;
            string url = TheMotherWin.WeiXinOfServiceOnlineInovkeAddress + "/api/share/v1/SendWXMessage";
            string payLoad = new WXRequestTextMessage { touser = openId, msgtype = WXRequestMessageType.text.ToString(), text = new WXTextMessageWrapper { content = content } }.ToJson();
            HttpAdapter.postAsyncStr(url, payLoad, (ret1, res1) =>
            {
                webBrowser.Document.InvokeScript("sendMsg", new object[] { content });
                rtxtInputBox.Text = "";
            }, (he) =>
            {
                MessageBoxAdapter.ShowInfo("发送失败：" + he.Message);
            });

        }
        #endregion

        #region 其他事件
        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //
            if (webBrowser.ReadyState == WebBrowserReadyState.Complete && e.Url.ToString() == _pageUrl)
            {
                webBrowser.Document.InvokeScript("setExecModeForSeat");
                webBrowser.Document.InvokeScript("setClientSize", new object[] { webBrowser.Width.ToString(), webBrowser.Height.ToString() });
                webBrowser.Document.InvokeScript("setWXUserOpenId", new object[] { this.Name });//--openId=frm.Name
            }
        }

        private void pbMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pbMin_MouseEnter(object sender, EventArgs e)
        {
            pbMin.Image = Properties.Resources.min_on;
        }

        private void pbMin_MouseLeave(object sender, EventArgs e)
        {
            pbMin.Image = Properties.Resources.min;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbClose_MouseEnter(object sender, EventArgs e)
        {
            pbClose.Image = Properties.Resources.close_on;
        }

        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            pbClose.Image = Properties.Resources.close;
        }

        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            downPosition = new Point(e.X, e.Y);
            down = true;
        }

        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (down && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point currentPosition = Control.MousePosition;
                currentPosition.Offset(-downPosition.X, -downPosition.Y);
                this.Location = currentPosition;
            }

        }

        private void pbOK_MouseEnter(object sender, EventArgs e)
        {
            pbOK.BackColor = SystemColors.ControlLight;
        }

        private void pbOK_MouseLeave(object sender, EventArgs e)
        {
            pbOK.BackColor = Color.FromArgb(246, 248, 248);
        }

        private void pbOK_Click(object sender, EventArgs e)
        {
            SendMsg();
        }
        #endregion

        #region 工具栏
        private void pbToolItem_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).BackColor = Color.White;
        }

        private void pbToolItem_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).BackColor = Color.FromArgb(137, 188, 222);
        }

        private void pbText_Click(object sender, EventArgs e)
        {

        }

        private void pbEmo_Click(object sender, EventArgs e)
        {

        }

        private void pbCapture_Click(object sender, EventArgs e)
        {

        }

        private void pbSendPic_Click(object sender, EventArgs e)
        {

        }
        private void pbPresent_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void frmServiceOnline_Shown(object sender, EventArgs e)
        {
            webBrowser.ObjectForScripting = this;
        }

        private void rtxtInputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendMsg();
            }
        }

       
    }
}
