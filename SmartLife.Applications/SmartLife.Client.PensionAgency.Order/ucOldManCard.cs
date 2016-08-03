using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using win.core.controls;

namespace SmartLife.Client.PensionAgency.Order
{
    public partial class ucOldManCard : UserControl
    { 

        public ucOldManCard()
        {
            InitializeComponent();

        } 

        public Label ClickLabel
        {
            get
            {
                return lblName;
            }
        }
    }
}
