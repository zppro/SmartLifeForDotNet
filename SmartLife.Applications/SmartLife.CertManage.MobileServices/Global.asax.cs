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

namespace SmartLife.CertManage.MobileServices
{
    public class Global : HttpApplication
    {
        //public static string gApplicationId = "IP004";//SmartLife.CertManage.MobileServices

        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
            IBatisNetManager.Init();//数据初始化
            GlobalManager.Init(GlobalManager.SmartLife_CertManage_MobileServices);
            GlobalManager.ReadParams();
            Assembly assem = Assembly.GetExecutingAssembly();
            StreamReader srForErrorMessage = new StreamReader(assem.GetManifestResourceStream("SmartLife.CertManage.MobileServices.ErrorCode.txt"));
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
            // Edit the base address of Service1 by replacing the "Service1" string below
            WebServiceHostFactory wsh = new WebServiceHostFactory();
            RouteTable.Routes.Add(new ServiceRoute("Oca/FamilyMemberService.IPhone", new WebServiceHostFactory(), typeof(Oca.FamilyMemberService4IPhone)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/FamilyMemberService.Android", new WebServiceHostFactory(), typeof(Oca.FamilyMemberService4Android)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/CallService.IPhone", new WebServiceHostFactory(), typeof(Oca.CallServiceService4IPhone)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/CallService.Android", new WebServiceHostFactory(), typeof(Oca.CallServiceService4Android)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/WorkOrderService.IPhone", new WebServiceHostFactory(), typeof(Oca.ServiceWorkOrderService4IPhone)));
            RouteTable.Routes.Add(new ServiceRoute("Oca/WorkOrderService.Android", new WebServiceHostFactory(), typeof(Oca.ServiceWorkOrderService4Android)));
        }
    }
}
