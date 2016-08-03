using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartLife.Share.Models.Auth.MerchantServices;

namespace SmartLife.Client.Merchant
{
    public class MerchantVar
    {
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
        
    }
}
