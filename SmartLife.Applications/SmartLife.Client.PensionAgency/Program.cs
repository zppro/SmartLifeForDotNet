using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SmartLife.Client.PensionAgency
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmLogin frm = new frmLogin();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                Application.Run(new frmMain());
            }
        }
    }
}
