using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client
{
    public class IPCCVar
    { 
        public static bool isEstablished { get; set; }
        public static string currentCaller { get; set; }
        public static string currentDispatchQueue { get; set; } 
        public static string currentConferenceNum { get; set; }
        public static string currentCallee { get; set; }
        public static Guid? currentCallServiceId { get; set; }
        public static string currentUniqueId { get; set; } //两人通话录音
        
        public static List<string> conferenceMembers { get; set; }
        

        public static Dictionary<string, string> telInfo { get; set; }

        public static void AddConfMember(string member)
        {
            if (!conferenceMembers.Contains(member))
            {
                conferenceMembers.Add(member);
            }
        }

        public static void RemoveConfMember(string member)
        {
            if (conferenceMembers.Contains(member))
            {
                conferenceMembers.Remove(member);
            }
        }

        public static void setTelInfo(int telType, string tel, string message)
        {
            telInfo[tel] = "[" + telType.ToString() + ":" + tel + ":" + message + "]";
        }

        public static string getTelInfo(string tel)
        {
            if (telInfo.ContainsKey(tel))
            {
                return telInfo[tel];
            }
            return tel;
        }

        public static void Clear()
        { 
            isEstablished = false;
            currentCaller = null;
            currentDispatchQueue = null;
            currentConferenceNum = null;
            currentCallee = null;
            currentCallServiceId = null;
            currentUniqueId = null;
            if (conferenceMembers != null && conferenceMembers.Count > 0)
            {
                conferenceMembers.Clear();
            }
            conferenceMembers = null;
        }
    }
}
