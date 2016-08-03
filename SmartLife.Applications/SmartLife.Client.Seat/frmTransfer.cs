using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using e0571.web.core.MVC;
using win.core.utils;

namespace SmartLife.Client.Seat
{
    public partial class frmTransfer : Form
    {
        public frmTransfer()
        {
            InitializeComponent();
        }

#if V2X
        public frmMainV2X TheMotherWin { get; set; }
#else
        public frmMain TheMotherWin { get; set; }
#endif

        private void frmTransfer_Load(object sender, EventArgs e)
        {
            BindingTransferQueue();
        }

        private void BindingTransferQueue()
        {
            cbTransferQueues.ValueMember = "Value";
            cbTransferQueues.DisplayMember = "Text";
            cbTransferQueues.DataSource = Common.GroupsToTransfer;
        }

        private bool CheckExtNo()
        {
            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!CheckExtNo())
            {
                return;
            }
            string transfer_num = rbExt.Checked ? txtTransferExtNo.Text.Trim() : e0571.web.core.Utils.TypeConverter.ChangeString((cbTransferQueues.SelectedItem as ListItem).Value);
            string urlToTransfer = TheMotherWin.getIPCCInterface4EComm("transfer", new Dictionary<string, string> { { "ext_no", EnvironmentVar.ExtCode }, { "transfer_num", transfer_num } });
            HttpAdapter.getAsyncTo(urlToTransfer, (result0, response0) =>
            {
                Console.WriteLine(string.Format("################### transfer {0}:{1}", transfer_num, result0));
                if (result0 == "+OK")
                {
                    BeginInvoke(new Action(() =>
                    {
                        this.Close();
                    }));
                }
                else
                {
                    MessageBoxAdapter.ShowDebug(result0);
                }
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbQueue_CheckedChanged(object sender, EventArgs e)
        {
            if (rbQueue.Enabled)
            {
                cbTransferQueues.Enabled = true;
                txtTransferExtNo.Enabled = false;
            }
            else
            {
                cbTransferQueues.Enabled = false;
                txtTransferExtNo.Enabled = true;
            }
        }

        private void rbExt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExt.Enabled)
            {
                cbTransferQueues.Enabled = false;
                txtTransferExtNo.Enabled = true;
            }
            else
            {
                cbTransferQueues.Enabled = true;
                txtTransferExtNo.Enabled = false;
            }
        }
    }
}
