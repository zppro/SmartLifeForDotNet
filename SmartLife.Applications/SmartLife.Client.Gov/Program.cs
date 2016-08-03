using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SmartLife.Client.Gov
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
            //Application.Run(new frmMain());

            frmLogin frm = new frmLogin();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                Application.Run(new frmMain());
            }

            //#region 象山居家养老
            //formLogin form = new formLogin();
            //form.ShowDialog();
            //if (form.DialogResult == DialogResult.OK)
            //{
            //    Application.Run(new formMain());
            //}
            //#endregion
        }
    }
}
