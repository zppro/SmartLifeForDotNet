using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using win.tools.ic;
using win.core.controls;
using win.core.utils;
using e0571.web.core.TypeExtension;
using SmartLife.Client.PensionAgency.Models;

namespace SmartLife.Client.PensionAgency.Order
{
    public partial class frmCardView : Form
    {
        public frmCardView()
        {
            InitializeComponent();

            
        }


        private API _icAPI;
        private string _CurrentICNo = "0";
        private string _IDNo;

        bool isStop = false;

        private void frmCardView_Load(object sender, EventArgs e)
        {
           
            MonitorICCardReader();
        }

        #region 监测读卡器
        private void MonitorICCardReader()
        {
            this.UIDoCircleTask(() =>
            {
                _CurrentICNo = _icAPI.LoadIC();
                Common.DebugLog("ICNo:" + _CurrentICNo); 
                if (_CurrentICNo != "0")
                {
                    if (lblICNoHead.Text != "No." + _CurrentICNo)
                    {
                        _icAPI.Beep();
                        lblICNoHead.Text = "No." + _CurrentICNo;
                    }
                    if (("IC " + _CurrentICNo) == lblICNo.Text)
                    {
                        lblTip.Text = "此人已分配此卡！";
                        btnMakeCard.Enabled = false;
                    }
                    else
                    {
                        if (lblICNo.Text == "IC ")
                        {
                            //人没有分配卡 
                            if (_icAPI.AuthenticationKey(0))
                            {
                                //检查卡是否已被分配
                                string read1 = _icAPI.ReadIC16Byte(1);
                                string read2 = _icAPI.ReadIC16Byte(2);

                                if ((read1 + read2) == "")
                                {
                                    //空卡,未被使用
                                    lblTip.Text = "点击制卡";
                                    btnMakeCard.Enabled = true;
                                }
                                else
                                { 
                                    lblTip.Text = "该卡已被其他人使用！";
                                    btnMakeCard.Enabled = false;
                                }
                            }
                        }
                        else
                        { 
                            lblTip.Text = "此人已分配了其他卡！";
                        }
                    }
                }
                else
                {
                    lblTip.Text = "请将卡放在读卡器上";
                    lblICNoHead.Text = "No.";
                    btnMakeCard.Enabled = false;
                }

            }, 700, () =>
            {

                return isStop;
            });
        }
        #endregion

        private string _OldManId;
        public void LoadInfo(API icApi,string oldmanId)
        {
            _icAPI = icApi;
            OldManInfo oldmanInfo = Data.OldMans.SingleOrDefault(item => item.OldManId == oldmanId);
            if (oldmanInfo != null)
            {
                _OldManId = oldmanInfo.OldManId;
                lblOldManName.Text = oldmanInfo.OldManName;
                lblGender.Text = oldmanInfo.GenderName;
                pbHeadPortrait.BackgroundImage = lblGender.Text == "男" ? Properties.Resources.oldman_m : Properties.Resources.oldman_f;
                lblAge.Text = "(" + oldmanInfo.Age + "岁)";
                _IDNo = oldmanInfo.IDNo;
                lblIDNo.Text = "ID " + oldmanInfo.IDNo.Substring(0, oldmanInfo.IDNo.Length - 6) + "******";
                lblICNo.Text = "IC " + oldmanInfo.ICNo;
                lblAddress.Text = oldmanInfo.Address;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            isStop = true;
            this.Close();
        }

        private void btnMakeCard_Click(object sender, EventArgs e)
        {
            _icAPI.Beep();
            int sector=0;
            if (_icAPI.AuthenticationKey(sector))
            {
                 
                int address = 1;

                string read1 = _icAPI.ReadIC16Byte(address);
                string read2 = _icAPI.ReadIC16Byte(address + 1);
                string raw = read1 + read2; 
                int ret0 = _icAPI.WriteIC(address, _IDNo);
                if (ret0 == 0)
                { 
                    HttpAdapter.postSyncAsJSON(SettingsVar.DataExchangePoint + "/Pam/PamService/PAMakeCard", new { OldManId = _OldManId.ToGuid(), OperatedBy = Data.UserId, ICNo = _CurrentICNo }.ToStringObjectDictionary(), new { PACode = SettingsVar.BindingPACode, ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(), (ret, res) =>
                    {
                        if ((bool)ret.Success)
                        {
                            MessageBoxAdapter.ShowInfo("制卡成功");
                            lblTip.Text = "制卡成功";
                            OldManInfo oldmanInfo = Data.OldMans.SingleOrDefault(item => item.OldManId == _OldManId);
                            if (oldmanInfo != null)
                            {
                                oldmanInfo.ICNo = _CurrentICNo;
                                lblICNo.Text = _CurrentICNo;
                            }
                            isStop = true;
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                        else
                        {
                            ret0 = _icAPI.WriteIC(address, raw);
                            MessageBoxAdapter.ShowError((string)ret.ErrorMessage);
                        }
                    }); 

                }
                else
                {
                    MessageBoxAdapter.ShowError("写入卡失败" + ret0.ToString());
                }
                 
            }
            else
            {
                MessageBoxAdapter.ShowError("验证密码失败");
            }
            
            
        }
         
    }
}
