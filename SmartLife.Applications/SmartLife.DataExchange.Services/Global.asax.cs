using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using System.IO;
using e0571.web.core.DataAccess;
using e0571.web.core.Utils;
using e0571.web.core.TypeInherited;
using SmartLife.Share.Behaviors; 
using e0571.web.core.Model;
using Newtonsoft.Json;
namespace SmartLife.DataExchange.Services
{
    public class Global : HttpApplication
    {
        public static string AreaId { get; set; }
        void Application_Start(object sender, EventArgs e)
        { 
            RegisterRoutes();
            IBatisNetManager.Init();//数据初始化 
            CacheManager.Init();
            GlobalManager.InitIndependent();


            //AreaId = JsonConvert.DeserializeObject<AppDeployArea>(CacheManager.ParmeterCacheProvider.Get("Sys_AppDeployArea").ToString()).id;
        }

        private void RegisterRoutes()
        {
            RouteTable.Routes.Add(new ServiceRoute("Pam/PamService", new WebServiceHostFactory(), typeof(PamService)));
            RouteTable.Routes.Add(new ServiceRoute("DayCareCenterService", new WebServiceHostFactory(), typeof(DayCareCenterService)));
            // Edit the base address of Service1 by replacing the "Service1" string below
            RouteTable.Routes.Add(new ServiceRoute("P12/C03/A01", new WebServiceHostFactory(), typeof(P12.C03.A01)));
            RouteTable.Routes.Add(new ServiceRoute("P12/C03/A04", new WebServiceHostFactory(), typeof(P12.C03.A04)));
            RouteTable.Routes.Add(new ServiceRoute("P12/C07/A01", new WebServiceHostFactory(), typeof(P12.C07.A01)));
        }
    }

    public class AppDeployArea
    {
        public string id { get; set; }
        public string code { get; set; }
    }
}
