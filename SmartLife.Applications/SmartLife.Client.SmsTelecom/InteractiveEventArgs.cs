using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client.SmsTelecom
{
    public delegate void dSectionSettingsSave(object source, SectionSettingsSaveEventArgs e);

    public class SectionSettingsSaveEventArgs : EventArgs
    {
        public string AuthEndPoint { get; set; }
        public string SmsEndPoint { get; set; }
        public string IsmgEndPoint { get; set; }
    }
}
