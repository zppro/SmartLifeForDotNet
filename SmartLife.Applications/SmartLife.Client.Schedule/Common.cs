using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e0571.web.core.TypeInherited;
using e0571.web.core.MVC;

namespace SmartLife.Client.Schedule
{
    public class Common
    {
        public static log4net.ILog schedule120701 = log4net.LogManager.GetLogger("Schedule120701");

        public const string INI_SECTION_SYNC = "Sync120701";
        public const string INI_KEY_SOURCE_BASE_ADDRESS = "SourceBaseAddress";
        public const string INI_KEY_TARGET_ADDRESS_TO_SAVE_ORDER = "TargetAddressToSaveOrder";
        

        #region 机构
        public const string CFG_SECTION_WEB = "Web";
        public const string CFG_KEY_AUTH_END_POINT = "AuthEndPoint";
        public static string CFG_FILE_PATH = Environment.CurrentDirectory + @"\config.ini";

        public const string INI_SECTION_WEB = "Web";
        public const string INI_KEY_SEND_REMINDER_POINT = "SendReminderPoint";

        public const string INI_SECTION_SYSTEM = "System";
        public const string INI_KEY_RUN_MODE = "RunMode";

        public const string APPLICATION_ID = "CP002";
        #endregion

        public static string INI_FILE_PATH = Environment.CurrentDirectory + @"\settings.ini";

        public static void DebugLog(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
