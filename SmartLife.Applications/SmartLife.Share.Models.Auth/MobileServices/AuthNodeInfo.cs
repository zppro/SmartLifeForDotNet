using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Share.Models.Auth.MobileServices
{
    public class AuthNodeInfo
    {
        public Guid? FamilyMemberId { get; set; }
        public string AccessPoint { get; set; }
    }
}
