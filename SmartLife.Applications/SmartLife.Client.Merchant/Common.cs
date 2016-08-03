using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e0571.web.core.MVC;
namespace SmartLife.Client.Merchant
{
    public class Common
    {
        public const string CFG_SECTION_WEB = "Web";
        public const string CFG_KEY_AUTH_END_POINT = "AuthEndPoint";
        public const string CFG_KEY_AUTH_DATA_POINT = "AuthDataPoint";
        public const string CFG_KEY_UPDATE_END_POINT = "UpdateEndPoint";
        public static string CFG_FILE_PATH = Environment.CurrentDirectory + @"\config.ini";


        public const string INI_SECTION_WEB = "Web";
        public const string INI_KEY_AUTH_END_POINT = "AuthEndPoint";
        public const string INI_SECTION_BIZ = "Biz";
        public const string INI_KEY_REMIND_TYPE = "RemindType";
        public const string INI_KEY_YYREMIND_FLAG = "YYRemindFlag";
        public const string INI_SECTION_LOCAL = "Local";
        public const string INI_KEY_USER_NAME_SINCE_LAST = "UserNameSinceLast";
        public const string INT_KEY_RUN_MODE = "RunMode";
        public const string INT_KEY_ASSIST_TOOLBAR_FLAG = "AssistToolbarFlag";
        public const string INT_KEY_ASSIST_TOOLBAR_POSITION_LAST = "AssistToolbarPositionLast";
        public const string INI_KEY_OBJECT_ID_SINCE_LAST = "ObjectIdSinceLast";
        public const string INI_KEY_OBJECT_NAME_SINCE_LAST = "ObjectNameSinceLast";
        public const string INI_SECTION_DATA = "Data";
        public const string INI_KEY_DEPLOY_NODE_OBJECTS = "DeployNodeObjects";
        public static string INI_FILE_PATH = Environment.CurrentDirectory + @"\settings.ini";

        public static string SOUND_FILE_PATH = Environment.CurrentDirectory + @"\Sound";
        public static string DEFAULT_SOUND_NAME_OF_DEALING = SOUND_FILE_PATH + @"\Dealing.wav";
        public static string DEFAULT_SOUND_NAME_OF_WAIT_RESPONSE = SOUND_FILE_PATH + @"\WattingResponse.wav";
        public static string DEFAULT_SOUND_NAME_OF_WEIXING = SOUND_FILE_PATH + @"\WeiXing.wav";

        public const string ERROR_USER_CANCEL = "用户放弃操作";

        public const string APPLICATION_ID = "CB001";

        public static List<ListItem> sourceTypes = new List<ListItem>() {
            new ListItem{ Text = "待响应的工单", Value="A0101"},
            new ListItem{ Text = "处理中的工单", Value="A0102"},
            new ListItem{ Text = "处理完成的工单", Value="A0103"},
            new ListItem{ Text = "已完成的工单查询", Value="A0104"},
            new ListItem{ Text = "待审核的微信申请", Value="B0101"}
        };

        public static void DebugLog(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    public enum RemindType
    {
        不提醒,
        统计信息,
        详细信息
    }
}
