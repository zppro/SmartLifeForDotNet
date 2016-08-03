using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace SmartLife.Client
{
    public class EnvironmentVar
    {
        public static Guid? UserId { get; set; }
        public static string UserCode { get; set; }
        public static string UserName { get; set; }
        public static Guid? ExtId { get; set; }
        public static string ExtCode { get; set; }
        public static string IPCCDial { get; set; }
        public static string IPCCRelayType { get; set; }
        public static string IPCCRelayType_DEFAULT = "SIP/ipcc710010/";
        public static string IPCCRelayPrefix { get; set; }
        public static string DialBackFlag { get; set; }
        public static Guid? CallCenterId { get; set; }
        public static string AreaId { get; set; }

        public static Dictionary<string, string> dictsOfQueueNoAndCallee { get; set; }

        public static string ToJSON()
        {
            string jsonStr = JsonConvert.SerializeObject(
                new { UserId = UserId, UserCode = UserCode, UserName = UserName, ExtId = ExtId, ExtCode = ExtCode, IPCCDial = IPCCDial, CallCenterId = CallCenterId, AreaId = AreaId }
                );

            return jsonStr;
        }

        public static void Clear()
        {
            UserId = null;
            UserCode = null;
            UserName = null;
            ExtId = null;
            ExtCode = null;
            IPCCDial = null;
            CallCenterId = null;
            AreaId = null;
            dictsOfQueueNoAndCallee = null;
        }


    }
}
