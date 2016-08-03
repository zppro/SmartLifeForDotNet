using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartLife.Client.PensionAgency.Models;

namespace SmartLife.Client.PensionAgency.SelfService
{
    public partial class ucOldManInfoSlim : UserControl
    {
        public ucOldManInfoSlim()
        {
            InitializeComponent();
        }

        public void LoadOldManInfo(OldManInfo oldmanInfo)
        {
            lblOldManName.Text = oldmanInfo.OldManName;
            lblGender.Text = oldmanInfo.GenderName;
            pbHeadPortrait.BackgroundImage = lblGender.Text == "男" ? Properties.Resources.oldman_m : Properties.Resources.oldman_f;
            lblAge.Text = "(" + oldmanInfo.Age + "岁)";
            lblIDNo.Text = "ID " + oldmanInfo.IDNo.Substring(0, oldmanInfo.IDNo.Length - 6) + "******";
            lblAddress.Text = oldmanInfo.Address;
        }
    }
}
