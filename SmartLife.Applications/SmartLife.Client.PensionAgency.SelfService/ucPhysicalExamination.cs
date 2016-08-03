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
    public partial class ucPhysicalExamination : UserControl
    {
        public ucPhysicalExamination()
        {
            InitializeComponent();
        }

        public void LoadAccount(string oldManId)
        { 
            lblDBP.Text = ((new Random().Next(0, 51)) + 90).ToString() + "mmHg";
            lblSBP.Text = ((new Random().Next(0,31)) + 60).ToString() + "mmHg";
            lblHR.Text = ((new Random().Next(0, 41)) + 60).ToString() + "次/分";
        }
         
    }
}
