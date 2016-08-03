using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using win.core.utils;
using win.core.controls;
using win.tools.ic;
using SmartLife.Client.PensionAgency.Models;

namespace SmartLife.Client.PensionAgency.SelfService
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

       

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblProductVersionComent.Text = Properties.Settings.Default.ProductVersionComment;
            lblVer.Text = VersionAdapter.GetVersion(VersionType.FILE);
            lblTechSupport.Text = Properties.Settings.Default.Company + " 技术支持";

            InitForm();
            LoadStationInfo();

            MonitorICCardReader();
        }

        private void InitForm()
        {
            Util.SetBtnTransparency(btnCheckUp); 

            Util.SetBtnTransparency(btnDining); 

            Util.SetBtnTransparency(btnCourse); 

            Util.SetBtnTransparency(btnAccount); 

            splitContainer1.Panel2.Enabled = false;
        }

        private void LoadStationInfo()
        {
            lblStationName.Text = SettingsVar.BindingPAName;
            
        }

        private string lastOldManId;

        API _icAPI = new API();
        private string _CurrentICNo = "0";
        private OldManInfo oldmanInfo;
        bool isStop = false;

        ucOldManInfoShow oldmanInfoShow;
        ucOldManInfoSlim oldmanInfoSlim;
        ucPhysicalExamination physicalExaminationShow;
        ucOrderInfo orderInfoShow;
        ucCourseInfo courseInfoShow;
        ucAccount accountShow;

        #region 监测读卡器
        private void MonitorICCardReader()
        {
            this.UIDoCircleTask(() =>
            {
                _CurrentICNo = _icAPI.LoadIC();
                if (_CurrentICNo != "0")
                {
                    oldmanInfo = Data.OldMans.SingleOrDefault(item => item.ICNo == _CurrentICNo);
                    if (oldmanInfo != null)
                    {
                        _icAPI.Beep();
                        if (oldmanInfoSlim == null)
                        {
                            oldmanInfoSlim = new ucOldManInfoSlim();
                            oldmanInfoSlim.Location = new Point(0, 0);
                            splitContainer1.Panel1.Controls.Add(oldmanInfoSlim);
                        }
                        oldmanInfoSlim.LoadOldManInfo(oldmanInfo);
                        oldmanInfoSlim.Hide();
                        
                        if (oldmanInfoShow == null)
                        {
                            oldmanInfoShow = new ucOldManInfoShow();
                            oldmanInfoShow.Location = new Point(0, 0);
                            splitContainer1.Panel1.Controls.Add(oldmanInfoShow);
                        }
                        oldmanInfoShow.LoadOldManInfo(oldmanInfo);
                        oldmanInfoShow.Show();
                        splitContainer1.Panel2.Enabled = true;

                        if (physicalExaminationShow != null)
                        {
                            physicalExaminationShow.Hide();
                        }
                        if (orderInfoShow != null)
                        {
                            orderInfoShow.Hide();
                        }
                        if (courseInfoShow != null)
                        {
                            courseInfoShow.Hide();
                        }
                        if (accountShow != null)
                        {
                            accountShow.Hide();
                        }
                    }
                }
            }, 700, () =>
            {
                return isStop;
            }, () =>
            {
                if (_icAPI.IcDev < 0)
                {
                    _icAPI.InitIC();
                }
                return _icAPI.IcDev < 0;
            });


        }
        #endregion

        #region 获取体检信息
        private void LoadPhysicalExaminationUI()
        {
            oldmanInfoSlim.Show();
            oldmanInfoShow.Hide();
            if (orderInfoShow != null)
            {
                orderInfoShow.Hide();
            }
            if (courseInfoShow != null)
            {
                courseInfoShow.Hide();
            }
            if (accountShow != null)
            {
                accountShow.Hide();
            }
            if (physicalExaminationShow == null)
            {
                physicalExaminationShow = new ucPhysicalExamination();
                physicalExaminationShow.Location = new Point(0, 150);
                splitContainer1.Panel1.Controls.Add(physicalExaminationShow);
                physicalExaminationShow.LoadAccount(oldmanInfo.OldManId);
            }
            else
            {
                if (lastOldManId != oldmanInfo.OldManId)
                {
                    physicalExaminationShow.LoadAccount(oldmanInfo.OldManId);
                    lastOldManId = oldmanInfo.OldManId;
                }
            }
            
            physicalExaminationShow.Show();
        }
        #endregion

        #region 获取用餐信息
        private void LoadOrderInfoUI()
        { 
            oldmanInfoSlim.Show();
            oldmanInfoShow.Hide();
            if (physicalExaminationShow != null)
            {
                physicalExaminationShow.Hide();
            }
            if (courseInfoShow != null)
            {
                courseInfoShow.Hide();
            }
            if (accountShow != null)
            {
                accountShow.Hide();
            }
            if (orderInfoShow == null)
            {
                orderInfoShow = new ucOrderInfo();
                orderInfoShow.Location = new Point(0, 150);
                splitContainer1.Panel1.Controls.Add(orderInfoShow);
                orderInfoShow.LoadOrderInfo(oldmanInfo.OldManId);
            }
            else
            {
                if (lastOldManId != oldmanInfo.OldManId)
                {
                    orderInfoShow.LoadOrderInfo(oldmanInfo.OldManId);
                    lastOldManId = oldmanInfo.OldManId;
                }
            }
            orderInfoShow.Show();
        }
        #endregion

        #region 获取课程信息
        private void LoadCourseInfoUI()
        {
            oldmanInfoSlim.Show();
            oldmanInfoShow.Hide();
            if (physicalExaminationShow != null)
            {
                physicalExaminationShow.Hide();
            }
            if (orderInfoShow != null)
            {
                orderInfoShow.Hide();
            }
            if (accountShow != null)
            {
                accountShow.Hide();
            }
            if (courseInfoShow == null)
            {
                courseInfoShow = new ucCourseInfo();
                courseInfoShow.Location = new Point(0, 150);
                splitContainer1.Panel1.Controls.Add(courseInfoShow);
                courseInfoShow.LoadCourseInfo();
            }
            
            courseInfoShow.Show();
        }
        #endregion

        #region 获取个人账号
        private void LoadOldManAccountUI()
        {
            oldmanInfoSlim.Show();
            oldmanInfoShow.Hide();
            if (physicalExaminationShow != null)
            {
                physicalExaminationShow.Hide();
            }
            if (orderInfoShow != null)
            {
                orderInfoShow.Hide();
            }
            if (courseInfoShow != null)
            {
                courseInfoShow.Hide();
            } 
            if (accountShow == null)
            {
                accountShow = new ucAccount();
                accountShow.Location = new Point(0, 150);
                splitContainer1.Panel1.Controls.Add(accountShow);
                accountShow.LoadAccount(oldmanInfo.OldManId);
            }
            else
            {
                if (lastOldManId != oldmanInfo.OldManId)
                {
                    accountShow.LoadAccount(oldmanInfo.OldManId);
                    lastOldManId = oldmanInfo.OldManId;
                }
            } 
            accountShow.Show();
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            isStop = true;
            _icAPI.ExitIC();
            this.Close();
        }  

        private void btnCheckUp_Click(object sender, EventArgs e)
        {
            LoadPhysicalExaminationUI(); 
        }

        private void btnDining_Click(object sender, EventArgs e)
        {
            LoadOrderInfoUI(); 
        }

        private void btnCourse_Click(object sender, EventArgs e)
        {
            LoadCourseInfoUI(); 
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            LoadOldManAccountUI(); 
        }

        private void xClock_Click(object sender, EventArgs e)
        {
            btnExit_Click(sender, e);
        }

        private void lblSettings_Click(object sender, EventArgs e)
        {
            xContextMenu.Show((sender as Control), -80, (sender as Control).Height + 1);
        }

        private void 在线更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.Upgrade();
        }

        
    }
}
