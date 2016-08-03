using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using win.core.utils;
using win.core.controls.crypto;
using System.Reflection;
using System.IO;

namespace SmartLife.Client.PensionAgency.SelfService
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
            frmSplash splasher = new frmSplash();
            splasher.ShowDialog();
            if (splasher.DialogResult== DialogResult.OK)
            {
                Application.Run(new frmMain());
            }
            
        }

        static void PrepareAppEnvironment()
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            log4net.Config.XmlConfigurator.Configure(assem.GetManifestResourceStream("SmartLife.Client.PensionAgency.SelfService.log4net.config"));
            SettingsVar.BindingPACode = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INT_KEY_BINDING_PA_CODE, Common.INI_FILE_PATH);
            SettingsVar.BindingPAName = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INT_KEY_BINDING_PA_NAME, Common.INI_FILE_PATH);
            SettingsVar.DataExchangePoint = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INT_KEY_DATA_EXCHANGE_POINT, Common.INI_FILE_PATH);
            SettingsVar.RunMode = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_RUN_MODE, Common.INI_FILE_PATH);
            if (SettingsVar.RunMode == "")
            {
                SettingsVar.RunMode = "0";
                INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_RUN_MODE, SettingsVar.RunMode, Common.INI_FILE_PATH);
            }

            Common.MachineKey = ZPCrypto.GetMachineKey(MachineHardwareType.CPU | MachineHardwareType.HARDDISK | MachineHardwareType.MAINBOARD);

        }
    }
}
