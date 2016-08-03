using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Share.Models.Auth.MerchantServices
{
    public class AuthMerchantRet
    {
        public string Token { get; set; }
        public string StationName { get; set; }
        public List<AuthNodeInfo> AuthNodeInfos { get; set; }
    }
}
