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
        public static string DialBackFlag { get; set; }
        public static Guid? CallCenterId { get; set; }
        public static string CallCenterIP { get; set; }
        public static int CallCenterPort { get; set; }
        public static string CallCenterIPInner { get; set; }
        public static int CallCenterPortInner { get; set; }
        public static string CallCenterIPProxy { get; set; }
        public static int CallCenterPortProxy { get; set; }
        public static string AreaId { get; set; }
        
         

        public static string ToJSON()
        {
            string jsonStr = JsonConvert.SerializeObject(
                new { UserId = UserId, UserCode = UserCode, UserName = UserName, ExtId = ExtId, ExtCode = ExtCode, CallCenterId = CallCenterId, AreaId = AreaId }
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
        }


    }
}
