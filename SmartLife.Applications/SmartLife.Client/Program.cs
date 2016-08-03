using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace SmartLife.Client
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

            PrepareAppEnvironment();
            frmMain frm = new frmMain();
            
            Application.Run(frm);
        }

        static void PrepareAppEnvironment()
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            log4net.Config.XmlConfigurator.Configure(assem.GetManifestResourceStream("SmartLife.Client.log4net.config"));
        }
    }
}
