using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Share.Models.PO.eComm
{ 
    public class CTI_Conference
    {
        public string ConferenceName { get; set; }//会议室名称： 分机号_conf
        public string Action { get; set; }//动作： conference-create，conference-destroy，add-member，del-member
        public string UpdateTime { get; set; }//动作发生的时间
        public string MemberPhoneNo { get; set; }//进入或者退出的成员号码
        public string MemberId { get; set; }
    }
}
