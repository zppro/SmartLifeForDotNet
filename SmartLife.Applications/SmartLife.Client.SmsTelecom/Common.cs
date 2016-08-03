using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client.SmsTelecom
{
    public class Common
    {
        public const string INI_SECTION_WEB = "Web";
        public const string INI_KEY_AUTH_END_POINT = "AuthEndPoint";
        public const string INI_KEY_SMS_END_POINT = "SmsEndPoint";
        public const string INI_KEY_ISMG_END_POINT = "IsmgEndPoint";
        public const string INI_SECTION_LOCAL = "Local";
        public static string INI_FILE_PATH = Environment.CurrentDirectory + @"\settings.ini";

        public const string ERROR_USER_CANCEL = "用户放弃操作";

        public static void DebugLog(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
