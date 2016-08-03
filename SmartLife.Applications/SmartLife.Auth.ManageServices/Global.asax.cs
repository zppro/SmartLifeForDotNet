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
using System.Dynamic;

namespace SmartLife.Auth.ManageServices
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
            IBatisNetManager.Init();//数据初始化

            GlobalManager.Init(GlobalManager.SmartLife_Auth_ManageServices);
            GlobalManager.ReadParams();
            Assembly assem = Assembly.GetExecutingAssembly();
            StreamReader srForErrorMessage = new StreamReader(assem.GetManifestResourceStream("SmartLife.Auth.ManageServices.ErrorCode.txt"));
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

            //初始化引用访问性
            GlobalManager.ApplicationAccessible.AddRange(BuilderFactory.DefaultBulder().List<ApplicationAccessibility>(new ApplicationAccessibility { InterfaceApplicationId = GlobalManager.SmartLife_Auth_ManageServices }).Select(item => item.AccessApplicationId).ToList());
            GlobalManager.DeployNodes.AddRange(BuilderFactory.DefaultBulder().List<DeployNode>(new DeployNode { ApplicationIdTo = GlobalManager.SmartLife_Auth_ManageServices }));
            CacheManager.Init();


            //注册数据连接ase
            GlobalManager.RegisterDatabaseConnections();
            /*
            var itemsOfProvider = BuilderFactory.DefaultBulder().List<DictionaryItem>(new DictionaryItem { DictionaryId = "00015" });
            IList<DatabaseConnect> databaseConnects = BuilderFactory.DefaultBulder().List<DatabaseConnect>();
            IList<dynamic> connects = databaseConnects.Select(item =>
            {
                dynamic connect = new ExpandoObject();
                connect.ConnectId = item.ConnectId;
                connect.Provider = itemsOfProvider.Single(it => it.ItemId == item.Provider).ItemName;
                connect.ServerName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(item.ServerName);
                connect.UserCode = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(item.UserCode);
                connect.UserPassword = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(item.UserPassword);
                connect.DatabaseName = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(item.DatabaseName);
                connect.IbatisConfigFileRefer = item.IbatisConfigFileRefer;
                connect.IbatisConfigFileValue = e0571.web.core.Service.ServiceManager.Instance.CryptoService.StringCrypto.Decrypt(item.IbatisConfigFileValue);
                return connect;
            }).ToList();
            IBatisNetManager.RegisterIBATIS(connects);
            */
        }

        private void RegisterRoutes()
        {
            // Share
            RouteTable.Routes.Add(new ServiceRoute("Share/Init", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__InitService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/Login", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__AuthenticationService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/ParameterData", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__ParameterDataService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/AjaxData", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__AjaxDataService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/TreeDataService", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__TreeDataService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/DictionaryDataService", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__DictionaryDataService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/MsdbJobService", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__MsdbJobService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/MsSQLScriptService", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__MsSQLScriptService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/IPCC", new WebServiceHostFactory(), typeof(SmartLife.Share.Behaviors.__IPCCService)));
            RouteTable.Routes.Add(new ServiceRoute("Share/Common", new WebServiceHostFactory(), typeof(AppShare.CommonService)));
            
            // Sys 
            RouteTable.Routes.Add(new ServiceRoute("Sys/MenuService", new WebServiceHostFactory(), typeof(Sys.MenuService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/WhiteListService", new WebServiceHostFactory(), typeof(Sys.WhiteListService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/ParameterService", new WebServiceHostFactory(), typeof(Sys.ParameterService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/TreeService", new WebServiceHostFactory(), typeof(Sys.TreeService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/TreeItemService", new WebServiceHostFactory(), typeof(Sys.TreeItemService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/DictionaryItemService", new WebServiceHostFactory(), typeof(Sys.DictionaryItemService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/DatabaseConnectService", new WebServiceHostFactory(), typeof(Sys.DatabaseConnectService)));
            RouteTable.Routes.Add(new ServiceRoute("Sys/ServerService", new WebServiceHostFactory(), typeof(Sys.ServerService)));

            // Pub
            RouteTable.Routes.Add(new ServiceRoute("Pub/UserService", new WebServiceHostFactory(), typeof(Pub.UserService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/GroupService", new WebServiceHostFactory(), typeof(Pub.GroupService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/AreaService", new WebServiceHostFactory(), typeof(Pub.AreaService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/ServiceStationService", new WebServiceHostFactory(), typeof(Pub.ServiceStationService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/ReportService", new WebServiceHostFactory(), typeof(Pub.ReportService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/ReminderService", new WebServiceHostFactory(), typeof(Pub.ReminderService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/SmsSendService", new WebServiceHostFactory(), typeof(Pub.SmsSendService)));
            RouteTable.Routes.Add(new ServiceRoute("Pub/DeviceService", new WebServiceHostFactory(), typeof(Pub.DeviceService)));

            //Auth
            RouteTable.Routes.Add(new ServiceRoute("Auth/GroupService", new WebServiceHostFactory(), typeof(Auth.GroupService)));
            RouteTable.Routes.Add(new ServiceRoute("Auth/LisenceUserService", new WebServiceHostFactory(), typeof(Auth.LisenceUserService)));
            RouteTable.Routes.Add(new ServiceRoute("Auth/ModuleService", new WebServiceHostFactory(), typeof(Auth.ModuleService)));
            
        }
    }
}
