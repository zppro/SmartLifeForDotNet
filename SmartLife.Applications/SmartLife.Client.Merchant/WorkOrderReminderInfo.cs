using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client.Merchant
{
    public class WorkOrderReminderInfo
    { 
        public string SourceType { get; set; }
        public string SourceKey { get; set; }
        public string RemindContent { get; set; }

        public string AccessPoint { get; set; }
        public Guid? StationId { get; set; }
    }
}
