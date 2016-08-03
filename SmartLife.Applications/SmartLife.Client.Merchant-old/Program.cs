using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using win.core.utils;


namespace SmartLife.Client.Merchant
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

            //http://localhost/SmartLife.Auth.Merchant.Services/v1/AuthenticateMerchant
            string authEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, Common.INI_FILE_PATH);
            string remindType = INIAdapter.ReadValue(Common.INI_SECTION_BIZ, Common.INI_KEY_REMIND_TYPE, Common.INI_FILE_PATH);
            if (string.IsNullOrEmpty(remindType))
            {
                SettingsVar.CurrentRemindType = RemindType.不提醒;
                INIAdapter.WriteValue(Common.INI_SECTION_BIZ, Common.INI_KEY_REMIND_TYPE, ((int)SettingsVar.CurrentRemindType).ToString(), Common.INI_FILE_PATH);
            }
            else
            {
                SettingsVar.CurrentRemindType = e0571.web.core.Utils.EnumAdapter.GetEnum<RemindType>(remindType);
            }
            DialogResult dr = DialogResult.OK;
            if (string.IsNullOrEmpty(authEndPoint))
            {
                frmSettings frmS = new frmSettings();
                frmS.SectionWebSettingsSave += new dSectionWebSettingsSave((o, ce) =>
                {
                    INIAdapter.WriteValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, ce.AuthEndPoint, Common.INI_FILE_PATH);
                });
                frmS.SectionBizSettingsSave += new dSectionBizSettingsSave((o, ce) =>
                {
                    SettingsVar.CurrentRemindType = ce.Type;
                    INIAdapter.WriteValue(Common.INI_SECTION_BIZ, Common.INI_KEY_REMIND_TYPE, ((int)ce.Type).ToString(), Common.INI_FILE_PATH);
                });
                dr = frmS.ShowDialog();
            }
            if (dr == DialogResult.OK)
            {
                frmLogin frm = new frmLogin();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    Application.Run(new frmMain());
                } 
            }
           
        }
    }
}
