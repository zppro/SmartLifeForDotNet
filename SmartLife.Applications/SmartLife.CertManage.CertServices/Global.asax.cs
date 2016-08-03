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
using SmartLife.Share.Behaviors;
using SmartLife.Share.Models.Cer;  
namespace SmartLife.CertManage.CertServices
{
    public class Global : HttpApplication
    {
        
        public static string gApplicationId = "IP001";//SmartLife.CertManage.CertServices
        public static List<Redirect> redirects = new List<Redirect>();
        public static string oldConnectString = "";

        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
            IBatisNetManager.Init();//数据初始化
            oldConnectString = IBatisNet.DataAccess.DaoManager.GetInstance().LocalDataSource.ConnectionString;
            GlobalManager.Init(gApplicationId);

            redirects.AddRange(BuilderFactory.DefaultBulder().List<Redirect>());
            GlobalManager.ApplicationAccessible.AddRange(BuilderFactory.DefaultBulder().List<ApplicationAccessibility>(new ApplicationAccessibility { InterfaceApplicationId = gApplicationId }).Select(item => item.AccessApplicationId).ToList());
            GlobalManager.DeployNodes.AddRange(BuilderFactory.DefaultBulder().List<DeployNode>());
            CacheManager.Init();  
        }

        private void RegisterRoutes()
        {
            RouteTable.Routes.Add(new ServiceRoute("v1", new WebServiceHostFactory(), typeof(AuthenticationService)));
        }
    }
}
