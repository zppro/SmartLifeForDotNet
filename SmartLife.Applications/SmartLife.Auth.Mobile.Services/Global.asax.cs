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

namespace SmartLife.Auth.Mobile.Services
{
    public class Global : HttpApplication
    {
        
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
            IBatisNetManager.Init();//数据初始化
            GlobalManager.Init(GlobalManager.SmartLife_Auth_Mobile_Services);
            Assembly assem = Assembly.GetExecutingAssembly();
            StreamReader srForErrorMessage = new StreamReader(assem.GetManifestResourceStream("SmartLife.Auth.Mobile.Services.ErrorCode.txt"));
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
            RouteTable.Routes.Add(new ServiceRoute("v1.IPhone", new WebServiceHostFactory(), typeof(V1MemberService4IPhone)));
            RouteTable.Routes.Add(new ServiceRoute("v1.Android", new WebServiceHostFactory(), typeof(V1MemberService4Android)));
            //---------------------上面的接口兼容老程序--------------------------------------->

            RouteTable.Routes.Add(new ServiceRoute("v1.Member.IPhone", new WebServiceHostFactory(), typeof(V1MemberService4IPhone)));
            RouteTable.Routes.Add(new ServiceRoute("v1.Member.Android", new WebServiceHostFactory(), typeof(V1MemberService4Android)));
            RouteTable.Routes.Add(new ServiceRoute("v1.Merchant.IPhone", new WebServiceHostFactory(), typeof(V1MerchantService4IPhone)));
            RouteTable.Routes.Add(new ServiceRoute("v1.Merchant.Android", new WebServiceHostFactory(), typeof(V1MerchantService4Android)));
        }
    }
}
