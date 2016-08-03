using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using e0571.web.core.TypeExtension;
using win.core.controls;
using win.core.utils;

namespace SmartLife.Client.PensionAgency.Order
{
    public partial class frmBindingPA : Form
    {
        public frmBindingPA()
        {
            InitializeComponent();
        }
         
       

        private void frmBindingPA_Load(object sender, EventArgs e)
        {
            InitForm();  
        }

        private void InitForm()
        {
            Util.SetBtnTransparency(btnBinding);
            Util.SetBtnTransparency(btnClose);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBinding_Click(object sender, EventArgs e)
        {
            BindingPA();
        } 

        private void BindingPA()
        {
            string bindingPACode = txtBindingCode.Text.Trim();
            if (bindingPACode.Substring(2, 6).Length < 6)
            {
                MessageBoxAdapter.ShowError("无效的PA码");
                return;
            }
            SettingsVar.BindingPACode = bindingPACode;
            INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INT_KEY_BINDING_PA_CODE, SettingsVar.BindingPACode, Common.INI_FILE_PATH);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        } 
             
    }
}
