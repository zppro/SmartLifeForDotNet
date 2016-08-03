using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;

using System.IO;
using System.Reflection;
using System.Collections.Concurrent;

using e0571.web.core.DataAccess;
using e0571.web.core.Utils;
using e0571.web.core.Model;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Cer; 
using SmartLife.Share.Models.PO.eComm;
using SmartLife.Share.Behaviors;

namespace SmartLife.IPCC
{
    public class Global : HttpApplication
    {
        public static ConcurrentDictionary<string, ConcurrentQueue<CTI_Call>> ctiCalls = new ConcurrentDictionary<string, ConcurrentQueue<CTI_Call>>();
        public static ConcurrentDictionary<string, ConcurrentQueue<CTI_Conference>> ctiConferences = new ConcurrentDictionary<string, ConcurrentQueue<CTI_Conference>>();

        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
            IBatisNetManager.Init();//数据初始化
            GlobalManager.InitIndependent();
        }

        private void RegisterRoutes()
        {
               
            RouteTable.Routes.Add(new ServiceRoute("api/eComm", new WebServiceHostFactory(), typeof(eCommService))); 
        }

        public static void DebugInfo(string info){
            GlobalManager.inputLogger.Info(info);
        }
    } 
}
