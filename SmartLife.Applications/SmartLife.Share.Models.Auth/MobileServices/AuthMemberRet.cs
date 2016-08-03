using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Share.Models.Auth.MobileServices
{
    public class AuthMemberRet
    {
        public string Token { get; set; }
        public Guid? MemberId { get; set; }
        public string MemberName { get; set; }
        public List<AuthNodeInfo> AuthNodeInfos { get; set; }
    }

}
