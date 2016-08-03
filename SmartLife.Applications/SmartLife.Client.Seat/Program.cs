using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using e0571.web.core.Utils;
using System.IO; 

namespace SmartLife.Client.Seat
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PrepareAppEnvironment();

#if V2X
            frmMainV2X frm = new frmMainV2X();
            Application.Run(frm);
#else
            frmMain frm = new frmMain();
            Application.Run(frm);
#endif
        }

        static void PrepareAppEnvironment()
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            log4net.Config.XmlConfigurator.Configure(assem.GetManifestResourceStream("SmartLife.Client.Seat.log4net.config"));

            FileAdapter.EnsurePath(Common.SOUND_FILE_PATH);

            if (!FileAdapter.Exists(Common.DEFAULT_SOUND_NAME_OF_CALL_IN))
            {
                using (FileStream fs = new FileStream(Common.DEFAULT_SOUND_NAME_OF_CALL_IN, FileMode.Create))
                {
                    byte[] data = new byte[Properties.Resources.Callin.Length];
                    Properties.Resources.Callin.Read(data, 0, data.Length);
                    fs.Write(data, 0, data.Length);
                } 
            }
            if (!FileAdapter.Exists(Common.DEFAULT_SOUND_NAME_OF_EXT_OFFLINE))
            {
                using (FileStream fs = new FileStream(Common.DEFAULT_SOUND_NAME_OF_EXT_OFFLINE, FileMode.Create))
                {
                    byte[] data = new byte[Properties.Resources.ExtOffline.Length];
                    Properties.Resources.ExtOffline.Read(data, 0, data.Length);
                    fs.Write(data, 0, data.Length);
                }
            }

            FileAdapter.EnsurePath(Common.TOOLS_FILE_PATH);
            if (!FileAdapter.Exists(Common.TOOLS_IPERF_EXE))
            {
                win.tools.Manager.ExportToFile(win.tools.ToolPackName.iperf, Common.TOOLS_FILE_PATH);
            }
        }
    }
}
