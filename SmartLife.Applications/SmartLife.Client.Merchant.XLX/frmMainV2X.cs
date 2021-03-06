﻿using System;
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
using System.IO;
using System.Dynamic;

using Newtonsoft.Json;
using win.core.controls;
using win.core.utils;
using e0571.web.core.Utils;
using e0571.web.core.TypeExtension;
using System.Xml.Linq;
using win.core.models;
using win.core.controls.shapes;
using e0571.web.core.TypeInherited;


namespace SmartLife.Client.Merchant
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class frmMainV2X : Form
    {
        const string _AutoLoginUrl = "/auto-login2.htm";
        Thread autoLoginThread;
        Thread autoLoginForProvinceThread;
        bool isAutoLogined = false;
        bool isAutoLoginedForProvince = false;
        bool flash4ajaxReady = false;
        bool flash4ajaxReadyForProvince = false;
        static Point downPosition;   //记录鼠标按下时的坐标
        bool down = false;                   //判断鼠标是状态 True:按下 False:抬起
        Point oldP = Point.Empty;
        Point newP = Point.Empty; 
        CheckState assistToolbarState = CheckState.Unchecked; 
        int assistToolbarPBCount = 0;
        PictureBox assistToolbarPB;
        const int CLOSE_SIZE = 8;
        WebBrowser fullScreenWB;


        #region 提示 Thread
        Thread Remind_CircleTask;
        bool bFetchReminderData = false;
        System.Media.SoundPlayer soundPlayer;

        frmWorkOrderNotify notifyWindow = new frmWorkOrderNotify();  //实例化类MainForm的一个对象
        Queue<WorkOrderReminderStatInfo> queuesOfWorkOrderReminderStatInfo = new Queue<WorkOrderReminderStatInfo>();
        Queue<WorkOrderReminderInfo> queuesOfWorkOrderReminderInfo = new Queue<WorkOrderReminderInfo>();
        #endregion

        public frmMainV2X()
        {
            InitializeComponent();
            //this.MaximumSize = new Size(SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height - 10);
            this.WindowState = FormWindowState.Maximized;

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            ReadSettings(); 
            //lblVer.Text = Properties.Settings.Default.ProductName + "(" + Properties.Settings.Default.ProductVersionComment + ") " + VersionAdapter.GetVersion(VersionType.ASSEMBLY);
            lblVer.Text = "心连心居家养老服务中心";
            wbWorkArea.ObjectForScripting = this;
            wbProvinceSum.ObjectForScripting = this;
            if (!MerchantVar.AccessPoint.EndsWith("404.htm"))
            {
                wbWorkArea.Url = new Uri(MerchantVar.AccessPoint + _AutoLoginUrl);
            }
            else
            {
                if (MerchantVar.AccessPoint.StartsWith("http://localhost"))
                {
                    wbWorkArea.Url = new Uri("http://localhost/SmartLife.CertManage.ManageCMS/404.htm");
                }
                else
                {
                    wbWorkArea.Url = new Uri(MerchantVar.AccessPoint);
                }
            }
            if (!MerchantVar.AccessOfProvice.EndsWith("404.htm"))
            {
                wbProvinceSum.Url = new Uri(MerchantVar.AccessOfProvice + _AutoLoginUrl);
            }
            else
            {
                if (MerchantVar.AccessOfProvice.StartsWith("http://localhost"))
                {
                    wbProvinceSum.Url = new Uri("http://localhost/SmartLife.Auth.ManageCMS/404.htm");
                }
                else
                {
                    wbProvinceSum.Url = new Uri(MerchantVar.AccessOfProvice);
                }
            }

            pnlAssistToolbarContainer.Visible = assistToolbarState == CheckState.Checked;

            xTabs.DrawMode = TabDrawMode.OwnerDrawFixed;
            xTabs.Padding = new System.Drawing.Point(CLOSE_SIZE * 2, CLOSE_SIZE / 2);
            xTabs.DrawItem += new DrawItemEventHandler(xTabs_DrawItem);
            xTabs.MouseDown += new MouseEventHandler(xTabs_MouseDown);

            InitActions();
           
            BuildControls();

            
            /*
            notifyWindow.ClickReminderInfoWin += new dNotifyWorkOrderReminderInfo(OnClickReminderInfoWin);
            notifyWindow.ClickReminderStatInfoWin += new dNotifyWorkOrderReminderStatInfo(OnClickReminderStatInfoWin);
            notifyWindow.IgnoreReminderInfo += new dIgnoreWorkOrderReminderInfo(OnIgnoreReminderInfo);
            notifyWindow.IgnoreReminderStatInfo += new dIgnoreWorkOrderReminderStatInfo(OnIgnoreReminderStatInfo);
            notifyWindow.CloseWin += new Action(OnCloseWin); 
            */
        }

        #region 帮助方法

        #region 读取设置
        private void ReadSettings()
        {
            string strAssistToolbarFlag = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INT_KEY_ASSIST_TOOLBAR_FLAG, Common.INI_FILE_PATH);
            if (!string.IsNullOrEmpty(strAssistToolbarFlag))
            {
                assistToolbarState = EnumAdapter.GetEnum<CheckState>(strAssistToolbarFlag);
            }
            string strAssistToolbarPositionLast = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INT_KEY_ASSIST_TOOLBAR_POSITION_LAST, Common.INI_FILE_PATH);
            if (!string.IsNullOrEmpty(strAssistToolbarPositionLast))
            {
                string[] positions = strAssistToolbarPositionLast.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                pnlAssistToolbarContainer.Location = new Point(int.Parse(positions[0]), int.Parse(positions[1]));
            }
            else
            {
                pnlAssistToolbarContainer.Location = new Point(pnlMain.Width - pnlAssistToolbarContainer.Width, 0);
            }
        }
        #endregion

        #region 操作栏
        private void InitActions()
        {
            pbdActions.AddDropDownItemAndHandle("全屏", (o, fe) =>
            { 
                pnlHead.Visible = false;
                pnlLeft.Visible = false;
                pnlRight.Visible = false;
                pnlTop.Visible = false;
                pnlBottom.Visible = false;

                fullScreenWB = xTabs.SelectedTab.Controls[0] as WebBrowser;
                fullScreenWB.Parent = this;
                fullScreenWB.Size = this.Size;
                fullScreenWB.Url = fullScreenWB.Url;
                fullScreenWB.BringToFront();

            });
            pbdActions.AddDropDownItemAndHandle("设置", (o, se) =>
            {
                frmSettings settings = new frmSettings();
                settings.SectionBizSettingsSave += new dSectionBizSettingsSave((co, ce) =>
                {
                    SettingsVar.CurrentRemindType = ce.Type;
                    SettingsVar.CurrentYYRemindFlag = ce.YYRemindFlag;
                    INIAdapter.WriteValue(Common.INI_SECTION_BIZ, Common.INI_KEY_REMIND_TYPE, ((int)ce.Type).ToString(), Common.INI_FILE_PATH);
                    INIAdapter.WriteValue(Common.INI_SECTION_BIZ, Common.INI_KEY_YYREMIND_FLAG, ce.YYRemindFlag.ToString(), Common.INI_FILE_PATH);
                });
                settings.ShowDialog();
            });


            pbdActions.AddDropDownItemAndHandle(new ToolStripMenuItem { Text = "辅助工具栏", CheckState = assistToolbarState }, (o, te) =>
            {
                ToolStripMenuItem clickedItem = ((te as ToolStripItemClickedEventArgs).ClickedItem as ToolStripMenuItem);
                if (clickedItem.CheckState == CheckState.Checked)
                {
                    clickedItem.CheckState = CheckState.Unchecked;
                }
                else
                {
                    clickedItem.CheckState = CheckState.Checked;
                }
                assistToolbarState = clickedItem.CheckState;
                INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INT_KEY_ASSIST_TOOLBAR_FLAG, assistToolbarState.ToString(), Common.INI_FILE_PATH);

                pnlAssistToolbarContainer.Visible = assistToolbarState == CheckState.Checked;
            });
        }
        #endregion

        #region 解析颜色
        private bool parseToColor(string rgbstr ,out int r,out int g ,out int b)
        {
            bool success = true; 
            string[] rgbs = rgbstr.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (rgbs.Length == 3)
            {
                if (!int.TryParse(rgbs[0], out r))
                {
                    success = false;
                    r = 255;
                }
                 
                if (!int.TryParse(rgbs[1].Trim(), out g)) 
                {
                    success = false;
                    g = 255;
                }

                if (!int.TryParse(rgbs[2].Trim(), out b))
                {
                    success = false;
                    b = 255;
                }

            }
            else
            {
                r = 255;
                g = 255;
                b = 255;
                success = false;
            } 
            return success;
        }
        #endregion

        #region 解析大小
        private bool parseToSize(string position, string sizeStr, out Size size)
        {
            bool success = true;
            int width, height;
            if (position == "Left" || position == "Right")
            {
                width = 114;
                height = 114;

            }
            else
            {
                width = 228;
                height = 114;
            }
            string[] arrSize = sizeStr.Split("^".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string[] sizes = arrSize[0].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (sizes.Length > 1)
            {

                if (!int.TryParse(sizes[0].Trim(), out width)) { }
                if (!int.TryParse(sizes[1].Trim(), out height)) { }
            }

            size = new Size(width, height);
            return success;
        }
        #endregion

        #region 解析位置
        private bool parseToLocation(string locationStr, out Point point)
        {
            bool success = true;
            int left, top;
            left = 10;
            top = 10;
            string[] arrLocation = locationStr.Split("^".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string[] locations = arrLocation[0].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (locations.Length > 1)
            {

                if (!int.TryParse(locations[0].Trim(), out left)) { }
                if (!int.TryParse(locations[1].Trim(), out top)) { }
            }

            point = new Point(left, top);
            return success;
        }
        #endregion

        #region 解析字体
        private bool parseToFont(string fontstr, out string fontName, out float emSize, out FontStyle fontStyle)
        {
            bool success = true;
            string[] arrFont = fontstr.Split("^".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string[] rgbs = arrFont[0].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (rgbs.Length < 2)
            {
                fontName = "";
                emSize = 0;
                fontStyle = FontStyle.Regular;
            }
            else 
            {
                fontName = rgbs[0].Trim();
                fontStyle = FontStyle.Regular;
                if (!float.TryParse(rgbs[1].Trim(), out emSize))
                {
                    success = false;
                }
                if (rgbs.Length > 2)
                {
                    fontStyle = EnumAdapter.GetEnum<FontStyle>(rgbs[2].Trim());
                }
            } 

            return success;
        }
        #endregion
         
        
        #endregion

        #region 供BS调用
        //客户端回调模式设置
        public void Ready4Login()
        {
            flash4ajaxReady = true;
        }
        public void Ready4LoginProvince()
        {
            flash4ajaxReadyForProvince = true;
        }
        //客户端回调环境设置
        public void SetClientEnvironment(string stationId) {
            MerchantVar.StationId = Guid.Parse(stationId);
        }
        public void OpenPopup(string pageUrl,string jsonStr)
        {
            /*
            frmPopup popup = new frmPopup();
            popup.TheMotherWin = this;
            popup.PageUrl = pageUrl;
            popup.PageParamsOfJson = jsonStr;
            popup.ShowDialog();*/

        }
        public bool IECheck()
        {
            System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcesses();
            return ps.Any(item => item.ProcessName.ToLower() == "iexplore");
        }
        public void OpenIE(string url)
        { 
            System.Diagnostics.Process.Start("IExplore.exe", url);
        }
        #endregion

        #region 构造磁贴
        private void BuildControls()
        {
            Control ctl = null;
            for (int index = 0; index < MerchantVar.AuthorizedModules.Count; index++)
            {
                dynamic module = MerchantVar.AuthorizedModules[index];
                if (module.ControlType == ControlType.split_button.ToString())
                {
                    ctl = new SplitButton();
                }
                else
                {
                    ctl = new Button();
                }
                ctl.Tag = module;
                ctl.Name = module.ModuleId;
                ctl.Text = module.AliasName;
                
                Size size;
                if (parseToSize(module.Position.ToString(), module.Size.ToString(), out size))
                {
                    ctl.Size = size;
                }
                Point point;
                if (parseToLocation(module.Location.ToString(), out point))
                {
                    ctl.Location = point;
                }

                if (!string.IsNullOrEmpty(module.ForeColor.ToString()))
                {
                    int r, g, b;
                    if (parseToColor(module.ForeColor.ToString(), out r, out g, out b))
                    {
                        ctl.ForeColor = Color.FromArgb(r, g, b);
                    }
                }
                if (!string.IsNullOrEmpty(module.BackColor.ToString()))
                {
                    int r, g, b;
                    if (parseToColor(module.BackColor.ToString(), out r, out g, out b))
                    {
                        ctl.BackColor = Color.FromArgb(r, g, b);
                    }
                }
                if (!string.IsNullOrEmpty(module.Font.ToString()))
                {
                    string fontName;
                    float emSize;
                    FontStyle fontStyle;
                    if (parseToFont(module.Font.ToString(), out fontName, out emSize, out fontStyle))
                    {
                        ctl.Font = new Font(fontName, emSize, fontStyle);
                    }
                }
                
                if (module.Position == "Left" || module.Position == "Right")
                {
                    if (module.Position == "Left")
                    {
                        CreateTile(ctl);
                        pnlLeft.Controls.Add(ctl);
                    }
                    else
                    {
                        CreateTile(ctl);
                        pnlRight.Controls.Add(ctl);
                    }
                }
                else
                {
                    if (module.Position == "Top")
                    {
                        CreateTile(ctl);
                        pnlTop.Controls.Add(ctl);
                    }
                    else
                    {
                        CreateTile(ctl);
                        pnlBottom.Controls.Add(ctl);
                    }
                }

            }
             
           
            pnlLeft.Visible = pnlLeft.Controls.Count > 0;
            pnlRight.Visible = pnlRight.Controls.Count > 0;
            pnlTop.Visible = pnlTop.Controls.Count > 0;
            pnlBottom.Visible = pnlBottom.Controls.Count > 0;
            int removeWidth = (pnlLeft.Visible ? pnlLeft.Width : 0) + (pnlRight.Visible ? pnlRight.Width : 0);
            int addHeight = (!pnlTop.Visible ? pnlTop.Height : 0) + (!pnlBottom.Visible ? pnlBottom.Height : 0);
            Control maxWidthControl;
            if (!pnlLeft.Visible)
            {
                xTabs.Location = new Point(0, xTabs.Location.Y);
            }
            else
            {
                maxWidthControl = GetControlWithMaxWidth(pnlLeft.Controls);
                pnlLeft.Width = maxWidthControl.Width + maxWidthControl.Location.X * 2;
            }
            xTabs.Width = pnlMain.Width - removeWidth;

            if (!pnlRight.Visible)
            { 
            }
            else
            {
                maxWidthControl = GetControlWithMaxWidth(pnlLeft.Controls);
                pnlRight.Width = maxWidthControl.Width + maxWidthControl.Location.X * 2;
            }

            if (!pnlTop.Visible)
            {
                pnlMain.Location = new Point(pnlMain.Location.X, 0);
            }
            else
            {
                pnlTop.Height = pnlTop.Controls[0].Height + pnlTop.Controls[0].Location.Y * 2;
            }

            if (!pnlBottom.Visible)
            {
                pnlMain.Height = pnlMain.Height + pnlBottom.Height;
                xTabs.Height = pnlMain.Height;
            }
            else
            {
                pnlBottom.Height = pnlBottom.Controls[0].Height + pnlBottom.Controls[0].Location.Y * 2;
            }

            pnlMain.Height = pnlMain.Height + addHeight;

            pnlAssistToolbarContainer.Height = 100 + (assistToolbarPBCount / 2) * 50;
        }
        private Control GetControlWithMaxWidth(System.Windows.Forms.Control.ControlCollection cc)
        {
            Control theOne = null;
            foreach (Control c in cc)
            {
                if (theOne == null)
                {
                    theOne = c;
                }
                else
                {
                    if (c.Width > theOne.Width)
                    {
                        theOne = c;
                    }
                }
            }
            return theOne;
        }
        private void CreateTile(Control ctl)
        {
            dynamic module = ctl.Tag as dynamic;
            
            if (ctl is SplitButton)
            {
                //因为继承 
                IList<BindingItem> items = JsonConvert.DeserializeObject<IList<BindingItem>>(module.ControlOption.ToString());
                foreach (BindingItem item in items)
                {
                    string pageKey = "_tab_" + module.ModuleId.ToString() + "$" + item.Text;
                    string pageText = item.Text;
                    string pageUrl = item.Value;
                    string pageOpenTarget = module.OpenTarget.ToString();
                    string pageCode = module.ModuleCode.ToString();
                    (ctl as SplitButton).AddDropDownItemAndHandle(item.Text, (o, e) => {

                        OpenTarget(pageKey, pageText, pageUrl, pageOpenTarget, pageCode);
                    });
                    (ctl as SplitButton).AlwaysDropDown = true; 
                    (ctl as SplitButton).PersistDropDownName = true;

                    if (module.AssistToolbarFlag == 1)
                    {
                        AddToAssistToolbar(module, item);
                    }
                }
            }
            else  if (ctl is Button)
            {
                string imageName = e0571.web.core.Utils.TypeConverter.ChangeString(module.Picture).Trim();
                Image image;
                if (imageName == string.Empty)
                {
                    image = Properties.Resources.coffee;
                }
                else
                {
                    image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(imageName.ToString());
                }
                LinkedList<TileItem> tileItemList = new LinkedList<TileItem>();
                ctl.Click += new EventHandler(btn_Click);

                tileItemList.AddLast(new TileItem()
                {
                    Image = image
                });

                Tile tile = new Tile(ctl, tileItemList);

                if (module.AssistToolbarFlag == 1)
                {
                    AddToAssistToolbar(module, null);
                }
            }

           
        }
        private void OpenTarget(string pageKey, string pageText, string pageUrl, string pageOpenTarget, string pageCode)
        {
            if (pageOpenTarget.StartsWith("_tab"))
            {
                if (isAutoLogined || isAutoLoginedForProvince)
                {
                    ChooseTab(pageKey, pageText, pageUrl, pageOpenTarget);
                }
            }
            else
            {
                if (pageOpenTarget.EndsWith("_province"))
                {
                    if (isAutoLoginedForProvince)
                    {
                        wbProvinceSum.Document.InvokeScript("switchPage", new object[] { pageUrl, null, pageOpenTarget, pageCode });
                    }
                }
                else
                {
                    if (isAutoLogined)
                    {
                        wbWorkArea.Document.InvokeScript("switchPage", new object[] { pageUrl, null, pageOpenTarget, pageCode });
                    }
                }
            }
        }
        private void ChooseTab(string tabPageKey, string tabPageText, string tabPageUrl, string tabOpenTarget)
        {
            if (xTabs.TabPages.ContainsKey(tabPageKey))
            {
                TabPage page = xTabs.TabPages[tabPageKey];
                xTabs.SelectedTab = page;
                //不刷新了
                //WebBrowser wb = page.Controls[0] as WebBrowser;
                //wb.Document.InvokeScript("switchPage", new object[] { module.AccessUrl, null, module.OpenTarget, module.ModuleCode });
            }
            else
            {
                TabPage page = new TabPage { Name = tabPageKey, Text = tabPageText };
                xTabs.TabPages.Add(page);
                xTabs.SelectedTab = page;
                WebBrowser wb = new WebBrowser();
                wb.Dock = DockStyle.Fill;
                wb.IsWebBrowserContextMenuEnabled = false;
                wb.ObjectForScripting = this;
                wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler((object o, WebBrowserDocumentCompletedEventArgs we) =>
                {

                    if (we.Url.ToString() == MerchantVar.AccessPoint + "/index.htm")
                    {
                        wb.Document.InvokeScript("setExecModeForMer");
                        wb.Document.InvokeScript("loadPage", new object[] { tabPageUrl });
                    }
                    else if (we.Url.ToString() == MerchantVar.AccessOfProvice + "/index.htm")
                    {
                        wb.Document.InvokeScript("setExecModeForMer");
                        wb.Document.InvokeScript("loadPage", new object[] { tabPageUrl });
                    }
                });
                page.Controls.Add(wb);
                if (tabOpenTarget == "_tab")
                {
                    wb.Url = new Uri(MerchantVar.AccessPoint + "/index.htm");
                }
                else if (tabOpenTarget == "_tab_province")
                {
                    wb.Url = new Uri(MerchantVar.AccessOfProvice + "/index.htm");
                }
            }
        }
        private void AddToAssistToolbar(dynamic module, BindingItem item)
        {
            assistToolbarPBCount++;
            string imageName = e0571.web.core.Utils.TypeConverter.ChangeString(module.Picture).Trim();
            Image image;
            if (imageName == string.Empty)
            {
                image = Properties.Resources.coffee;
            }
            else
            {
                image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(imageName.ToString());
            }
            string assistToolbarContentKey = module.ModuleId.ToString();
            string pageKey;
            string pageText;
            string pageUrl;
            string pageOpenTarget = module.OpenTarget.ToString();
            string pageCode = module.ModuleCode.ToString();
            if (item == null)
            {
                pageText = module.AliasName.ToString();
                pageUrl = module.AccessUrl.ToString();
                pageKey = "_tab_" + module.ModuleId.ToString();
            }
            else
            {
                pageText = item.Text; 
                pageUrl = item.Value;
                pageKey = "_tab_" + module.ModuleId.ToString() + "$" + item.Text;
            }

            Size assistToolbarSize = new Size(48, 48);
            Point assistToolbarLocation = Point.Empty;
            int index = pnlAssistToolbar.Controls.Count;
            if (index % 2 == 0)
            {
                assistToolbarLocation = new Point(1, ((index / 2) + 1) * 3 + (index / 2) * 48);
            }
            else
            {
                assistToolbarLocation = new Point(50, ((index / 2) + 1) * 3 + (index / 2) * 48);
            } 
            
            PictureBox pb = new PictureBox();
            pb.Size = assistToolbarSize;
            pb.Location = assistToolbarLocation;
            pb.Image = image;
            pb.BackColor = SystemColors.MenuHighlight;
            pb.MouseEnter += new EventHandler(pb_MouseEnter);
            pb.MouseLeave += new EventHandler(pb_MouseLeave);
            pb.Click += new EventHandler((s, e) =>
            {
                 
                if (assistToolbarPB == null)
                {
                    assistToolbarPB = s as PictureBox;
                    assistToolbarPB.BackColor = Color.Red; 
                }
                else
                {
                    if (assistToolbarPB == s)
                    {

                        if (pb.BackColor == SystemColors.MenuHighlight)
                        {
                            assistToolbarPB.BackColor = Color.Red; 
                        }
                        else
                        {
                            assistToolbarPB.BackColor = SystemColors.MenuHighlight; 
                        }
                    }
                    else
                    {
                        assistToolbarPB.BackColor = SystemColors.MenuHighlight;
                        assistToolbarPB = s as PictureBox;
                        pb.BackColor = Color.Red; 
                    }
                }

                OpenTarget(pageKey, pageText, pageUrl, pageOpenTarget, pageCode);
                
            });
            pnlAssistToolbar.Controls.Add(pb);
            xTooltip.SetToolTip(pb, pageText);
        }  
        private void btn_Click(object sender, EventArgs e)
        {
            if (isAutoLogined)
            {
                Button btn = sender as Button;
                //this.BackColor = btn.BackColor;
                dynamic module = btn.Tag as dynamic;
                string pageKey = "_tab_" + module.ModuleId.ToString();
                string pageText = module.AliasName.ToString();
                string pageUrl = module.AccessUrl.ToString();
                string pageOpenTarget = module.OpenTarget.ToString();
                string pageCode = module.ModuleCode;
                OpenTarget(pageKey, pageText, pageUrl, pageOpenTarget, pageCode);
            }
        }
        
        private void pb_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        } 
        private void pb_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        #endregion

        #region 其他控件事件处理
        private void frmMainV2X_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (autoLoginThread != null)
            {
                if (autoLoginThread.IsAlive)
                {
                    autoLoginThread.Abort();
                }
                autoLoginThread = null;
            }
            if (autoLoginForProvinceThread != null)
            {
                if (autoLoginForProvinceThread.IsAlive)
                {
                    autoLoginForProvinceThread.Abort();
                }
                autoLoginForProvinceThread = null;
            } 
            if (Remind_CircleTask != null )
            {
                if (Remind_CircleTask.IsAlive)
                {
                    Remind_CircleTask.Abort();
                }
                Remind_CircleTask = null;
            }
        }
        private void wbProvinceSum_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            wbProvinceSum.Document.InvokeScript("setExecModeForMer");
            wbProvinceSum.Document.InvokeScript("setClientSize", new object[] { wbProvinceSum.Width.ToString(), wbProvinceSum.Height.ToString() });
            if (e.Url.ToString() == MerchantVar.AccessOfProvice + _AutoLoginUrl)
            {
                autoLoginForProvinceThread = ThreadAdapter.WhileCircleTask(
                () =>
                {
                    if (!isAutoLoginedForProvince && flash4ajaxReadyForProvince)
                    {
                        this.Invoke(new Action(() =>
                        {
                            Common.DebugLog("----------------------------begin setAutoLogin for Province-------------------");
                            string jsonStr = JsonConvert.SerializeObject(new { Code = "demo", Password = "123" });
                            wbProvinceSum.Document.InvokeScript("setAutoLogin", new object[] { jsonStr });
                        }));
                        isAutoLoginedForProvince = true;
                    }
                },
                100,
                () =>
                {
                    return isAutoLoginedForProvince;
                }//flash准备好了则停止
                );

            }
            else if (e.Url.ToString() == MerchantVar.AccessOfProvice + "/index.htm")
            {
                wbProvinceSum.Document.InvokeScript("loadPage", new object[] { "welcome.htm" });
            }
        }
        private void wbWorkArea_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            wbWorkArea.Document.InvokeScript("setExecModeForMer");
            wbWorkArea.Document.InvokeScript("setClientSize", new object[] { wbWorkArea.Width.ToString(), wbWorkArea.Height.ToString() });
            if (e.Url.ToString() == MerchantVar.AccessPoint + _AutoLoginUrl)
            {
                autoLoginThread = ThreadAdapter.WhileCircleTask(
                () =>
                {
                    if (!isAutoLogined && flash4ajaxReady)
                    {
                        this.Invoke(new Action(() =>
                        {
                            Common.DebugLog("----------------------------begin setAutoLogin-------------------");
                            string jsonStr = JsonConvert.SerializeObject(new { Code = MerchantVar.UserCode, Password = MerchantVar.Password });
                            wbWorkArea.Document.InvokeScript("setAutoLogin", new object[] { jsonStr });
                        }));
                        isAutoLogined = true;
                        
                        Common.DebugLog("do _MessageLoopForRemind_Call");
                        //开启线程获取数据
                        bFetchReminderData = true;
                        //登录之后开启提示报警线程
                        /////_MessageLoopForRemind_Call();
                    }
                },
                100,
                () =>
                {
                    return isAutoLogined;
                }//flash准备好了则停止
                );
               
            }
            else if (e.Url.ToString() == MerchantVar.AccessPoint + "/index.htm")
            {
                wbWorkArea.Document.InvokeScript("loadPage", new object[] { "xlx/welcome.htm" });
            }
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

        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            downPosition = new Point(e.X, e.Y);
            down = true;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
        private void pbMax_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal; 
            }
            else
            {
                WindowState = FormWindowState.Maximized; 
            }
        }

        private void pbMax_MouseEnter(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                pbMax.Image = Properties.Resources.normal_on;
            }
            else
            {
                pbMax.Image = Properties.Resources.max_on;
            }
        }

        private void pbMax_MouseLeave(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                pbMax.Image = Properties.Resources.normal;
            }
            else
            {
                pbMax.Image = Properties.Resources.max;
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

        private void pbClose_MouseEnter(object sender, EventArgs e)
        {
            pbClose.Image = Properties.Resources.close_on;
        }

        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            pbClose.Image = Properties.Resources.close;
        }
         
        private void btnESC_Click(object sender, EventArgs e)
        {
            pnlHead.Visible = true;

            pnlLeft.Visible = pnlLeft.Controls.Count > 0;
            pnlRight.Visible = pnlRight.Controls.Count > 0;
            pnlTop.Visible = pnlTop.Controls.Count > 0;
            pnlBottom.Visible = pnlBottom.Controls.Count > 0;

            if (fullScreenWB != null)
            {
                fullScreenWB.Parent = xTabs.SelectedTab;
                fullScreenWB.Url = fullScreenWB.Url;
                fullScreenWB = null;
            }
        }
      
        private void pnlAssistToolbarTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int tx, ty;
                tx = Cursor.Position.X - newP.X;
                ty = Cursor.Position.Y - newP.Y;
                
                Common.DebugLog("tx:" + tx.ToString() + ",ty:" + ty.ToString());
                if (tx != 0 || ty != 0)
                {
                    int tempx = oldP.X + tx;
                    int tempy = oldP.Y + ty;
                    if (tempx < 0)
                    {
                        tempx = 0;
                    }
                    if (tempx > pnlMain.Width - pnlAssistToolbarContainer.Width)
                    {
                        tempx = pnlMain.Width - pnlAssistToolbarContainer.Width;
                    }
                    if (tempy < 0)
                    {
                        tempy = 0;
                    }
                    if (tempy > pnlMain.Height - pnlAssistToolbarContainer.Height)
                    {
                        tempy = pnlMain.Height - pnlAssistToolbarContainer.Height;
                    }
                    //Common.DebugLog("tempx:" + tempx.ToString() + ",tempy:" + tempy.ToString());
                    pnlAssistToolbarContainer.Location = new Point(tempx, tempy);
                }
                pnlAssistToolbarContainer.BringToFront();
            }
        }

        private void pnlAssistToolbarTitle_MouseDown(object sender, MouseEventArgs e)
        {
            oldP.X = this.pnlAssistToolbarContainer.Location.X;
            oldP.Y = this.pnlAssistToolbarContainer.Location.Y;

            newP.X = Cursor.Position.X;
            newP.Y = Cursor.Position.Y;
            pnlAssistToolbarContainer.BringToFront();
        }

        private void pnlAssistToolbarTitle_MouseUp(object sender, MouseEventArgs e)
        {
            INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INT_KEY_ASSIST_TOOLBAR_POSITION_LAST, pnlAssistToolbarContainer.Location.X.ToString() + "," + pnlAssistToolbarContainer.Location.Y.ToString(), Common.INI_FILE_PATH);
        }

        private void pnlAssistToolbarTitle_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.SizeAll;
        }

        private void pnlAssistToolbarTitle_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
        private void xTabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {

                Rectangle myTabRect = xTabs.GetTabRect(e.Index);

                //先添加TabPage属性   
                e.Graphics.DrawString(xTabs.TabPages[e.Index].Text
                , this.Font, SystemBrushes.ControlText, myTabRect.X + 2, myTabRect.Y + 2);
                if (e.Index > 0)
                {
                    //再画一个矩形框
                    using (Pen p = new Pen(Color.Red))
                    {
                        myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 6), 2);
                        myTabRect.Width = CLOSE_SIZE;
                        myTabRect.Height = CLOSE_SIZE;
                        e.Graphics.DrawRectangle(p, myTabRect);
                    }
                    //填充矩形框
                    Color recColor = Color.Red;
                    using (Brush b = new SolidBrush(recColor))
                    {
                        e.Graphics.FillRectangle(b, myTabRect);
                    }
                    //画关闭符号
                    using (Pen objpen = new Pen(Color.White))
                    {
                        //"\"线
                        Point p1 = new Point(myTabRect.X + 6, myTabRect.Y + 6);
                        Point p2 = new Point(myTabRect.X + myTabRect.Width - 6, myTabRect.Y + myTabRect.Height - 6);
                        e.Graphics.DrawLine(objpen, p1, p2);

                        //"/"线
                        Point p3 = new Point(myTabRect.X + 6, myTabRect.Y + myTabRect.Height - 6);
                        Point p4 = new Point(myTabRect.X + myTabRect.Width - 6, myTabRect.Y + 6);
                        e.Graphics.DrawLine(objpen, p3, p4);
                    }
                }
                e.Graphics.Dispose();
            }
            catch (Exception)
            {

            }
        }

        private void xTabs_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && xTabs.SelectedIndex > 0)
            {
                int x = e.X, y = e.Y;
                //计算关闭区域   
                Rectangle myTabRect = xTabs.GetTabRect(xTabs.SelectedIndex);

                myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
                myTabRect.Width = CLOSE_SIZE;
                myTabRect.Height = CLOSE_SIZE;

                //如果鼠标在区域内就关闭选项卡   
                bool isClose = x > myTabRect.X && x < myTabRect.Right
                 && y > myTabRect.Y && y < myTabRect.Bottom;

                if (isClose == true)
                {
                    (xTabs.SelectedTab.Controls[0] as WebBrowser).Url = new Uri("about:blank");
                    xTabs.TabPages.Remove(xTabs.SelectedTab);
                }
            }
        }

        
        #endregion

        #region 供子窗体调用
        public Point getWorkAreaLocation()
        {
            return xTabs.Location;
        }

        public Size getWorkAreaSize()
        {
            return xTabs.Size;
        }
        #endregion 

        #region  提醒
        private void _MessageLoopForRemind_Call()
        {
            Remind_CircleTask = ThreadAdapter.DoCircleTask(() =>
            {
                if (bFetchReminderData) {
                    FetchReminderData();
                }

                frmWorkOrderNotify.IconFlickerFlag = true;
                string YYPlayFlag = "";
                if (SettingsVar.CurrentRemindType == RemindType.详细信息 && queuesOfWorkOrderReminderInfo.Count > 0)
                {
                    var xItem = queuesOfWorkOrderReminderInfo.Dequeue();
                    YYPlayFlag = xItem.SourceType;
                    notifyWindow.ReceiveReminderInfo(xItem);
                }
                else if (SettingsVar.CurrentRemindType == RemindType.统计信息 && queuesOfWorkOrderReminderStatInfo.Count > 0)
                {
                    var xItem = queuesOfWorkOrderReminderStatInfo.Dequeue();
                    YYPlayFlag = xItem.SourceType;
                    notifyWindow.ReceiveReminderStatInfo(xItem);
                }
                if (SettingsVar.CurrentYYRemindFlag == 1 && !string.IsNullOrEmpty(YYPlayFlag))
                {
                    PlayRemindYY(YYPlayFlag);
                }
            },
            20000,
            () =>
            {
                return false;   //一直执行
            },
            () =>
            {
                return SettingsVar.CurrentRemindType == RemindType.不提醒;
            });
        }

        //填充数据
        private void FetchReminderData()
        {
            try
            {
                //清空队列
                queuesOfWorkOrderReminderInfo.Clear();
                queuesOfWorkOrderReminderStatInfo.Clear();

                string dataPointOfMerchantUrl = MerchantVar.DataPointOfMerchant;
                if (!string.IsNullOrEmpty(dataPointOfMerchantUrl))
                {
                    string url = dataPointOfMerchantUrl + "/Pub/ReminderService/GetReminderStatGroupBySourceType";
                    if (SettingsVar.CurrentRemindType == RemindType.详细信息)
                    {
                        url = dataPointOfMerchantUrl + "/Pub/ReminderService/GetReminderItems";
                    }
                    url += "/AllSourceTable,AllSourceColumn";
                    HttpAdapter.getSyncTo(url, null, new { ApplicationId = Common.APPLICATION_ID, StationId = MerchantVar.StationId }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if ((bool)ret.Success)
                        {
                            foreach (var row in ret.rows)
                            {
                                dynamic item = new ExpandoObject();
                                DynamicAdapter.Parse(item, XElement.Parse(row.ToString()));
                                if (SettingsVar.CurrentRemindType == RemindType.详细信息)
                                {
                                    WorkOrderReminderInfo dataItem = (item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<WorkOrderReminderInfo>();

                                    dataItem.AccessPoint = dataPointOfMerchantUrl;
                                    dataItem.StationId = MerchantVar.StationId;
                                    queuesOfWorkOrderReminderInfo.Enqueue(dataItem);
                                }
                                else if (SettingsVar.CurrentRemindType == RemindType.统计信息)
                                {
                                    WorkOrderReminderStatInfo dataItem = (item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<WorkOrderReminderStatInfo>();
                                    dataItem.AccessPoint = dataPointOfMerchantUrl;
                                    dataItem.StationId = MerchantVar.StationId;
                                    queuesOfWorkOrderReminderStatInfo.Enqueue(dataItem);
                                }
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBoxAdapter.ShowConfirm(ex.Message);
            }
        }

        //播放语音
        private void PlayRemindYY(string playYYType)
        {
            string strYYUrl = "";
            if (playYYType == "A0101")
            {
                strYYUrl = Common.DEFAULT_SOUND_NAME_OF_WAIT_RESPONSE;
            }
            else if (playYYType == "A0102")
            {
                strYYUrl = Common.DEFAULT_SOUND_NAME_OF_DEALING;
            }
            else if (playYYType == "B0101") {
                strYYUrl = Common.DEFAULT_SOUND_NAME_OF_WEIXING;
            }
            if (!string.IsNullOrEmpty(strYYUrl))
            {
                if (soundPlayer == null)
                {
                    soundPlayer = new System.Media.SoundPlayer();
                }
                soundPlayer.SoundLocation = strYYUrl;
                soundPlayer.Load();
                soundPlayer.Play();
            }
        }


        private void OnClickReminderInfoWin(WorkOrderReminderInfo item)
        {
            IEnumerable<Button> clickBtns = pnlTop.Controls.OfType<Button>();

            string strAliasName = "";
            if (item.SourceType == "B0101")
            {
                strAliasName = "微信审核";
            }
            else if (item.SourceType.IndexOf("A01") > -1)
            {
                strAliasName = "工单处理";
            }

            dynamic module;
            foreach (var btn in clickBtns)
            {
                module = btn.Tag as dynamic;
                if (!string.IsNullOrEmpty(strAliasName) && (string)module.AliasName == strAliasName && wbWorkArea.Url.ToString().IndexOf((string)module.AccessUrl) <0)
                {
                    btn.PerformClick();
                }
            }
            /*if (item.SourceType == "A0101")
            {
                //待响应的工单
                wbWorkArea.Document.InvokeScript("responseWorkOrder", new object[] { item.SourceKey, item.StationId });
            }
            else if (item.SourceType == "A0102")
            {
                //处理中的工单
                wbWorkArea.Document.InvokeScript("dispatchServeMan", new object[] { item.SourceKey, item.StationId });
            }*/

            OnIgnoreReminderInfo(item);
        }
        private void OnClickReminderStatInfoWin(WorkOrderReminderStatInfo item)
        {
            IEnumerable<Button> clickBtns = pnlTop.Controls.OfType<Button>();
            
            string strAliasName = "";
            if (item.SourceType == "B0101")
            {
                strAliasName = "微信审核";
            }
            else if (item.SourceType.IndexOf("A01") > -1)
            {
                strAliasName = "工单处理";
            }

            dynamic module;
            foreach (var btn in clickBtns)
            {
                module = btn.Tag as dynamic;
                if (!string.IsNullOrEmpty(strAliasName) && (string)module.AliasName == strAliasName && wbWorkArea.Url.ToString().IndexOf((string)module.AccessUrl) < 0)
                {
                    btn.PerformClick();
                }
            }

            OnIgnoreReminderStatInfo(item);
        }

        private void OnIgnoreReminderInfo(WorkOrderReminderInfo item)
        {
            //string url = item.AccessPoint.Replace("http://115.236.175.110:17001/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Pub/ReminderService/IgnoreReminder";
            HttpAdapter.postAsyncAsJSON(item.AccessPoint + "/Pub/ReminderService/IgnoreReminder", new { ResponseAppType = "00004", ObjectType = "Merchant", ObjectKey = item.StationId, SourceType = item.SourceType, SourceKey = item.SourceKey }.ToStringObjectDictionary(), new { ApplicationId = Common.APPLICATION_ID, StationId = MerchantVar.StationId }.ToStringObjectDictionary(), (ret, res) =>
            {

            });
        }
        private void OnIgnoreReminderStatInfo(WorkOrderReminderStatInfo item)
        {
            //string url = item.AccessPoint.Replace("http://115.236.175.110:17001/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Pub/ReminderService/IgnoreReminder";
            HttpAdapter.postAsyncAsJSON(item.AccessPoint + "/Pub/ReminderService/IgnoreReminder", new { ResponseAppType = "00004", ObjectType = "Merchant", ObjectKey = item.StationId, SourceType = item.SourceType }.ToStringObjectDictionary(), new { ApplicationId = Common.APPLICATION_ID, StationId = MerchantVar.StationId }.ToStringObjectDictionary(), (ret, res) =>
            {
                //MessageBoxAdapter.ShowDebug(ret.ErrorMessage);
            });
        }
        private void OnCloseWin()
        {
            if (queuesOfWorkOrderReminderInfo.Count == 0 && queuesOfWorkOrderReminderStatInfo.Count == 0)
            {
                bFetchReminderData = true;
            }
            else {
                bFetchReminderData = false;
            }
        }

        #endregion

       

        
    }

    public static class Util
    {
        public enum Effect { Roll, Slide, Center, Blend }

        public static void Animate(Control ctl, Effect effect, int msec, int angle)
        {
            int flags = effmap[(int)effect];
            if (ctl.Visible) { flags |= 0x10000; angle += 180; }
            else
            {
                if (ctl.TopLevelControl == ctl) flags |= 0x20000;
                else if (effect == Effect.Blend) throw new ArgumentException();
            }
            flags |= dirmap[(angle % 360) / 45];
            bool ok = AnimateWindow(ctl.Handle, msec, flags);
            if (!ok) throw new Exception("Animation failed");
            ctl.Visible = !ctl.Visible;
        }

        private static int[] dirmap = { 1, 5, 4, 6, 2, 10, 8, 9 };
        private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };

        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);
    }
}
