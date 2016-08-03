using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace SmartLife.Client.PensionAgency
{
    internal class PensionAgencyVar
    {
        public static Guid? StationId { get; set; }//客户端回填

        public static string StationCode { get; private set; } 

        public static string UserCode { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string CityId { get; set; }
        public static string CityCode { get; set; }
        public static string CityName { get; set; }
        public static string Area1 { get; set; }
        public static string AreaCode1 { get; set; }
        public static string AreaName1 { get; set; }
        public static string AccessPoint { get; set; }
        public static string AccessOfProvice { get; set; }
        public static string DataPointOfManage { get; set; }
        public static List<dynamic> AuthorizedModules { get; set; }
        public static void Load(dynamic gov)
        {
            UserName = gov.UserName;
            CityId = gov.CityId;
            CityCode = gov.CityCode;
            CityName = gov.CityName;
            Area1 = gov.Area1;
            AreaCode1 = gov.AreaCode1;
            AreaName1 = gov.AreaName1;
            AccessPoint = gov.AccessPoint;
            AccessOfProvice = gov.AccessOfProvice;
            DataPointOfManage = gov.DataPointOfManage;
            AuthorizedModules = new List<dynamic>();
            for(int i=0;i<gov.AuthorizedModules.Count;i++){
                AuthorizedModules.Add(gov.AuthorizedModules[i]);
            }

        }
    }
}
