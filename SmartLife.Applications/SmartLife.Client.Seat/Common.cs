using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e0571.web.core.TypeInherited;
using e0571.web.core.MVC;
using win.core.models;

namespace SmartLife.Client.Seat
{
    public class Common
    {
        public static log4net.ILog ipccLogger = log4net.LogManager.GetLogger("IPCC");

        public const string CFG_SECTION_WEB = "Web"; 
        public const string CFG_KEY_UPDATE_END_POINT = "UpdateEndPoint";
        public static string CFG_FILE_PATH = Environment.CurrentDirectory + @"\config.ini";

        public const string INI_SECTION_SYSTEM = "System";
        public const string INI_KEY_PLATFORM_ADDRESS = "PlatformAddress";
        public const string INI_KEY_SERVICE_ONLINE = "ServiceOnline";
        public const string INI_KEY_PLAY_TONE = "PlayTone";
        public const string INI_KEY_PLAY_TONE_STIME = "PlayToneStartTime";
        public const string INI_KEY_PLAY_TONE_ETIME = "PlayToneEndTime";
        public static string INI_FILE_PATH = Environment.CurrentDirectory + @"\settings.ini";

        public static string SOUND_FILE_PATH = Environment.CurrentDirectory + @"\Sound";
        public static string DEFAULT_SOUND_NAME_OF_CALL_IN = SOUND_FILE_PATH + @"\Callin.wav";
        public static string DEFAULT_SOUND_NAME_OF_EXT_OFFLINE = SOUND_FILE_PATH + @"\ExtOffline.wav";

        public static string TOOLS_FILE_PATH = Environment.CurrentDirectory + @"\tools";
        public static string TOOLS_IPERF_EXE = TOOLS_FILE_PATH + @"\iperf.exe";
        
        public const string RUNMODE_INTERNET = "0";
        public const string RUNMODE_DEBUG = "1";
        public const string RUNMODE_INTRANET = "2";

        public static IDictionary<string, string> ADDRESS_DICT = new StringStringDictionary();

        public const string ADDRESS_KEY_PROCESS_EMERGENCY = "ProcessEmergency";
        public const string ADDRESS_KEY_PROCESS_COMMONSERVICE = "ProcessCommonService";
        public const string ADDRESS_KEY_PROCESS_FAMILYCALLS = "ProcessFamilyCalls";
        public const string ADDRESS_KEY_PROCESS_LIFE = "ProcessLife";

        public const string APPLICATION_ID = "CS002";

        public static List<ListItem> GroupsToTransfer = new List<ListItem>();
       
        public static void DebugLog(string msg)
        {
            ipccLogger.Info(msg);
            //Console.WriteLine(msg);
        }
    }

    internal class ListBoxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    internal class ConferenceMemberItem
    {
        public string MemberId { get; set; }
        public bool IsMute { get; set; }
        public bool IsDeaf { get; set; }
    }
}
