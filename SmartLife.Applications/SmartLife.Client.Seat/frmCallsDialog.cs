using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartLife.Client.Seat
{
    public partial class frmCallsDialog : Form
    {
        public List<string> PhoneNos { get; set; }
        public string ChooseCallNo { get; set; }

        public frmCallsDialog()
        {
            InitializeComponent();
        }

        private void frmCallsDialog_Load(object sender, EventArgs e)
        {
            if (PhoneNos.Count > 0)
            {
                RadioButton rb_CallNo;
                int iflag = 1; 
                foreach (var phoneNo in PhoneNos)
                {
                    rb_CallNo = new RadioButton();
                    rb_CallNo.Name = "rb_CallNo_" + iflag.ToString();
                    rb_CallNo.Text = phoneNo;
                    rb_CallNo.Location = new Point(17, 34 * iflag);
                    rb_CallNo.Width = 150;

                    if (phoneNo== ChooseCallNo)
                    {
                        rb_CallNo.Checked = true;
                    }
                    gb_CallNo.Controls.Add(rb_CallNo);
                    iflag++;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            IEnumerable<RadioButton> rblist = gb_CallNo.Controls.OfType<RadioButton>();
            ChooseCallNo = rblist.FirstOrDefault(s => s.Checked).Text;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
