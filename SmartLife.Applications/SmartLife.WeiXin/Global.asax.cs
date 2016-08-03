using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using e0571.web.core.DataAccess;
using e0571.web.core.Utils;
using e0571.web.core.Model;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Cer;
using SmartLife.Share.Behaviors;
using System.Reflection;
using SmartLife.Share.Models.WeiXin.Meb;

namespace SmartLife.WeiXin
{
    public class Global : HttpApplication
    {
        public static ServiceAccount TheServiceAccount;
        

        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(); 
            IBatisNetManager.Init();//数据初始化
            GlobalManager.Init(GlobalManager.SmartLife_WeiXin);
            Assembly assem = Assembly.GetExecutingAssembly();
            StreamReader srForErrorMessage = new StreamReader(assem.GetManifestResourceStream("SmartLife.WeiXin.ErrorCode.txt"));
            string[] lines = srForErrorMessage.ReadToEnd().Split('\n');
            srForErrorMessage.Close();
            foreach (var line in lines)
            {
                if (line.Trim() != "")
                {
                    var keyValues = line.Split("=".ToCharArray());
                    if (!GlobalManager.errorCodeMessages.ContainsKey(keyValues[0]))
                    {
                        GlobalManager.errorCodeMessages.Add(keyValues[0], keyValues[1].Replace("\r", ""));
                    }
                }
            }

            TheServiceAccount = BuilderFactory.DefaultBulder().Load<ServiceAccount, ServiceAccountPK>(new ServiceAccountPK { AccountCode = "zj-leblue" });
            
        }

        private void RegisterRoutes()
        {
            // Edit the base address of Service1 by replacing the "Service1" string below 
            RouteTable.Routes.Add(new ServiceRoute("Api/WXButtonService", new WebServiceHostFactory(), typeof(Services.WXButtonService)));
            RouteTable.Routes.Add(new ServiceRoute("Api/BoundAcconutService", new WebServiceHostFactory(), typeof(Services.BoundAcconutService)));
            RouteTable.Routes.Add(new ServiceRoute("Api/PushMessage", new WebServiceHostFactory(), typeof(Services.PushMessage)));
            RouteTable.Routes.Add(new ServiceRoute("Api/BoundOldmanService", new WebServiceHostFactory(), typeof(Services.BoundOldmanService)));
            RouteTable.Routes.Add(new ServiceRoute("Api/GovernmentService", new WebServiceHostFactory(), typeof(Services.GovernmentService)));
            RouteTable.Routes.Add(new ServiceRoute("Api/OldManService", new WebServiceHostFactory(), typeof(Services.OldManService)));
            RouteTable.Routes.Add(new ServiceRoute("Api/SearchService", new WebServiceHostFactory(), typeof(Services.SearchService)));
        }
    }
}
