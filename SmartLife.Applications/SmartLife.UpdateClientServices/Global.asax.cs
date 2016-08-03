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
using SmartLife.Share.Models.Bas;
using SmartLife.Share.Behaviors;
using System.Reflection;
using System.Dynamic;
namespace SmartLife.UpdateClientServices
{
    public class Global : HttpApplication
    {
        public static bool invalid = false;

        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
            IBatisNetManager.Init();//数据初始化
        }

        private void RegisterRoutes()
        {
            // Edit the base address of Service1 by replacing the "Service1" string below
            RouteTable.Routes.Add(new ServiceRoute("Publish", new WebServiceHostFactory(), typeof(PublishService)));
        }
    }
}
