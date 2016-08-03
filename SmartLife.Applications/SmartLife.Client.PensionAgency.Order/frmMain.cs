using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using win.core.utils;
using win.tools.ic;


namespace SmartLife.Client.PensionAgency.Order
{
    public partial class frmMain : Form
    { 
        public frmMain()
        {
            InitializeComponent();
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbOrder_Click(object sender, EventArgs e)
        {
            API icAPI = new API();
            icAPI.InitIC();
            if (icAPI.IcDev < 0)
            {
                MessageBoxAdapter.ShowError("初始化IC读卡设备失败，请确认是否已连接IC读卡设备");
                return;
            }
            frmOrder frm = new frmOrder();
            frm.LoadAPI(icAPI);
            DialogResult result = frm.ShowDialog();
            icAPI.ExitIC();
        }

        private void pbMakeCard_Click(object sender, EventArgs e)
        {
            frmMakeCard frmMakeCardWin = new frmMakeCard();
            frmMakeCardWin.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Common.Upgrade();
        }
    }
}
