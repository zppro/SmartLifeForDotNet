using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartLife.Client.PensionAgency.SelfService
{
    public partial class ucAccount : UserControl
    {
        public ucAccount()
        {
            InitializeComponent();
        }

        public void LoadAccount(string oldManId)
        {
            lblGOVAccount.Text = "未开通";
            lblSelfAccount.Text = "￥0.00";
        }
    }
}
