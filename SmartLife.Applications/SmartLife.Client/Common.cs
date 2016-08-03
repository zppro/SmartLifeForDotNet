using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client
{
    public class Common
    {
        public static log4net.ILog ipccLogger = log4net.LogManager.GetLogger("IPCC");

        public const string INI_SECTION_SMARTCARE = "SMARTCARE";
        public const string INI_KEY_RUNMODE = "RunMode";
        public const string INI_KEY_CMSHOST_0 = "CMSHostOuter";
        public const string INI_KEY_CMSHOST_1 = "CMSHostTest";
        public const string INI_KEY_CMSHOST_2 = "CMSHostInner";
        public const string INI_KEY_VIRPATH_0 = "VirPathOuter";
        public const string INI_KEY_VIRPATH_1 = "VirPathTest";
        public const string INI_KEY_VIRPATH_2 = "VirPathInner";
        public const string INI_SECTION_CALLCENTER = "CALLCENTER";
        public const string INI_KEY_CALLCENTER_NODE_ADDRESS = "CallCenterNodeAddress";
        public static string INI_FILE_PATH = Environment.CurrentDirectory + @"\settings.ini";

        public static string GetValueByFiledsName(string name)
        {
            string fieldvalue = "";
            var commonfield = typeof(Common).GetField(name);
            if (commonfield != null)
            {
                fieldvalue = (string)commonfield.GetValue(null);
            }
            return fieldvalue;
        }
    }
}
