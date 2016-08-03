using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using System.IO;
using e0571.web.core.DataAccess;
using e0571.web.core.Utils;
using e0571.web.core.TypeInherited;
using SmartLife.Share.Behaviors;
using System.Reflection;

namespace SmartLife.CertManage.MerchantServices
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
            IBatisNetManager.Init();//数据初始化
            GlobalManager.Init(GlobalManager.SmartLife_CertManage_MerchantServices);
            GlobalManager.ReadParams();
            Assembly assem = Assembly.GetExecutingAssembly();
            StreamReader srForErrorMessage = new StreamReader(assem.GetManifestResourceStream("SmartLife.CertManage.MerchantServices.ErrorCode.txt"));
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
        }

        private void RegisterRoutes()
        {
            RouteTable.Routes.Add(new ServiceRoute("Share/AjaxData", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__AjaxDataService)));
            // Edit the base address of Service1 by replacing the "Service1" string below
            RouteTable.Routes.Add(new ServiceRoute("Pub/ReminderService", new WebServiceHostFactory(), typeof(Pub.ReminderService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/WorkOrderService", new WebServiceHostFactory(), typeof(Oca.ServiceWorkOrderService)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/MerchantService", new WebServiceHostFactory(), typeof(Oca.MerchantService)));

        }
    }
}
