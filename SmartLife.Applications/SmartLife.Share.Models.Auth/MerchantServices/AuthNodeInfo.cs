using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Share.Models.Auth.MerchantServices
{
    public class AuthNodeInfo
    {
        public Guid? StationId { get; set; }
        public string AccessPoint { get; set; }
        public string Remark { get; set; }
    }
}
