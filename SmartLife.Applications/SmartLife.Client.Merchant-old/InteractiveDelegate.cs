using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client.Merchant
{
    public delegate void dSectionWebSettingsSave(object source, SectionWebSettingsSaveEventArgs e);
    public delegate void dSectionBizSettingsSave(object source, SectionBizSettingsSaveEventArgs e);
    public delegate void dNotifyWorkOrderReminderStatInfo(WorkOrderReminderStatInfo item);
    public delegate void dNotifyWorkOrderReminderInfo(WorkOrderReminderInfo item);
    public delegate void dIgnoreWorkOrderReminderStatInfo(WorkOrderReminderStatInfo item);
    public delegate void dIgnoreWorkOrderReminderInfo(WorkOrderReminderInfo item);
}
