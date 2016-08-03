using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using win.core.controls;
using win.core.utils;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.Utils;
using System.Xml;
using Newtonsoft.Json;
using System.Dynamic;
using System.Xml.Linq;

using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;

namespace SmartLife.Client.Merchant
{
    
    public partial class frmWorkOrderProcessingDetail : Form
    {
        private System.IO.Stream streamToPrint;
        string streamType;

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt
        (
            IntPtr hdcDest, // handle to destination DC
            int nXDest, // x-coord of destination upper-left corner
            int nYDest, // y-coord of destination upper-left corner
            int nWidth, // width of destination rectangle
            int nHeight, // height of destination rectangle
            IntPtr hdcSrc, // handle to source DC
            int nXSrc, // x-coordinate of source upper-left corner
            int nYSrc, // y-coordinate of source upper-left corner
            System.Int32 dwRop // raster operation code
        );

        public frmWorkOrderProcessingDetail()
        {
            InitializeComponent();
            xLoadingPanel.OnRotateStateChanged += new LoadingPanel.RotateStateChangedHandler(() => { });
        }

        string LastError { get; set; }

        public string AccessPoint { get; set; }
        public Guid? StationId { get; set; }
        public string WorkOrderId { get; set; }

        dynamic item;

        private void frmWorkOrderProcessingDetail_Load(object sender, EventArgs e)
        {
            InitForm();
            FetchData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SubmitData(); 
        }

        #region 帮助方法
        private void InitForm()
        {
            lblCurrentStation.Text = MerchantVar.CurrentMerchant.StationName;
            txtServeManName.Visible = false; 
        } 
        #endregion

        #region 数据操作 
        private void FetchData()
        {
            new Action(() =>
            {
                //string url = AccessPoint.Replace("http://115.236.175.110:17000/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Oca/WorkOrderService/GetWorkOrderInfo/" + WorkOrderId;
                string url = AccessPoint + "/Oca/WorkOrderService/GetWorkOrderInfo/" + WorkOrderId;
                HttpAdapter.getSyncTo(url, null, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode, StationId = StationId }.ToStringObjectDictionary(), (ret, res) =>
                {
                    if ((bool)ret.Success)
                    {
                        item = new ExpandoObject();
                        DynamicAdapter.Parse(item, XElement.Parse(ret.ret.ToString()));
                        LastError = null;
                    }
                    else
                    {
                        LastError = ret.ErrorMessage;
                    }
                });


            }).BeginInvoke(new AsyncCallback((ar) =>
            {
                //AsyncResult result = (AsyncResult)ar;
                this.UIInvoke(() =>
                {
                    if (item != null)
                    {
                        int doStatus = int.Parse(item.StringObjectDictionary.DoStatus);
                        IDictionary<string, object> dataItem = (item.StringObjectDictionary as IDictionary<string, object>);
                        if (doStatus > 0 && doStatus < 4)
                        {
                            if (e0571.web.core.Utils.TypeConverter.ChangeString(dataItem["ServeResult"]) == ""
                            ||
                            e0571.web.core.Utils.TypeConverter.ChangeString(dataItem["FeedbackToOldMan"]) == ""
                            )
                            {
                                //处置督办
                                txtServeManName.Visible = true;
                                
                            }
                        } 

                        lblWorkOrderNo.Text = item.StringObjectDictionary.WorkOrderNo;
                        lblOperatedByName.Text = item.StringObjectDictionary.OperatedByName;
                        lblDoStatus.Text = WorkOrderInfo.getDoStatusName(doStatus);

                        foreach (var key in dataItem.Keys)
                        {
                            Control[] controlsOfInfo = tlpInfo.Controls.Find("lbl" + key, true);
                            if (controlsOfInfo.Length == 1)
                            {
                                (controlsOfInfo[0] as Label).Text = e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]);

                                if (key == "Gender")
                                {
                                    (controlsOfInfo[0] as Label).Text = WorkOrderInfo.getGenderName(e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]));
                                }
                                else if (key == "ServeFee" || key == "ServeFeeByGov" || key == "ServeFeeBySelf")
                                {
                                    if (e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]) != "")
                                    {
                                        (controlsOfInfo[0] as Label).Text = e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]) + "/小时";
                                    }
                                }
                            }

                            //查找process内的主动控件 
                            if (txtServeManName.Visible && txtServeManName.Name == ("txt" + key))
                            {
                                txtServeManName.Text = e0571.web.core.Utils.TypeConverter.ChangeString(dataItem[key]);
                                txtServeManName.BackColor = Color.LightGreen;
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
        private void SubmitData()
        {
            if (item != null)
            {
                new Action(() =>
                {
                    int doStatus = int.Parse(item.StringObjectDictionary.DoStatus);

                    if (doStatus == 0)
                    {
                        
                    }
                    else
                    {

                        //商家填写工单服务过程信息 
                        if (MessageBoxAdapter.ShowConfirm("您确定要派遣人员吗") == System.Windows.Forms.DialogResult.OK)
                        {
                            bool canSubmit = true;
                            StringObjectDictionary postParam = new { WorkOrderId = Guid.Parse(WorkOrderId) }.ToStringObjectDictionary();

                            if (txtServeManName.Text.Trim() != "")
                            {
                                postParam["ServeManName"] = txtServeManName.Text.Trim();
                            }
                            else
                            {
                                LastError = "必须填写服务人";
                                canSubmit = false;
                            } 


                            if (canSubmit)
                            {
                                //string url = AccessPoint.Replace("http://115.236.175.110:17000/merchantservices", "http://localhost/SmartLife.CertManage.MerchantServices") + "/Oca/WorkOrderService/DispatchServeMan";
                                string url = AccessPoint + "/Oca/WorkOrderService/DispatchServeMan";
                                HttpAdapter.postSyncAsJSON(url, postParam, new { ApplicationId = Common.APPLICATION_ID, Token = MerchantVar.CurrentMerchant.Token, StationCode = MerchantVar.StationCode, StationId = StationId }.ToStringObjectDictionary(), (ret, res) =>
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
                        else
                        {
                            LastError = Common.ERROR_USER_CANCEL;
                        }
                    }


                }).BeginInvoke(new AsyncCallback((ar) =>
                {
                    //AsyncResult result = (AsyncResult)ar; 
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


            }
        }
        #endregion

        #region 打印
        private void xPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Image image = Image.FromStream(this.streamToPrint);
            int x = e.MarginBounds.X;
            int y = e.MarginBounds.Y;
            int width = image.Width;
            int height = image.Height;
            if ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
            {
                width = e.MarginBounds.Width;
                height = image.Height * e.MarginBounds.Width / image.Width;
            }
            else
            {
                height = e.MarginBounds.Height;
                width = image.Width * e.MarginBounds.Height / image.Height;
            }
            Rectangle destRect = new Rectangle(x, y, width, height);
            e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Graphics g1 = this.CreateGraphics();
            Image MyImage = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height, g1);
            Graphics g2 = Graphics.FromImage(MyImage);
            IntPtr dc1 = g1.GetHdc();
            IntPtr dc2 = g2.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            g1.ReleaseHdc(dc1);
            g2.ReleaseHdc(dc2);
            MyImage.Save(@"c:\PrintPage.jpg", ImageFormat.Jpeg);
            FileStream fileStream = new FileStream(@"c:\PrintPage.jpg", FileMode.Open, FileAccess.Read);
            StartPrint(fileStream, "Image");
            fileStream.Close();
            if (System.IO.File.Exists(@"c:\PrintPage.jpg"))
            {
                System.IO.File.Delete(@"c:\PrintPage.jpg");
            } 
        }

        public void StartPrint(Stream streamToPrint, string streamType)
        {
            this.streamToPrint = streamToPrint;
            this.streamType = streamType;
            System.Windows.Forms.PrintDialog xPrintDialog = new PrintDialog();

            xPrintDialog.AllowSomePages = true;
            xPrintDialog.ShowHelp = true;
            xPrintDialog.Document = xPrintDocument;
            DialogResult result = xPrintDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                xPrintDocument.Print();
                //docToPrint.Print();
            }
        }
        #endregion

        private void btnPrint_MouseEnter(object sender, EventArgs e)
        {
            txtServeManName.Visible = false;
        }

        private void btnPrint_MouseLeave(object sender, EventArgs e)
        {
            txtServeManName.Visible = true;
        }
      

    }
}
