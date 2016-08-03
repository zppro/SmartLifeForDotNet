using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client.SmsV2X
{
    public delegate void dSectionSettingsSave(object source, SectionSettingsSaveEventArgs e);

    public class SectionSettingsSaveEventArgs : EventArgs
    {
        public string AuthEndPoint { get; set; }
        public string AuthSmsEndPoint { get; set; }
        public string SmsEndPoint { get; set; }
        public string TxtSmsSetting { get; set; }
        public string OrderSmsSetting { get; set; }
    }
}
