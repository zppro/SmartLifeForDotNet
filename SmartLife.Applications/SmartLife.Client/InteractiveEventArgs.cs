using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client
{
    public class TransferPhoneEventArgs : EventArgs
    {
        public Guid? CallServiceId { get; set; }
        public int PhoneType { get; set; }
        public string PhoneNo { get; set; }
        public string Message { get; set; }
        public bool Cancel { get; set; }
    }

    public class RemovePhoneEventArgs : EventArgs
    {
        public Guid? CallServiceId { get; set; }
        public int PhoneType { get; set; }
        public string PhoneNo { get; set; }
        public string Message { get; set; }
        public bool Cancel { get; set; }
    }

    public class RemoteHostConnectEventArgs : EventArgs
    {
        public string RunMode { get; set; }
        public string RemoteHost { get; set; }
        public string VirPath { get; set; }
    }

    public class CallCenterSettingsSaveEventArgs : EventArgs
    {
        public string CallCenterNodeAddress { get; set; }
    }

    public class DialBackNoWinEventArgs : EventArgs
    {
        public string CallServiceId { get; set; }
        public string OldManId { get; set; }
        public string CallNo { get; set; }
        public string OldManName { get; set; }
    }

    public class FastTransferEventArgs : EventArgs
    {
        public string QueueNo { get; set; }
    }
}
