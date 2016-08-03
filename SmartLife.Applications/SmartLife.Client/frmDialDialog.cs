using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartLife.Share.Models.PO;

namespace SmartLife.Client
{
    public partial class frmDialDialog : Form
    {
        System.Timers.Timer _tickTimer = null;

        public frmDialDialog()
        {
            InitializeComponent();
        }

        public event dCancelCallBySeat OnCancelCallBySeat;

        public string DialText { get; set; }

        public Guid? CallServiceId { get; set; }

        public CallService CurrentCallService { get; set; }

        public string DialType { get; set; }

        
        private void frmDialDialog_Load(object sender, EventArgs e)
        {
            lblMessage.Text = DialText;
            _tickTimer = new System.Timers.Timer(1 * 500);
            _tickTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs ee)
            {
                BeginInvoke(new Action(() =>
                {
                    if (lblMessage.Text.IndexOf(".") == -1)
                    {
                        lblMessage.Text += ".";
                    }
                    else if (lblMessage.Text.IndexOf(".") + 5 == lblMessage.Text.Length)
                    {
                        lblMessage.Text = lblMessage.Text.Substring(0, lblMessage.Text.Length - 5);
                    }
                    else
                    {
                        lblMessage.Text += ".";
                    }
                })); 

            });//到达时间的时候执行事件； 
            _tickTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            _tickTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；  
        }

        public void Established()
        {
            _tickTimer.Stop();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _tickTimer.Stop();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            if (OnCancelCallBySeat != null)
            {
                OnCancelCallBySeat();
            }
           
        }

    }
}
