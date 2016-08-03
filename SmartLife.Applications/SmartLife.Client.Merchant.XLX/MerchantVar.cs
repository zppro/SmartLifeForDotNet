using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using SmartLife.Share.Models.Auth.MerchantServices;

namespace SmartLife.Client.Merchant
{
    public class MerchantVar
    {
        public static Guid? StationId { get; set; }//客户端回填

        public static string StationCode { get; private set; }

        public static AuthMerchantRet CurrentMerchant { get; set; }

        public static void Load(string stationCode, dynamic currentMerchant)
        {
            StationCode = stationCode;
            CurrentMerchant = new AuthMerchantRet { StationName = currentMerchant.StationName, Token = currentMerchant.Token };

            CurrentMerchant.AuthNodeInfos = new List<AuthNodeInfo>();
            foreach (var nodeInfo in currentMerchant.AuthNodeInfos)
            {
                CurrentMerchant.AuthNodeInfos.Add(new AuthNodeInfo { StationId = nodeInfo.StationId, Remark = nodeInfo.Remark, AccessPoint = nodeInfo.AccessPoint });
            }

        }

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
        public static string DataPointOfMerchant { get; set; }
        public static List<dynamic> AuthorizedModules { get; set; }
          
        public static void LoadV2X(dynamic mer)
        {
            UserName = mer.UserName;
            CityId = mer.CityId;
            CityCode = mer.CityCode;
            CityName = mer.CityName;
            Area1 = mer.Area1;
            AreaCode1 = mer.AreaCode1;
            AreaName1 = mer.AreaName1;
            AccessPoint = mer.AccessPoint;
            AccessOfProvice = mer.AccessOfProvice;
            DataPointOfManage = mer.DataPointOfManage;
            DataPointOfMerchant = mer.DataPointOfMerchant;
            AuthorizedModules = new List<dynamic>();
            for (int i = 0; i < mer.AuthorizedModules.Count; i++)
            {
                AuthorizedModules.Add(mer.AuthorizedModules[i]);
            } 
        }
        
    }
}
