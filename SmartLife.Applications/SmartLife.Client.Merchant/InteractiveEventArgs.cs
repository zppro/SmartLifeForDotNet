using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client.Merchant
{
    public class SectionWebSettingsSaveEventArgs : EventArgs
    {
        public string AuthEndPoint { get; set; }
    }

    public class SectionBizSettingsSaveEventArgs : EventArgs
    {
        public RemindType Type { get; set; }
        public int YYRemindFlag { get; set; }
    }
}
