using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e0571.web.core.TypeInherited;
using e0571.web.core.MVC; 

namespace SmartLife.Client.PensionAgency
{
    public class Common
    {
        public static log4net.ILog ipccLogger = log4net.LogManager.GetLogger("PensionAgency");

        public const string CFG_SECTION_WEB = "Web";
        public const string CFG_KEY_AUTH_END_POINT = "AuthEndPoint";
        public const string CFG_KEY_AUTH_DATA_POINT = "AuthDataPoint";
        public const string CFG_KEY_UPDATE_END_POINT = "UpdateEndPoint";
        public static string CFG_FILE_PATH = Environment.CurrentDirectory + @"\config.ini";
        

        public const string INI_SECTION_LOCAL = "Local";
        public const string INI_KEY_USER_NAME_SINCE_LAST = "UserNameSinceLast";
        public const string INI_KEY_RUN_MODE = "RunMode";
        public const string INT_KEY_ASSIST_TOOLBAR_FLAG = "AssistToolbarFlag";
        public const string INT_KEY_ASSIST_TOOLBAR_POSITION_LAST = "AssistToolbarPositionLast";
        public const string INI_KEY_OBJECT_ID_SINCE_LAST = "ObjectIdSinceLast";
        public const string INI_KEY_OBJECT_NAME_SINCE_LAST = "ObjectNameSinceLast";
        public const string INI_SECTION_DATA = "Data";
        public const string INI_KEY_DEPLOY_NODE_OBJECTS = "DeployNodeObjects";
        public static string INI_FILE_PATH = Environment.CurrentDirectory + @"\settings.ini";
         

        public const string APPLICATION_ID = "CC001";
         

        public static void DebugLog(string msg)
        {
            Console.WriteLine(msg);
        }

        
    } 
}
