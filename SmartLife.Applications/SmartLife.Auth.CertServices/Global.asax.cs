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
using e0571.web.core.Service;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Behaviors;
using SmartLife.Share.Models.Cer;
using System.Reflection;
using System.Dynamic;

namespace SmartLife.Auth.CertServices
{
    public class Global : HttpApplication
    {
        public static List<Redirect> redirects = new List<Redirect>();
        public static string oldConnectString = ""; 

        void Application_Start(object sender, EventArgs e)
        {
            StreamReader srForSettings = new StreamReader(HttpContext.Current.Server.MapPath("~/bin/Settings.txt"));
            string[] lines0 = srForSettings.ReadToEnd().Split('\n');
            srForSettings.Close();
            foreach (var line in lines0)
            {
                if (line.Trim() != "")
                {
                    var keyValues = line.Split("=".ToCharArray());
                    if (!GlobalManager.settingsForPhysicalDeploy.ContainsKey(keyValues[0]))
                    {
                        GlobalManager.settingsForPhysicalDeploy.Add(keyValues[0], keyValues[1].Replace("\r", ""));
                    }
                }
            }

            RegisterRoutes();
            IBatisNetManager.Init();//数据初始化

            oldConnectString = IBatisNet.DataAccess.DaoManager.GetInstance().LocalDataSource.ConnectionString;
            GlobalManager.Init(GlobalManager.SmartLife_Auth_CertServices);

            Assembly assem = Assembly.GetExecutingAssembly();
            StreamReader srForErrorMessage = new StreamReader(assem.GetManifestResourceStream("SmartLife.Auth.CertServices.ErrorCode.txt"));
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

            redirects.AddRange(BuilderFactory.DefaultBulder().List<Redirect>()); 

            GlobalManager.DeployNodes.AddRange(BuilderFactory.DefaultBulder().List<DeployNode>());
            CacheManager.Init();

            //注册数据连接
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
        }

        private void RegisterRoutes()
        {
            RouteTable.Routes.Add(new ServiceRoute("v1/Data", new WebServiceHostFactory(), typeof(AuthDataServices)));
            if (GlobalManager.settingsForPhysicalDeploy[GlobalManager.SETTINGS_ITEM_OPEN_DEVICE].Contains(GlobalManager.SETTINGS_VALUE_OPEN_DEVICE_PC))
            {
                RouteTable.Routes.Add(new ServiceRoute("v1/PC", new WebServiceHostFactory(), typeof(AuthPC)));
                RouteTable.Routes.Add(new ServiceRoute("v1/Gov", new WebServiceHostFactory(), typeof(AuthClientGov)));
                RouteTable.Routes.Add(new ServiceRoute("v1/Mer", new WebServiceHostFactory(), typeof(AuthClientMer)));
                RouteTable.Routes.Add(new ServiceRoute("v1/PensionAgency", new WebServiceHostFactory(), typeof(AuthClientPensionAgency)));
                RouteTable.Routes.Add(new ServiceRoute("v1/Schdule", new WebServiceHostFactory(), typeof(AuthSchdule)));

                RouteTable.Routes.Add(new ServiceRoute("v1/AIO/JingLianWen", new WebServiceHostFactory(), typeof(AuthAIOJingLianWen)));
            }


            if (GlobalManager.settingsForPhysicalDeploy[GlobalManager.SETTINGS_ITEM_OPEN_DEVICE].Contains(GlobalManager.SETTINGS_VALUE_OPEN_DEVICE_IOS) ||
                GlobalManager.settingsForPhysicalDeploy[GlobalManager.SETTINGS_ITEM_OPEN_DEVICE].Contains(GlobalManager.SETTINGS_VALUE_OPEN_DEVICE_ANDROID))
            {
                //RouteTable.Routes.Add(new ServiceRoute("v1/Activiate", new WebServiceHostFactory(), typeof(ActiviateService)));

                if (GlobalManager.settingsForPhysicalDeploy[GlobalManager.SETTINGS_ITEM_OPEN_DEVICE].Contains(GlobalManager.SETTINGS_VALUE_OPEN_DEVICE_IOS))
                {
                    RouteTable.Routes.Add(new ServiceRoute("v1/IOS", new WebServiceHostFactory(), typeof(AuthMobileIOS)));
                }

                if (GlobalManager.settingsForPhysicalDeploy[GlobalManager.SETTINGS_ITEM_OPEN_DEVICE].Contains(GlobalManager.SETTINGS_VALUE_OPEN_DEVICE_ANDROID))
                {
                    RouteTable.Routes.Add(new ServiceRoute("v1/ANDROID", new WebServiceHostFactory(), typeof(AuthMobileAndroid)));
                }
            }


        }
    }
}
