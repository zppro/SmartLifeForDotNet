using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace SmartLife.WeiXinCloud.Schedule
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
            Application.Run(new frmMain());
        }

        static void PrepareAppEnvironment()
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            log4net.Config.XmlConfigurator.Configure(assem.GetManifestResourceStream("SmartLife.WeiXinCloud.Schedule.log4net.config"));
        }
    }
}
