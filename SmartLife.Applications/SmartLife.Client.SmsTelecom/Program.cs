using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Reflection;
using win.core.utils;

namespace SmartLife.Client.SmsTelecom
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            TaskBarAdapter.CheckCreated();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PrepareAppEnvironment();
            string authEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, Common.INI_FILE_PATH);
            string smsEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_SMS_END_POINT, Common.INI_FILE_PATH);

            DialogResult dr = DialogResult.OK;
            if (string.IsNullOrEmpty(authEndPoint) || string.IsNullOrEmpty(smsEndPoint))
            {
                frmSetting frmS = new frmSetting();
                frmS.SectionSettingsSave += new dSectionSettingsSave((o, ce) =>
                {
                    INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, ce.AuthEndPoint, Common.INI_FILE_PATH);
                    INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_SMS_END_POINT, ce.SmsEndPoint, Common.INI_FILE_PATH);
                    INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_ISMG_END_POINT, ce.IsmgEndPoint, Common.INI_FILE_PATH);
                });
                dr = frmS.ShowDialog();
            }
            if (dr == DialogResult.OK)
            {
                Application.Run(new frmMain());
            }
        }

        static void PrepareAppEnvironment()
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            log4net.Config.XmlConfigurator.Configure(assem.GetManifestResourceStream("SmartLife.Client.Sms.log4net.config"));
        }
    }
}
