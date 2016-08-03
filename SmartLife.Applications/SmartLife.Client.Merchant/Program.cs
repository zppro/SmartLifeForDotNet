using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using win.core.utils;
using System.Reflection;
using System.IO;

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

            //TaskBarAdapter.CheckCreated();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PrepareAppEnvironment();

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
#if V2X
            SettingsVar.CurrentYYRemindFlag = INIAdapter.ReadValue(Common.INI_SECTION_BIZ, Common.INI_KEY_YYREMIND_FLAG, Common.INI_FILE_PATH) == "1" ? 1 : 0;
            INIAdapter.WriteValue(Common.INI_SECTION_BIZ, Common.INI_KEY_YYREMIND_FLAG, SettingsVar.CurrentYYRemindFlag.ToString(), Common.INI_FILE_PATH);

            frmLogin frm = new frmLogin();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                Application.Run(new frmMainV2X());
            }
#else
             //http://localhost/SmartLife.Auth.Merchant.Services/v1/AuthenticateMerchant
            string authEndPoint = INIAdapter.ReadValue(Common.INI_SECTION_WEB, Common.INI_KEY_AUTH_END_POINT, Common.INI_FILE_PATH);
            
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
#endif





        }

        static void PrepareAppEnvironment()
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            log4net.Config.XmlConfigurator.Configure(assem.GetManifestResourceStream("SmartLife.Client.Merchant.log4net.config"));


            FileAdapter.EnsurePath(Common.SOUND_FILE_PATH);

            if (!FileAdapter.Exists(Common.DEFAULT_SOUND_NAME_OF_DEALING))
            {
                using (FileStream fs = new FileStream(Common.DEFAULT_SOUND_NAME_OF_DEALING, FileMode.Create))
                {
                    byte[] data = new byte[Properties.Resources.Dealing.Length];
                    Properties.Resources.Dealing.Read(data, 0, data.Length);
                    fs.Write(data, 0, data.Length);
                }
            }
            if (!FileAdapter.Exists(Common.DEFAULT_SOUND_NAME_OF_WAIT_RESPONSE))
            {
                using (FileStream fs = new FileStream(Common.DEFAULT_SOUND_NAME_OF_WAIT_RESPONSE, FileMode.Create))
                {
                    byte[] data = new byte[Properties.Resources.WattingResponse.Length];
                    Properties.Resources.WattingResponse.Read(data, 0, data.Length);
                    fs.Write(data, 0, data.Length);
                }
            }
            if (!FileAdapter.Exists(Common.DEFAULT_SOUND_NAME_OF_WEIXING))
            {
                using (FileStream fs = new FileStream(Common.DEFAULT_SOUND_NAME_OF_WEIXING, FileMode.Create))
                {
                    byte[] data = new byte[Properties.Resources.WeiXing.Length];
                    Properties.Resources.WeiXing.Read(data, 0, data.Length);
                    fs.Write(data, 0, data.Length);
                }
            }
        }
    }
}
