using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using win.core.controls;
using win.core.utils;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.Utils;
using System.Xml;
using Newtonsoft.Json;
using System.Dynamic;
using System.Xml.Linq;


namespace SmartLife.Client.Merchant
{
    public partial class frmServiceSettings : Form
    {
        public frmServiceSettings()
        {
            InitializeComponent();
            xLoadingPanel.OnRotateStateChanged += new LoadingPanel.RotateStateChangedHandler(() => { });
        }

        dynamic item;
        dynamic serverModeType;
        dynamic serverItemType;
        string stationId = "";
        List<object> serveControlList = new List<object>();

        string LastError { get; set; }

        private void frmServiceSettings_Load(object sender, EventArgs e)
        {
            List<KeyValuePair<object, string>> listKeyValue = new List<KeyValuePair<object, string>>();
            if (MerchantVar.CurrentMerchant.AuthNodeInfos.Count > 0)
            {
                foreach (var nodeInfo in MerchantVar.CurrentMerchant.AuthNodeInfos)
                {
                    listKeyValue.Add(new KeyValuePair<object, string>(nodeInfo.StationId, nodeInfo.Remark));
                }
            }
            cbbArea.DataSource = listKeyValue;
            cbbArea.DisplayMember = "value";
            cbbArea.ValueMember = "key";
            cbbArea.SelectedIndexChanged += cbbArea_SelectedIndexChanged;

            dtpServeTimeBeginOfDay.Text = DateTime.Now.ToString("HH:mm:ss");
            dtpServeTimeEndOfDay.Text = DateTime.Now.ToString("HH:mm:ss");

            InitFormInfo();
            stationId = ((KeyValuePair<object, string>)cbbArea.SelectedItem).Key.ToString();
            FetchData();
        }

        #region 商家操作
        private void cbbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSelectValue = ((KeyValuePair<object, string>)cbbArea.SelectedItem).Key.ToString();
            if (stationId != strSelectValue)
            {
                stationId = strSelectValue;
                FetchData();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SubmitDataOfMerchant();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbxTmp = (CheckBox)sender;
            bool bChecked = cbxTmp.Checked;
            string key = "";

            if (cbxTmp.Name == "cbxAll")
            {
                dymicFindControl(tlpServerItem, bChecked);
            }
            if (cbxTmp.Parent.Name.IndexOf("pnlServerItemA") > -1)
            {
                key = "pnlServerItemB" + cbxTmp.Parent.Name.Replace("pnlServerItemA", "");
                dymicFindControl(tlpServerItem.Controls[key], bChecked);
            }
            if (cbxTmp.Parent.Name.IndexOf("pnlServerItemB") > -1)
            {
                key = cbxTmp.Name.Replace("cbx", "txt");
                cbxTmp.Parent.Controls[key].Visible = bChecked;
            }
        }

        //提交服务信息
        private void ServiceItemOk_Click(object sender, EventArgs e)
        {
            SubmitDataOfMerchantServeItem();
        }

        private void ServiceItemCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region 数据操作
        dynamic serveItemA;
        dynamic serveItemB;

        //初始化界面信息
        private void InitFormInfo()
        {
            string url = "";
            //if (MerchantVar.CurrentMerchant.AuthNodeInfos.Count > 0)
            //{
            //    foreach (var nodeInfo in MerchantVar.CurrentMerchant.AuthNodeInfos)
            //    {
            //        if (nodeInfo.StationId.ToString() != stationid)
            //        {
            //            continue;
            //        }
            //        url = nodeInfo.AccessPoint ;
            //    }
            //}

            if (MerchantVar.CurrentMerchant.AuthNodeInfos.Count > 0)
            {
                url = MerchantVar.CurrentMerchant.AuthNodeInfos.FirstOrDefault(s => s.StationId != null).AccessPoint;
            }
            //url = "http://localhost/SmartLife.CertManage.MerchantServices";
            if (url != "")
            {
                new Action(() =>
                {
                    HttpAdapter.getSyncTo(url + "/Share/AjaxData/GetDictionaryItem/11012", null, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        serveItemA = ret;
                    });
                    HttpAdapter.getSyncTo(url + "/Share/AjaxData/GetDictionaryItem/11013", null, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        serveItemB = ret;
                    });
                }).BeginInvoke(new AsyncCallback((ar) =>
                {
                    this.UIInvoke(() =>
                    {
                        if (serveItemA != null && serveItemB != null)
                        {
                            List<CheckBox> itemBList;
                            List<UnderlineTextBox> txtBList;
                            CheckBox cbxItem;
                            UnderlineTextBox txtItem;
                            int iRow = 1;
                            foreach (var itemA in serveItemA)
                            {
                                itemBList = new List<CheckBox>();
                                txtBList = new List<UnderlineTextBox>();

                                int iflag = 0;
                                int iRowHeight = 0;
                                foreach (var itemB in serveItemB)
                                {
                                    if (((string)itemB.ItemCode).Substring(0, 2) == (string)itemA.ItemCode)
                                    {
                                        //文本框
                                        txtItem = new UnderlineTextBox();
                                        txtItem.Name = "txt" + (string)itemB.ItemId + "of" + (string)itemA.ItemCode;
                                        txtItem.Text = "0";
                                        txtItem.Visible = false;

                                        //复选框
                                        cbxItem = new CheckBox();
                                        cbxItem.AutoSize = true;
                                        cbxItem.Name = "cbx" + (string)itemB.ItemId + "of" + (string)itemA.ItemCode;
                                        cbxItem.Text = (string)itemB.ItemName;
                                        //cbxItem.Width = 180;

                                        iRowHeight = 6 + 24 * iflag;
                                        if (itemBList.Count % 2 == 0)
                                        {
                                            cbxItem.Location = new Point(6, iRowHeight);
                                            txtItem.Location = new Point(29 + cbxItem.Width, iRowHeight);
                                        }
                                        else
                                        {
                                            cbxItem.Location = new Point(176, iRowHeight);
                                            txtItem.Location = new Point(209 + cbxItem.Width + txtItem.Width, iRowHeight);
                                            iflag++;
                                        }
                                        cbxItem.CheckedChanged += cbxAll_CheckedChanged;
                                        itemBList.Add(cbxItem);
                                        txtBList.Add(txtItem);

                                    }
                                }

                                tlpServerItem.RowStyles.Clear();
                                tlpServerItem.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                                Panel pItem = new Panel();
                                pItem.Name = "pnlServerItemA" + (string)itemA.ItemCode;
                                pItem.Dock = DockStyle.Fill;
                                pItem.AutoSize = true;
                                tlpServerItem.Controls.Add(pItem, 0, iRow);

                                cbxItem = new CheckBox();
                                cbxItem.AutoSize = true;
                                cbxItem.Name = "cbx" + (string)itemA.ItemId + "of" + (string)itemA.ItemCode;
                                cbxItem.Text = (string)itemA.ItemName;
                                cbxItem.Dock = DockStyle.Fill;
                                cbxItem.CheckedChanged += cbxAll_CheckedChanged;
                                pItem.Controls.Add(cbxItem);

                                pItem = new Panel();
                                pItem.Name = "pnlServerItemB" + (string)itemA.ItemCode;
                                pItem.Height = 24 * ((itemBList.Count % 2) == 0 ? (itemBList.Count / 2) : (itemBList.Count / 2 + 1));
                                pItem.Dock = DockStyle.Fill;
                                tlpServerItem.Controls.Add(pItem, 1, iRow);
                                pItem.Controls.AddRange(itemBList.ToArray());
                                pItem.Controls.AddRange(txtBList.ToArray());

                                iRow++;
                            }
                        }
                        xLoadingPanel.Stop();
                    });
                }), null);

                xLoadingPanel.Start();
            }

        }

        private void FetchData()
        {
            if (string.IsNullOrEmpty(stationId)) return;
            new Action(() =>
            {
                if (!string.IsNullOrEmpty(stationId))
                {
                    string url = MerchantVar.CurrentMerchant.AuthNodeInfos.FirstOrDefault(s => s.StationId.ToString() == stationId).AccessPoint;
                    if (!string.IsNullOrEmpty(url))
                    {
                        //url = url.Replace("http://115.236.175.110:17000/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Oca/MerchantService/";
                        url = url + "/Oca/MerchantService/";
                        HttpAdapter.getSyncTo(url + stationId, null, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode }.ToStringObjectDictionary(), (ret, res) =>
                        {
                            if ((bool)ret.Success && ret.instance != null && ret.instance.StationId != null)
                            {
                                item = ret.instance;
                                LastError = null;
                            }
                            else
                            {
                                LastError = ret.ErrorMessage;
                            }
                        });

                        HttpAdapter.getSyncTo(url + "GetServeModes/" + stationId, null, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode }.ToStringObjectDictionary(), (ret, res) =>
                        {
                            serverModeType = ret;
                        });

                        HttpAdapter.getSyncTo(url + "GetServeItems/" + stationId, null, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode }.ToStringObjectDictionary(), (ret, res) =>
                        {
                            serverItemType = ret;
                        });
                    }
                }
            }).BeginInvoke(new AsyncCallback((ar) =>
            {
                this.UIInvoke(() =>
                {
                    if (item != null)
                    {
                        foreach (var key in item)
                        {
                            if (pnlAcceptType.Name == ("pnl" + (string)key.Name))
                            {
                                IEnumerable<CheckBox> cbxsAcceptType = pnlAcceptType.Controls.OfType<CheckBox>();
                                string[] cbxs = e0571.web.core.Utils.TypeConverter.ChangeString(key.Value).Split(',');
                                if (cbxs.Length > 0)
                                {
                                    foreach (var cbx in cbxsAcceptType)
                                    {
                                        foreach (string cb in cbxs)
                                        {
                                            if (cbx.Name == "cbx" + cb + "Of11019")
                                            {
                                                cbx.Checked = true;
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }
                            if (pnlSettlementPeriod.Name == ("pnl" + (string)key.Name))
                            {
                                IEnumerable<RadioButton> rbsSettlementPeriod = pnlSettlementPeriod.Controls.OfType<RadioButton>();
                                foreach (var rb in rbsSettlementPeriod)
                                {
                                    if (rb.Name == "rb" + e0571.web.core.Utils.TypeConverter.ChangeString(key.Value) + "Of00014")
                                    {
                                        rb.Checked = true;
                                        break;
                                    }
                                }
                            }
                            if (pnlSettlementMode.Name == ("pnl" + (string)key.Name))
                            {
                                IEnumerable<RadioButton> rbsSettlementMode = pnlSettlementMode.Controls.OfType<RadioButton>();
                                foreach (var rb in rbsSettlementMode)
                                {
                                    if (rb.Name == "rb" + e0571.web.core.Utils.TypeConverter.ChangeString(key.Value) + "Of11025")
                                    {
                                        rb.Checked = true;
                                        break;
                                    }
                                }
                            }
                            if (pnlWorkDay.Name == ("pnl" + (string)key.Name))
                            {
                                IEnumerable<CheckBox> cbxsWorkDay = pnlWorkDay.Controls.OfType<CheckBox>();
                                string[] cbxs = e0571.web.core.Utils.TypeConverter.ChangeString(key.Value).Split(',');
                                if (cbxs.Length > 0)
                                {
                                    foreach (var cbx in cbxsWorkDay)
                                    {
                                        foreach (string cb in cbxs)
                                        {
                                            if (cbx.Name == "cbx_Day_" + cb)
                                            {
                                                cbx.Checked = true;
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }
                            if (dtpServeTimeBeginOfDay.Name == ("dtp" + (string)key.Name))
                            {
                                dtpServeTimeBeginOfDay.Text = e0571.web.core.Utils.TypeConverter.ChangeString(key.Value);
                            }
                            if (dtpServeTimeEndOfDay.Name == ("dtp" + (string)key.Name))
                            {
                                dtpServeTimeEndOfDay.Text = e0571.web.core.Utils.TypeConverter.ChangeString(key.Value);
                            }
                            if (txtServeTimeOfDayDescription.Name == ("txt" + (string)key.Name))
                            {
                                txtServeTimeOfDayDescription.Text = e0571.web.core.Utils.TypeConverter.ChangeString(key.Value);
                            }
                            if (txtServeExtraComment.Name == ("txt" + (string)key.Name))
                            {
                                txtServeExtraComment.Text = e0571.web.core.Utils.TypeConverter.ChangeString(key.Value);
                            }
                        }
                    }

                    if (serverModeType != null)
                    {
                        IEnumerable<CheckBox> cbxModeTypes = tabPage2.Controls.OfType<CheckBox>();
                        CheckBox cbxModeItem;
                        foreach (var modeitem in serverModeType)
                        {
                            cbxModeItem = cbxModeTypes.FirstOrDefault(s => s.Name.IndexOf((string)modeitem.ServeMode) > -1);
                            if (cbxModeItem != null)
                            {
                                cbxModeItem.Checked = true;
                            }
                        }
                    }

                    if (serverItemType != null)
                    {
                        serveControlList.Clear();
                        dymicFindControl(tlpServerItem);
                        string name = "";
                        string txt = "";
                        foreach (var key in serverItemType)
                        {
                            name = (string)key.ServeItemB;
                            txt = (string)key.ServeFee;
                            foreach (var c in serveControlList)
                            {
                                if (c is CheckBox)
                                {
                                    if (((CheckBox)c).Name.IndexOf(name) > -1) ((CheckBox)c).Checked = true;
                                }
                                if (c is UnderlineTextBox)
                                {
                                    if (((UnderlineTextBox)c).Name.IndexOf(name) > -1) ((UnderlineTextBox)c).Text = txt;
                                }
                            }
                        }
                        
                    }

                    xLoadingPanel.Stop();
                    if (!string.IsNullOrEmpty(LastError))
                    {
                        MessageBoxAdapter.ShowError(LastError);
                    }
                });
            }), null);
            xLoadingPanel.Start();
        }

        private void SubmitDataOfMerchant()
        {
            //if (item != null)
            //{
                new Action(() =>
                {
                    if (MessageBoxAdapter.ShowConfirm("您确定要提交修改吗") == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(stationId)) {
                            string url = MerchantVar.CurrentMerchant.AuthNodeInfos.FirstOrDefault(s => s.StationId.ToString() == stationId).AccessPoint;

                            if (!string.IsNullOrEmpty(url))
                            {
                                StringObjectDictionary postParam = new
                                {
                                    StationId = Guid.Parse(stationId)
                                }.ToStringObjectDictionary();

                                //接单方式
                                var cbxAcceptTypeList = pnlAcceptType.Controls.OfType<CheckBox>().Where(c => c.Checked);
                                if (cbxAcceptTypeList != null)
                                {
                                    foreach (var row in cbxAcceptTypeList)
                                    {
                                        postParam["AcceptType"] = row.Name.Substring(3, 5) + ",";
                                    }
                                }
                                //结算周期
                                RadioButton rbOfSettlementPeriod = pnlSettlementPeriod.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                                if (rbOfSettlementPeriod != null)
                                {
                                    postParam["SettlementPeriod"] = rbOfSettlementPeriod.Name.Substring(2, 5);
                                }
                                //结算方式
                                RadioButton rbOfSettlementMode = pnlSettlementMode.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                                if (rbOfSettlementPeriod != null)
                                {
                                    postParam["SettlementMode"] = rbOfSettlementPeriod.Name.Substring(2, 5);
                                }

                                //接单方式
                                var cbxWorkDayList = pnlWorkDay.Controls.OfType<CheckBox>().Where(c => c.Checked);
                                if (cbxWorkDayList != null)
                                {
                                    string strday = "";
                                    foreach (var row in cbxWorkDayList)
                                    {
                                        strday += row.Name.Substring(row.Name.Length - 1, 1) + ",";
                                    }
                                    postParam["WorkDay"] = strday;
                                }
                                //
                                DateTime serveBeginTime, serveEndTime;
                                if (DateTime.TryParse(dtpServeTimeBeginOfDay.Text, out serveBeginTime))
                                {
                                    postParam["ServeTimeBeginOfDay"] = serveBeginTime.ToString("HH:mm");
                                }
                                if (DateTime.TryParse(dtpServeTimeEndOfDay.Text, out serveEndTime))
                                {
                                    postParam["ServeTimeEndOfDay"] = serveEndTime.ToString("HH:mm");
                                }

                                postParam["ServeTimeOfDayDescription"] = txtServeTimeOfDayDescription.Text.Trim();
                                postParam["ServeExtraComment"] = txtServeExtraComment.Text.Trim();

                                //url = url.Replace("http://115.236.175.110:17000/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") ;
                                HttpAdapter.postSyncAsJSON(url + "/Oca/MerchantService", postParam, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode }.ToStringObjectDictionary(), (ret, res) =>
                                {
                                    if ((bool)ret.Success)
                                    {
                                        LastError = null;
                                    }
                                    else
                                    {
                                        LastError = ret.ErrorMessage;
                                    }
                                });
                            }
                        }
                    }
                    else
                    {
                        LastError = Common.ERROR_USER_CANCEL;
                    }

                }).BeginInvoke(new AsyncCallback((ar) =>
                {
                    this.UIInvoke(() =>
                    {
                        xLoadingPanel.Stop();
                        if (!string.IsNullOrEmpty(LastError))
                        {
                            if (LastError != Common.ERROR_USER_CANCEL)
                            {
                                MessageBoxAdapter.ShowError(LastError);
                            }
                        }
                        else
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    });
                }), null);

                xLoadingPanel.Start();
            //}
        }

        private void SubmitDataOfMerchantServeItem()
        {
            //if (item != null)
            //{
            new Action(() =>
            {
                if (MessageBoxAdapter.ShowConfirm("您确定要提交修改吗") == System.Windows.Forms.DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(stationId))
                    {
                        string url = MerchantVar.CurrentMerchant.AuthNodeInfos.FirstOrDefault(s => s.StationId.ToString() == stationId).AccessPoint;

                        if (!string.IsNullOrEmpty(url))
                        {
                            StringObjectDictionary postParam = new { Status = 1 }.ToStringObjectDictionary();
                            IList<string> ServeItemBList = new List<string>();
                            IList<string> ServeFeeList = new List<string>();
                            IList<string> ServeModeList = new List<string>();

                            serveControlList.Clear();
                            dymicFindControl(tlpServerItem);
                            IEnumerable<object> checkedList = serveControlList.Where(s => (s is CheckBox) && ((CheckBox)s).Checked == true);
                            UnderlineTextBox obj_Txt;
                            CheckBox obj_Cbx;
                            foreach (var c in checkedList)
                            {
                                obj_Cbx = (CheckBox)c;
                                string txtKey = obj_Cbx.Name.Replace("cbx", "txt");
                                obj_Txt = (UnderlineTextBox)serveControlList.FirstOrDefault(s => (s is UnderlineTextBox) && ((UnderlineTextBox)s).Name == txtKey);
                                if (obj_Txt != null && !string.IsNullOrEmpty(obj_Txt.Text))
                                {
                                    ServeItemBList.Add(txtKey.Substring(3, 5));
                                    ServeFeeList.Add(obj_Txt.Text);
                                    postParam["Status"] = 1;
                                }
                                else {
                                    postParam["Status"] = 0;
                                    LastError = "必须填写【" + obj_Cbx.Text + "】每小时的服务费用";
                                    obj_Cbx.Focus();
                                    break;
                                }
                            }

                            if ((int)postParam["Status"] > 0)
                            {
                                IEnumerable<CheckBox> cbxModeTypes = tabPage2.Controls.OfType<CheckBox>();
                                foreach (var c in cbxModeTypes)
                                {
                                    if (c.Checked)
                                    {
                                        ServeModeList.Add(c.Name.Substring(3, 5));
                                    }
                                }
                                postParam["ServeItemB"] = ServeItemBList;
                                postParam["ServeFee"] = ServeFeeList;
                                postParam["ServeMode"] = ServeModeList;

                                //url = url.Replace("http://115.236.175.110:17000/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Oca/MerchantService/SetServeItemsAndServeModes/" + stationId;
                                url = url + "/Oca/MerchantService/SetServeItemsAndServeModes/" + stationId;
                                HttpAdapter.postSyncAsJSON(url, postParam, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode }.ToStringObjectDictionary(), (ret, res) =>
                                {
                                    if ((bool)ret.Success)
                                    {
                                        LastError = null;
                                    }
                                    else
                                    {
                                        LastError = ret.ErrorMessage;
                                    }
                                });
                            }
                        }
                    }
                }
                else
                {
                    LastError = Common.ERROR_USER_CANCEL;
                }

            }).BeginInvoke(new AsyncCallback((ar) =>
            {
                this.UIInvoke(() =>
                {
                    xLoadingPanel.Stop();
                    if (!string.IsNullOrEmpty(LastError))
                    {
                        if (LastError != Common.ERROR_USER_CANCEL)
                        {
                            MessageBoxAdapter.ShowError(LastError);
                        }
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                });
            }), null);
            xLoadingPanel.Start();
            //}
        }
        #endregion

        #region 公共方法
        //递归查找
        private void dymicFindControl(Control ctl,bool bChecked)
        {
            //object retControl = new object();
            foreach (Control c in ctl.Controls)
            {
                if (c is Panel) dymicFindControl(c,bChecked); //递归 
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = bChecked;
                }
                if (c is UnderlineTextBox)
                {
                    ((UnderlineTextBox)c).Visible = bChecked;
                }
            }
            //return retControl;
        }

        private void dymicFindControl(Control ctl)
        {
            foreach (Control c in ctl.Controls)
            {
                if (c is Panel) dymicFindControl(c); //递归 
                if (c is CheckBox)
                {
                    serveControlList.Add((CheckBox)c);
                }
                if (c is UnderlineTextBox)
                {
                    serveControlList.Add((UnderlineTextBox)c);
                }
            }
        }

        #endregion 

    }

    #region 重载文本框
    public class UnderlineTextBox : TextBox
    {
        public UnderlineTextBox()
        {
            //InitializeComponent();
            this.Width = 30;
            this.BorderStyle = BorderStyle.None;
        }
        private Color _linecolor = Color.Black ;
        /// <summary>
        /// 线条颜色
        /// </summary>
        public Color LineColor
        {
            get
            {
                return this._linecolor;
            }
            set
            {
                this._linecolor = value;
                this.Invalidate();
            }
        }
        private const int WM_PAINT = 0xF;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                DrawLine();
            }
        }
        private void DrawLine()
        {
            Graphics g = this.CreateGraphics();
            using(Pen p = new Pen(this._linecolor ))
            {
                g.DrawLine(p,0,this.Height -1 ,this.Width ,this.Height -1);
            }
        }
    }
    #endregion
}
