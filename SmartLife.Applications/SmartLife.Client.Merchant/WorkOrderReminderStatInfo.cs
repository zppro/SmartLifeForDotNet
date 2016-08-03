using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client.Merchant
{
    public class WorkOrderReminderStatInfo
    {
        public string SourceType { get; set; }
        public string ReminderNum { get; set; }

        public string AccessPoint { get; set; }
        public Guid? StationId { get; set; }
    }
}
