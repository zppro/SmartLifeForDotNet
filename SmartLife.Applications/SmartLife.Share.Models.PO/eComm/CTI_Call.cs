using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Share.Models.PO.eComm
{
    public class CTI_Call
    {
        public string ExtNo { get; set; }//来去电的所属分机号码
        public string PeerNumber { get; set; }//来去电号码，系统会自动的将手机
        public string CallState { get; set; }//ring--振铃,hold-呼叫保持,idle--挂断,busy--接通
        public string CallType { get; set; }//呼叫类型，in—表示呼入，out—呼出，lo---内部分机通话
        public string CallTime { get; set; }//来电时间，以 yyyy-MM-dd HH:mm:ss 格式返回
        public string Trunk { get; set; } //被叫号码
        public string Uuid { get; set; }//IPCC呼叫记录内部唯一号
    }
     
}
