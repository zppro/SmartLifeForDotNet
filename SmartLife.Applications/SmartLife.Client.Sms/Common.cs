using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client.Sms
{
    public class Common
    {
        public const string INI_SECTION_WEB = "Web";
        public const string INI_KEY_AUTH_END_POINT = "AuthEndPoint";
        public const string INI_KEY_AUTHSMS_END_POINT = "AuthSmsEndPoint";
        public const string INI_KEY_SMS_END_POINT = "SmsEndPoint";
        public const string INI_SECTION_LOCAL = "Local";
        public const string INI_KEY_TXT_SMS_SETTING_LAST = "TxtSmsSettingLast";
        public const string INI_KEY_ORDER_SMS_SETTING_LAST = "OrderSmsSettingLast";
        public static string INI_FILE_PATH = Environment.CurrentDirectory + @"\settings.ini";

        public const string ERROR_USER_CANCEL = "用户放弃操作";
    }
}
