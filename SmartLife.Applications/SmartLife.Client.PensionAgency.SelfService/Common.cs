using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using win.core.utils;
using win.core.update;

namespace SmartLife.Client.PensionAgency.SelfService
{
    public class Common
    {
        public static log4net.ILog ipccLogger = log4net.LogManager.GetLogger("PensionAgencySelfService");

        public const string CFG_SECTION_WEB = "Web"; 
        public const string CFG_KEY_AUTH_DATA_POINT = "AuthDataPoint";
        public const string CFG_KEY_UPDATE_END_POINT = "UpdateEndPoint";
        public static string CFG_FILE_PATH = Environment.CurrentDirectory + @"\config.ini";

        public const string INI_SECTION_LOCAL = "Local";
        public const string INI_KEY_RUN_MODE = "RunMode";
        public const string INT_KEY_BINDING_PA_CODE = "BindingPACode";
        public const string INT_KEY_BINDING_PA_NAME = "BindingPAName";
        public const string INT_KEY_DATA_EXCHANGE_POINT = "DataExchangePoint";
        public static string INI_FILE_PATH = Environment.CurrentDirectory + @"\settings.ini";

        public static int msDelay = 500;
        public static int msDot = 300; 

        public static string MachineKey;


        public const string APPLICATION_ID = "CC002";


        public static void DebugLog(string msg)
        {
            DebugLog(msg, false);
        }
        public static void DebugLog(string msg, bool writeToFile)
        {
            if (writeToFile)
            {
                ipccLogger.Debug(msg);
            }
            else
            {
                Console.WriteLine(msg);
            }
        }

        public static void Upgrade()
        {
            string updateFolder = AppDomain.CurrentDomain.BaseDirectory + Common.APPLICATION_ID;
            FileAdapter.EnsurePath(updateFolder);
            FileAdapter.Copy(Updater.AppName, updateFolder + @"\" + Updater.AppName, true);
            foreach (var file in Updater.DepandenceFiles)
            {
                FileAdapter.Copy(file, AppDomain.CurrentDomain.BaseDirectory + Common.APPLICATION_ID + @"\" + file, true);
            }

            string updateEndPoint = INIAdapter.ReadValue(Common.CFG_SECTION_WEB, Common.CFG_KEY_UPDATE_END_POINT, Common.CFG_FILE_PATH);
            string args = String.Format("-n {0} -v {1} -c {2}", AppAdapter.GetName(), VersionAdapter.GetPureVersion(VersionType.ASSEMBLY), updateEndPoint + "/" + Common.APPLICATION_ID);
            System.Diagnostics.Process.Start(updateFolder + @"\" + Updater.AppName, args);
        }

        
    } 
}
