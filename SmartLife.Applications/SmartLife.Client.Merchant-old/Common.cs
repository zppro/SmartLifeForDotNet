using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e0571.web.core.MVC;
namespace SmartLife.Client.Merchant
{
    public class Common
    {
        public const string INI_SECTION_WEB = "Web";
        public const string INI_KEY_AUTH_END_POINT = "AuthEndPoint";
        public const string INI_SECTION_BIZ = "Biz";
        public const string INI_KEY_REMIND_TYPE = "RemindType";
        public const string INI_SECTION_LOCAL = "Local";
        public const string INI_KEY_USER_NAME_SINCE_LAST = "UserNameSinceLast";
        public static string INI_FILE_PATH = Environment.CurrentDirectory + @"\settings.ini";

        public const string ERROR_USER_CANCEL = "用户放弃操作";

        public const string APPLICATION_ID = "CB001";

        public static List<ListItem> sourceTypes = new List<ListItem>() {
            new ListItem{ Text = "待响应的工单", Value="A0101"},
            new ListItem{ Text = "处理中的工单", Value="A0102"},
            new ListItem{ Text = "处理完成的工单", Value="A0103"},
            new ListItem{ Text = "已完成的工单查询", Value="A0104"}
        };
    }

    public enum RemindType
    {
        不提醒,
        统计信息,
        详细信息
    }
}
